using System.Drawing;
using System.Windows.Forms;
using Salud_Provincial.Presentacion.Controles;

namespace Salud_Provincial.Presentacion
{
    // Paleta y estilos del sistema de diseño (estilo Bento: tarjetas, menta + azul cielo).
    internal static class Tema
    {
        public static readonly Color Menta = Color.FromArgb(79, 209, 197);          // #4FD1C5
        public static readonly Color Cielo = Color.FromArgb(66, 153, 225);          // #4299E1
        public static readonly Color MentaOscuro = Color.FromArgb(49, 151, 149);
        public static readonly Color MentaClaro = Color.FromArgb(230, 255, 250);
        public static readonly Color Oscuro = Color.FromArgb(45, 55, 72);           // #2D3748
        public static readonly Color Texto = Color.FromArgb(26, 32, 44);            // #1A202C
        public static readonly Color TextoSuave = Color.FromArgb(113, 128, 150);    // #718096
        public static readonly Color Fondo = Color.FromArgb(244, 246, 249);         // #F4F6F9
        public static readonly Color Borde = Color.FromArgb(226, 232, 240);
        public static readonly Color GrisHover = Color.FromArgb(237, 242, 247);
        public static readonly Color Peligro = Color.FromArgb(229, 62, 62);
        public static readonly Color PeligroClaro = Color.FromArgb(255, 245, 245);
        public static readonly Color Exito = Color.FromArgb(56, 161, 105);
        public static readonly Color FilaInactiva = Color.FromArgb(247, 250, 252);

        private static readonly Dictionary<(float, bool), Font> _fuentes = new();

        // Fuente en caché. negrita = Segoe UI Semibold.
        public static Font Fuente(float puntos, bool negrita = false)
        {
            var clave = (puntos, negrita);
            if (!_fuentes.TryGetValue(clave, out var f))
            {
                f = new Font(negrita ? "Segoe UI Semibold" : "Segoe UI", puntos, FontStyle.Regular, GraphicsUnit.Point);
                _fuentes[clave] = f;
            }
            return f;
        }

        public static void Formulario(Form f)
        {
            f.BackColor = Fondo;
            f.ForeColor = Texto;
        }

        // ---------- Botones ----------
        public static void BotonPrimario(BotonPildora b) => b.Estilo(Oscuro, Color.FromArgb(26, 32, 44), Color.White, Color.Transparent);
        public static void BotonSecundario(BotonPildora b) => b.Estilo(Color.White, GrisHover, Texto, Borde);
        public static void BotonPeligro(BotonPildora b) => b.Estilo(Color.White, PeligroClaro, Peligro, Color.FromArgb(254, 178, 178));
        public static void BotonAfirmativo(BotonPildora b) => b.Estilo(MentaOscuro, Color.FromArgb(44, 122, 123), Color.White, Color.Transparent);

        // ---------- Tabla ----------
        public static void Tabla(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.GridColor = GrisHover;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.RowHeadersVisible = false;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 42;
            g.RowTemplate.Height = 44;

            var cab = g.ColumnHeadersDefaultCellStyle;
            cab.BackColor = FilaInactiva;
            cab.ForeColor = TextoSuave;
            cab.SelectionBackColor = FilaInactiva;
            cab.SelectionForeColor = TextoSuave;
            cab.Font = Fuente(9.5F, true);
            cab.Padding = new Padding(12, 0, 8, 0);

            var celda = g.DefaultCellStyle;
            celda.BackColor = Color.White;
            celda.ForeColor = Texto;
            celda.SelectionBackColor = MentaClaro;
            celda.SelectionForeColor = Texto;
            celda.Font = Fuente(10F);
            celda.Padding = new Padding(12, 0, 8, 0);
        }

        // ---------- Menú y barra de estado (se funden con el lienzo) ----------
        public static void Menu(MenuStrip m)
        {
            m.Renderer = new ToolStripProfessionalRenderer(new Colores()) { RoundedEdges = false };
            m.BackColor = Fondo;
            m.ForeColor = Texto;
            m.Padding = new Padding(10, 4, 0, 2);
        }

        public static void BarraEstado(StatusStrip s)
        {
            s.Renderer = new ToolStripProfessionalRenderer(new Colores()) { RoundedEdges = false };
            s.BackColor = Fondo;
            s.ForeColor = TextoSuave;
            foreach (ToolStripItem item in s.Items)
                item.ForeColor = TextoSuave;
        }

        private sealed class Colores : ProfessionalColorTable
        {
            public Colores() { UseSystemColors = false; }

            public override Color MenuStripGradientBegin => Fondo;
            public override Color MenuStripGradientEnd => Fondo;
            public override Color MenuItemSelected => MentaClaro;
            public override Color MenuItemSelectedGradientBegin => MentaClaro;
            public override Color MenuItemSelectedGradientEnd => MentaClaro;
            public override Color MenuItemPressedGradientBegin => MentaClaro;
            public override Color MenuItemPressedGradientMiddle => MentaClaro;
            public override Color MenuItemPressedGradientEnd => MentaClaro;
            public override Color MenuItemBorder => Menta;
            public override Color MenuBorder => Borde;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color SeparatorDark => Borde;
            public override Color SeparatorLight => Color.White;
            public override Color StatusStripGradientBegin => Fondo;
            public override Color StatusStripGradientEnd => Fondo;
        }
    }
}
