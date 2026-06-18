using CitasApp.Domain.Interfaces;
using CitasApp.Models;

namespace CitasApp.Application.Services;

public class PacienteService
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacienteService(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public IEnumerable<Paciente> ObtenerTodos()
    {
        return _pacienteRepository.ObtenerTodos();
    }

    public Paciente? ObtenerPorId(int id)
    {
        return _pacienteRepository.ObtenerTodos().FirstOrDefault(p => p.Id == id);
    }
}
