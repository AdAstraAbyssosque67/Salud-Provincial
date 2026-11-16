using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Seguridad;

namespace Salud_Provincial.Presentacion
{
    public partial class FrmEstablecimientos : Form
    {
        private readonly IEstablecimientoServicio _servicio = FabricaServicios.Establecimientos();
        private readonly ITipoEstablecimientoServicio _tipos = FabricaServicios.Tipos();
        private readonly IProvinciaServicio _provincias = FabricaServicios.Provincias();

        private Establecimiento? _actual;   // null = modo "nuevo"
        private bool _cargando;

        private static bool EsAdmin => SesionUsuario.Instancia.EsAdminNacional;

        public FrmEstablecimientos()
        {
            InitializeComponent();
            Tema.Formulario(this);
            Tema.Tabla(dgvEstablecimientos);
            Tema.BotonPrimario(btnGuardar);
            Tema.BotonSecundario(btnNuevo);
            Tema.BotonEncabezado(btnRefrescar);
        }

        // Cada vez que se abre, se vuelve a consultar todo.
        private void FrmEstablecimientos_Load(object sender, EventArgs e)
        {
            colProvincia.Visible = EsAdmin;
            lblAmbito.Text = EsAdmin
                ? "Vista nacional: todas las provincias"
                : "Provincia: " + SesionUsuario.Instancia.NombreProvincia;
            try
            {
                CargarCombos();
                CargarLista(null);
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }

        private void CargarCombos()
        {
            cmbTipo.DisplayMember = "Nombre";
            cmbTipo.ValueMember = "TipoEstablecimientoId";
            cmbTipo.DataSource = _tipos.Listar();
            cmbTipo.SelectedIndex = -1;

            cmbProvincia.DisplayMember = "Nombre";
            cmbProvincia.ValueMember = "ProvinciaId";
            cmbProvincia.DataSource = _provincias.Listar();
            cmbProvincia.SelectedIndex = -1;
        }

        private void CargarLista(int? seleccionarId)
        {
            List<Establecimiento> lista;
            _cargando = true;
            try
            {
                lista = _servicio.Listar(chkMostrarInactivos.Checked);
                dgvEstablecimientos.DataSource = lista;
                dgvEstablecimientos.ClearSelection();
            }
            finally
            {
                _cargando = false;
            }

            lblResumen.Text = lista.Count == 0
                ? "No hay establecimientos para mostrar. Use \"Nuevo\" para registrar uno."
                : $"{lista.Count} establecimiento(s). Seleccione una fila para ver o editar sus datos.";

            if (seleccionarId.HasValue)
            {
                foreach (DataGridViewRow fila in dgvEstablecimientos.Rows)
                {
                    if (fila.DataBoundItem is Establecimiento est && est.EstablecimientoId == seleccionarId.Value)
                    {
                        fila.Selected = true;
                        dgvEstablecimientos.CurrentCell = fila.Cells[colNombre.Index];
                        MostrarEstablecimiento(est);
                        return;
                    }
                }
            }
            PrepararNuevo();
        }

        private void PrepararNuevo()
        {
            _actual = null;
            dgvEstablecimientos.ClearSelection();
            errorProvider.Clear();

            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cmbTipo.SelectedIndex = -1;

            if (EsAdmin)
            {
                cmbProvincia.SelectedIndex = -1;
                cmbProvincia.Enabled = true;
            }
            else
            {
                // La provincia del usuario provincial es fija (Negocio igual usa la de la sesión).
                if (SesionUsuario.Instancia.ProvinciaId is int pid)
                    cmbProvincia.SelectedValue = pid;
                else
                    cmbProvincia.SelectedIndex = -1;
                cmbProvincia.Enabled = false;
            }

            lblTituloDetalle.Text = "Nuevo establecimiento";
            ActualizarBotonEstado();
            txtNombre.Focus();
        }

        private void MostrarEstablecimiento(Establecimiento est)
        {
            _actual = est;
            errorProvider.Clear();

            txtNombre.Text = est.Nombre;
            txtDireccion.Text = est.Direccion ?? string.Empty;
            txtTelefono.Text = est.Telefono ?? string.Empty;

            // Si el tipo/provincia está inactivo no aparece en el combo y queda en blanco.
            cmbTipo.SelectedIndex = -1;
            cmbTipo.SelectedValue = est.TipoEstablecimientoId;
            cmbProvincia.SelectedIndex = -1;
            cmbProvincia.SelectedValue = est.ProvinciaId;
            cmbProvincia.Enabled = false;   // al editar, la provincia no cambia

            lblTituloDetalle.Text = est.Activo ? "Editar establecimiento" : "Establecimiento inactivo";
            ActualizarBotonEstado();
            errorProvider.Clear();
        }

        /// <summary>Desactivar (rojo) o Reactivar (turquesa) según el registro seleccionado.</summary>
        private void ActualizarBotonEstado()
        {
            bool activo = _actual?.Activo ?? true;
            btnEstado.Text = activo ? "Desactivar" : "Reactivar";
            if (activo) Tema.BotonPeligro(btnEstado); else Tema.BotonTurquesa(btnEstado);
            btnEstado.Enabled = _actual != null;
        }

        private void dgvEstablecimientos_SelectionChanged(object sender, EventArgs e)
        {
            if (_cargando || dgvEstablecimientos.SelectedRows.Count == 0)
                return;

            if (dgvEstablecimientos.SelectedRows[0].DataBoundItem is Establecimiento est)
                MostrarEstablecimiento(est);
        }

        private void dgvEstablecimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvEstablecimientos.Rows[e.RowIndex].DataBoundItem is not Establecimiento est)
                return;

