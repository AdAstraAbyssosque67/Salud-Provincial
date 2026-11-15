using System;

namespace Salud_Provincial.AccesoDatos.Excepciones
{
    public enum TipoErrorDatos
    {
        Desconocido,
        /// <summary>No se pudo llegar a la base de datos.</summary>
        Conexion,
        /// <summary>Viola una restricción UNIQUE (dato repetido).</summary>
        Duplicado,
        /// <summary>Viola una llave foránea o un CHECK.</summary>
        Restriccion,
        /// <summary>Una regla de auditoría o protección de la base de datos rechazó la operación.</summary>
        ReglaBaseDatos
    }

    /// <summary>
    /// Error de acceso a datos ya traducido a algo que la capa de Negocio puede entender,
    /// sin exponer detalles técnicos de SQL Server.
    /// </summary>
    public class AccesoDatosException : Exception
    {
        public TipoErrorDatos Tipo { get; }

        public AccesoDatosException(TipoErrorDatos tipo, string mensaje, Exception? interna = null)
            : base(mensaje, interna)
        {
            Tipo = tipo;
        }
    }
}
