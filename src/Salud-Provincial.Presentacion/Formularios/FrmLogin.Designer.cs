using System.Drawing;
using System.Windows.Forms;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelFormulario = new Panel();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            chkMostrarClave = new CheckBox();
            btnIngresar = new BotonPildora();
            btnSalir = new BotonPildora();
            panelMarca = new PanelDegradado();
            logoMarca = new LogoMarca();
            lblLema = new Label();
            lblSubLema = new Label();
            lblMinisterio = new Label();
            panelFormulario.SuspendLayout();
            panelMarca.SuspendLayout();
            SuspendLayout();
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.White;
            panelFormulario.Controls.Add(lblTitulo);
            panelFormulario.Controls.Add(lblSubtitulo);
            panelFormulario.Controls.Add(lblUsuario);
            panelFormulario.Controls.Add(txtUsuario);
            panelFormulario.Controls.Add(lblClave);
            panelFormulario.Controls.Add(txtClave);
            panelFormulario.Controls.Add(chkMostrarClave);
            panelFormulario.Controls.Add(btnIngresar);
            panelFormulario.Controls.Add(btnSalir);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(383, 0);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(397, 440);
            panelFormulario.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 22F);
            lblTitulo.ForeColor = Color.FromArgb(26, 32, 44);
            lblTitulo.Location = new Point(27, 38);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(241, 50);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Iniciar sesión";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.FromArgb(113, 128, 150);
            lblSubtitulo.Location = new Point(30, 90);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(167, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Ingrese sus credenciales";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI Semibold", 9F);
            lblUsuario.ForeColor = Color.FromArgb(113, 128, 150);
            lblUsuario.Location = new Point(30, 140);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(29, 164);
            txtUsuario.MaxLength = 50;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Nombre de usuario";
            txtUsuario.Size = new Size(340, 32);
            txtUsuario.TabIndex = 0;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Font = new Font("Segoe UI Semibold", 9F);
            lblClave.ForeColor = Color.FromArgb(113, 128, 150);
            lblClave.Location = new Point(30, 212);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(86, 20);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña";
            // 
            // txtClave
            // 
            txtClave.BorderStyle = BorderStyle.FixedSingle;
            txtClave.Font = new Font("Segoe UI", 11F);
            txtClave.Location = new Point(29, 236);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.PlaceholderText = "Contraseña";
            txtClave.Size = new Size(340, 32);
            txtClave.TabIndex = 1;
            // 
            // chkMostrarClave
            // 
            chkMostrarClave.AutoSize = true;
            chkMostrarClave.ForeColor = Color.FromArgb(113, 128, 150);
            chkMostrarClave.Location = new Point(30, 278);
            chkMostrarClave.Name = "chkMostrarClave";
            chkMostrarClave.Size = new Size(158, 24);
            chkMostrarClave.TabIndex = 2;
            chkMostrarClave.Text = "Mostrar contraseña";
            chkMostrarClave.UseVisualStyleBackColor = true;
            chkMostrarClave.CheckedChanged += chkMostrarClave_CheckedChanged;
            // 
            // btnIngresar
            // 
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI Semibold", 10F);
            btnIngresar.Location = new Point(29, 320);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(340, 46);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnSalir
            // 
            btnSalir.DialogResult = DialogResult.Cancel;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI Semibold", 10F);
            btnSalir.Location = new Point(29, 376);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(340, 40);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            // 
            // panelMarca
            // 
            panelMarca.Controls.Add(logoMarca);
            panelMarca.Controls.Add(lblLema);
            panelMarca.Controls.Add(lblSubLema);
            panelMarca.Controls.Add(lblMinisterio);
            panelMarca.Dock = DockStyle.Left;
            panelMarca.Location = new Point(0, 0);
            panelMarca.Name = "panelMarca";
            panelMarca.Size = new Size(383, 440);
            panelMarca.TabIndex = 1;
            // 
            // logoMarca
            // 
            logoMarca.BackColor = Color.Transparent;
            logoMarca.Location = new Point(28, 28);
            logoMarca.Name = "logoMarca";
            logoMarca.Size = new Size(262, 44);
            logoMarca.TabIndex = 0;
            // 
            // lblLema
            // 
            lblLema.AutoSize = true;
            lblLema.BackColor = Color.Transparent;
            lblLema.Font = new Font("Segoe UI Semibold", 21F);
            lblLema.ForeColor = Color.FromArgb(26, 32, 44);
            lblLema.Location = new Point(28, 116);
            lblLema.Name = "lblLema";
            lblLema.Size = new Size(354, 94);
            lblLema.TabIndex = 1;
            lblLema.Text = "Información de salud\r\ncentralizada";
            // 
            // lblSubLema
            // 
            lblSubLema.BackColor = Color.Transparent;
            lblSubLema.Font = new Font("Segoe UI", 11F);
            lblSubLema.ForeColor = Color.FromArgb(45, 55, 72);
            lblSubLema.Location = new Point(30, 240);
            lblSubLema.Name = "lblSubLema";
            lblSubLema.Size = new Size(260, 80);
            lblSubLema.TabIndex = 2;
            lblSubLema.Text = "Hospitales y centros de atención de todo el país, en una sola plataforma.";
            // 
            // lblMinisterio
            // 
            lblMinisterio.AutoSize = true;
            lblMinisterio.BackColor = Color.Transparent;
            lblMinisterio.Font = new Font("Segoe UI", 9F);
            lblMinisterio.ForeColor = Color.FromArgb(45, 55, 72);
            lblMinisterio.Location = new Point(30, 396);
            lblMinisterio.Name = "lblMinisterio";
            lblMinisterio.Size = new Size(137, 20);
            lblMinisterio.TabIndex = 3;
            lblMinisterio.Text = "Ministerio de Salud";
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnSalir;
            ClientSize = new Size(780, 440);
            Controls.Add(panelFormulario);
            Controls.Add(panelMarca);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salud-Provincial - Iniciar sesión";
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            panelMarca.ResumeLayout(false);
            panelMarca.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFormulario;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblClave;
        private TextBox txtClave;
        private CheckBox chkMostrarClave;
        private BotonPildora btnIngresar;
        private BotonPildora btnSalir;
        private PanelDegradado panelMarca;
        private LogoMarca logoMarca;
        private Label lblLema;
        private Label lblSubLema;
        private Label lblMinisterio;
    }
}
