using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Proyecto_de_practicas.Modules.Articulos.DTO;

namespace Proyecto_de_practicas.Modules.Ubicaciones.DTO
{
    public class TipoArticuloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Estado { get; set; } = 1;

        [NotMapped]
        public IFormFile? Imagen { get; set; }  // Para subir la imagen

        public string? ImagenPath { get; set; } // Para guardar la ruta en DB
    }

}
