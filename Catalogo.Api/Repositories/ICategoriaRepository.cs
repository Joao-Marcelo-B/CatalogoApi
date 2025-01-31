using Catalogo.Api.Model;
using Catalogo.Api.Pagination;

namespace Catalogo.Api.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
}
