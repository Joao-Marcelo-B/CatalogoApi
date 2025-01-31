using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Api.Migrations;

/// <inheritdoc />
public partial class PopulaCategorias : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("insert into categorias(Nome, ImagemUrl) values ('Bebidas', 'bebidas.jpg')");
        mb.Sql("insert into categorias(Nome, ImagemUrl) values ('Lanches', 'lanches.jpg')");
        mb.Sql("insert into categorias(Nome, ImagemUrl) values ('Sobremesas', 'sobremesas.jpg')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("delete from categorias");
    }
}
