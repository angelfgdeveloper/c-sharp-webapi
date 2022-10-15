using webapi.Models;

namespace webapi.Services;

public class TareasService : ITareasService
{
  private TareasContext context;

  public TareasService(TareasContext dbContext)
  {
    this.context = dbContext;
  }
  
  public IEnumerable<Tarea> Get()
  {
    return this.context.Tareas;
  }

  // Sincrono
  public void SaveSincrono(Tarea Tarea)
  {
    this.context.Add(Tarea);
    this.context.SaveChanges();
  }

  // Asincrono
  public async Task Save(Tarea Tarea)
  {
    this.context.Add(Tarea);
    await this.context.SaveChangesAsync(); // Guardar
  }

  public async Task Update(Guid id, Tarea Tarea)
  {
    var TareaActual = this.context.Tareas.Find(id);

    if (TareaActual != null)
    {
      TareaActual.Titulo = Tarea.Titulo; 
      TareaActual.Descripcion = Tarea.Descripcion;

      await this.context.SaveChangesAsync(); // Guardar
    }
    
  }

  public async Task Delete(Guid id)
  {
    var TareaActual = this.context.Tareas.Find(id);

    if (TareaActual != null)
    {
      this.context.Remove(TareaActual);
      await this.context.SaveChangesAsync(); // Guardar
    }

  }

}

public interface ITareasService
{
   IEnumerable<Tarea> Get();
   Task Save(Tarea Tarea);
   Task Update(Guid id, Tarea Tarea);
   Task Delete(Guid id);
}