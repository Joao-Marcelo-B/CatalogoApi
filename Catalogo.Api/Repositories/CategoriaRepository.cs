using Catalogo.Api.Contexts;
using Catalogo.Api.Models;
using Catalogo.Api.Pagination;
using X.PagedList;
using X.PagedList.Extensions;

namespace Catalogo.Api.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(CatalogoContext context) : base(context)
    {
    }

    public async Task<PagedResult<Categoria>> GetCategoriasV2Async(CategoriasParameters categoriasParameters)
    {
        var categorias = GetAsQueryable()
                            .OrderBy(on => on.CategoriaId);

        var categoriasOrdenadas = await categorias.ToPagedListAsync(categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdenadas;
    }

    public async Task<PagedResult<Categoria>> GetCategoriasFiltroNomeV2Async(CategoriasFiltroNome filtro)
    {
        var categorias = GetAsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            categorias = categorias.Where(x => x.Nome!.Contains(filtro.Nome))
                                   .OrderBy(on => on.Nome);
        }

        var categoriasFiltradas = await categorias.ToPagedListAsync(filtro.PageNumber, filtro.PageSize);

        return categoriasFiltradas;
    }

    public IPagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        var categorias = GetAsQueryable()
                .OrderBy(on => on.CategoriaId);

        //var categoriasOrdenadas = PagedList<Categoria>.ToPageList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);

        var categoriasOrdenadas = categorias.ToPagedList(categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdenadas;
    }

    public IPagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome filtro)
    {
        var categorias = GetAsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            categorias = categorias.Where(x => x.Nome!.Contains(filtro.Nome))
                                   .OrderBy(on => on.Nome);
        }

        //var categoriasFiltradas = PagedList<Categoria>.ToPageList(categorias, filtro.PageNumber, filtro.PageSize);

        var categoriasFiltradas = categorias.ToPagedList(filtro.PageNumber, filtro.PageSize);

        return categoriasFiltradas;
    }
}
