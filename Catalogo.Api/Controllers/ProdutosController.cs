using Catalogo.Api.Contexts;
using Catalogo.Api.Model;
using Catalogo.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    //private readonly IRepository<Produto> _produtoRepository;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository)
    {
        //_produtoRepository = repository;
        _produtoRepository = produtoRepository;
    }

    [HttpGet("categorias/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);
        if (produtos is null)
            return NotFound();

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _produtoRepository.GetAll();
        if (produtos is null) 
            return NotFound();

        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _produtoRepository.Get(x => x.CategoriaId == id);
        if (produto is null) 
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Produto produto)
    {
        if (produto is null) 
            return NotFound();

        var novoProduto = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) 
            return BadRequest("Identifcador do produto não condiz.");

        var produtoAtualizado = _produtoRepository.Update(produto);
        
        return Ok(produtoAtualizado);
    }

    //[HttpGet("teste/{valor:alpha:length(5)}")]
    //public ActionResult<Produto> Get2(string valor)
    //{
    //    return _repository.Produtos.FirstOrDefault();
    //}

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(x => x.ProdutoId == id);
        if (produto is null)
            return NotFound();

        var produtoDeletado = _produtoRepository.Delete(produto);

        return Ok(produtoDeletado);
    }
}
