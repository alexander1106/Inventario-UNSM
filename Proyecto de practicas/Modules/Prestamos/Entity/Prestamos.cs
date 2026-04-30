using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;

namespace Proyecto_de_practicas.Modules.Articulos.Entities;
public class Prestamos
{
    public int Id { get; set; }


    public virtual Articulo Articulo { get; set; } = null!;
    public string? NombreSolicitante { get; set; }
    public DateTime? FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public int Estado { get; set; } = 1;
    public bool EstadoPrestamo { get; set; }
}
