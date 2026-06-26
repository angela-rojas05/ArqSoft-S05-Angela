using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class CitaService
{
    private readonly ICitaRepository _citaRepository;
    private readonly IEnumerable<ICitaObserver> _observers;

    public CitaService(ICitaRepository citaRepository, IEnumerable<ICitaObserver> observers)
    {
        _citaRepository = citaRepository;
        _observers = observers;
    }

    public void Confirmar(int citaId)
    {
        var cita = _citaRepository.ObtenerTodas()
            .FirstOrDefault(c => c.Id == citaId);
        if (cita == null) return;

        foreach (var observer in _observers)
            observer.Notificar(cita);
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