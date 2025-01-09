using Catalogo.Api.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly CatalogoContext _context;

    public ProdutosController(CatalogoContext context)
    {
        _context = context;
    }
}
