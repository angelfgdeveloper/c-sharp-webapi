using webapi.Models;

namespace webapi.Services;

public class CategoriaService : ICategoriaService
{
  private TareasContext context;

  public CategoriaService(TareasContext dbContext)
  {
    this.context = dbContext;
  }
  
  public IEnumerable<Categoria> Get()
  {
    return this.context.Categorias;
  }

  // Sincrono
  public void SaveSincrono(Categoria categoria)
  {
    this.context.Add(categoria);
    this.context.SaveChanges();
  }

  // Asincrono
  public async Task Save(Categoria categoria)
  {
    this.context.Add(categoria);
    await this.context.SaveChangesAsync(); // Guardar
  }

  public async Task Update(Guid id, Categoria categoria)
  {
    var categoriaActual = this.context.Categorias.Find(id);

    if (categoriaActual != null)
    {
      categoriaActual.Nombre = categoria.Nombre; 
      categoriaActual.Descripcion = categoria.Descripcion;
      categoriaActual.Peso = categoria.Peso;

      await this.context.SaveChangesAsync(); // Guardar
    }
    
  }

  public async Task Delete(Guid id)
  {
    var categoriaActual = this.context.Categorias.Find(id);

    if (categoriaActual != null)
    {
      this.context.Remove(categoriaActual);
      await this.context.SaveChangesAsync(); // Guardar
    }

  }

}

public interface ICategoriaService
{
   IEnumerable<Categoria> Get();
   Task Save(Categoria categoria);
   Task Update(Guid id, Categoria categoria);
   Task Delete(Guid id);
}