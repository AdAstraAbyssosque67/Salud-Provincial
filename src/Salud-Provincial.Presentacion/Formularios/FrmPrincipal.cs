using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Seguridad;

namespace Salud_Provincial.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private readonly IAutenticacionServicio _autenticacion = FabricaServicios.Autenticacion();

        /// <summary>True si el usuario eligió "Cerrar sesión" (Program vuelve al login).</summary>
        public bool CerrarSesionSolicitada { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
            Tema.Formulario(this);
            Tema.Menu(menuPrincipal);
            Tema.BarraEstado(statusPrincipal);
            Tema.BotonLateral(btnNavEstablecimientos);
            Tema.BotonLateral(btnNavClave);
            Tema.BotonLateral(btnNavCerrarSesion);
            Tema.BotonPrimario(btnIrEstablecimientos);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            var sesion = SesionUsuario.Instancia;
            string rol = sesion.EsAdminNacional ? "Administrador nacional" : "Provincial";
            string provincia = sesion.EsAdminNacional ? "Nacional" : (sesion.NombreProvincia ?? string.Empty);

            lblUsuarioEstado.Text = "Usuario: " + sesion.UsuarioActual.NombreCompleto;
            lblRolEstado.Text = "Rol: " + rol;
            lblProvinciaEstado.Text = "Provincia: " + provincia;

            lblBienvenida.Text = "Bienvenido/a, " + sesion.UsuarioActual.NombreCompleto;
            lblDatosUsuario.Text = "Rol: " + rol + "\r\nÁmbito: " + provincia;
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Siempre se limpia la sesión al cerrar la ventana principal.
            try { _autenticacion.CerrarSesion(); } catch { }
        }

        private void mnuEstablecimientos_Click(object sender, EventArgs e)
        {
            using var frm = new FrmEstablecimientos();
            frm.ShowDialog(this);
        }

        private void mnuCambiarClave_Click(object sender, EventArgs e)
        {
            using var frm = new FrmCambiarClave();
            frm.ShowDialog(this);
        }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesionSolicitada = true;
            Close();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
