﻿using System.ComponentModel.DataAnnotations;

namespace Catalogo.Api.DTOs;

public class LoginModel
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
