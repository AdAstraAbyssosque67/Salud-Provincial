using System.Security.Cryptography;
using System.Text;

namespace Salud_Provincial.Negocio.Seguridad
{
    /// <summary>
    /// Hash SHA-256 de las claves. Coincide con el que usa el script SaludDB.sql en los datos de prueba
    /// (HASHBYTES('SHA2_256', ...)), por eso los usuarios de prueba pueden iniciar sesión.
    /// Mejora opcional para un sistema real: PBKDF2 (Rfc2898DeriveBytes) con una sal distinta por usuario.
    /// </summary>
    public static class HashClave
    {
        public static byte[] Calcular(string clave)
            => SHA256.HashData(Encoding.UTF8.GetBytes(clave));

        /// <summary>Comparación en tiempo constante (evita ataques por tiempo de respuesta).</summary>
        public static bool Verificar(string clave, byte[] hashAlmacenado)
            => CryptographicOperations.FixedTimeEquals(Calcular(clave), hashAlmacenado);
    }
}
