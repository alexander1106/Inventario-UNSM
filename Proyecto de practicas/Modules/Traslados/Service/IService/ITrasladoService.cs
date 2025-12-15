using Proyecto_de_practicas.Modules.Traslados.Entities;

namespace Proyecto_de_practicas.Modules.Traslados.Service.IService
{
    public interface ITrasladoService
    {
        Task<List<Traslado>> GetAllAsync();
        Task<Traslado> GetByIdAsync(int id);
        Task<Traslado> CreateAsync(Traslado traslado);
        Task<Traslado> UpdateAsync(Traslado traslado);
        Task<bool> DeleteAsync(int id);
        Task<bool> RealizarTrasladoAsync(Traslado traslado);

    }
}
