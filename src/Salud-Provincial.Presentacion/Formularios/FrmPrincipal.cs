using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Seguridad;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private readonly IAutenticacionServicio _autenticacion = FabricaServicios.Autenticacion();
        private readonly IEstablecimientoServicio _establecimientos = FabricaServicios.Establecimientos();

        // True si el usuario eligió "Cerrar sesión" (Program vuelve al login).
        public bool CerrarSesionSolicitada { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
            Tema.Formulario(this);
            Tema.BarraEstado(statusPrincipal);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            var sesion = SesionUsuario.Instancia;
            string rol = sesion.EsAdminNacional ? "Administrador nacional" : "Provincial";
            string ambito = sesion.EsAdminNacional ? "Nacional" : (sesion.NombreProvincia ?? string.Empty);
            string nombre = sesion.UsuarioActual.NombreCompleto;

            lblUsuarioEstado.Text = "Usuario: " + nombre;
            lblRolEstado.Text = "Rol: " + rol;
            lblProvinciaEstado.Text = "Provincia: " + ambito;

            string primerNombre = nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? nombre;
            tarjetaBienvenida.Titulo = "Hola, " + primerNombre;
            tarjetaBienvenida.Texto = $"Rol: {rol}\r\nÁmbito: {ambito}\r\nSesión: {sesion.InicioSesion:dd/MM/yyyy HH:mm}";
            tarjetaBienvenida.TextoBoton = "Gestionar establecimientos";

            itemProvincias.Visible = sesion.EsAdminNacional;
            OrdenarSidebar();
            tarjetaSidebar.Resize += (_, _) => PosicionarCerrarSesion();
            MarcarActivo(itemInicio);
            CargarDashboard();
        }

        // Calcula los indicadores con los datos que Negocio permite ver a este usuario.
        private void CargarDashboard()
        {
            try
            {
                bool admin = SesionUsuario.Instancia.EsAdminNacional;
                List<Establecimiento> todos = _establecimientos.Listar(true);
                List<Establecimiento> activos = todos.Where(x => x.Activo).ToList();

                kpiActivos.Valor = activos.Count.ToString();
                kpiActivos.Detalle = admin
                    ? $"en {activos.Select(x => x.ProvinciaId).Distinct().Count()} provincia(s)"
                    : "en su provincia";

                kpiInactivos.Valor = (todos.Count - activos.Count).ToString();
                kpiInactivos.Detalle = "desactivados";

                kpiTipos.Valor = activos.Select(x => x.TipoEstablecimientoId).Distinct().Count().ToString();
                kpiTipos.Detalle = "tipos en uso";

                medidor.Porcentaje = todos.Count == 0 ? 0 : activos.Count * 100.0 / todos.Count;
                medidor.Detalle = $"{activos.Count} de {todos.Count} registrados";

                var grupos = admin
                    ? todos.GroupBy(x => x.NombreProvincia ?? "Sin provincia")
                    : todos.GroupBy(x => x.NombreTipo ?? "Sin tipo");
                tarjetaBarras.Titulo = admin ? "Establecimientos por provincia" : "Establecimientos por tipo";
                tarjetaBarras.Establecer(grupos
                    .OrderBy(grp => grp.Key)
                    .Select(grp => new PuntoBarra
                    {
                        Etiqueta = grp.Key,
                        Primario = grp.Count(x => x.Activo),
                        Secundario = grp.Count(x => !x.Activo)
                    }));

                var recientes = todos
                    .OrderByDescending(x => x.FechaCreacion)
                    .Take(12)
                    .Select(x => new ItemLista
                    {
                        Titulo = x.Nombre,
                        Subtitulo = x.NombreTipo ?? string.Empty,
                        Derecha = x.Activo ? "Activo" : "Inactivo",
                        ColorDerecha = x.Activo ? Tema.Exito : Tema.TextoSuave,
                        DerechaDetalle = admin ? (x.NombreProvincia ?? string.Empty) : x.FechaCreacion.ToString("dd/MM/yyyy"),
                        Atenuado = !x.Activo
                    })
                    .ToList();
                tarjetaLista.Establecer(recientes, todos.Count);
            }
            catch (Exception ex)
            {
                ManejadorErrores.Mostrar(ex, this);
            }
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Siempre se limpia la sesión al cerrar la ventana principal.
            try { _autenticacion.CerrarSesion(); } catch { }
        }

        private Form? _hijo;

        private void Mostrar(Form frm, ItemNavegacion? activo)
        {
            CerrarHijo();
            _hijo = frm;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.MinimumSize = Size.Empty;
            frm.Dock = DockStyle.Fill;
            tlpDashboard.Visible = false;
            panelContenido.Controls.Add(frm);
            frm.Show();
            MarcarActivo(activo);
        }

        private void CerrarHijo()
        {
            if (_hijo == null) return;
            panelContenido.Controls.Remove(_hijo);
            _hijo.Dispose();
            _hijo = null;
        }

        private void MostrarInicio()
        {
            CerrarHijo();
            tlpDashboard.Visible = true;
            CargarDashboard();
            MarcarActivo(itemInicio);
        }

        private void MarcarActivo(ItemNavegacion? activo)
        {
            foreach (var it in new[] { itemInicio, itemEstablecimientos, itemProvincias, itemClave })
                it.Activo = it == activo;
        }

        private void OrdenarSidebar()
        {
            int y = 24;
            logoMarca.Location = new Point(14, y);
            y += logoMarca.Height + 24;
            foreach (var it in new[] { itemInicio, itemEstablecimientos, itemProvincias, itemClave })
            {
                if (!it.Visible) continue;
                it.Location = new Point(14, y);
                y += it.Height + 6;
            }

            itemCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            itemCerrarSesion.Width = itemInicio.Width;
            itemCerrarSesion.BringToFront();
            PosicionarCerrarSesion();
        }

        private void PosicionarCerrarSesion()
        {
            itemCerrarSesion.Location = new Point(14, tarjetaSidebar.ClientSize.Height - itemCerrarSesion.Height - 24);
        }

        private void AbrirEstablecimientos(FiltroEstado filtro)
            => Mostrar(new FrmEstablecimientos(filtro), itemEstablecimientos);

        private void itemInicio_Click(object sender, EventArgs e) => MostrarInicio();
        private void mnuEstablecimientos_Click(object sender, EventArgs e) => AbrirEstablecimientos(FiltroEstado.Todos);
        private void kpiActivos_FlechaClick(object sender, EventArgs e) => AbrirEstablecimientos(FiltroEstado.Activos);
        private void kpiInactivos_FlechaClick(object sender, EventArgs e) => AbrirEstablecimientos(FiltroEstado.Inactivos);
        private void kpiTipos_FlechaClick(object sender, EventArgs e) => AbrirEstablecimientos(FiltroEstado.Activos);
        private void itemProvincias_Click(object sender, EventArgs e) => Mostrar(new FrmProvinciasUsuarios(), itemProvincias);

        private void mnuCambiarClave_Click(object sender, EventArgs e)
        {
            using var frm = new FrmCambiarClave();
            frm.ShowDialog(this);
        }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            CerrarSesionSolicitada = true;
            Close();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}