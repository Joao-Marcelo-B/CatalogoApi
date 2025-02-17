using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("/v{version:apiVersion}/teste")]
[ApiVersion("1.0", Deprecated = true)]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "TesteV1 - GET - Api Versão 1.0";
    }
}
