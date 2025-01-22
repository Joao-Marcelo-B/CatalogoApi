using Catalogo.Api.Contexts;
using Catalogo.Api.Extensions;
using Catalogo.Api.Filters;
using Catalogo.Api.Logging;
using Catalogo.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilter));
                })
                .AddJsonOptions(options =>
                                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<ApiLoggingFilter>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? catalogoConnection = builder.Configuration.GetConnectionString("CatalogoConnection");
builder.Services.AddDbContext<CatalogoContext>(options => 
                options.UseMySql(catalogoConnection, 
                ServerVersion.AutoDetect(catalogoConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    //adicionar o codigo antes do request
    await next(context);
    //adicionar o codigo depois do request
});

app.MapControllers();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Método não encontrado");
//});

app.Run();
