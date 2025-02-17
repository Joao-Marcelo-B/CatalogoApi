using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("/v{version:apiVersion}/teste")]
[ApiVersion("2.0")]
public class TesteV2Controller : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "TesteV2- GET - Api Versão 2.0";
    }
}
