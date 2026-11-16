using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    /// <summary>
    /// Todas las operaciones se limitan a la provincia del usuario en sesión
    /// (el administrador nacional las ve y administra todas).
    /// </summary>
    public interface IEstablecimientoServicio
    {
        List<Establecimiento> Listar(bool incluirInactivos = false);
        Establecimiento? ObtenerPorId(int establecimientoId);

        /// <summary>
        /// Un usuario provincial siempre crea en su propia provincia (se ignora el ProvinciaId enviado).
        /// El administrador nacional debe indicar la provincia.
        /// </summary>
        Establecimiento Crear(Establecimiento datos);

        /// <summary>No permite cambiar la provincia del establecimiento.</summary>
        void Actualizar(Establecimiento datos);

        void Desactivar(int establecimientoId);
        void Reactivar(int establecimientoId);
    }
}
