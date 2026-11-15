using System;
using Microsoft.Data.SqlClient;
using Salud_Provincial.Entidades.Base;

namespace Salud_Provincial.AccesoDatos.Ayudantes
{
    /// <summary>Métodos cortos para leer columnas por nombre, con manejo de NULL.</summary>
    internal static class LectorExtensiones
    {
        public static string Texto(this SqlDataReader r, string columna)
            => r.GetString(r.GetOrdinal(columna));

        public static string? TextoNulo(this SqlDataReader r, string columna)
        {
            int i = r.GetOrdinal(columna);
            return r.IsDBNull(i) ? null : r.GetString(i);
        }

        public static int Entero(this SqlDataReader r, string columna)
            => r.GetInt32(r.GetOrdinal(columna));

        public static int? EnteroNulo(this SqlDataReader r, string columna)
        {
            int i = r.GetOrdinal(columna);
            return r.IsDBNull(i) ? null : r.GetInt32(i);
        }

        public static long Largo(this SqlDataReader r, string columna)
            => r.GetInt64(r.GetOrdinal(columna));

        public static bool Booleano(this SqlDataReader r, string columna)
            => r.GetBoolean(r.GetOrdinal(columna));

        public static DateTime Fecha(this SqlDataReader r, string columna)
            => r.GetDateTime(r.GetOrdinal(columna));

        public static DateTime? FechaNula(this SqlDataReader r, string columna)
        {
            int i = r.GetOrdinal(columna);
            return r.IsDBNull(i) ? null : r.GetDateTime(i);
        }

        public static byte[] Bytes(this SqlDataReader r, string columna)
            => r.GetFieldValue<byte[]>(r.GetOrdinal(columna));

        /// <summary>Llena Activo y los 4 campos de auditoría comunes a las entidades de negocio.</summary>
        public static void LeerAuditoria(this SqlDataReader r, EntidadAuditable entidad)
        {
            entidad.Activo = r.Booleano("Activo");
            entidad.CreadoPor = r.EnteroNulo("CreadoPor");
            entidad.FechaCreacion = r.Fecha("FechaCreacion");
            entidad.ModificadoPor = r.EnteroNulo("ModificadoPor");
            entidad.FechaModificacion = r.FechaNula("FechaModificacion");
        }
    }
}
