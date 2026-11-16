using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    public interface ITipoEstablecimientoServicio
    {
        /// <summary>Cualquier usuario en sesión. Los inactivos solo los ve el administrador nacional.</summary>
        List<TipoEstablecimiento> Listar(bool incluirInactivos = false);

        // Solo administrador nacional (es un catálogo nacional):
        TipoEstablecimiento Crear(string nombre, string? descripcion);
        void Actualizar(int tipoId, string nombre, string? descripcion);
        void Desactivar(int tipoId);
        void Reactivar(int tipoId);
    }
}
