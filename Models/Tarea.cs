using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

// En DB lo interpreta como int
public enum Prioridad
{
    Baja, Media, Alta
}

public class Tarea
{
    //[Key] // Etiqueta para decirle la clave de nuestra tabla ID
    public Guid TareaId { get; set; }

    //[ForeignKey("CategoriaId")] // Clave foranea o relacion de la tabla
    public Guid CategoriaId { get; set; }

    //[Required] // Obligatorio
    //[MaxLength(200)] // Largo de caracteres del string
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public Prioridad PrioridadTarea { get; set; }
    public DateTime FechaCreacion { get; set; }
    public virtual Categoria Categoria { get; set; }

    //[NotMapped] // Propiedad que no ira en la tabla de DB (Campo omitido)
    public string Resumen { get; set; }
}
