using System.Drawing;
using System.Windows.Forms;

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
            chkMostrarInactivos = new CheckBox();
            btnRefrescar = new Button();
            panelLista = new Panel();
            dgvEstablecimientos = new DataGridView();
            colNombre = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colDireccion = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colProvincia = new DataGridViewTextBoxColumn();
            lblResumen = new Label();
            panelDetalle = new Panel();
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
            btnGuardar = new Button();
            btnNuevo = new Button();
            btnEstado = new Button();
            errorProvider = new ErrorProvider(components);
            panelEncabezado.SuspendLayout();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEstablecimientos).BeginInit();
            panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // panelEncabezado
            // 
            panelEncabezado.BackColor = Color.FromArgb(31, 181, 197);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(lblAmbito);
            panelEncabezado.Controls.Add(chkMostrarInactivos);
            panelEncabezado.Controls.Add(btnRefrescar);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1000, 70);
            panelEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(16, 6);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(187, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Establecimientos";
            // 
            // lblAmbito
            // 
            lblAmbito.AutoSize = true;
            lblAmbito.ForeColor = Color.White;
            lblAmbito.Location = new Point(19, 42);
            lblAmbito.Name = "lblAmbito";
            lblAmbito.Size = new Size(60, 20);
            lblAmbito.TabIndex = 1;
            lblAmbito.Text = "Ámbito";
            // 
            // chkMostrarInactivos
            // 
            chkMostrarInactivos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkMostrarInactivos.AutoSize = true;
            chkMostrarInactivos.ForeColor = Color.White;
            chkMostrarInactivos.Location = new Point(690, 23);
            chkMostrarInactivos.Name = "chkMostrarInactivos";
            chkMostrarInactivos.Size = new Size(150, 24);
            chkMostrarInactivos.TabIndex = 0;
            chkMostrarInactivos.Text = "Mostrar inactivos";
            chkMostrarInactivos.UseVisualStyleBackColor = true;
            chkMostrarInactivos.CheckedChanged += chkMostrarInactivos_CheckedChanged;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefrescar.Location = new Point(860, 19);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(120, 32);
            btnRefrescar.TabIndex = 1;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // panelLista
            // 
            panelLista.BackColor = Color.FromArgb(242, 245, 247);
            panelLista.Controls.Add(dgvEstablecimientos);
            panelLista.Controls.Add(lblResumen);
            panelLista.Dock = DockStyle.Fill;
            panelLista.Location = new Point(0, 70);
            panelLista.Name = "panelLista";
            panelLista.Padding = new Padding(16);
            panelLista.Size = new Size(660, 550);
            panelLista.TabIndex = 1;
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
            dgvEstablecimientos.Location = new Point(16, 16);
            dgvEstablecimientos.MultiSelect = false;
            dgvEstablecimientos.Name = "dgvEstablecimientos";
            dgvEstablecimientos.ReadOnly = true;
            dgvEstablecimientos.RowHeadersWidth = 51;
            dgvEstablecimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstablecimientos.Size = new Size(628, 490);
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
            lblResumen.ForeColor = Color.FromArgb(127, 140, 141);
            lblResumen.Location = new Point(16, 506);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(628, 28);
            lblResumen.TabIndex = 1;
            lblResumen.Text = "Resumen";
            lblResumen.TextAlign = ContentAlignment.BottomLeft;
            // 
            // panelDetalle
            // 
            panelDetalle.BackColor = Color.White;
            panelDetalle.Controls.Add(lblTituloDetalle);
            panelDetalle.Controls.Add(lblNombre);
            panelDetalle.Controls.Add(txtNombre);
            panelDetalle.Controls.Add(lblTipo);
            panelDetalle.Controls.Add(cmbTipo);
            panelDetalle.Controls.Add(lblDireccion);
            panelDetalle.Controls.Add(txtDireccion);
            panelDetalle.Controls.Add(lblTelefono);
            panelDetalle.Controls.Add(txtTelefono);
            panelDetalle.Controls.Add(lblProvincia);
            panelDetalle.Controls.Add(cmbProvincia);
            panelDetalle.Controls.Add(lblObligatorio);
            panelDetalle.Controls.Add(btnGuardar);
            panelDetalle.Controls.Add(btnNuevo);
            panelDetalle.Controls.Add(btnEstado);
            panelDetalle.Dock = DockStyle.Right;
            panelDetalle.Location = new Point(660, 70);
            panelDetalle.Name = "panelDetalle";
            panelDetalle.Size = new Size(340, 550);
            panelDetalle.TabIndex = 2;
            // 
            // lblTituloDetalle
            // 
            lblTituloDetalle.AutoSize = true;
            lblTituloDetalle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTituloDetalle.ForeColor = Color.FromArgb(18, 146, 159);
            lblTituloDetalle.Location = new Point(20, 16);
            lblTituloDetalle.Name = "lblTituloDetalle";
            lblTituloDetalle.Size = new Size(238, 30);
            lblTituloDetalle.TabIndex = 0;
            lblTituloDetalle.Text = "Nuevo establecimiento";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 60);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(76, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(20, 84);
            txtNombre.MaxLength = 150;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej.: Hospital Regional";
            txtNombre.Size = new Size(300, 27);
            txtNombre.TabIndex = 0;
            txtNombre.TextChanged += LimpiarError_Cambio;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(20, 126);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(50, 20);
            lblTipo.TabIndex = 3;
            lblTipo.Text = "Tipo *";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(20, 150);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(300, 28);
            cmbTipo.TabIndex = 1;
            cmbTipo.SelectedIndexChanged += LimpiarError_Cambio;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(20, 192);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(75, 20);
            lblDireccion.TabIndex = 5;
            lblDireccion.Text = "Dirección";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(20, 216);
            txtDireccion.MaxLength = 250;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "Calle, sector, ciudad";
            txtDireccion.Size = new Size(300, 27);
            txtDireccion.TabIndex = 2;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(20, 258);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(71, 20);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(20, 282);
            txtTelefono.MaxLength = 30;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "809-000-0000";
            txtTelefono.Size = new Size(300, 27);
            txtTelefono.TabIndex = 3;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new Point(20, 324);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(86, 20);
            lblProvincia.TabIndex = 9;
            lblProvincia.Text = "Provincia *";
            // 
            // cmbProvincia
            // 
            cmbProvincia.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new Point(20, 348);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(300, 28);
            cmbProvincia.TabIndex = 4;
            cmbProvincia.SelectedIndexChanged += LimpiarError_Cambio;
            // 
            // lblObligatorio
            // 
            lblObligatorio.AutoSize = true;
            lblObligatorio.Font = new Font("Segoe UI", 8.5F);
            lblObligatorio.ForeColor = Color.FromArgb(127, 140, 141);
            lblObligatorio.Location = new Point(20, 390);
            lblObligatorio.Name = "lblObligatorio";
            lblObligatorio.Size = new Size(120, 20);
            lblObligatorio.TabIndex = 11;
            lblObligatorio.Text = "* Campo obligatorio";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(20, 430);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(300, 42);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(20, 484);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(145, 36);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEstado
            // 
            btnEstado.Enabled = false;
            btnEstado.Location = new Point(175, 484);
            btnEstado.Name = "btnEstado";
            btnEstado.Size = new Size(145, 36);
            btnEstado.TabIndex = 7;
            btnEstado.Text = "Desactivar";
            btnEstado.UseVisualStyleBackColor = true;
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
            ClientSize = new Size(1000, 620);
            Controls.Add(panelLista);
            Controls.Add(panelDetalle);
            Controls.Add(panelEncabezado);
            MinimumSize = new Size(1000, 660);
            Name = "FrmEstablecimientos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Establecimientos";
            Load += FrmEstablecimientos_Load;
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEstablecimientos).EndInit();
            panelDetalle.ResumeLayout(false);
            panelDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblAmbito;
        private CheckBox chkMostrarInactivos;
        private Button btnRefrescar;
        private Panel panelLista;
        private DataGridView dgvEstablecimientos;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colDireccion;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colProvincia;
        private Label lblResumen;
        private Panel panelDetalle;
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
        private Button btnGuardar;
        private Button btnNuevo;
        private Button btnEstado;
        private ErrorProvider errorProvider;
    }
}
