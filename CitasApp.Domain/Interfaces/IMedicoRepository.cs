using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IMedicoRepository
    {
        // Cambiado a IEnumerable para que coincida con la clase
        IEnumerable<Medico> ObtenerTodos();

        Medico? ObtenerPorId(int id);

        // NOTA: Si en tu controlador usas Agregar, Actualizar o Eliminar, 
        // deberías declararlos aquí también, por ejemplo:
        void Agregar(Medico medico);
        void Actualizar(Medico medico);
        void Eliminar(int id);
    }
}