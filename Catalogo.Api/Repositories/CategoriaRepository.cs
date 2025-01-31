using Catalogo.Api.Contexts;
using Catalogo.Api.Model;
using Catalogo.Api.Pagination;

namespace Catalogo.Api.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(CatalogoContext context) : base(context)
    {
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        var categorias = Get()
                .OrderBy(on => on.CategoriaId);

        var categoriasOrdenadas = PagedList<Categoria>.ToPageList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdenadas;
    }
}
