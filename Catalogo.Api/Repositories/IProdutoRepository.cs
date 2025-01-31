using Catalogo.Api.Model;
using Catalogo.Api.Pagination;

namespace Catalogo.Api.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
