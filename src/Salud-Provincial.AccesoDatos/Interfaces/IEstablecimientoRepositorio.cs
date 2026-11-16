using System.Collections.Generic;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Interfaces
{
    // 
    // Todas las operaciones reciben un AlcanceProvincia: así el aislamiento por provincia
    // (Patrón Pool) se aplica siempre en la consulta, nunca depende del formulario.
    // 
    public interface IEstablecimientoRepositorio
    {
        List<Establecimiento> Listar(AlcanceProvincia alcance, bool incluirInactivos);
        Establecimiento? ObtenerPorId(int establecimientoId, AlcanceProvincia alcance);

        // Inserta y devuelve el nuevo EstablecimientoId. Rechaza provincias fuera del alcance.
        int Insertar(Establecimiento establecimiento, int usuarioId, AlcanceProvincia alcance);

        // No permite cambiar la provincia. Devuelve false si no existe o está fuera del alcance.
        bool Actualizar(Establecimiento establecimiento, int usuarioId, AlcanceProvincia alcance);

        bool CambiarEstado(int establecimientoId, bool activo, int usuarioId, AlcanceProvincia alcance);
    }
}
