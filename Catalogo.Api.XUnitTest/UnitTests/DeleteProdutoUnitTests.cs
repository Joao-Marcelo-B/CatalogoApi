using Catalogo.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.XUnitTest.UnitTests;

public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestController> 
{
    private readonly ProdutosController _controller;
    public DeleteProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.mapper);
    }

    [Fact]
    public async Task DeleteProduto_Return_OkResult()
    {
        //Arrange
        var prodId = 3;

        //Act
        var data = await _controller.Delete(prodId);

        //Assert
        data.Should().NotBeNull();
        data.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteProduto_Return_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.Delete(prodId);

        //Assert
        data.Should().NotBeNull();
        data.Result.Should().BeOfType<NotFoundResult>();
    }
}
