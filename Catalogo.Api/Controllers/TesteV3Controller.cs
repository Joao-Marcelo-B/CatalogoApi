using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("teste")]
[ApiVersion(3)]
[ApiVersion(4)]
public class TesteV3Controller : ControllerBase
{
    [MapToApiVersion(3)]
    [HttpGet]
    public string GetVersion3()
    {
        return "TesteV3- GET - Api Versão 3.0";
    }

    [MapToApiVersion(4)]
    [HttpGet]
    public string GetVersion4()
    {
        return "TesteV4- GET - Api Versão 4.0";
    }
}
