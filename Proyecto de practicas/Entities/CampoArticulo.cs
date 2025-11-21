using System.ComponentModel.DataAnnotations;

namespace Proyecto_de_practicas.Models
{
    public class CampoArticulo
    {

        public int Id { get; set; }

        public string NombreCampo { get; set; } = null!;

        public string TipoDato { get; set; } = "string"; // string, number, boolean, date


        // Relación con TipoArticulo
        public int TipoArticuloId { get; set; }
        public virtual TipoArticulo TipoArticulo { get; set; } = null!;

    }
}