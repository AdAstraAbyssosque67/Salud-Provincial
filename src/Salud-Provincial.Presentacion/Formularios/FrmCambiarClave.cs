using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;

namespace Salud_Provincial.Presentacion
{
    public partial class FrmCambiarClave : Form
    {
        private readonly IAutenticacionServicio _autenticacion = FabricaServicios.Autenticacion();

        public FrmCambiarClave()
        {
            InitializeComponent();
            Tema.BotonPrimario(btnAceptar);
            Tema.BotonSecundario(btnCancelar);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Solo comparación de confirmación (las demás reglas las valida Negocio).
            if (txtClaveNueva.Text != txtConfirmar.Text)
            {
                MessageBox.Show(this, "La confirmación no coincide con la nueva contraseña.",
                    "Salud-Provincial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmar.Clear();
                txtConfirmar.Focus();
                return;
            }

            try
            {
                _autenticacion.CambiarMiClave(txtClaveActual.Text, txtClaveNueva.Text);
                MessageBox.Show(this, "Contraseña cambiada correctamente.",
                    "Salud-Provincial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }
    }
}
