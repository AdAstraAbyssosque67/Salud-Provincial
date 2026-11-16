using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Salud_Provincial.AccesoDatos.Conexion
{
    // 
    // Patrón Singleton: una única instancia que conoce la cadena de conexión.
    // No guarda una conexión abierta (el pool de ADO.NET ya reutiliza las conexiones físicas):
    // cada operación pide una SqlConnection nueva y la cierra con "using".
    // 
    public sealed class ConexionBD
    {
        // Lazy<T> garantiza una sola instancia aunque haya varios hilos.
        private static readonly Lazy<ConexionBD> _instancia = new Lazy<ConexionBD>(() => new ConexionBD());

        public static ConexionBD Instancia => _instancia.Value;

        private readonly string _cadenaConexion;

        private ConexionBD()
        {
            ConnectionStringSettings? config = ConfigurationManager.ConnectionStrings["SaludDB"];

            if (config == null || string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new InvalidOperationException(
                    "Falta la cadena de conexión 'SaludDB' en el App.config del proyecto de Presentación.");

            _cadenaConexion = config.ConnectionString;
        }

        // Devuelve una conexión nueva, sin abrir. Quien la pide debe cerrarla (using).
        public SqlConnection CrearConexion() => new SqlConnection(_cadenaConexion);

        // Comprueba si se puede llegar a la base de datos.
        public bool ProbarConexion()
        {
            try
            {
                using SqlConnection cn = CrearConexion();
                cn.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
