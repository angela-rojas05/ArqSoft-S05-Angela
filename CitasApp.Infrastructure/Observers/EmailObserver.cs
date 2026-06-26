using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Observers
{
    public class EmailObserver : ICitaObserver
    {
        public void Notificar(Cita cita)
        {
            Console.WriteLine($"[Email] Recordatorio — Cita {cita.Id} | Motivo: {cita.Motivo} | Estado: {cita.Estado} | Fecha: {cita.Fecha} {cita.Hora}");
        }
    }
}