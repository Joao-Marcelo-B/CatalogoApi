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
    private readonly IProdutoRepository _repository;

    public ProdutosController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _repository.GetProdutos().ToList();
        if (produtos is null) 
            return NotFound();

        return Ok(produtos);
    }

    [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.GetProduto(id);
        if (produto is null) 
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Produto produto)
    {
        if (produto is null) 
            return NotFound();

        var novoProduto = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) 
            return BadRequest("Identifcador do produto não condiz.");

        var resultado = _repository.Update(produto);
        if(resultado)
            return Ok(produto);
        else
            return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
    }

    //[HttpGet("teste/{valor:alpha:length(5)}")]
    //public ActionResult<Produto> Get2(string valor)
    //{
    //    return _repository.Produtos.FirstOrDefault();
    //}

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _repository.GetProduto(id);
        if (produto is null) 
            return NotFound();

        var resultado = _repository.Delete(id);
        if (resultado)
            return Ok(produto);
        else
            return StatusCode(500, $"Falha ao deletar o produto de id = {id}");
    }
}
