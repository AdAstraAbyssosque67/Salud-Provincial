using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    public interface IAutenticacionServicio
    {
        // 
        // Valida las credenciales, registra el intento en la bitácora de accesos e inicia la sesión.
        // Lanza NegocioException con un mensaje apto para el usuario si algo falla.
        // 
        Usuario IniciarSesion(string nombreUsuario, string clave);

        void CerrarSesion();

        // Cambia la clave del usuario en sesión (pide la clave actual).
        void CambiarMiClave(string claveActual, string claveNueva);
    }
}
