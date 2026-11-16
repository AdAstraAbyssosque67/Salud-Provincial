using System;

namespace Salud_Provincial.Negocio.Excepciones
{
    // 
    // Error de regla de negocio o de validación. Su Message está escrito para el usuario:
    // la capa de Presentación puede mostrarlo tal cual en un MessageBox.
    // 
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje, Exception? interna = null)
            : base(mensaje, interna)
        {
        }
    }

    // El usuario no tiene permiso para la operación (no inició sesión o no es administrador nacional).
    public class PermisoDenegadoException : NegocioException
    {
        public PermisoDenegadoException(string mensaje, Exception? interna = null)
            : base(mensaje, interna)
        {
        }
    }
}
