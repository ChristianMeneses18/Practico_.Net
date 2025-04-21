using AutoMapper;
using Ejercicio3.API.Mappings;
using Ejercicio3.Core.Interfaces;
using Ejercicio3.Data.Context;
using Ejercicio3.Data.Repositories;
using Ejercicio3.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API REST EJERICIO 3 PRACTICO .NET",
        Version = "v1",
        Description = "Esta es una api que implementa los metodos para realizar un crud de categorias y productos",
        Contact = new OpenApiContact
        {
            Name = "Christian Meneses",
            Email = "christianmeneses617@gmail.com"
        }
    });
    options.EnableAnnotations();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
