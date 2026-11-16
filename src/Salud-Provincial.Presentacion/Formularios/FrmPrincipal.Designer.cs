using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion
{
    partial class FrmPrincipal
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
            menuPrincipal = new MenuStrip();
            mnuArchivo = new ToolStripMenuItem();
            mnuCambiarClave = new ToolStripMenuItem();
            mnuCerrarSesion = new ToolStripMenuItem();
            mnuSalir = new ToolStripMenuItem();
            mnuEstablecimientos = new ToolStripMenuItem();
            statusPrincipal = new StatusStrip();
            lblUsuarioEstado = new ToolStripStatusLabel();
            lblRolEstado = new ToolStripStatusLabel();
            lblProvinciaEstado = new ToolStripStatusLabel();
            panelMenuLateral = new Panel();
            btnNavCerrarSesion = new Button();
            btnNavClave = new Button();
            btnNavEstablecimientos = new Button();
            lblMenu = new Label();
            panelContenido = new Panel();
            panelTarjeta = new Panel();
            lblBienvenida = new Label();
            lblDatosUsuario = new Label();
            btnIrEstablecimientos = new Button();
            panelTituloPagina = new Panel();
            lblTituloPagina = new Label();
            menuPrincipal.SuspendLayout();
            statusPrincipal.SuspendLayout();
            panelMenuLateral.SuspendLayout();
            panelContenido.SuspendLayout();
            panelTarjeta.SuspendLayout();
            panelTituloPagina.SuspendLayout();
            SuspendLayout();
            // 
            // menuPrincipal
            // 
            menuPrincipal.ImageScalingSize = new Size(20, 20);
            menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuArchivo, mnuEstablecimientos });
            menuPrincipal.Location = new Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new Size(1000, 28);
            menuPrincipal.TabIndex = 0;
            // 
            // mnuArchivo
            // 
            mnuArchivo.DropDownItems.AddRange(new ToolStripItem[] { mnuCambiarClave, mnuCerrarSesion, mnuSalir });
            mnuArchivo.Name = "mnuArchivo";
            mnuArchivo.Size = new Size(73, 24);
            mnuArchivo.Text = "&Archivo";
            // 
            // mnuCambiarClave
            // 
            mnuCambiarClave.Name = "mnuCambiarClave";
            mnuCambiarClave.Size = new Size(224, 26);
            mnuCambiarClave.Text = "Cambiar contraseña";
            mnuCambiarClave.Click += mnuCambiarClave_Click;
            // 
            // mnuCerrarSesion
            // 
            mnuCerrarSesion.Name = "mnuCerrarSesion";
            mnuCerrarSesion.Size = new Size(224, 26);
            mnuCerrarSesion.Text = "Cerrar sesión";
            mnuCerrarSesion.Click += mnuCerrarSesion_Click;
            // 
            // mnuSalir
            // 
            mnuSalir.Name = "mnuSalir";
            mnuSalir.Size = new Size(224, 26);
            mnuSalir.Text = "Salir";
            mnuSalir.Click += mnuSalir_Click;
            // 
            // mnuEstablecimientos
            // 
            mnuEstablecimientos.Name = "mnuEstablecimientos";
            mnuEstablecimientos.Size = new Size(135, 24);
            mnuEstablecimientos.Text = "&Establecimientos";
            mnuEstablecimientos.Click += mnuEstablecimientos_Click;
            // 
            // statusPrincipal
            // 
            statusPrincipal.ImageScalingSize = new Size(20, 20);
            statusPrincipal.Items.AddRange(new ToolStripItem[] { lblUsuarioEstado, lblRolEstado, lblProvinciaEstado });
            statusPrincipal.Location = new Point(0, 614);
            statusPrincipal.Name = "statusPrincipal";
            statusPrincipal.Size = new Size(1000, 26);
            statusPrincipal.TabIndex = 1;
            // 
            // lblUsuarioEstado
            // 
            lblUsuarioEstado.Name = "lblUsuarioEstado";
            lblUsuarioEstado.Padding = new Padding(0, 0, 24, 0);
            lblUsuarioEstado.Size = new Size(86, 20);
            lblUsuarioEstado.Text = "Usuario:";
            // 
            // lblRolEstado
            // 
            lblRolEstado.Name = "lblRolEstado";
            lblRolEstado.Padding = new Padding(0, 0, 24, 0);
            lblRolEstado.Size = new Size(58, 20);
            lblRolEstado.Text = "Rol:";
            // 
            // lblProvinciaEstado
            // 
            lblProvinciaEstado.Name = "lblProvinciaEstado";
            lblProvinciaEstado.Size = new Size(72, 20);
            lblProvinciaEstado.Text = "Provincia:";
            // 
            // panelMenuLateral
            // 
            panelMenuLateral.BackColor = Color.White;
            panelMenuLateral.Controls.Add(btnNavCerrarSesion);
            panelMenuLateral.Controls.Add(btnNavClave);
            panelMenuLateral.Controls.Add(btnNavEstablecimientos);
            panelMenuLateral.Controls.Add(lblMenu);
            panelMenuLateral.Dock = DockStyle.Left;
            panelMenuLateral.Location = new Point(0, 28);
            panelMenuLateral.Name = "panelMenuLateral";
            panelMenuLateral.Size = new Size(244, 586);
            panelMenuLateral.TabIndex = 2;
            // 
            // btnNavCerrarSesion
            // 
            btnNavCerrarSesion.Location = new Point(0, 140);
            btnNavCerrarSesion.Name = "btnNavCerrarSesion";
            btnNavCerrarSesion.Size = new Size(230, 46);
            btnNavCerrarSesion.TabIndex = 2;
            btnNavCerrarSesion.Text = "Cerrar sesión";
            btnNavCerrarSesion.UseVisualStyleBackColor = true;
            btnNavCerrarSesion.Click += mnuCerrarSesion_Click;
            // 
            // btnNavClave
            // 
            btnNavClave.Location = new Point(0, 94);
            btnNavClave.Name = "btnNavClave";
            btnNavClave.Size = new Size(230, 46);
            btnNavClave.TabIndex = 1;
            btnNavClave.Text = "Cambiar contraseña";
            btnNavClave.UseVisualStyleBackColor = true;
            btnNavClave.Click += mnuCambiarClave_Click;
            // 
            // btnNavEstablecimientos
            // 
            btnNavEstablecimientos.Location = new Point(0, 48);
            btnNavEstablecimientos.Name = "btnNavEstablecimientos";
            btnNavEstablecimientos.Size = new Size(230, 46);
            btnNavEstablecimientos.TabIndex = 0;
            btnNavEstablecimientos.Text = "Establecimientos";
            btnNavEstablecimientos.UseVisualStyleBackColor = true;
            btnNavEstablecimientos.Click += mnuEstablecimientos_Click;
            // 
            // lblMenu
            // 
            lblMenu.AutoSize = true;
            lblMenu.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblMenu.ForeColor = Color.FromArgb(127, 140, 141);
            lblMenu.Location = new Point(20, 20);
            lblMenu.Name = "lblMenu";
            lblMenu.Size = new Size(54, 20);
            lblMenu.TabIndex = 0;
            lblMenu.Text = "MENÚ";
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.FromArgb(242, 245, 247);
            panelContenido.Controls.Add(panelTarjeta);
            panelContenido.Controls.Add(panelTituloPagina);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(244, 28);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(756, 586);
            panelContenido.TabIndex = 3;
            // 
            // panelTarjeta
            // 
            panelTarjeta.BackColor = Color.White;
            panelTarjeta.Controls.Add(lblBienvenida);
            panelTarjeta.Controls.Add(lblDatosUsuario);
            panelTarjeta.Controls.Add(btnIrEstablecimientos);
            panelTarjeta.Location = new Point(30, 94);
            panelTarjeta.Name = "panelTarjeta";
            panelTarjeta.Size = new Size(560, 240);
            panelTarjeta.TabIndex = 1;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.FromArgb(44, 62, 80);
            lblBienvenida.Location = new Point(24, 20);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(188, 37);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido/a";
            // 
            // lblDatosUsuario
            // 
            lblDatosUsuario.AutoSize = true;
            lblDatosUsuario.Font = new Font("Segoe UI", 11F);
            lblDatosUsuario.ForeColor = Color.FromArgb(127, 140, 141);
            lblDatosUsuario.Location = new Point(26, 75);
            lblDatosUsuario.Name = "lblDatosUsuario";
            lblDatosUsuario.Size = new Size(77, 50);
            lblDatosUsuario.TabIndex = 1;
            lblDatosUsuario.Text = "Rol:\r\nÁmbito:";
            // 
            // btnIrEstablecimientos
            // 
            btnIrEstablecimientos.Location = new Point(24, 165);
            btnIrEstablecimientos.Name = "btnIrEstablecimientos";
            btnIrEstablecimientos.Size = new Size(280, 46);
            btnIrEstablecimientos.TabIndex = 2;
            btnIrEstablecimientos.Text = "Gestionar establecimientos";
            btnIrEstablecimientos.UseVisualStyleBackColor = true;
            btnIrEstablecimientos.Click += mnuEstablecimientos_Click;
            // 
            // panelTituloPagina
            // 
            panelTituloPagina.BackColor = Color.FromArgb(31, 181, 197);
            panelTituloPagina.Controls.Add(lblTituloPagina);
            panelTituloPagina.Dock = DockStyle.Top;
            panelTituloPagina.Location = new Point(0, 0);
            panelTituloPagina.Name = "panelTituloPagina";
            panelTituloPagina.Size = new Size(756, 64);
            panelTituloPagina.TabIndex = 0;
            // 
            // lblTituloPagina
            // 
            lblTituloPagina.AutoSize = true;
            lblTituloPagina.Font = new Font("Segoe UI", 18F);
            lblTituloPagina.ForeColor = Color.White;
            lblTituloPagina.Location = new Point(20, 12);
            lblTituloPagina.Name = "lblTituloPagina";
            lblTituloPagina.Size = new Size(89, 41);
            lblTituloPagina.TabIndex = 0;
            lblTituloPagina.Text = "Inicio";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 640);
            Controls.Add(panelContenido);
            Controls.Add(panelMenuLateral);
            Controls.Add(statusPrincipal);
            Controls.Add(menuPrincipal);
            MainMenuStrip = menuPrincipal;
            MinimumSize = new Size(900, 560);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salud-Provincial";
            FormClosed += FrmPrincipal_FormClosed;
            Load += FrmPrincipal_Load;
            menuPrincipal.ResumeLayout(false);
            menuPrincipal.PerformLayout();
            statusPrincipal.ResumeLayout(false);
            statusPrincipal.PerformLayout();
            panelMenuLateral.ResumeLayout(false);
            panelMenuLateral.PerformLayout();
            panelContenido.ResumeLayout(false);
            panelTarjeta.ResumeLayout(false);
            panelTarjeta.PerformLayout();
            panelTituloPagina.ResumeLayout(false);
            panelTituloPagina.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuPrincipal;
        private ToolStripMenuItem mnuArchivo;
        private ToolStripMenuItem mnuCambiarClave;
        private ToolStripMenuItem mnuCerrarSesion;
        private ToolStripMenuItem mnuSalir;
        private ToolStripMenuItem mnuEstablecimientos;
        private StatusStrip statusPrincipal;
        private ToolStripStatusLabel lblUsuarioEstado;
        private ToolStripStatusLabel lblRolEstado;
        private ToolStripStatusLabel lblProvinciaEstado;
        private Panel panelMenuLateral;
        private Label lblMenu;
        private Button btnNavEstablecimientos;
        private Button btnNavClave;
        private Button btnNavCerrarSesion;
        private Panel panelContenido;
        private Panel panelTarjeta;
        private Label lblBienvenida;
        private Label lblDatosUsuario;
        private Button btnIrEstablecimientos;
        private Panel panelTituloPagina;
        private Label lblTituloPagina;
    }
}
