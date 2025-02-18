using Catalogo.Api.Models;
using Catalogo.Api.Pagination;
using X.PagedList;

namespace Catalogo.Api.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedResult<Categoria>> GetCategoriasV2Async(CategoriasParameters categoriasParameters);
    Task<PagedResult<Categoria>> GetCategoriasFiltroNomeV2Async(CategoriasFiltroNome filtro);

    IPagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
    IPagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome filtro);
}
