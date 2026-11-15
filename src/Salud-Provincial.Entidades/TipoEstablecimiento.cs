using Salud_Provincial.Entidades.Base;

namespace Salud_Provincial.Entidades.Modelos
{
    /// <summary>Catálogo nacional de tipos de establecimiento (compartido por todas las provincias).</summary>
    public class TipoEstablecimiento : EntidadAuditable
    {
        public int TipoEstablecimientoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public override string ToString() => Nombre;
    }
}
