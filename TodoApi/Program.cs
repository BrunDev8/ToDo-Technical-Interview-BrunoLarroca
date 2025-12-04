using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AccesoDatos.Data;
using AccesoDatos.Repositorios;
using Dominio.InterfacesRepositorio;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("TodoContext"))
);

// Registrar repositorios
builder.Services.AddScoped<IListRepositorio, ListRepositorio>();
builder.Services.AddScoped<IItemRepositorio, ItemRepositorio>();

// Registrar casos de uso de List
builder.Services.AddScoped<IListarListsCU, ListarListsCU>();
builder.Services.AddScoped<IObtenerListPorIdCU, ObtenerListPorIdCU>();
builder.Services.AddScoped<ICrearListCU, CrearListCU>();
builder.Services.AddScoped<IActualizarListCU, ActualizarListCU>();
builder.Services.AddScoped<IEliminarListCU, EliminarListCU>();

// Registrar casos de uso de Item
builder.Services.AddScoped<IListarItemsCU, ListarItemsCU>();
builder.Services.AddScoped<IObtenerItemPorIdCU, ObtenerItemPorIdCU>();
builder.Services.AddScoped<ICrearItemCU, CrearItemCU>();
builder.Services.AddScoped<IActualizarItemCU, ActualizarItemCU>();
builder.Services.AddScoped<IEliminarItemCU, EliminarItemCU>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "TodoApi",
        Description = "API REST para gestión de listas de tareas (Todo Lists) con sus respectivos ítems",
    });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
