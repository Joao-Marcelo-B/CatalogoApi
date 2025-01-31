using Catalogo.Api.Contexts;
using Catalogo.Api.Model;
using Catalogo.Api.Pagination;

namespace Catalogo.Api.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(CatalogoContext context) : base(context)
    {
    }

    //public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters)
    //{
    //    return Get()
    //            .OrderBy(on => on.Nome)
    //            .Skip((produtosParameters.PageNumber - 1) * produtosParameters.PageSize)
    //            .Take(produtosParameters.PageSize).ToList();
    //}

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        var produtos = Get()
                .OrderBy(on => on.ProdutoId);
        var produtosOrdenados = PagedList<Produto>.ToPageList(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);

        return produtosOrdenados;
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(p => p.CategoriaId == id);
    }
}
