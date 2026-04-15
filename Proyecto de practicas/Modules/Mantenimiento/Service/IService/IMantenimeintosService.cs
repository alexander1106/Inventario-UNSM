using Proyecto_de_practicas.Modules.Mantenimiento.DTO;
using Proyecto_de_practicas.Modules.Mantenimiento.Entity;

namespace Proyecto_de_practicas.Modules.Mantenimiento.Service.IService
{
    public interface IMantenimeintosService
    {
        Task<List<Mantenimientos>> GetAll();
        Task<Mantenimientos?> GetById(int id);
        Task<Mantenimientos> Create(MantenimientosCreateDTO dto);
        Task<bool> UpdateEstado(int id, MantenimientosUpdateDTO dto);
        Task<bool> Delete(int id);
    }
}