using System.Security.Claims;
using CitasApp.Controllers;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CitasApp.Tests.Controllers
{
    // -----------------------------
    // Repositorios Fake
    // -----------------------------

    public class CitaRepositoryFake : ICitaRepository
    {
        private readonly List<Cita> _citas;

        public CitaRepositoryFake(List<Cita> citas)
        {
            _citas = citas;
        }

        public IEnumerable<Cita> ObtenerTodas()
        {
            return _citas;
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _citas.Where(c => c.PacienteId == pacienteId).ToList();
        }

        public void Agregar(Cita cita)
        {
            _citas.Add(cita);
        }
    }

    public class PacienteRepositoryFake : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes;

        public PacienteRepositoryFake(List<Paciente> pacientes)
        {
            _pacientes = pacientes;
        }

        public IEnumerable<Paciente> ObtenerTodos()
        {
            return _pacientes;
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _pacientes.FirstOrDefault(p => p.Id == id);
        }

        public void Agregar(Paciente paciente)
        {
            _pacientes.Add(paciente);
        }

        public void Actualizar(Paciente paciente)
        {
        }

        public void Eliminar(int id)
        {
        }
    }

    public class MedicoRepositoryFake : IMedicoRepository
    {
        private readonly List<Medico> _medicos;

        public MedicoRepositoryFake(List<Medico> medicos)
        {
            _medicos = medicos;
        }

        public IEnumerable<Medico> ObtenerTodos()
        {
            return _medicos;
        }

        public Medico? ObtenerPorId(int id)
        {
            return _medicos.FirstOrDefault(m => m.Id == id);
        }

        public void Agregar(Medico medico)
        {
            _medicos.Add(medico);
        }

        public void Actualizar(Medico medico)
        {
        }

        public void Eliminar(int id)
        {
        }
    }

    // -----------------------------
    // Pruebas
    // -----------------------------

    public class CitaControllerAdminTests
    {
        private CitaController CrearController(out List<Cita> citas)
        {
            citas = new List<Cita>
            {
                new Cita
                {
                    Id = 1,
                    PacienteId = 10,
                    Estado = "Pendiente"
                },

                new Cita
                {
                    Id = 2,
                    PacienteId = 20,
                    Estado = "Confirmada"
                },

                new Cita
                {
                    Id = 3,
                    PacienteId = 10,
                    Estado = "Pendiente"
                }
            };

            var pacientes = new List<Paciente>
            {
                new Paciente
                {
                    Id = 10,
                    Email = "paciente1@correo.com"
                },

                new Paciente
                {
                    Id = 20,
                    Email = "paciente2@correo.com"
                }
            };

            var medicos = new List<Medico>
            {
                new Medico
                {
                    Id = 1,
                    Nombre = "Dr. Pérez"
                }
            };

            var controller = new CitaController(
                new CitaRepositoryFake(citas),
                new PacienteRepositoryFake(pacientes),
                new MedicoRepositoryFake(medicos));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"admin@test.com")
            };

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(claims, "Test"))
                }
            };

            return controller;
        }

        [Fact]
        public void Index_RegresaTodasLasCitas()
        {
            // Arrange
            var controller = CrearController(out var citasEsperadas);

            // Act
            var resultado = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(resultado);

            var modelo = Assert.IsAssignableFrom<IEnumerable<Cita>>(resultado.Model);

            Assert.Equal(citasEsperadas.Count, modelo.Count());
        }

        [Fact]
        public void Index_CargaPacientesYMedicosEnViewBag()
        {
            // Arrange
            var controller = CrearController(out _);

            // Act
            controller.Index();

            // Assert
            Assert.NotNull(controller.ViewBag.Pacientes);
            Assert.NotNull(controller.ViewBag.Medicos);
        }

        [Fact]
        public void PorPaciente_RegresaSoloLasCitasDelPaciente()
        {
            // Arrange
            var controller = CrearController(out _);

            // Act
            var resultado = controller.PorPaciente(10) as ViewResult;

            // Assert
            Assert.NotNull(resultado);

            var modelo = Assert.IsAssignableFrom<IEnumerable<Cita>>(resultado.Model);

            Assert.All(modelo, c => Assert.Equal(10, c.PacienteId));
        }
    }
}