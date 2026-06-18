using CitasApp.Domain.Interfaces;
using CitasApp.Models;
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
    }
}