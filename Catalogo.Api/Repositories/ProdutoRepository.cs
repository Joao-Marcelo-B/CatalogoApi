using Catalogo.Api.Contexts;
using Catalogo.Api.Model;

namespace Catalogo.Api.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public Produto? GetProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
        if(produto == null)
            throw new ArgumentNullException("Produto é null");

        return produto;
    }

    public IQueryable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    public Produto Create(Produto produto)
    {
        if(produto == null)
            throw new ArgumentNullException("Produto é null");

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    public bool Update(Produto produto)
    {
        if(produto is null)
            throw new ArgumentNullException("Produto é null");

        if(_context.Produtos.Any(x => x.ProdutoId == produto.ProdutoId))
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if(produto is not null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
