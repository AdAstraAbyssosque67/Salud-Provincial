using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion
{
    /// <summary>Colores y estilos comunes (turquesa + naranja, fondo claro, diseño plano).</summary>
    internal static class Tema
    {
        public static readonly Color Turquesa = Color.FromArgb(31, 181, 197);
        public static readonly Color TurquesaOscuro = Color.FromArgb(18, 146, 159);
        public static readonly Color TurquesaClaro = Color.FromArgb(222, 245, 248);
        public static readonly Color Naranja = Color.FromArgb(245, 166, 35);
        public static readonly Color NaranjaOscuro = Color.FromArgb(224, 146, 26);
        public static readonly Color Fondo = Color.FromArgb(242, 245, 247);
        public static readonly Color Texto = Color.FromArgb(44, 62, 80);
        public static readonly Color TextoSuave = Color.FromArgb(127, 140, 141);
        public static readonly Color Peligro = Color.FromArgb(217, 83, 79);
        public static readonly Color PeligroClaro = Color.FromArgb(252, 235, 234);
        public static readonly Color Exito = Color.FromArgb(46, 158, 107);
        public static readonly Color Borde = Color.FromArgb(220, 226, 230);
        public static readonly Color FilaInactiva = Color.FromArgb(238, 241, 242);

        public static void Formulario(Form f)
        {
            f.BackColor = Fondo;
            f.ForeColor = Texto;
        }

        // ---------- Botones ----------
        /// <summary>Acción principal (naranja): Ingresar, Guardar, Gestionar...</summary>
        public static void BotonPrimario(Button b) => Estilo(b, Naranja, NaranjaOscuro, Color.White, Naranja);

        /// <summary>Acción positiva (turquesa): Reactivar.</summary>
        public static void BotonTurquesa(Button b) => Estilo(b, Turquesa, TurquesaOscuro, Color.White, Turquesa);

        /// <summary>Acción secundaria (contorno turquesa): Nuevo, Cancelar, Salir.</summary>
        public static void BotonSecundario(Button b) => Estilo(b, Color.White, TurquesaClaro, TurquesaOscuro, Turquesa);

        /// <summary>Acción delicada (contorno rojo): Desactivar.</summary>
        public static void BotonPeligro(Button b) => Estilo(b, Color.White, PeligroClaro, Peligro, Peligro);

        /// <summary>Botón sobre un encabezado turquesa.</summary>
        public static void BotonEncabezado(Button b) => Estilo(b, Turquesa, TurquesaOscuro, Color.White, Color.White);

        /// <summary>Opción de la barra lateral.</summary>
        public static void BotonLateral(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = TurquesaClaro;
            b.FlatAppearance.MouseDownBackColor = TurquesaClaro;
            b.BackColor = Color.White;
            b.ForeColor = Texto;
            b.Font = new Font("Segoe UI", 10.5F);
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Padding = new Padding(20, 0, 0, 0);
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        private static void Estilo(Button b, Color fondo, Color hover, Color texto, Color borde)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = borde;
            b.FlatAppearance.MouseOverBackColor = hover;
            b.FlatAppearance.MouseDownBackColor = hover;
            b.BackColor = fondo;
            b.ForeColor = texto;
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        // ---------- Tabla ----------
        public static void Tabla(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.GridColor = Borde;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.RowHeadersVisible = false;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 38;
            g.RowTemplate.Height = 34;

            var encabezado = g.ColumnHeadersDefaultCellStyle;
            encabezado.BackColor = Turquesa;
            encabezado.ForeColor = Color.White;
            encabezado.SelectionBackColor = Turquesa;
            encabezado.SelectionForeColor = Color.White;
            encabezado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            encabezado.Padding = new Padding(6, 0, 6, 0);

            var celda = g.DefaultCellStyle;
            celda.BackColor = Color.White;
            celda.ForeColor = Texto;
            celda.SelectionBackColor = Naranja;     // fila elegida, como el horario elegido de la referencia
            celda.SelectionForeColor = Color.White;
            celda.Padding = new Padding(6, 0, 6, 0);

            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 252, 253);
            g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Naranja;
            g.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }

        // ---------- Menú y barra de estado ----------
        public static void Menu(MenuStrip m)
        {
            m.Renderer = new ToolStripProfessionalRenderer(new Colores()) { RoundedEdges = false };
            m.BackColor = Color.White;
            m.ForeColor = Texto;
            m.Padding = new Padding(8, 4, 0, 4);
        }

        public static void BarraEstado(StatusStrip s)
        {
            s.Renderer = new ToolStripProfessionalRenderer(new Colores()) { RoundedEdges = false };
            s.BackColor = TurquesaOscuro;
            s.ForeColor = Color.White;
            foreach (ToolStripItem item in s.Items)
                item.ForeColor = Color.White;
        }

        private sealed class Colores : ProfessionalColorTable
        {
            public Colores() { UseSystemColors = false; }

            public override Color MenuStripGradientBegin => Color.White;
            public override Color MenuStripGradientEnd => Color.White;
            public override Color MenuItemSelected => TurquesaClaro;
            public override Color MenuItemSelectedGradientBegin => TurquesaClaro;
            public override Color MenuItemSelectedGradientEnd => TurquesaClaro;
            public override Color MenuItemPressedGradientBegin => TurquesaClaro;
            public override Color MenuItemPressedGradientMiddle => TurquesaClaro;
            public override Color MenuItemPressedGradientEnd => TurquesaClaro;
            public override Color MenuItemBorder => Turquesa;
            public override Color MenuBorder => Borde;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color SeparatorDark => Borde;
            public override Color SeparatorLight => Color.White;
            public override Color StatusStripGradientBegin => TurquesaOscuro;
            public override Color StatusStripGradientEnd => TurquesaOscuro;
        }
    }
}
