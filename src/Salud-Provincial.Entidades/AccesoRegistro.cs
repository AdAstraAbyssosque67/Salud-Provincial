namespace Salud_Provincial.Entidades.Modelos
{
    /// <summary>Una fila de la bitácora de accesos (tabla BitacoraAccesos).</summary>
    public class AccesoRegistro
    {
        public long AccesoId { get; set; }
        public DateTime FechaHora { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int? UsuarioId { get; set; }
        public bool Exitoso { get; set; }
        public string? Motivo { get; set; }
        public string? Equipo { get; set; }
    }
}
