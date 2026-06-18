using CitasApp.Domain.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonPacienteRepository()
        {
            _path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "data",
                "pacientes.json");

            // Asegurar que la carpeta "data" exista al iniciar el repositorio
            var directory = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        // ── Helper para guardar la lista serializada en el archivo ──────────────
        private void GuardarTodos(IEnumerable<Paciente> pacientes)
        {
            var json = JsonSerializer.Serialize(pacientes, _options);
            File.WriteAllText(_path, json);
        }

        // ── Métodos de la Interfaz (Contrato de IPacienteRepository) ────────────

        // CORRECCIÓN 1: Cambiado de List a IEnumerable para cumplir la interfaz
        public IEnumerable<Paciente> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new List<Paciente>();

            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Paciente>>(json, _options) ?? new List<Paciente>();
        }

        public Paciente? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(p => p.Id == id);

        // CORRECCIÓN 2: Implementación de los métodos faltantes de la interfaz
        public void Agregar(Paciente paciente)
        {
            var pacientes = ObtenerTodos().ToList();
            // Generar ID autoincremental de forma segura
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            GuardarTodos(pacientes);
        }

        public void Actualizar(Paciente paciente)
        {
            var pacientes = ObtenerTodos().ToList();
            var index = pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index != -1)
            {
                pacientes[index] = paciente;
                GuardarTodos(pacientes);
            }
        }

        public void Eliminar(int id)
        {
            var pacientes = ObtenerTodos().ToList();
            var aEliminar = pacientes.FirstOrDefault(p => p.Id == id);
            if (aEliminar != null)
            {
                pacientes.Remove(aEliminar);
                GuardarTodos(pacientes);
            }
        }
    }
}