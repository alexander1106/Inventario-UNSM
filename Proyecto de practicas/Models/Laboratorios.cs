namespace Proyecto_de_practicas.Models
{
    public class Laboratorios : Ubicacion
    {
        public Laboratorios() { } // para EF Core

        public Laboratorios(string nombre, int pisosId, string estado = "Activo")
            : base(nombre, pisosId, estado)
        {
        }
    }
}