            if (e.ColumnIndex == colEstado.Index)
            {
                e.Value = est.Activo ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
                e.CellStyle.ForeColor = est.Activo ? Tema.Exito : Tema.TextoSuave;
            }

            if (!est.Activo)
            {
                e.CellStyle.BackColor = Tema.FilaInactiva;   // equivalente al "no disponible" de la referencia
                if (e.ColumnIndex != colEstado.Index)
                    e.CellStyle.ForeColor = Color.Gray;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            PrepararNuevo();
        }

        private void LimpiarError_Cambio(object sender, EventArgs e)
        {
            if (sender is Control c)
                errorProvider.SetError(c, string.Empty);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Solo ayudas de experiencia de usuario; las reglas reales las valida Negocio.
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "Escriba el nombre del establecimiento.");
                txtNombre.Focus();
                return;
            }
            if (cmbTipo.SelectedValue is not int tipoId)
            {
                errorProvider.SetError(cmbTipo, "Seleccione el tipo de establecimiento.");
                cmbTipo.Focus();
                return;
            }

            int provinciaId;
            if (_actual != null)
            {
                provinciaId = _actual.ProvinciaId;
            }
            else if (cmbProvincia.SelectedValue is int pid)
            {
                provinciaId = pid;
            }
            else
            {
                errorProvider.SetError(cmbProvincia, "Seleccione la provincia.");
                cmbProvincia.Focus();
                return;
            }

            var datos = new Establecimiento
            {
                EstablecimientoId = _actual?.EstablecimientoId ?? 0,
                Nombre = txtNombre.Text.Trim(),
                Direccion = string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                TipoEstablecimientoId = tipoId,
                ProvinciaId = provinciaId
            };

            try
            {
                int id;
                if (_actual == null)
                {
                    id = _servicio.Crear(datos).EstablecimientoId;
                }
                else
                {
                    _servicio.Actualizar(datos);
                    id = datos.EstablecimientoId;
                }

                CargarLista(id);
                MessageBox.Show(this, "Establecimiento guardado correctamente.",
                    "Salud-Provincial", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            if (_actual == null)
                return;

            try
            {
                if (_actual.Activo)
                {
                    var respuesta = MessageBox.Show(this,
                        $"¿Desea desactivar el establecimiento \"{_actual.Nombre}\"?\r\n\r\n" +
                        "Podrá reactivarlo después desde \"Mostrar inactivos\".",
                        "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta != DialogResult.Yes)
                        return;
                    _servicio.Desactivar(_actual.EstablecimientoId);
                }
                else
                {
                    _servicio.Reactivar(_actual.EstablecimientoId);
                }

                CargarLista(_actual.EstablecimientoId);
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        private void chkMostrarInactivos_CheckedChanged(object sender, EventArgs e)
        {
            Recargar();
        }

        private void Recargar()
        {
            try
            {
                CargarLista(_actual?.EstablecimientoId);
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }
    }
}
