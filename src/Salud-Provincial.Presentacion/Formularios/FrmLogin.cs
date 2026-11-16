using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;

namespace Salud_Provincial.Presentacion
{
    public partial class FrmLogin : Form
    {
        private readonly IAutenticacionServicio _autenticacion = FabricaServicios.Autenticacion();

        public FrmLogin()
        {
            InitializeComponent();
            Tema.BotonPrimario(btnIngresar);
            Tema.BotonSecundario(btnSalir);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                _autenticacion.IniciarSesion(txtUsuario.Text.Trim(), txtClave.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
                txtClave.Clear();
                txtUsuario.Focus();
                txtUsuario.SelectAll();
            }
        }

        private void chkMostrarClave_CheckedChanged(object sender, EventArgs e)
        {
            txtClave.PasswordChar = chkMostrarClave.Checked ? '\0' : '*';
        }
    }
}
