using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Enums;

namespace Salud_Provincial.Entidades.Modelos
{
    public class Usuario : EntidadAuditable
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;

        // Hash SHA-256 de la clave (32 bytes). Nunca se guarda ni se muestra la clave en texto plano.
        public byte[] ClaveHash { get; set; } = Array.Empty<byte>();

        public string NombreCompleto { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }

        // Null cuando el rol es AdminNacional.
        public int? ProvinciaId { get; set; }

        // Solo para mostrar en pantalla (se llena con un JOIN).
        public string? NombreProvincia { get; set; }

        public bool EsAdminNacional => Rol == RolUsuario.AdminNacional;
    }
}
