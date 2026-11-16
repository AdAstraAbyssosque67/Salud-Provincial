using System;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Seguridad;
using Salud_Provincial.Negocio.Validaciones;

namespace Salud_Provincial.Negocio.Servicios
{
    public class AutenticacionServicio : ServicioBase, IAutenticacionServicio
    {
        // Mismo mensaje para "usuario no existe" y "clave incorrecta": no revela qué usuarios existen.
        private const string MensajeCredenciales = "Usuario o contraseña incorrectos.";

        private readonly IUsuarioRepositorio _usuarios;
        private readonly IProvinciaRepositorio _provincias;
        private readonly IAuditoriaRepositorio _auditoria;

        public AutenticacionServicio()
            : this(new UsuarioRepositorio(), new ProvinciaRepositorio(), new AuditoriaRepositorio())
        {
        }

        public AutenticacionServicio(IUsuarioRepositorio usuarios, IProvinciaRepositorio provincias,
                                     IAuditoriaRepositorio auditoria)
        {
            _usuarios = usuarios;
            _provincias = provincias;
            _auditoria = auditoria;
        }

        public Usuario IniciarSesion(string nombreUsuario, string clave)
        {
            string nombre = (nombreUsuario ?? string.Empty).Trim();

            if (nombre.Length == 0 || string.IsNullOrEmpty(clave))
                throw new NegocioException("Ingrese su usuario y contraseña.");

            return Ejecutar(() =>
            {
                Usuario? usuario = _usuarios.ObtenerParaLogin(nombre);

                if (usuario == null || !HashClave.Verificar(clave, usuario.ClaveHash))
                {
                    RegistrarAcceso(nombre, usuario?.UsuarioId, false,
                                    usuario == null ? "Usuario no existe" : "Clave incorrecta");
                    throw new NegocioException(MensajeCredenciales);
                }

                // Estas dos comprobaciones se hacen DESPUÉS de verificar la clave,
                // así solo quien conoce la clave sabe que la cuenta existe.
                if (!usuario.Activo)
                {
                    RegistrarAcceso(nombre, usuario.UsuarioId, false, "Usuario desactivado");
                    throw new NegocioException("Su usuario está desactivado. Contacte al administrador.");
                }

                if (usuario.ProvinciaId is int provinciaId)
                {
                    Provincia? provincia = _provincias.ObtenerPorId(provinciaId);
                    if (provincia == null || !provincia.Activo)
                    {
                        RegistrarAcceso(nombre, usuario.UsuarioId, false, "Provincia desactivada");
                        throw new NegocioException(
                            "La provincia de su usuario está desactivada. Contacte al administrador nacional.");
                    }
                }

                RegistrarAcceso(nombre, usuario.UsuarioId, true, null);

                // La sesión nunca guarda el hash de la clave.
                usuario.ClaveHash = Array.Empty<byte>();
                Sesion.Iniciar(usuario);
                return usuario;
            });
        }

        public void CerrarSesion() => Sesion.Cerrar();

        public void CambiarMiClave(string claveActual, string claveNueva)
        {
            ExigirSesion();
            Validador.Clave(claveNueva);

            Ejecutar(() =>
            {
                Usuario? usuario = _usuarios.ObtenerParaLogin(Sesion.UsuarioActual!.NombreUsuario);

                if (usuario == null || !HashClave.Verificar(claveActual ?? string.Empty, usuario.ClaveHash))
                    throw new NegocioException("La contraseña actual no es correcta.");

                if (HashClave.Verificar(claveNueva, usuario.ClaveHash))
                    throw new NegocioException("La nueva contraseña debe ser distinta de la actual.");

                bool cambiada = _usuarios.CambiarClave(
                    usuario.UsuarioId, HashClave.Calcular(claveNueva), usuario.UsuarioId, Alcance());

                if (!cambiada)
                    throw new NegocioException("No se pudo cambiar la contraseña.");
            });
        }

        private void RegistrarAcceso(string nombreUsuario, int? usuarioId, bool exitoso, string? motivo)
        {
            // La columna admite 50 caracteres; se recorta lo que escriba alguien en el login.
            string nombre = nombreUsuario.Length > 50 ? nombreUsuario.Substring(0, 50) : nombreUsuario;

            _auditoria.RegistrarAcceso(new AccesoRegistro
            {
                NombreUsuario = nombre,
                UsuarioId = usuarioId,
                Exitoso = exitoso,
                Motivo = motivo
            });
        }
    }
}
