using Microsoft.AspNetCore.Mvc;
using CitasApp.Domain.Models;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Controllers
{
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

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerTodas());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        public IActionResult Create()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos().ToList();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pacientes = _pacienteRepo.ObtenerTodos().ToList();
                ViewBag.Medicos = _medicoRepo.ObtenerTodos().ToList();
                return View(cita);
            }

            _citaRepo.Agregar(cita);
            return RedirectToAction(nameof(Index));
        }
    }
}