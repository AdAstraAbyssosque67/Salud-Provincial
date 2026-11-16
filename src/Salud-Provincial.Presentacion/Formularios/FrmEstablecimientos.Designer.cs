using System.Drawing;
using System.Windows.Forms;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    partial class FrmEstablecimientos
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
            components = new System.ComponentModel.Container();
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            lblAmbito = new Label();
            btnRefrescar = new BotonPildora();
            panelCuerpo = new Panel();
            tarjetaLista = new TarjetaBase();
            dgvEstablecimientos = new DataGridView();
            colNombre = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colDireccion = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colProvincia = new DataGridViewTextBoxColumn();
            lblResumen = new Label();
            tarjetaDetalle = new TarjetaBase();
            lblTituloDetalle = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblProvincia = new Label();
            cmbProvincia = new ComboBox();
            lblObligatorio = new Label();
            btnGuardar = new BotonPildora();
            btnNuevo = new BotonPildora();
            btnEstado = new BotonPildora();
            errorProvider = new ErrorProvider(components);
            panelEncabezado.SuspendLayout();
            panelCuerpo.SuspendLayout();
            tarjetaLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEstablecimientos).BeginInit();
            tarjetaDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // panelEncabezado
            // 
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(lblAmbito);
            panelEncabezado.Controls.Add(btnRefrescar);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1100, 84);
            panelEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Regular, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(26, 32, 44);
            lblTitulo.Location = new Point(24, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(220, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Establecimientos";
            // 
            // lblAmbito
            // 
            lblAmbito.AutoSize = true;
            lblAmbito.ForeColor = Color.FromArgb(113, 128, 150);
            lblAmbito.Location = new Point(27, 52);
            lblAmbito.Name = "lblAmbito";
            lblAmbito.Size = new Size(60, 20);
            lblAmbito.TabIndex = 1;
            lblAmbito.Text = "Ámbito";
            // 
            // btnRefrescar
            // 
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(932, 23);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(130, 36);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = false;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // panelCuerpo
            // 
            panelCuerpo.Controls.Add(tarjetaLista);
            panelCuerpo.Controls.Add(tarjetaDetalle);
            panelCuerpo.Dock = DockStyle.Fill;
            panelCuerpo.Location = new Point(0, 84);
            panelCuerpo.Name = "panelCuerpo";
            panelCuerpo.Padding = new Padding(16, 0, 16, 16);
            panelCuerpo.Size = new Size(1100, 596);
            panelCuerpo.TabIndex = 1;
            // 
            // tarjetaLista
            // 
            tarjetaLista.BackColor = Color.White;
            tarjetaLista.Controls.Add(dgvEstablecimientos);
            tarjetaLista.Controls.Add(lblResumen);
            tarjetaLista.Dock = DockStyle.Fill;
            tarjetaLista.Location = new Point(16, 0);
            tarjetaLista.Name = "tarjetaLista";
            tarjetaLista.Padding = new Padding(22, 22, 22, 14);
            tarjetaLista.Size = new Size(688, 580);
            tarjetaLista.TabIndex = 0;
            // 
            // dgvEstablecimientos
            // 
            dgvEstablecimientos.AllowUserToAddRows = false;
            dgvEstablecimientos.AllowUserToDeleteRows = false;
            dgvEstablecimientos.AllowUserToResizeRows = false;
            dgvEstablecimientos.AutoGenerateColumns = false;
            dgvEstablecimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstablecimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEstablecimientos.Columns.AddRange(new DataGridViewColumn[] { colNombre, colTipo, colDireccion, colTelefono, colEstado, colProvincia });
            dgvEstablecimientos.Dock = DockStyle.Fill;
            dgvEstablecimientos.Location = new Point(22, 22);
            dgvEstablecimientos.MultiSelect = false;
            dgvEstablecimientos.Name = "dgvEstablecimientos";
            dgvEstablecimientos.ReadOnly = true;
            dgvEstablecimientos.RowHeadersWidth = 51;
            dgvEstablecimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstablecimientos.Size = new Size(644, 502);
            dgvEstablecimientos.TabIndex = 0;
            dgvEstablecimientos.CellFormatting += dgvEstablecimientos_CellFormatting;
            dgvEstablecimientos.SelectionChanged += dgvEstablecimientos_SelectionChanged;
            // 
            // colNombre
            // 
            colNombre.DataPropertyName = "Nombre";
            colNombre.FillWeight = 130F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colTipo
            // 
            colTipo.DataPropertyName = "NombreTipo";
            colTipo.HeaderText = "Tipo";
            colTipo.MinimumWidth = 6;
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            // 
            // colDireccion
            // 
            colDireccion.DataPropertyName = "Direccion";
            colDireccion.FillWeight = 130F;
            colDireccion.HeaderText = "Dirección";
            colDireccion.MinimumWidth = 6;
            colDireccion.Name = "colDireccion";
            colDireccion.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.DataPropertyName = "Telefono";
            colTelefono.FillWeight = 80F;
            colTelefono.HeaderText = "Teléfono";
            colTelefono.MinimumWidth = 6;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.DataPropertyName = "Activo";
            colEstado.FillWeight = 60F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 6;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colProvincia
            // 
            colProvincia.DataPropertyName = "NombreProvincia";
            colProvincia.HeaderText = "Provincia";
            colProvincia.MinimumWidth = 6;
            colProvincia.Name = "colProvincia";
            colProvincia.ReadOnly = true;
            // 
            // lblResumen
            // 
            lblResumen.Dock = DockStyle.Bottom;
            lblResumen.ForeColor = Color.FromArgb(113, 128, 150);
            lblResumen.Location = new Point(22, 524);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(644, 34);
            lblResumen.TabIndex = 1;
            lblResumen.Text = "Resumen";
            lblResumen.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tarjetaDetalle
            // 
            tarjetaDetalle.BackColor = Color.White;
            tarjetaDetalle.Controls.Add(lblTituloDetalle);
            tarjetaDetalle.Controls.Add(lblNombre);
            tarjetaDetalle.Controls.Add(txtNombre);
            tarjetaDetalle.Controls.Add(lblTipo);
            tarjetaDetalle.Controls.Add(cmbTipo);
            tarjetaDetalle.Controls.Add(lblDireccion);
            tarjetaDetalle.Controls.Add(txtDireccion);
            tarjetaDetalle.Controls.Add(lblTelefono);
            tarjetaDetalle.Controls.Add(txtTelefono);
            tarjetaDetalle.Controls.Add(lblProvincia);
            tarjetaDetalle.Controls.Add(cmbProvincia);
            tarjetaDetalle.Controls.Add(lblObligatorio);
            tarjetaDetalle.Controls.Add(btnGuardar);
            tarjetaDetalle.Controls.Add(btnNuevo);
            tarjetaDetalle.Controls.Add(btnEstado);
            tarjetaDetalle.Dock = DockStyle.Right;
            tarjetaDetalle.Location = new Point(704, 0);
            tarjetaDetalle.Name = "tarjetaDetalle";
            tarjetaDetalle.Size = new Size(380, 580);
            tarjetaDetalle.TabIndex = 1;
            // 
            // lblTituloDetalle
            // 
            lblTituloDetalle.AutoSize = true;
            lblTituloDetalle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Regular, GraphicsUnit.Point);
            lblTituloDetalle.ForeColor = Color.FromArgb(26, 32, 44);
            lblTituloDetalle.Location = new Point(28, 26);
            lblTituloDetalle.Name = "lblTituloDetalle";
            lblTituloDetalle.Size = new Size(230, 32);
            lblTituloDetalle.TabIndex = 0;
            lblTituloDetalle.Text = "Nuevo establecimiento";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblNombre.ForeColor = Color.FromArgb(113, 128, 150);
            lblNombre.Location = new Point(29, 76);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Location = new Point(28, 100);
            txtNombre.MaxLength = 150;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej.: Hospital Regional";
            txtNombre.Size = new Size(316, 27);
            txtNombre.TabIndex = 0;
            txtNombre.TextChanged += LimpiarError_Cambio;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblTipo.ForeColor = Color.FromArgb(113, 128, 150);
            lblTipo.Location = new Point(29, 138);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(42, 20);
            lblTipo.TabIndex = 3;
            lblTipo.Text = "Tipo *";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FlatStyle = FlatStyle.Flat;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(28, 162);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(316, 28);
            cmbTipo.TabIndex = 1;
            cmbTipo.SelectedIndexChanged += LimpiarError_Cambio;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblDireccion.ForeColor = Color.FromArgb(113, 128, 150);
            lblDireccion.Location = new Point(29, 200);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(65, 20);
            lblDireccion.TabIndex = 5;
            lblDireccion.Text = "Dirección";
            // 
            // txtDireccion
            // 
            txtDireccion.BorderStyle = BorderStyle.FixedSingle;
            txtDireccion.Location = new Point(28, 224);
            txtDireccion.MaxLength = 250;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "Calle, sector, ciudad";
            txtDireccion.Size = new Size(316, 27);
            txtDireccion.TabIndex = 2;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblTelefono.ForeColor = Color.FromArgb(113, 128, 150);
            lblTelefono.Location = new Point(29, 262);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(62, 20);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Location = new Point(28, 286);
            txtTelefono.MaxLength = 30;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "809-000-0000";
            txtTelefono.Size = new Size(316, 27);
            txtTelefono.TabIndex = 3;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblProvincia.ForeColor = Color.FromArgb(113, 128, 150);
            lblProvincia.Location = new Point(29, 324);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(73, 20);
            lblProvincia.TabIndex = 9;
            lblProvincia.Text = "Provincia *";
            // 
            // cmbProvincia
            // 
            cmbProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProvincia.FlatStyle = FlatStyle.Flat;
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new Point(28, 348);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(316, 28);
            cmbProvincia.TabIndex = 4;
            cmbProvincia.SelectedIndexChanged += LimpiarError_Cambio;
            // 
            // lblObligatorio
            // 
            lblObligatorio.AutoSize = true;
            lblObligatorio.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
            lblObligatorio.ForeColor = Color.FromArgb(160, 174, 192);
            lblObligatorio.Location = new Point(29, 388);
            lblObligatorio.Name = "lblObligatorio";
            lblObligatorio.Size = new Size(120, 20);
            lblObligatorio.TabIndex = 11;
            lblObligatorio.Text = "* Campo obligatorio";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(28, 424);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(316, 46);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(28, 482);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(152, 42);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEstado
            // 
            btnEstado.Enabled = false;
            btnEstado.Location = new Point(192, 482);
            btnEstado.Name = "btnEstado";
            btnEstado.Size = new Size(152, 42);
            btnEstado.TabIndex = 7;
            btnEstado.Text = "Desactivar";
            btnEstado.UseVisualStyleBackColor = false;
            btnEstado.Click += btnEstado_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // FrmEstablecimientos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 680);
            Controls.Add(panelCuerpo);
            Controls.Add(panelEncabezado);
            MinimumSize = new Size(1100, 720);
            Name = "FrmEstablecimientos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Establecimientos";
            Load += FrmEstablecimientos_Load;
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelCuerpo.ResumeLayout(false);
            tarjetaLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEstablecimientos).EndInit();
            tarjetaDetalle.ResumeLayout(false);
            tarjetaDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblAmbito;
        private BotonPildora btnRefrescar;
        private Panel panelCuerpo;
        private TarjetaBase tarjetaLista;
        private DataGridView dgvEstablecimientos;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colDireccion;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colProvincia;
        private Label lblResumen;
        private TarjetaBase tarjetaDetalle;
        private Label lblTituloDetalle;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblProvincia;
        private ComboBox cmbProvincia;
        private Label lblObligatorio;
        private BotonPildora btnGuardar;
        private BotonPildora btnNuevo;
        private BotonPildora btnEstado;
        private ErrorProvider errorProvider;
    }
}
