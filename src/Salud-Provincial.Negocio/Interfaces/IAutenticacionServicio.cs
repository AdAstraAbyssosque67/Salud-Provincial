using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    public interface IAutenticacionServicio
    {
        /// <summary>
        /// Valida las credenciales, registra el intento en la bitácora de accesos e inicia la sesión.
        /// Lanza NegocioException con un mensaje apto para el usuario si algo falla.
        /// </summary>
        Usuario IniciarSesion(string nombreUsuario, string clave);

        void CerrarSesion();

        /// <summary>Cambia la clave del usuario en sesión (pide la clave actual).</summary>
        void CambiarMiClave(string claveActual, string claveNueva);
    }
}
