using Catalogo.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Contexts;

public class CatalogoContext : IdentityDbContext<ApplicationUser>
{
    public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
 