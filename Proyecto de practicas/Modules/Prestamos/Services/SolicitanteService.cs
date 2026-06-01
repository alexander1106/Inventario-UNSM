using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Modules.Prestamos.DTO;
using Proyecto_de_practicas.Modules.Prestamos.Repository;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;

namespace Proyecto_de_practicas.Modules.Prestamos.Services


{
    public class SolicitanteService : ISolicitanteService
    {
        private readonly ISolicitanteRepository _repository;

        public SolicitanteService(ISolicitanteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SolicitanteDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(x => new SolicitanteDto
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Correo = x.Correro,
                Cargo = x.Cargo,
                Ciclo = x.Ciclo,
                Telefono = x.Telefono,
                UbicacionId = x.UbicacionId
            }).ToList();
        }
        public async Task<List<SolicitanteDto>> GetByUsuarioAsync(int usuarioId)
        {
            var data = await _repository.GetByUsuarioAsync(usuarioId);

            return data.Select(x => new SolicitanteDto
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Correo = x.Correro,
                Telefono = x.Telefono,
                Cargo = x.Cargo,
                Ciclo = x.Ciclo,
                UbicacionId = x.UbicacionId
            }).ToList();
        }
        public async Task<SolicitanteDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null) return null;

            return new SolicitanteDto
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Nombres = x.Nombres,
                Telefono= x.Telefono,
                Cargo = x.Cargo,
                Ciclo= x.Ciclo,
                Apellidos = x.Apellidos,
                Correo = x.Correro,
                UbicacionId = x.UbicacionId
            };
        }

        public async Task<SolicitanteDto> CreateAsync(SolicitanteDto dto)
        {
            // 1. VALIDACIÓN: si el código ya existe
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
            {
                throw new Exception("El código del solicitante ya existe");
            }

            // 2. CREAR ENTIDAD
            var entity = new Solicitantes
            {
                Codigo = dto.Codigo,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correro = dto.Correo,
                Ciclo = dto.Ciclo,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                UbicacionId = dto.UbicacionId
            };

            await _repository.AddAsync(entity);

            dto.Id = entity.Id;
            return dto;
        }
        public async Task<bool> UpdateAsync(int id, SolicitanteDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return false;

            entity.Codigo = dto.Codigo;
            entity.Nombres = dto.Nombres;
            entity.Cargo = dto.Cargo;
            entity.Ciclo = dto.Ciclo;
            entity.Telefono = dto.Telefono;
            entity.Apellidos = dto.Apellidos;
            entity.Correro = dto.Correo;
            entity.UbicacionId = dto.UbicacionId;

            await _repository.UpdateAsync(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);

            return true;
        }
    }
}