using Catalogo.Api.Contexts;
using Catalogo.Api.Model;

namespace Catalogo.Api.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(CatalogoContext context) : base(context)
    {
    }
}
