using CitasApp.Models;

namespace CitasApp.Interfaces
{
    public interface ICitaRepository
    {
        // Cambiado a IEnumerable y terminado en "Todas" para que cuadre con tu clase
        IEnumerable<Cita> ObtenerTodas();

        List<Cita> ObtenerPorPaciente(int pacienteId);
    }
}