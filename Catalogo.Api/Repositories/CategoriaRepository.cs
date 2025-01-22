using Catalogo.Api.Contexts;
using Catalogo.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly CatalogoContext _context;

    public CategoriaRepository(CatalogoContext context)
    {
        _context = context;
    }

    public Categoria? GetCategoria(int id)
    {
        return _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
    }

    public IEnumerable<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public Categoria Create(Categoria categoria)
    {
        if (categoria == null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);

        if(categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public Categoria Update(Categoria categoria)
    { 
        if(categoria is null)
            throw new ArgumentNullException(nameof(categoria)); 

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return categoria;
    }
}
