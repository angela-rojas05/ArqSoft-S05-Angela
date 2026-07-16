using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Authorization;

namespace CitasApp.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _repo;

    
    public PacienteController(IPacienteRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.ObtenerTodos());
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _repo.ObtenerPorId(id);

            return paciente == null
                ? NotFound()
                : View(paciente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            _repo.Agregar(paciente);
            return RedirectToAction(nameof(Index));
        }
    }
}