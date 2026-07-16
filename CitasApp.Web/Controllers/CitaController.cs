using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;
 using Microsoft.AspNetCore.Authorization;

namespace CitasApp.Controllers
{

   

    [Authorize]
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo,
                              IPacienteRepository pacienteRepo,
                              IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        private void CargarListasDropdown()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos().ToList();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos().ToList();
        }

        public IActionResult Index()
        {
            CargarListasDropdown();
            return View(_citaRepo.ObtenerTodas());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            CargarListasDropdown();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        public IActionResult Create()
        {
            CargarListasDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                CargarListasDropdown();
                return View(cita);
            }

            _citaRepo.Agregar(cita);
            return RedirectToAction(nameof(Index));
        }
    }
}