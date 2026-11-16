using System.Drawing;
using System.Windows.Forms;
using Salud_Provincial.Presentacion.Controles;

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
            statusPrincipal = new StatusStrip();
            lblUsuarioEstado = new ToolStripStatusLabel();
            lblRolEstado = new ToolStripStatusLabel();
            lblProvinciaEstado = new ToolStripStatusLabel();
            panelSidebar = new Panel();
            tarjetaSidebar = new TarjetaBase();
            logoMarca = new LogoMarca();
            itemInicio = new ItemNavegacion();
            itemEstablecimientos = new ItemNavegacion();
            itemClave = new ItemNavegacion();
            itemCerrarSesion = new ItemNavegacion();
            panelContenido = new Panel();
            tlpDashboard = new TableLayoutPanel();
            kpiActivos = new TarjetaKpi();
            kpiInactivos = new TarjetaKpi();
            kpiTipos = new TarjetaKpi();
            tarjetaBienvenida = new TarjetaAccion();
            medidor = new TarjetaMedidor();
            tarjetaLista = new TarjetaLista();
            tarjetaBarras = new TarjetaBarras();
            statusPrincipal.SuspendLayout();
            panelSidebar.SuspendLayout();
            tarjetaSidebar.SuspendLayout();
            panelContenido.SuspendLayout();
            tlpDashboard.SuspendLayout();
            SuspendLayout();
            // 
            // statusPrincipal
            // 
            statusPrincipal.ImageScalingSize = new Size(20, 20);
            statusPrincipal.Items.AddRange(new ToolStripItem[] { lblUsuarioEstado, lblRolEstado, lblProvinciaEstado });
            statusPrincipal.Location = new Point(0, 774);
            statusPrincipal.Name = "statusPrincipal";
            statusPrincipal.Size = new Size(1280, 26);
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
            // panelSidebar
            // 
            panelSidebar.Controls.Add(tarjetaSidebar);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(12, 12, 2, 12);
            panelSidebar.Size = new Size(280, 774);
            panelSidebar.TabIndex = 2;
            panelSidebar.Paint += panelSidebar_Paint;
            // 
            // tarjetaSidebar
            // 
            tarjetaSidebar.BackColor = Color.White;
            tarjetaSidebar.Controls.Add(logoMarca);
            tarjetaSidebar.Controls.Add(itemInicio);
            tarjetaSidebar.Controls.Add(itemEstablecimientos);
            tarjetaSidebar.Controls.Add(itemClave);
            tarjetaSidebar.Controls.Add(itemCerrarSesion);
            tarjetaSidebar.Dock = DockStyle.Fill;
            tarjetaSidebar.Location = new Point(12, 12);
            tarjetaSidebar.Name = "tarjetaSidebar";
            tarjetaSidebar.Padding = new Padding(24);
            tarjetaSidebar.Size = new Size(266, 750);
            tarjetaSidebar.TabIndex = 0;
            // 
            // logoMarca
            // 
            logoMarca.BackColor = Color.Transparent;
            logoMarca.Location = new Point(14, 218);
            logoMarca.Name = "logoMarca";
            logoMarca.Size = new Size(234, 44);
            logoMarca.TabIndex = 0;
            // 
            // itemInicio
            // 
            itemInicio.Activo = true;
            itemInicio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            itemInicio.Location = new Point(10, 166);
            itemInicio.Name = "itemInicio";
            itemInicio.Size = new Size(238, 46);
            itemInicio.TabIndex = 1;
            itemInicio.Text = "Inicio";
            itemInicio.Click += itemInicio_Click;
            // 
            // itemEstablecimientos
            // 
            itemEstablecimientos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            itemEstablecimientos.Icono = TipoIcono.Edificio;
            itemEstablecimientos.Location = new Point(14, 114);
            itemEstablecimientos.Name = "itemEstablecimientos";
            itemEstablecimientos.Size = new Size(238, 46);
            itemEstablecimientos.TabIndex = 2;
            itemEstablecimientos.Text = "Establecimientos";
            itemEstablecimientos.Click += mnuEstablecimientos_Click;
            // 
            // itemClave
            // 
            itemClave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            itemClave.Icono = TipoIcono.Candado;
            itemClave.Location = new Point(14, 62);
            itemClave.Name = "itemClave";
            itemClave.Size = new Size(238, 46);
            itemClave.TabIndex = 3;
            itemClave.Text = "Cambiar contraseña";
            itemClave.Click += mnuCambiarClave_Click;
            // 
            // itemCerrarSesion
            // 
            itemCerrarSesion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            itemCerrarSesion.Icono = TipoIcono.Salir;
            itemCerrarSesion.Location = new Point(14, 690);
            itemCerrarSesion.Name = "itemCerrarSesion";
            itemCerrarSesion.Size = new Size(238, 46);
            itemCerrarSesion.TabIndex = 5;
            itemCerrarSesion.Text = "Cerrar sesión";
            itemCerrarSesion.Click += mnuCerrarSesion_Click;
            // 
            // panelContenido
            // 
            panelContenido.Controls.Add(tlpDashboard);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(280, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(0, 6, 8, 6);
            panelContenido.Size = new Size(1000, 774);
            panelContenido.TabIndex = 3;
            // 
            // tlpDashboard
            // 
            tlpDashboard.ColumnCount = 3;
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            tlpDashboard.Controls.Add(kpiActivos, 0, 0);
            tlpDashboard.Controls.Add(kpiInactivos, 1, 0);
            tlpDashboard.Controls.Add(kpiTipos, 2, 0);
            tlpDashboard.Controls.Add(tarjetaBienvenida, 0, 1);
            tlpDashboard.Controls.Add(medidor, 1, 1);
            tlpDashboard.Controls.Add(tarjetaLista, 2, 1);
            tlpDashboard.Controls.Add(tarjetaBarras, 0, 2);
            tlpDashboard.Dock = DockStyle.Fill;
            tlpDashboard.Location = new Point(0, 6);
            tlpDashboard.Name = "tlpDashboard";
            tlpDashboard.RowCount = 3;
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDashboard.Size = new Size(992, 762);
            tlpDashboard.TabIndex = 0;
            // 
            // kpiActivos
            // 
            kpiActivos.BackColor = Color.White;
            kpiActivos.Dock = DockStyle.Fill;
            kpiActivos.Location = new Point(2, 2);
            kpiActivos.Margin = new Padding(2);
            kpiActivos.Name = "kpiActivos";
            kpiActivos.Padding = new Padding(24);
            kpiActivos.Size = new Size(326, 146);
            kpiActivos.TabIndex = 0;
            kpiActivos.Titulo = "Establecimientos activos";
            kpiActivos.FlechaClick += mnuEstablecimientos_Click;
            // 
            // kpiInactivos
            // 
            kpiInactivos.BackColor = Color.White;
            kpiInactivos.Dock = DockStyle.Fill;
            kpiInactivos.Location = new Point(332, 2);
            kpiInactivos.Margin = new Padding(2);
            kpiInactivos.Name = "kpiInactivos";
            kpiInactivos.Padding = new Padding(24);
            kpiInactivos.Size = new Size(326, 146);
            kpiInactivos.TabIndex = 1;
            kpiInactivos.Titulo = "Establecimientos inactivos";
            kpiInactivos.FlechaClick += mnuEstablecimientos_Click;
            // 
            // kpiTipos
            // 
            kpiTipos.BackColor = Color.White;
            kpiTipos.Dock = DockStyle.Fill;
            kpiTipos.Gradiente = true;
            kpiTipos.Location = new Point(662, 2);
            kpiTipos.Margin = new Padding(2);
            kpiTipos.Name = "kpiTipos";
            kpiTipos.Padding = new Padding(24);
            kpiTipos.Size = new Size(328, 146);
            kpiTipos.TabIndex = 2;
            kpiTipos.Titulo = "Tipos de establecimiento";
            kpiTipos.FlechaClick += mnuEstablecimientos_Click;
            // 
            // tarjetaBienvenida
            // 
            tarjetaBienvenida.BackColor = Color.White;
            tarjetaBienvenida.Dock = DockStyle.Fill;
            tarjetaBienvenida.Location = new Point(2, 152);
            tarjetaBienvenida.Margin = new Padding(2);
            tarjetaBienvenida.Name = "tarjetaBienvenida";
            tarjetaBienvenida.Padding = new Padding(24);
            tarjetaBienvenida.Size = new Size(326, 246);
            tarjetaBienvenida.TabIndex = 3;
            tarjetaBienvenida.Accion += mnuEstablecimientos_Click;
            // 
            // medidor
            // 
            medidor.BackColor = Color.White;
            medidor.Dock = DockStyle.Fill;
            medidor.Location = new Point(332, 152);
            medidor.Margin = new Padding(2);
            medidor.Name = "medidor";
            medidor.Padding = new Padding(24);
            medidor.Size = new Size(326, 246);
            medidor.TabIndex = 4;
            medidor.Titulo = "Establecimientos activos";
            // 
            // tarjetaLista
            // 
            tarjetaLista.BackColor = Color.White;
            tarjetaLista.Dock = DockStyle.Fill;
            tarjetaLista.Location = new Point(662, 152);
            tarjetaLista.Margin = new Padding(2);
            tarjetaLista.Name = "tarjetaLista";
            tarjetaLista.Padding = new Padding(24);
            tlpDashboard.SetRowSpan(tarjetaLista, 2);
            tarjetaLista.Size = new Size(328, 608);
            tarjetaLista.TabIndex = 5;
            tarjetaLista.TextoVacio = "Aún no hay establecimientos registrados.";
            tarjetaLista.Titulo = "Registrados recientemente";
            // 
            // tarjetaBarras
            // 
            tarjetaBarras.BackColor = Color.White;
            tlpDashboard.SetColumnSpan(tarjetaBarras, 2);
            tarjetaBarras.Dock = DockStyle.Fill;
            tarjetaBarras.Location = new Point(2, 402);
            tarjetaBarras.Margin = new Padding(2);
            tarjetaBarras.Name = "tarjetaBarras";
            tarjetaBarras.Padding = new Padding(24);
            tarjetaBarras.Size = new Size(656, 358);
            tarjetaBarras.TabIndex = 6;
            tarjetaBarras.Titulo = "Establecimientos";
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 800);
            Controls.Add(panelContenido);
            Controls.Add(panelSidebar);
            Controls.Add(statusPrincipal);
            MinimumSize = new Size(1100, 760);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salud-Provincial";
            FormClosed += FrmPrincipal_FormClosed;
            Load += FrmPrincipal_Load;
            statusPrincipal.ResumeLayout(false);
            statusPrincipal.PerformLayout();
            panelSidebar.ResumeLayout(false);
            tarjetaSidebar.ResumeLayout(false);
            panelContenido.ResumeLayout(false);
            tlpDashboard.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusPrincipal;
        private ToolStripStatusLabel lblUsuarioEstado;
        private ToolStripStatusLabel lblRolEstado;
        private ToolStripStatusLabel lblProvinciaEstado;
        private Panel panelSidebar;
        private TarjetaBase tarjetaSidebar;
        private LogoMarca logoMarca;
        private ItemNavegacion itemInicio;
        private ItemNavegacion itemEstablecimientos;
        private ItemNavegacion itemClave;
        private ItemNavegacion itemCerrarSesion;
        private Panel panelContenido;
        private TableLayoutPanel tlpDashboard;
        private TarjetaKpi kpiActivos;
        private TarjetaKpi kpiInactivos;
        private TarjetaKpi kpiTipos;
        private TarjetaAccion tarjetaBienvenida;
        private TarjetaMedidor medidor;
        private TarjetaLista tarjetaLista;
        private TarjetaBarras tarjetaBarras;
    }
}
