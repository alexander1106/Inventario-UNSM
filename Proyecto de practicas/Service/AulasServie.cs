using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;

namespace Proyecto_de_practicas.Service
{
    public class AulasServie : IAulasService
    {
        private readonly IAulasRepository aulasRepository;

        public AulasServie(IAulasRepository aulasRepository)
        {
            this.aulasRepository = aulasRepository;
        }

        public async Task<Aulas?> ActuallizarAula(Aulas aulas)
        {
            var existente = await aulasRepository.GetByIdAsync(aulas.Id);
            if (existente != null)
            {
                throw new Exception("Ya exuste un aula con ese nombre");
            }
            return await aulasRepository.AddAsync(aulas);
        }

        public async Task<Aulas> AddAula(Aulas aulas)
        {
            // Lógica de negocio: nombre único
            var existente = await aulasRepository.GetByNombreAsync(aulas.Nombre);
            if (existente != null)
                throw new Exception("Ya existe un laboratorio con ese nombre");

            return await aulasRepository.AddAsync(aulas);
        }

        public async Task<bool> EliminarAula(int id)
        {
            return await aulasRepository.DeleteAsync(id);
        }

        public async Task<Aulas?> GetAula(int id)
        {
            return await aulasRepository.GetByIdAsync(id);

        }

        public async Task<List<Aulas>> GetListAula()
        {
            return await aulasRepository.GetAllAsync();
        }
    }
}
