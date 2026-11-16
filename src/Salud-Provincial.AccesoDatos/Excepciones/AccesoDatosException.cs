using System;

namespace Salud_Provincial.AccesoDatos.Excepciones
{
    public enum TipoErrorDatos
    {
        Desconocido,
        // No se pudo llegar a la base de datos.
        Conexion,
        // Viola una restricción UNIQUE (dato repetido).
        Duplicado,
        // Viola una llave foránea o un CHECK.
        Restriccion,
        // Una regla de auditoría o protección de la base de datos rechazó la operación.
        ReglaBaseDatos
    }

    // 
    // Error de acceso a datos ya traducido a algo que la capa de Negocio puede entender,
    // sin exponer detalles técnicos de SQL Server.
    // 
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
