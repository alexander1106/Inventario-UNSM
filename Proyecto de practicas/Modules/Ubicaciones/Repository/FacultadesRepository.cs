using System;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Ubicaciones.DTO;
using Proyecto_de_practicas.Modules.Ubicaciones.Entities;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;
namespace Proyecto_de_practicas.Modules.Ubicaciones.Repository
{
    public class FacultadesRepository : IFacultadesRepository
    {
        private readonly AplicationDBContext _context;

        public FacultadesRepository(AplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FacultadesDetalleDto>> GetAllDetalleAsync()
        {
            return await _context.Facultades
                .Select(f => new FacultadesDetalleDto
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Direccion = f.Direccion,
                    Estado = f.Estado,
                    SedeId = f.SedeId, // ✅ AQUI

                    NroEscuelas = f.Escuelas.Count(),
                    NroBienes = 0
                })
                .ToListAsync();
        }

        public async Task<FacultadesDetalleDto?> GetDetalleByIdAsync(int id)
        {
            return await _context.Facultades
                .Where(f => f.Id == id)
                .Select(f => new FacultadesDetalleDto
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Direccion = f.Direccion,
                    Estado = f.Estado,
                    SedeId = f.SedeId,
                    NroEscuelas = f.Escuelas.Count(),
                    NroBienes = 0
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Facultades>> GetAllAsync()
        {
            return await _context.Facultades
                .Include(f => f.Sede)
                .ToListAsync();
        }

        public async Task<Facultades?> GetByIdAsync(int id)
        {
            return await _context.Facultades
                .Include(f => f.Sede)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Facultades>> GetBySedeIdAsync(int sedeId)
        {
            return await _context.Facultades
                .Where(f => f.SedeId == sedeId)
                .ToListAsync();
        }

        public async Task<Facultades> CreateAsync(Facultades facultad)
        {
            _context.Facultades.Add(facultad);
            await _context.SaveChangesAsync();
            return facultad;
        }

        public async Task UpdateAsync(Facultades facultad)
        {
            _context.Facultades.Update(facultad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var facultad = await _context.Facultades.FindAsync(id);

            if (facultad != null)
            {
                _context.Facultades.Remove(facultad);
                await _context.SaveChangesAsync();
            }
        }

    }
}