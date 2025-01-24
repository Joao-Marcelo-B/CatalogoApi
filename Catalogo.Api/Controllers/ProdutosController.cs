using AutoMapper;
using Catalogo.Api.DTOs;
using Catalogo.Api.Model;
using Catalogo.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("categorias/{id:int}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
    {
        var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos is null)
            return NotFound();

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _unitOfWork.ProdutoRepository.GetAll();
        if (produtos is null) 
            return NotFound();

        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _unitOfWork.ProdutoRepository.Get(x => x.CategoriaId == id);
        if (produto is null) 
            return NotFound();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post([FromBody] ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null) 
            return NotFound();

        var produto = _mapper.Map<Produto>(produtoDTO);

        var novoProduto = _unitOfWork.ProdutoRepository.Create(produto);
        _unitOfWork.Commit();

        var novoProdutoDTO = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.ProdutoId) 
            return BadRequest("Identifcador do produto não condiz.");

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
        _unitOfWork.Commit();

        var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDTO);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateRequest> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if(patchProdutoDTO is null || id <= 0)
            return BadRequest();

        var produto = _unitOfWork.ProdutoRepository.Get(x => x.ProdutoId == id);
        if(produto is null)
            return NotFound();

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);
        if(!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);
        _unitOfWork.ProdutoRepository.Update(produto);
        _unitOfWork.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    //[HttpGet("teste/{valor:alpha:length(5)}")]
    //public ActionResult<Produto> Get2(string valor)
    //{
    //    return _repository.Produtos.FirstOrDefault();
    //}

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _unitOfWork.ProdutoRepository.Get(x => x.ProdutoId == id);
        if (produto is null)
            return NotFound();

        var produtoDeletado = _unitOfWork.ProdutoRepository.Delete(produto);
        _unitOfWork.Commit();

        var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDTO);
    }
}
