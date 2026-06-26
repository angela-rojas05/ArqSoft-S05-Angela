using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services;

public class MedicoService
{
    private readonly IMedicoRepository _medicoRepository;

    public MedicoService(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    public IEnumerable<Medico> ObtenerTodos()
    {
        return _medicoRepository.ObtenerTodos();
    }

    public Medico? ObtenerPorId(int id)
    {
        return _medicoRepository.ObtenerTodos().FirstOrDefault(m => m.Id == id);
    }
}
