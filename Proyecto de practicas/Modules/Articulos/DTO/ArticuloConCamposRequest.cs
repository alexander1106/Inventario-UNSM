using System;
using System.Collections.Generic;

namespace Proyecto_de_practicas.Modules.Articulos.DTO
{
    public class ArticuloConCamposRequest
    {
        public string CodigoPatrimonial { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaAdquision { get; set; }
        public decimal ValorAdquisitivo { get; set; }
        public string Condicion { get; set; } = string.Empty;
        public int TipoArticuloId { get; set; }
        public int? UbicacionId { get; set; } // Permitimos nulo si el SP maneja DBNull.Value
        public int Estado { get; set; }
        public int? VidaUtil { get; set; } // Cambiado a int? para evitar ceros por defecto en la BD

        // ✨ NUEVOS CAMPOS TÉCNICOS Y LOGÍSTICOS
        public string? CodigoBarra { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? NroSerie { get; set; }
        public string? Medidas { get; set; }
        public string? Color { get; set; }

        // ✨ NUEVOS CAMPOS CONTABLES Y DEPRECIACIÓN
        public string? Mayor { get; set; }
        public string? SubCta { get; set; }
        public decimal HValorInicial { get; set; }
        public decimal HDeprInicial { get; set; }
        public decimal HDeprAjustada { get; set; }
        public decimal HDeprEjercicio { get; set; }
        public decimal ValorNeto { get; set; }

        public List<CampoValorDto>? CamposValores { get; set; } = new();
    }
}