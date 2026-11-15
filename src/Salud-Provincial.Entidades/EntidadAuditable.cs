namespace Salud_Provincial.Entidades.Base
{
    /// <summary>
    /// Campos de trazabilidad y borrado lógico comunes a las entidades de negocio.
    /// </summary>
    public abstract class EntidadAuditable
    {
        /// <summary>Borrado lógico: false = desactivado (nunca se elimina físicamente).</summary>
        public bool Activo { get; set; } = true;

        public int? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
