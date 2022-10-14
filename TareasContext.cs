using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi;

public class TareasContext: DbContext
{
    // Los modelos se manejan en plural para referirse a la Tabla de la DB
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    // Configuracion general de Entity Framework
    public TareasContext(DbContextOptions<TareasContext> options): base(options) { }

    // Sobreescritura para crear los modelos - Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriaInit = new List<Categoria>();
        /*Guid.NewGuid() => Id random*/
        categoriaInit.Add(new Categoria() { 
            CategoriaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b320ae"),
            Nombre = "Actividades pendientes",
            Peso = 20
        });

        categoriaInit.Add(new Categoria() { 
            CategoriaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b320ef"),
            Nombre = "Actividades personales",
            Peso = 50
        });

        // Configuracion de modelo Categoria
        modelBuilder.Entity<Categoria>(categoria => 
        {
            // Las tablas deben ir en singular
            // Validaciones de nuestros campos
            categoria.ToTable("Categoria");
            categoria.HasKey(c => c.CategoriaId);

            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion).IsRequired(false); // No es requerido;
            categoria.Property(c => c.Peso); // #1 Nuevo campo en base de datos
            // #2 Terminal: Generar migracion => dotnet ef migrations add ColumnPesoCategoria
            // #3 Actualizar DB: dotnet ef database update

            categoria.HasData(categoriaInit); // Lista de datos
        });

        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b32010"),
            CategoriaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b320ae"),
            PrioridadTarea = Prioridad.Media,
            Titulo = "Pago de servicios públicos",
            FechaCreacion = DateTime.Now
        });

        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b32011"),
            CategoriaId = Guid.Parse("ced2eed1-8422-4699-8a77-e1c215b320ef"),
            PrioridadTarea = Prioridad.Baja,
            Titulo = "Términar de ver películas en Netflix",
            FechaCreacion = DateTime.Now
        });

        // Configuracion de modelo Tarea
        modelBuilder.Entity<Tarea>(tarea => // Ingresamos a cada una de las propiedades
        {
            // Las tablas deben ir en singular
            // Validaciones de nuestros campos
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId); // Asignar ID

            // Relacion entre ambos campos
            tarea.HasOne(t => t.Categoria).WithMany(c => c.Tareas).HasForeignKey(t => t.CategoriaId);

            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(t => t.Descripcion).IsRequired(false); // No es requerida
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);

            // Ignorar campo
            tarea.Ignore(t => t.Resumen);

            tarea.HasData(tareasInit); // Lista de datos
            // Agregamos los datos insertados en la DB
            // Terminal => dotnet ef migrations add InitialData
            // dotnet ef database update
        });

    }
}
