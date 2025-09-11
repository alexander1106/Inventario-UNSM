using Proyecto_de_practicas.Models;

namespace Proyecto_de_practicas.Service
{
    public interface IAulasService
    {
        Task<List<Aulas>> GetListAula();
        Task<Aulas?> GetAula(int id);
        Task<Aulas> AddAula(Aulas aulas);
        Task<Aulas?> ActuallizarAula(Aulas aulas);
        Task<bool> EliminarAula(int id);
    }
}
