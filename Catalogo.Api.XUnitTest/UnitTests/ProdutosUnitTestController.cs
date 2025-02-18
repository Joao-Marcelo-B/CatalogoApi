using AutoMapper;
using Catalogo.Api.Contexts;
using Catalogo.Api.DTOs.Mappings;
using Catalogo.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.XUnitTest.UnitTests;

public class ProdutosUnitTestController
{
    public IUnitOfWork repository;
    public IMapper mapper;
    public static DbContextOptions<CatalogoContext> dbContextOptions { get; }
    public static string connectionString = "Server=localhost;Port=3307;DataBase=Catalogo;Uid=root;Pwd=root";

    static ProdutosUnitTestController()
    {
        dbContextOptions = new DbContextOptionsBuilder<CatalogoContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
    }

    public ProdutosUnitTestController()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoDTOMappingProfile());
        });

        mapper = config.CreateMapper();

        var context = new CatalogoContext(dbContextOptions);

        repository = new UnitOfWork(context);
    }
}
