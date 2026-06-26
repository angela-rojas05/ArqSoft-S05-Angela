using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class MemoriaPacienteRepository : IPacienteRepository
    {
        private static readonly List<Paciente> _pacientes = new();

        public IEnumerable<Paciente> ObtenerTodos()
        {
            return _pacientes.ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _pacientes.FirstOrDefault(p => p.Id == id);
        }

        public void Agregar(Paciente paciente)
        {
            paciente.Id = _pacientes.Count > 0 ? _pacientes.Max(p => p.Id) + 1 : 1;
            _pacientes.Add(paciente);
        }

        public void Actualizar(Paciente paciente)
        {
            var index = _pacientes.FindIndex(p => p.Id == paciente.Id);

            if (index != -1)
            {
                _pacientes[index] = paciente;
            }
        }

        public void Eliminar(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);

            if (paciente != null)
            {
                _pacientes.Remove(paciente);
            }
        }
    }
}
