using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Servicios;

namespace Salud_Provincial.Negocio
{
    /// <summary>
    /// Patrón Factory: la capa de Presentación pide los servicios aquí y los recibe como interfaces,
    /// sin conocer las clases concretas ni la capa de Acceso a Datos.
    /// </summary>
    public static class FabricaServicios
    {
        public static IAutenticacionServicio Autenticacion() => new AutenticacionServicio();
        public static IProvinciaServicio Provincias() => new ProvinciaServicio();
        public static ITipoEstablecimientoServicio Tipos() => new TipoEstablecimientoServicio();
        public static IUsuarioServicio Usuarios() => new UsuarioServicio();
        public static IEstablecimientoServicio Establecimientos() => new EstablecimientoServicio();
        public static IAuditoriaServicio Auditoria() => new AuditoriaServicio();
    }
}
