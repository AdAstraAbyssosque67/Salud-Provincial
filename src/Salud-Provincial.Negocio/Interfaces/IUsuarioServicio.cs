using System.Collections.Generic;
using Salud_Provincial.Entidades.Enums;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    /// <summary>Gestión de usuarios: solo el administrador nacional.</summary>
    public interface IUsuarioServicio
    {
        List<Usuario> Listar(bool incluirInactivos = false);

        /// <param name="provinciaId">Obligatoria para el rol Provincial; se ignora para AdminNacional.</param>
        Usuario Crear(string nombreUsuario, string nombreCompleto, string clave, RolUsuario rol, int? provinciaId);

        void Actualizar(int usuarioId, string nombreCompleto, RolUsuario rol, int? provinciaId);
        void RestablecerClave(int usuarioId, string nuevaClave);
        void Desactivar(int usuarioId);
        void Reactivar(int usuarioId);
    }
}
