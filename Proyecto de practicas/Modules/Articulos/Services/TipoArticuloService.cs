using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Modules.Articulos.Entities;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Service;

namespace Proyecto_de_practicas.Modules.Articulos.Services
{
    public class TipoArticuloService : ITipoArticuloService
    {
        private readonly ITipoArticuloRepository _repo;
        private readonly IMapper _mapper;

        public TipoArticuloService(ITipoArticuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TipoArticuloDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TipoArticuloDTO>>(entities);
        }

        public async Task<TipoArticuloDTO?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TipoArticuloDTO>(entity);
        }
        public async Task<TipoArticuloDTO> AddAsync(TipoArticuloDTO dto)
        {
            bool existe = await _repo.ExisteNombreAsync(dto.Nombre);
            if (existe)
                throw new InvalidOperationException("Ya existe un tipo de artículo con ese nombre.");

            var entity = _mapper.Map<TipoArticulo>(dto);
            var result = await _repo.AddAsync(entity);
            return _mapper.Map<TipoArticuloDTO>(result);
        }

        public async Task<TipoArticuloDTO> UpdateAsync(int id, TipoArticuloDTO dto)
        {
            // 🔒 Validar relación
            bool tieneRelacion = await _repo.TieneRelacionConArticulosAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException(
                    "No se puede editar este tipo de artículo porque tiene artículos relacionados."
                );

            // 🔒 Validar nombre duplicado (excluyendo el mismo ID)
            bool existe = await _repo.ExisteNombreAsync(dto.Nombre, id);
            if (existe)
                throw new InvalidOperationException(
                    "Ya existe un tipo de artículo con ese nombre."
                );

            var entity = _mapper.Map<TipoArticulo>(dto);
            entity.Id = id;

            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<TipoArticuloDTO>(result);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            // Validar relación
            bool tieneRelacion = await _repo.TieneRelacionConArticulosAsync(id);
            if (tieneRelacion)
                throw new InvalidOperationException("No se puede eliminar este tipo de artículo porque tiene artículos relacionados.");

            return await _repo.DeleteAsync(id);
        }
        

        public async Task<TipoArticuloDTO?> ObtenerPorIdAsync(int id)
        {
            var entity = await _repo.GetByIdWithArticulosAsync(id);
            if (entity == null) return null;

            return _mapper.Map<TipoArticuloDTO>(entity);
        }


        public async Task<List<string>> GetEncabezadoArticulosAsync(int idTipoArticulo)
        {
            return await _repo.GetEncabezadoArticulosAsync(idTipoArticulo);
        }

    }
}