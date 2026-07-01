namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public static class DepreciacionCalculator
    {
        // Resta del valor de adquisición la depreciación (%) acumulada por cada año
        // completo transcurrido entre la fecha de adquisición y la fecha actual.
        public static decimal CalcularValorActual(double valorAdquisitivo, DateTime fechaAdquision, double depreciacionAnualPorcentaje)
        {
            if (depreciacionAnualPorcentaje <= 0 || valorAdquisitivo <= 0)
                return (decimal)valorAdquisitivo;

            var hoy = DateTime.Now;

            int aniosTranscurridos = hoy.Year - fechaAdquision.Year;
            if (hoy.Date < fechaAdquision.Date.AddYears(aniosTranscurridos))
                aniosTranscurridos--;
            if (aniosTranscurridos < 0)
                aniosTranscurridos = 0;

            decimal valor = (decimal)valorAdquisitivo;
            decimal depreciacionAnualMonto = valor * ((decimal)depreciacionAnualPorcentaje / 100m);
            decimal valorActual = valor - (depreciacionAnualMonto * aniosTranscurridos);

            return valorActual < 0 ? 0 : valorActual;
        }
    }
}
