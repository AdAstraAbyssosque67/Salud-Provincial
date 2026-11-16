using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Excepciones;

namespace Salud_Provincial.AccesoDatos.Ayudantes
{
    /// <summary>Traduce los errores de SQL Server a AccesoDatosException.</summary>
    internal static class SqlErrores
    {
        public static AccesoDatosException Traducir(SqlException ex)
        {
            System.Diagnostics.Debug.WriteLine($"SQL {ex.Number}: {ex.Message}");
            switch (ex.Number)
            {
                case 2627:  // violación de PRIMARY KEY / UNIQUE constraint
                case 2601:  // índice único duplicado
                    return new AccesoDatosException(TipoErrorDatos.Duplicado,
                        "Ya existe un registro con esos datos.", ex);

                case 547:   // violación de FOREIGN KEY o CHECK
                    return new AccesoDatosException(TipoErrorDatos.Restriccion,
                        "Los datos no cumplen una relación o regla de la base de datos.", ex);

                // Mensajes propios definidos en los triggers de SaludDB.sql
                case 50001: // DELETE físico bloqueado
                case 50002: // falta ModificadoPor
                case 50003: // intento de cambiar la provincia de un establecimiento
                case 50004: // bitácora inmutable
                    return new AccesoDatosException(TipoErrorDatos.ReglaBaseDatos, ex.Message, ex);

                case -1:
                case 2:
                case 53:
                case 4060:
                case 18456:
                case -2146893019: // fallo de certificado/SSL en la conexión
                    return new AccesoDatosException(TipoErrorDatos.Conexion,
                        "No se pudo conectar con la base de datos.", ex);

                default:
                    return new AccesoDatosException(TipoErrorDatos.Desconocido,
                        "Ocurrió un error al acceder a la base de datos.", ex);
            }
        }
    }
}
