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

        // 📂 Archivo que recibes desde el formulario (Swagger, Angular, Postman)
        [JsonIgnore] // No se serializa al devolver JSON
        public IFormFile? Imagen { get; set; }

        // 📝 Ruta de la imagen que se guarda en la BD
        public string? ImagenPath { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<ArticuloDto>? Articulos { get; set; }
    }
}
