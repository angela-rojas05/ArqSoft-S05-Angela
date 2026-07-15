using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCitaRepository()
        {
            _path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "data",
                "citas.json");
        }

        // CORRECCIÓN: Cambiado de List<Cita> a IEnumerable<Cita>
        public IEnumerable<Cita> ObtenerTodas()
        {
            if (!File.Exists(_path)) return new List<Cita>(); // Retorna lista vacía si no existe el archivo

            var json = File.ReadAllText(_path);
            var citasJson = JsonSerializer.Deserialize<List<CitaJson>>(json, _options) ?? new();

            return citasJson.Select(c => new Cita
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                Fecha = DateOnly.Parse(c.Fecha),
                Hora = TimeOnly.Parse(c.Hora),
                Motivo = c.Motivo,
                Estado = c.Estado
            }).ToList();
        }

        // CORRECCIÓN: Cambiado de List<Cita> a IEnumerable<Cita> para cumplir con la interfaz
        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodas().Where(c => c.PacienteId == pacienteId).ToList();

        public void Agregar(Cita cita)
        {
            var citas = ObtenerTodas().ToList();

            cita.Id = citas.Count == 0 ? 1 : citas.Max(c => c.Id) + 1;
            citas.Add(cita);

            var citasJson = citas.Select(c => new CitaJson
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                Fecha = c.Fecha.ToString(),
                Hora = c.Hora.ToString(),
                Motivo = c.Motivo,
                Estado = c.Estado
            }).ToList();

            var json = JsonSerializer.Serialize(citasJson, _options);
            File.WriteAllText(_path, json);
        }
    }
}