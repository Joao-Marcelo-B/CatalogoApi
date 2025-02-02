using Catalogo.Api.Models;
using Catalogo.Api.Pagination;
using X.PagedList;

namespace Catalogo.Api.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedResult<Categoria>> GetCategoriasV2Async(CategoriasParameters categoriasParameters);
    Task<PagedResult<Categoria>> GetCategoriasFiltroNomeV2Async(CategoriasFiltroNome filtro);

    Task<IPagedList<Categoria>> GetCategoriasV1Async(CategoriasParameters categoriasParameters);
    Task<IPagedList<Categoria>> GetCategoriasFiltroNomeV1Async(CategoriasFiltroNome filtro);
}
