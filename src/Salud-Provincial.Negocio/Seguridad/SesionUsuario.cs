using System;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;

namespace Salud_Provincial.Negocio.Seguridad
{
    // 
    // Patrón Singleton: guarda el usuario autenticado (y por tanto su provincia) mientras la aplicación está abierta.
    // Es la pieza clave del multi-tenant: los servicios le preguntan a la sesión qué provincia aplicar,
    // en lugar de confiar en lo que envíe un formulario.
    // 
    public sealed class SesionUsuario
    {
        private static readonly Lazy<SesionUsuario> _instancia = new Lazy<SesionUsuario>(() => new SesionUsuario());

        public static SesionUsuario Instancia => _instancia.Value;

        private SesionUsuario()
        {
        }

        // Usuario autenticado (sin ClaveHash) o null si no hay sesión.
        public Usuario? UsuarioActual { get; private set; }

        public DateTime? InicioSesion { get; private set; }

        public bool HaIniciadoSesion => UsuarioActual != null;

        public int UsuarioId => Requerir().UsuarioId;
        public bool EsAdminNacional => Requerir().EsAdminNacional;
        public int? ProvinciaId => Requerir().ProvinciaId;
        public string? NombreProvincia => Requerir().NombreProvincia;

        // 
        // Solo lo puede llamar la capa de Negocio (internal): la Presentación no puede "fabricar" una sesión.
        // 
        internal void Iniciar(Usuario usuario)
        {
            UsuarioActual = usuario;
            InicioSesion = DateTime.Now;
        }

        public void Cerrar()
        {
            UsuarioActual = null;
            InicioSesion = null;
        }

        // 
        // Qué provincias puede ver o modificar el usuario en sesión:
        // el administrador nacional ve todas; un usuario provincial, solo la suya.
        // 
        public AlcanceProvincia ObtenerAlcance()
        {
            Usuario usuario = Requerir();

            if (usuario.EsAdminNacional)
                return AlcanceProvincia.Todas();

            if (usuario.ProvinciaId is int provinciaId)
                return AlcanceProvincia.De(provinciaId);

            throw new PermisoDenegadoException("Su usuario no tiene una provincia asignada.");
        }

        private Usuario Requerir()
            => UsuarioActual ?? throw new PermisoDenegadoException("Debe iniciar sesión para continuar.");
    }
}
