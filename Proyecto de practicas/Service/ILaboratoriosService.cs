using Proyecto_de_practicas.Models;
namespace Proyecto_de_practicas.Service
{
    public interface ILaboratoriosService
    {
        Task<List<Laboratorios>> GetListLaboratorios();
        Task<Laboratorios?> GetLaboratorios(int id);
        Task<Laboratorios> AddLaboratorios(Laboratorios laboratorio);
        Task<Laboratorios?> ActualizarLaboratorioAsync(Laboratorios laboratorio);
        Task<bool> EliminarLaboratorioAsync(int id);
    }
}
