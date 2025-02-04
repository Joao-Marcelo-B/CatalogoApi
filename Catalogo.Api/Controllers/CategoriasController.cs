using Catalogo.Api.DTOs;
using Catalogo.Api.DTOs.Mappings;
using Catalogo.Api.Filters;
using Catalogo.Api.Models;
using Catalogo.Api.Pagination;
using Catalogo.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriasController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public CategoriasController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    //[HttpGet("produtos")]
    //public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    //{
    //    return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
    //}

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetCategoriasV2Async(categoriasParameters);
        return ObterProdutos(categorias);
    }

    [HttpGet("filter/nome/pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasFiltroNome filtros)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetCategoriasFiltroNomeV2Async(filtros);
        return ObterProdutos(categorias);
    }

    private ActionResult<IEnumerable<CategoriaDTO>> ObterProdutos(PagedResult<Categoria> categorias)
    {
        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var categoriaDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriaDTO);
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetAllAsync();

        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> Get(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetAsync(x => x.CategoriaId == id);

        if (categoria == null)
            return NotFound("Categoria não encontrada...");
        
        var categoriaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest();

        var categoria = categoriaDTO.ToCategoria();
        if(categoria is null)
            return BadRequest();

        var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
        await _unitOfWork.CommitAsync();

        var categoriaCriadaDTO = categoriaCriada.ToCategoriaDTO();
        if(categoriaCriadaDTO is null)
            return BadRequest();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriadaDTO.CategoriaId }, categoriaCriadaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();
        if (categoria == null)
            return BadRequest();
        

        var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
        await _unitOfWork.CommitAsync();

        var categoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();
        if (categoriaAtualizadaDTO == null)
            return BadRequest();

        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetAsync(x => x.CategoriaId ==  id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        _unitOfWork.CategoriaRepository.Delete(categoria);
        await _unitOfWork.CommitAsync();

        var categoriaExcluidaDTO = categoria.ToCategoriaDTO();
        if(categoriaExcluidaDTO == null)
            return BadRequest();

        return Ok(categoriaExcluidaDTO);
    }

    [HttpGet("teste")]
    public string Teste() =>
        " TESTE - TESTE ".Trim();
}
