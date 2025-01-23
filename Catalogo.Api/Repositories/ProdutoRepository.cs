using Catalogo.Api.Contexts;
using Catalogo.Api.Model;

namespace Catalogo.Api.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(CatalogoContext context) : base(context)
    {
    } 

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(p => p.CategoriaId == id);
    }
}
