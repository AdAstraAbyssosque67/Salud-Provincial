namespace Salud_Provincial.Entidades.Base
{
    // 
    // Campos de trazabilidad y borrado lógico comunes a las entidades de negocio.
    // 
    public abstract class EntidadAuditable
    {
        // Borrado lógico: false = desactivado (nunca se elimina físicamente).
        public bool Activo { get; set; } = true;

        public int? CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
