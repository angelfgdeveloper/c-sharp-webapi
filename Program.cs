using webapi;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CONEXION A DB
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnCategorias"));

// Inyeccion de dependencias con interfaces
//builder.Services.AddSingleton<HelloWorldService>(); // Se crea una unica instancia de la dependencia => No recomendable
builder.Services.AddScoped<IHelloWorldService, HelloWorldService>(); // App staless
// builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService()); // Inyectando de dependencia con interface

// EJEMPLO REAL Categorias & Tareas
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITareasService, TareasService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Ambiente para desarrollador - no para produccion
{
    // https://localhost:7294/swagger/index.html => Para ver la documentacion
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

//app.UseWelcomePage(); // Envia a una página de Bienvenida

//app.UseTime(); // Middleware personalizado

app.MapControllers();

app.Run();
