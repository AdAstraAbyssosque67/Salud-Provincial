using Salud_Provincial.Entidades.Base;

namespace Salud_Provincial.Entidades.Modelos
{
    // Provincia: es el "tenant" de la arquitectura multi-tenant (Patrón Pool).
    public class Provincia : EntidadAuditable
    {
        public int ProvinciaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public override string ToString() => Nombre;
    }
}
