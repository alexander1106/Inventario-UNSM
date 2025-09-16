namespace Proyecto_de_practicas.Models
{
    public class Aulas : Ubicacion
    {
        public Aulas() { } // para EF Core

        public Aulas(string nombre, int pisosId, string estado = "Activo")
            : base(nombre, pisosId, estado)
        {
        }
    }
}