using CitasApp.Models;

namespace CitasApp.Interfaces
{
    public interface IPacienteRepository
    {
        // Cambiado a IEnumerable para que encaje con tu clase repositorio
        IEnumerable<Paciente> ObtenerTodos();

        Paciente? ObtenerPorId(int id);

        // Opcional: Si vas a usar los demás métodos desde tus servicios o controladores,
        // agrégalos aquí para que queden expuestos:
        void Agregar(Paciente paciente);
        void Actualizar(Paciente paciente);
        void Eliminar(int id);
    }
}