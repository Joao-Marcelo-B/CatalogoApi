using Catalogo.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Contexts;

public class CatalogoContext : DbContext
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}
