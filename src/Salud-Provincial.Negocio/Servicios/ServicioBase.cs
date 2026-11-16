using System;
using Salud_Provincial.AccesoDatos.Excepciones;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Seguridad;

namespace Salud_Provincial.Negocio.Servicios
{
    /// <summary>Código común de los servicios: control de permisos y traducción de errores.</summary>
    public abstract class ServicioBase
    {
        protected static SesionUsuario Sesion => SesionUsuario.Instancia;

        protected static void ExigirSesion()
        {
            if (!Sesion.HaIniciadoSesion)
                throw new PermisoDenegadoException("Debe iniciar sesión para continuar.");
        }

        protected static void ExigirAdminNacional()
        {
            ExigirSesion();
            if (!Sesion.EsAdminNacional)
                throw new PermisoDenegadoException("Solo el administrador nacional puede realizar esta operación.");
        }

        /// <summary>Alcance de provincias del usuario en sesión (lanza PermisoDenegadoException si no hay sesión).</summary>
        protected static AlcanceProvincia Alcance() => Sesion.ObtenerAlcance();

        /// <summary>
        /// Ejecuta una acción de acceso a datos y convierte sus errores técnicos en NegocioException,
        /// para que la Presentación solo tenga que manejar un tipo de error.
        /// </summary>
        /// <param name="mensajeDuplicado">Mensaje amable si el error es un dato repetido (UNIQUE).</param>
        protected static T Ejecutar<T>(Func<T> accion, string? mensajeDuplicado = null)
        {
            try
            {
                return accion();
            }
            catch (AccesoDatosException ex) when (ex.Tipo == TipoErrorDatos.Duplicado && mensajeDuplicado != null)
            {
                throw new NegocioException(mensajeDuplicado, ex);
            }
            catch (AccesoDatosException ex)
            {
                throw new NegocioException(ex.Message, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new PermisoDenegadoException(ex.Message, ex);
            }
        }

        protected static void Ejecutar(Action accion, string? mensajeDuplicado = null)
            => Ejecutar<object?>(() => { accion(); return null; }, mensajeDuplicado);
    }
}
