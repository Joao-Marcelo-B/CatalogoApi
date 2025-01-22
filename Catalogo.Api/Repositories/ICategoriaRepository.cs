using Catalogo.Api.Model;

namespace Catalogo.Api.Repositories;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();
    Categoria? GetCategoria(int id);
    Categoria Create(Categoria categoria);
    Categoria Update(Categoria categoria);
    Categoria Delete(int id);
}
