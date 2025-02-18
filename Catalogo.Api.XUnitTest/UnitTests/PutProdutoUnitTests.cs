using Catalogo.Api.Controllers;
using Catalogo.Api.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.XUnitTest.UnitTests;

public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public PutProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PutProduto_Return_OkResult()
    {
        //Arrage
        var prodId = 2;

        var updatedProdutoDto = new ProdutoDTO
        {
            ProdutoId = prodId,
            Nome = "Produto de Teste Atualizado",
            Descricao = "Produto de Teste Atualizado",
            Preco = 200,
            ImagemUrl = "http://teste.net/2.jpg",
            CategoriaId = 2
        };

        //Act
        var result = await _controller.Put(prodId, updatedProdutoDto);

        //Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task PutProduto_Return_BadRequest()
    {
        //Arrange
        var prodId = 1000;

        var meuProduto = new ProdutoDTO
        {
            ProdutoId = 14,
            Nome = "Produto de Teste Atualizado",
            Descricao = "Produto de Teste Atualizado",
            Preco = 200,
            ImagemUrl = "http://teste.net/2.jpg",
            CategoriaId = 2
        };

        //Act
        var data = await _controller.Put(prodId, meuProduto);

        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
    }
}