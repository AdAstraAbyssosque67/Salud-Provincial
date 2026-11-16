using System.Linq;
using System.Text.RegularExpressions;
using Salud_Provincial.Negocio.Excepciones;

namespace Salud_Provincial.Negocio.Validaciones
{
    /// <summary>
    /// Validaciones de entrada. Se hacen aquí (Negocio) y no solo en los formularios,
    /// porque cualquier pantalla futura pasaría por los mismos servicios.
    /// Todos los métodos devuelven el valor ya limpio (sin espacios sobrantes) o lanzan NegocioException.
    /// </summary>
    public static class Validador
    {
        private static readonly Regex RxCodigo = new Regex(@"^[A-Z0-9]{2,10}$");
        private static readonly Regex RxUsuario = new Regex(@"^[A-Za-z0-9._-]{3,50}$");
        private static readonly Regex RxTelefono = new Regex(@"^[0-9+()\-\s]{7,20}$");

        /// <param name="campo">Nombre con artículo para el mensaje, por ejemplo "El nombre" o "La dirección".</param>
        public static string TextoRequerido(string? valor, string campo, int maximo)
        {
            string texto = (valor ?? string.Empty).Trim();

            if (texto.Length == 0)
                throw new NegocioException($"{campo} es obligatorio.");
            if (texto.Length > maximo)
                throw new NegocioException($"{campo} no puede superar {maximo} caracteres.");

            return texto;
        }

        /// <returns>El texto limpio, o null si viene vacío.</returns>
        public static string? TextoOpcional(string? valor, string campo, int maximo)
        {
            string texto = (valor ?? string.Empty).Trim();

            if (texto.Length == 0)
                return null;
            if (texto.Length > maximo)
                throw new NegocioException($"{campo} no puede superar {maximo} caracteres.");

            return texto;
        }

        /// <summary>Código de provincia: 2 a 10 letras o números, siempre en mayúsculas.</summary>
        public static string Codigo(string? valor)
        {
            string codigo = TextoRequerido(valor, "El código", 10).ToUpperInvariant();

            if (!RxCodigo.IsMatch(codigo))
                throw new NegocioException("El código debe tener de 2 a 10 letras o números, sin espacios.");

            return codigo;
        }

        public static string NombreUsuario(string? valor)
        {
            string usuario = TextoRequerido(valor, "El nombre de usuario", 50);

            if (!RxUsuario.IsMatch(usuario))
                throw new NegocioException(
                    "El nombre de usuario debe tener de 3 a 50 caracteres: letras, números, punto, guion o guion bajo.");

            return usuario;
        }

        /// <returns>El teléfono limpio, o null si viene vacío.</returns>
        public static string? Telefono(string? valor)
        {
            string? telefono = TextoOpcional(valor, "El teléfono", 20);

            if (telefono != null && !RxTelefono.IsMatch(telefono))
                throw new NegocioException(
                    "El teléfono no es válido. Use solo números, espacios, +, - y paréntesis (7 a 20 caracteres).");

            return telefono;
        }

        /// <summary>Política mínima de contraseña: 8 caracteres, con letras y números.</summary>
        public static void Clave(string? clave)
        {
            if (string.IsNullOrEmpty(clave) || clave.Length < 8 ||
                !clave.Any(char.IsLetter) || !clave.Any(char.IsDigit))
                throw new NegocioException("La contraseña debe tener al menos 8 caracteres e incluir letras y números.");
        }
    }
}
