using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Interfaces
{
    public interface IProvinciaRepositorio
    {
        List<Provincia> Listar(bool incluirInactivas);
        Provincia? ObtenerPorId(int provinciaId);

        // Inserta y devuelve el nuevo ProvinciaId.
        int Insertar(Provincia provincia, int usuarioId);

        // <returns>true si se modificó un registro.</returns>
        bool Actualizar(Provincia provincia, int usuarioId);

        // Borrado lógico. Devuelve false si el registro no existe o ya tenía ese estado.
        bool CambiarEstado(int provinciaId, bool activo, int usuarioId);
    }
}
