using Catalogo.Api.DTOs;
using Catalogo.Api.Models;
using Catalogo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    public AuthController(ITokenService tokenService,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration,
                          ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    [Route("CreateRole")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles Added");
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = $"Role {roleName} added successfully" });
            } else
            {
                _logger.LogInformation(2, "Error");
                return BadRequest(new Response { Status = "Error", Message = $"Issue adding the new {roleName} role" });
            }
        }

        return BadRequest(new Response { Status = "Error", Message = "Role alredy exist." });
    }

    [HttpPost]
    [Route("AddUserToRole")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = $"User {email} added to role {roleName} successfully" });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = $"Issue adding user {email} to role {roleName}" });
            }
        }
        return BadRequest(new Response { Status = "Error", Message = $"Unable to find user" });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName!);

        if( user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
            });
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName!);
        if(userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
        };

        var result = await _userManager.CreateAsync(user, model.Password!);
        if(!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed." });
        }

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("refresh-token")]
    [Authorize(Policy ="ExclusiveOnly")]
    public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
    {
        if(tokenModel is null)
            return BadRequest("Invalid client request");

        string? accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException("Access token is missing");

        string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException("Refresh token is missing");

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
        if(principal is null || principal.Identity is null)
            return BadRequest("Invalid client request");

        string username = principal.Identity.Name ?? throw new InvalidOperationException("Invalid username");

        var user = await _userManager.FindByNameAsync(username);
        if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return BadRequest("Invalid client request");

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims, _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }

    [Authorize]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            return BadRequest("Invalid client request");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
