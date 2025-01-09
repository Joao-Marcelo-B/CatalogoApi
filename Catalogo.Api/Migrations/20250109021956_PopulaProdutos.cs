using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Api.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('Pepsi', 'Refrigerante de cola', 4.45, 'pepsi.jpg', 50, now(), 1)");
            mb.Sql("insert into produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('Guaraná', 'Refrigerante de guaraná', 4.45, 'guarana.jpg', 50, now(), 2)");
            mb.Sql("insert into produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('Hamburguer', 'Pão, carne e queijo', 12.45, 'x-bacon.jpg', 50, now(), 2)");
            mb.Sql("insert into produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) values ('X-Bacon', 'Pão, carne, queijo e bacon', 15.45, 'x-bacon.jpg', 50, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from produtos");
        }
    }
}
