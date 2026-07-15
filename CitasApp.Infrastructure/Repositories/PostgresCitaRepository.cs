using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class PostgresCitaRepository : ICitaRepository
    {
        private readonly CitasAppDbContext _context;

        public PostgresCitaRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cita> ObtenerTodas()
        {
            return _context.Citas.ToList();
        }

        public void Agregar(Cita cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _context.Citas
                .Where(c => c.PacienteId == pacienteId)
                .ToList();
        }

    }
}