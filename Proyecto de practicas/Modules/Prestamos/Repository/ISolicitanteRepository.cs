namespace Proyecto_de_practicas.Modules.Prestamos.Repository
{
    public interface ISolicitanteRepository
    {
        Task<List<Solicitantes>> GetAllAsync();
        Task<Solicitantes?> GetByIdAsync(int id);
        Task<Solicitantes> AddAsync(Solicitantes solicitante);
        Task UpdateAsync(Solicitantes solicitante);
        Task<bool> ExisteCodigoAsync(string codigo);
        Task DeleteAsync(Solicitantes solicitante);
        Task<List<Solicitantes>> GetByUsuarioAsync(int usuarioId);
    }
}
