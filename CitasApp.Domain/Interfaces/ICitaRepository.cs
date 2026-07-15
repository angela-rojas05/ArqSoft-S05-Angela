using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaRepository
    {
        // Cambiado a IEnumerable y terminado en "Todas" para que cuadre con tu clase
        IEnumerable<Cita> ObtenerTodas();

        List<Cita> ObtenerPorPaciente(int pacienteId);

        void Agregar(Cita cita);
    }
}