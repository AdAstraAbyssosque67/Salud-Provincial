namespace Salud_Provincial.Entidades.Modelos
{
    // Una fila de la bitácora de cambios (tabla Auditoria). Es de solo lectura.
    public class AuditoriaRegistro
    {
        public long AuditoriaId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Tabla { get; set; } = string.Empty;
        public int RegistroId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public int? UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public int? ProvinciaId { get; set; }
        public string? ValoresAnteriores { get; set; }
        public string? ValoresNuevos { get; set; }
        public string? Equipo { get; set; }
    }
}
