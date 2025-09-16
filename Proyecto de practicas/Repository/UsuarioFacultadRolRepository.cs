using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;
using Microsoft.EntityFrameworkCore;


namespace Proyecto_de_practicas.Repository
{
    public class UsuarioFacultadRolRepository : IUsuarioFacultadRolRepository
    {
        private readonly AplicationDBContext _context;

        public UsuarioFacultadRolRepository(AplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteAsignacionAsync(int idUsuario, int idFacultad, int idRol)
        {
            return await _context.UsuarioFacultadRol
                .AnyAsync(x => x.IdUsuario == idUsuario &&
                               x.IdFacultad == idFacultad &&
                               x.IdRol == idRol);
        }

        public async Task AgregarAsync(UsuarioFacultadRol entidad)
        {
            await _context.UsuarioFacultadRol.AddAsync(entidad);
        }

        public async Task GuardarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
