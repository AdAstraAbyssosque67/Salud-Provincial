using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    // 
    // Todas las operaciones se limitan a la provincia del usuario en sesión
    // (el administrador nacional las ve y administra todas).
    // 
    public interface IEstablecimientoServicio
    {
        List<Establecimiento> Listar(bool incluirInactivos = false);
        Establecimiento? ObtenerPorId(int establecimientoId);

        // 
        // Un usuario provincial siempre crea en su propia provincia (se ignora el ProvinciaId enviado).
        // El administrador nacional debe indicar la provincia.
        // 
        Establecimiento Crear(Establecimiento datos);

        // No permite cambiar la provincia del establecimiento.
        void Actualizar(Establecimiento datos);

        void Desactivar(int establecimientoId);
        void Reactivar(int establecimientoId);
    }
}
