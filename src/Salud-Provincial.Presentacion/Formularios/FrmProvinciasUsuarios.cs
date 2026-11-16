using Salud_Provincial.Entidades.Enums;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    public class FrmProvinciasUsuarios : Form
    {
        private readonly IProvinciaServicio _provincias = FabricaServicios.Provincias();
        private readonly IUsuarioServicio _usuarios = FabricaServicios.Usuarios();

        private readonly TextBox txtCodigo = new() { Width = 316 };
        private readonly TextBox txtProvincia = new() { Width = 316 };
        private readonly TextBox txtNombre = new() { Width = 316 };
        private readonly TextBox txtUsuario = new() { Width = 316 };
        private readonly TextBox txtClave = new() { Width = 316, UseSystemPasswordChar = true };
        private readonly ComboBox cmbProvincia = new() { Width = 316, DropDownStyle = ComboBoxStyle.DropDownList };

        public FrmProvinciasUsuarios()
        {
            Tema.Formulario(this);
            var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(0, 16, 16, 16) };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            Controls.Add(tabla);

            var cardProv = Tarjeta("Nueva provincia", tabla, 0);
            Campo(cardProv, "Código *", txtCodigo);
            Campo(cardProv, "Nombre *", txtProvincia);
            Boton(cardProv, "Agregar provincia", btnProvincia_Click);

            var cardUsr = Tarjeta("Nuevo usuario provincial", tabla, 1);
            Campo(cardUsr, "Nombre completo *", txtNombre);
            Campo(cardUsr, "Usuario *", txtUsuario);
            Campo(cardUsr, "Contraseña inicial *", txtClave);
            Campo(cardUsr, "Provincia *", cmbProvincia);
            Boton(cardUsr, "Crear usuario", btnUsuario_Click);

            Load += (_, _) => CargarProvincias();
        }

        private static FlowLayoutPanel Tarjeta(string titulo, TableLayoutPanel tabla, int columna)
        {
            var card = new TarjetaBase { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(24), Margin = new Padding(16, 0, 0, 0) };
            var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, BackColor = Color.White };
            flow.Controls.Add(new Label { Text = titulo, Font = Tema.Fuente(15F, true), AutoSize = true, Margin = new Padding(0, 0, 0, 16) });
            card.Controls.Add(flow);
            tabla.Controls.Add(card, columna, 0);
            return flow;
        }

        private static void Campo(FlowLayoutPanel f, string etiqueta, Control c)
        {
            f.Controls.Add(new Label { Text = etiqueta, AutoSize = true, ForeColor = Tema.TextoSuave, Margin = new Padding(0, 8, 0, 2) });
            f.Controls.Add(c);
        }

        private static void Boton(FlowLayoutPanel f, string texto, EventHandler click)
        {
            var b = new BotonPildora { Text = texto, Size = new Size(316, 46), Margin = new Padding(0, 24, 0, 0) };
            Tema.BotonPrimario(b);
            b.Click += click;
            f.Controls.Add(b);
        }

        private void CargarProvincias()
        {
            try
            {
                cmbProvincia.DisplayMember = "Nombre";
                cmbProvincia.ValueMember = "ProvinciaId";
                cmbProvincia.DataSource = _provincias.Listar();
                cmbProvincia.SelectedIndex = -1;
            }
            catch (Exception ex) { ManejadorErrores.Mostrar(ex, this); }
        }

        private void btnProvincia_Click(object? s, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtProvincia.Text)) { Aviso("Escriba el código y el nombre de la provincia."); return; }
            try
            {
                _provincias.Crear(txtCodigo.Text.Trim(), txtProvincia.Text.Trim());
                txtCodigo.Clear();
                txtProvincia.Clear();
                CargarProvincias();
                Aviso("Provincia agregada.");
            }
            catch (Exception ex) { ManejadorErrores.Mostrar(ex, this); }
        }

        private void btnUsuario_Click(object? s, EventArgs e)
        {
            if (cmbProvincia.SelectedValue is not int provinciaId) { Aviso("Seleccione la provincia."); return; }
            try
            {
                _usuarios.Crear(txtUsuario.Text.Trim(), txtNombre.Text.Trim(),
                                txtClave.Text, RolUsuario.Provincial, provinciaId);
                txtNombre.Clear(); txtUsuario.Clear(); txtClave.Clear();
                cmbProvincia.SelectedIndex = -1;
                Aviso("Usuario creado.");
            }
            catch (Exception ex) { ManejadorErrores.Mostrar(ex, this); }
        }

        private void Aviso(string m) =>
            MessageBox.Show(this, m, "Salud-Provincial", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
