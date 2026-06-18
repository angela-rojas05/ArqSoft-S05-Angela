using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonMedicoRepository()
        {
            _path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "data",
                "medicos.json");

            // Asegurar que la carpeta "data" y el archivo existan al iniciar
            var directory = Path.GetDirectoryName(_path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        // ── Helper para guardar de forma centralizada ───────────────────────────
        private void GuardarTodos(IEnumerable<Medico> medicos)
        {
            var json = JsonSerializer.Serialize(medicos, _options);
            File.WriteAllText(_path, json);
        }

        // ── Métodos de la Interfaz (Contrato de IMedicoRepository) ──────────────

        public IEnumerable<Medico> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new List<Medico>();

            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Medico>>(json, _options) ?? new List<Medico>();
        }

        public Medico? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var medicos = ObtenerTodos().ToList();
            // Generar ID auto-incrementable (si no hay elementos, empieza en 1)
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            GuardarTodos(medicos);
        }

        public void Actualizar(Medico medico)
        {
            var medicos = ObtenerTodos().ToList();
            var index = medicos.FindIndex(m => m.Id == medico.Id);
            if (index != -1)
            {
                medicos[index] = medico;
                GuardarTodos(medicos);
            }
        }

        public void Eliminar(int id)
        {
            var medicos = ObtenerTodos().ToList();
            var aEliminar = medicos.FirstOrDefault(m => m.Id == id);
            if (aEliminar != null)
            {
                medicos.Remove(aEliminar);
                GuardarTodos(medicos);
            }
        }
    }
}