using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class CitaService
{
    private readonly ICitaRepository _citaRepository;

    public CitaService(ICitaRepository citaRepository)
    {
        _citaRepository = citaRepository;
    }

    public IEnumerable<Cita> ObtenerTodos()
    {
        return _citaRepository.ObtenerTodas();
    }

    public List<Cita> ObtenerPorPaciente(int pacienteId)
    {
        return _citaRepository.ObtenerTodas()
            .Where(c => c.PacienteId == pacienteId)
            .ToList();
    }
}
