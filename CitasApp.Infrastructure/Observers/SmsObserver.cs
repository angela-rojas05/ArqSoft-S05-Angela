using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class SmsObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[SMS] Cita {cita.Id} confirmada — Paciente {cita.PacienteId} con Médico {cita.MedicoId} el {cita.Fecha} a las {cita.Hora}");
        }
    }
}