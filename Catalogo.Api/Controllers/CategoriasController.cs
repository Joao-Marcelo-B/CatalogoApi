using Catalogo.Api.DTOs;
using Catalogo.Api.DTOs.Mappings;
using Catalogo.Api.Filters;
using Catalogo.Api.Model;
using Catalogo.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _unitOfWork.CategoriaRepository.GetAll();

        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _unitOfWork.CategoriaRepository.Get(x => x.CategoriaId == id);

        if (categoria == null)
            return NotFound("Categoria não encontrada...");
        
        var categoriaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest();

        var categoria = categoriaDTO.ToCategoria();
        if(categoria is null)
            return BadRequest();

        var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
        _unitOfWork.Commit();

        var categoriaCriadaDTO = categoriaCriada.ToCategoriaDTO();
        if(categoriaCriadaDTO is null)
            return BadRequest();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriadaDTO.CategoriaId }, categoriaCriadaDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();
        if (categoria == null)
            return BadRequest();
        

        var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
        _unitOfWork.Commit();

        var categoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();
        if (categoriaAtualizadaDTO == null)
            return BadRequest();

        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _unitOfWork.CategoriaRepository.Get(x => x.CategoriaId ==  id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        _unitOfWork.CategoriaRepository.Delete(categoria);
        _unitOfWork.Commit();

        var categoriaExcluidaDTO = categoria.ToCategoriaDTO();
        if(categoriaExcluidaDTO == null)
            return BadRequest();

        return Ok(categoriaExcluidaDTO);
    }

    [HttpGet("teste")]
    public string Teste() =>
        " TESTE - TESTE ".Trim();
}
