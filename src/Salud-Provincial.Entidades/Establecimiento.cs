using Salud_Provincial.Entidades.Base;

namespace Salud_Provincial.Entidades.Modelos
{
    public class Establecimiento : EntidadAuditable
    {
        public int EstablecimientoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public int TipoEstablecimientoId { get; set; }

        // Discriminador del Patrón Pool: a qué provincia pertenece el registro.
        public int ProvinciaId { get; set; }

        // Solo para mostrar en pantalla (se llenan con un JOIN).
        public string? NombreTipo { get; set; }
        public string? NombreProvincia { get; set; }
    }
}
