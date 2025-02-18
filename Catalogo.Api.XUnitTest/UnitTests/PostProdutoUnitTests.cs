using Catalogo.Api.Controllers;
using Catalogo.Api.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.XUnitTest.UnitTests;

public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;
    public PostProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task PostProduto_Return_CreatedStatusCode()
    {
        //Arrage
        var novoProdutoDto = new ProdutoDTO
        {
            Nome = "Produto de Teste",
            Descricao = "Produto de Teste",
            Preco = 100,
            ImagemUrl = "http://teste.net/1.jpg",
            CategoriaId = 2
        };

        //Act
        var data = await _controller.Post(novoProdutoDto);

        //Assert
        var createdResult = data.Result.Should().BeOfType<CreatedAtRouteResult>();
        createdResult.Subject.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task PostProduto_Return_BadRequest()
    {
        ProdutoDTO prod = null;

        var data = await _controller.Post(prod);

        var badRequestResult = data.Result.Should().BeOfType<BadRequestResult>();
        badRequestResult.Subject.StatusCode.Should().Be(400);
    }
}
