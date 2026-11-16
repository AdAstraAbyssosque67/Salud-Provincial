using System.Drawing;
using System.Windows.Forms;

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
            btnIngresar = new Button();
            btnSalir = new Button();
            panelMarca = new Panel();
            lblMarca = new Label();
            lblLema = new Label();
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
            panelFormulario.Location = new Point(331, 0);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(409, 420);
            panelFormulario.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(50, 45);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(201, 41);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Iniciar sesión";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.FromArgb(127, 140, 141);
            lblSubtitulo.Location = new Point(52, 92);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(167, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Ingrese sus credenciales";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.ForeColor = Color.FromArgb(44, 62, 80);
            lblUsuario.Location = new Point(52, 140);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(59, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(50, 164);
            txtUsuario.MaxLength = 50;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Nombre de usuario";
            txtUsuario.Size = new Size(340, 32);
            txtUsuario.TabIndex = 0;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.ForeColor = Color.FromArgb(44, 62, 80);
            lblClave.Location = new Point(52, 210);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(83, 20);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña";
            // 
            // txtClave
            // 
            txtClave.Font = new Font("Segoe UI", 11F);
            txtClave.Location = new Point(50, 234);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.PlaceholderText = "Contraseña";
            txtClave.Size = new Size(340, 32);
            txtClave.TabIndex = 1;
            // 
            // chkMostrarClave
            // 
            chkMostrarClave.AutoSize = true;
            chkMostrarClave.ForeColor = Color.FromArgb(127, 140, 141);
            chkMostrarClave.Location = new Point(52, 274);
            chkMostrarClave.Name = "chkMostrarClave";
            chkMostrarClave.Size = new Size(158, 24);
            chkMostrarClave.TabIndex = 2;
            chkMostrarClave.Text = "Mostrar contraseña";
            chkMostrarClave.UseVisualStyleBackColor = true;
            chkMostrarClave.CheckedChanged += chkMostrarClave_CheckedChanged;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(50, 315);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(340, 42);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnSalir
            // 
            btnSalir.DialogResult = DialogResult.Cancel;
            btnSalir.Location = new Point(50, 366);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(340, 36);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // panelMarca
            // 
            panelMarca.BackColor = Color.FromArgb(31, 181, 197);
            panelMarca.Controls.Add(lblMarca);
            panelMarca.Controls.Add(lblLema);
            panelMarca.Controls.Add(lblMinisterio);
            panelMarca.Dock = DockStyle.Left;
            panelMarca.Location = new Point(0, 0);
            panelMarca.Name = "panelMarca";
            panelMarca.Size = new Size(331, 420);
            panelMarca.TabIndex = 1;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(24, 120);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(308, 50);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "Salud-Provincial";
            // 
            // lblLema
            // 
            lblLema.AutoSize = true;
            lblLema.Font = new Font("Segoe UI", 11F);
            lblLema.ForeColor = Color.White;
            lblLema.Location = new Point(28, 185);
            lblLema.Name = "lblLema";
            lblLema.Size = new Size(298, 75);
            lblLema.TabIndex = 1;
            lblLema.Text = "Información de salud centralizada\r\nde hospitales y centros de\r\natención de todo el país.";
            // 
            // lblMinisterio
            // 
            lblMinisterio.AutoSize = true;
            lblMinisterio.Font = new Font("Segoe UI", 9F);
            lblMinisterio.ForeColor = Color.White;
            lblMinisterio.Location = new Point(28, 380);
            lblMinisterio.Name = "lblMinisterio";
            lblMinisterio.Size = new Size(137, 20);
            lblMinisterio.TabIndex = 2;
            lblMinisterio.Text = "Ministerio de Salud";
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnSalir;
            ClientSize = new Size(740, 420);
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
        private Button btnIngresar;
        private Button btnSalir;
        private Panel panelMarca;
        private Label lblMarca;
        private Label lblLema;
        private Label lblMinisterio;
    }
}
