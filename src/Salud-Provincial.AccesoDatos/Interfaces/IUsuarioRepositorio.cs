using System.Collections.Generic;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        // Para el login: es el único método que devuelve ClaveHash. Devuelve activos e inactivos.
        Usuario? ObtenerParaLogin(string nombreUsuario);

        List<Usuario> Listar(AlcanceProvincia alcance, bool incluirInactivos);
        Usuario? ObtenerPorId(int usuarioId, AlcanceProvincia alcance);

        // Inserta y devuelve el nuevo UsuarioId. Usuario.ClaveHash ya debe venir con el hash.
        int Insertar(Usuario usuario, int usuarioId, AlcanceProvincia alcance);

        // Actualiza nombre completo, rol y provincia (no cambia la clave ni el nombre de usuario).
        bool Actualizar(Usuario usuario, int usuarioId, AlcanceProvincia alcance);

        bool CambiarClave(int usuarioObjetivoId, byte[] nuevoHash, int usuarioId, AlcanceProvincia alcance);
        bool CambiarEstado(int usuarioObjetivoId, bool activo, int usuarioId, AlcanceProvincia alcance);
    }
}
