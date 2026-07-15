using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class PostgresPacienteRepository : IPacienteRepository
    {
        private readonly CitasAppDbContext _context;

        public PostgresPacienteRepository(CitasAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            return _context.Pacientes.ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public void Agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public void Actualizar(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
        }
    }
}