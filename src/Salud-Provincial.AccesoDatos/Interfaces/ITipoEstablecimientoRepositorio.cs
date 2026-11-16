using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Interfaces
{
    public interface ITipoEstablecimientoRepositorio
    {
        List<TipoEstablecimiento> Listar(bool incluirInactivos);
        TipoEstablecimiento? ObtenerPorId(int tipoId);
        int Insertar(TipoEstablecimiento tipo, int usuarioId);
        bool Actualizar(TipoEstablecimiento tipo, int usuarioId);
        bool CambiarEstado(int tipoId, bool activo, int usuarioId);

        // Cantidad de establecimientos activos que usan este tipo (para validar antes de desactivarlo).
        int ContarEstablecimientosActivos(int tipoId);
    }
}
