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

    public async Task<PagedResult<Produto>> GetProdutosAsync(ProdutosParameters produtosParameters)
    {
        var produtos = GetAsQueryable()
                            .OrderBy(on => on.ProdutoId);
        var produtosOrdenados = await produtos.ToPagedListAsync(produtosParameters.PageNumber, produtosParameters.PageSize);

        return produtosOrdenados;
    }

    public async Task<PagedResult<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams)
    {
        var produtos = GetAsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(x => x.Preco > produtosFiltroParams.Preco.Value)
                                    .OrderBy(on => on.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(x => x.Preco < produtosFiltroParams.Preco.Value)
                                    .OrderBy(on => on.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(x => x.Preco == produtosFiltroParams.Preco.Value)
                                    .OrderBy(on => on.Preco);
            }
        }
        var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

        return produtosFiltrados;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();
        var produtosPorCategoria = produtos.Where(x => x.CategoriaId == id);

        return produtosPorCategoria;
    }
}
