using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly CitaService _citaService;
    private readonly PacienteService _pacienteService;
    private readonly MedicoService _medicoService;

    public CitasController(
        CitaService citaService,
        PacienteService pacienteService,
        MedicoService medicoService)
    {
        _citaService = citaService;
        _pacienteService = pacienteService;
        _medicoService = medicoService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_citaService.ObtenerTodos());
    }

    [HttpGet("porpaciente/{pacienteId}")]
    public IActionResult PorPaciente(int pacienteId)
    {
        var paciente = _pacienteService.ObtenerPorId(pacienteId);

        if (paciente == null)
        {
            return NotFound();
        }

        var citas = _citaService.ObtenerPorPaciente(pacienteId);

        return citas.Count == 0 ? NotFound() : Ok(citas);
    }


    [HttpPost("{citaId}/confirmar")]
    public IActionResult Confirmar(int citaId)
    {
        _citaService.Confirmar(citaId);
        return Ok($"Cita {citaId} confirmada. Observers notificados.");
    }
}
