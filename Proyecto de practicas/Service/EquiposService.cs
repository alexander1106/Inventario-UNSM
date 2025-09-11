using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Service
{
    public class EquiposService : IEquiposService
    {
            private readonly IEquiposRepository _repository;

            public EquiposService(IEquiposRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Equipos>> GetListEquipos()
            {
                return await _repository.GetAllAsync();
            }

            public async Task<Equipos?> GetEquipos(int id)
            {
                return await _repository.GetByIdAsync(id);
            }

            public async Task<Equipos> AddEquipos(Equipos equipo)
            {
                // Lógica de negocio: nombre único
                var existente = await _repository.GetByNombreAsync(equipo.Nombre);
                if (existente != null)
                    throw new Exception("Ya existe un equipo con ese nombre");

                return await _repository.AddAsync(equipo);
            }

            public async Task<Equipos?> ActualizarEquipoAsync(Equipos equipo)
            {
                var existente = await _repository.GetByIdAsync(equipo.Id);
                if (existente == null) return null;

                if (string.IsNullOrEmpty(equipo.Nombre))
                    throw new Exception("El nombre no puede estar vacío");

                existente.Nombre = equipo.Nombre;

                return await _repository.UpdateAsync(existente);
            }

            public async Task<bool> EliminarEquipoAsync(int id)
            {
                return await _repository.DeleteAsync(id);
            }
        }
}

