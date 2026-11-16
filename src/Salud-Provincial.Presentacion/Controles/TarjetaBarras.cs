using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    public class PuntoBarra
    {
        public string Etiqueta { get; set; } = "";
        public double Primario { get; set; }      // parte inferior (menta)
        public double Secundario { get; set; }    // parte superior (azul)
    }

    /// <summary>Gráfico de columnas apiladas con esquinas superiores redondeadas.</summary>
    public class TarjetaBarras : TarjetaBase
    {
        private string _titulo = "";
        private List<PuntoBarra> _datos = new();
        private readonly ToolTip _tip = new();
        private int _indiceHover = -1;

        [DefaultValue("")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [DefaultValue("Activos")] public string NombrePrimario { get; set; } = "Activos";
        [DefaultValue("Inactivos")] public string NombreSecundario { get; set; } = "Inactivos";

        public void Establecer(IEnumerable<PuntoBarra> datos)
        {
            _datos = datos.ToList();
            _indiceHover = -1;
            Invalidate();
        }

        private RectangleF AreaPlot()
        {
            var r = AreaTarjeta;
            return new RectangleF(r.X + 22 + 34, r.Y + 64, r.Width - 44 - 34, r.Height - 64 - 72);
        }

        private static (double paso, double maximo) Escala(double maxValor)
        {
            if (maxValor <= 0) return (1, 4);
            double crudo = maxValor / 4;
            double mag = Math.Pow(10, Math.Floor(Math.Log10(crudo)));
            double n = crudo / mag;
            double paso = (n <= 1 ? 1 : n <= 2 ? 2 : n <= 5 ? 5 : 10) * mag;
            if (paso < 1) paso = 1;
            return (paso, Math.Ceiling(maxValor / paso) * paso);
        }

        protected override void DibujarContenido(Graphics g, RectangleF r)
        {
            Dibujo.Texto(g, Titulo, Tema.Fuente(13F), Tema.Texto, new RectangleF(r.X + 22, r.Y + 18, r.Width - 44, 30));

            var plot = AreaPlot();
            if (plot.Width < 60 || plot.Height < 40) return;

            if (_datos.Count == 0)
            {
                Dibujo.Texto(g, "Sin datos para mostrar", Tema.Fuente(10.5F), Tema.TextoSuave,
                    plot, StringAlignment.Center, StringAlignment.Center);
                return;
            }

            double maxValor = _datos.Max(d => d.Primario + d.Secundario);
            var (paso, maximo) = Escala(maxValor);
            double k = plot.Height / maximo;

            // Rejilla y eje Y
            using (var penRejilla = new Pen(Tema.Borde, 1F) { DashStyle = DashStyle.Dash })
            {
                for (double v = 0; v <= maximo + 0.001; v += paso)
                {
                    float y = (float)(plot.Bottom - v * k);
                    g.DrawLine(penRejilla, plot.X, y, plot.Right, y);
                    Dibujo.Texto(g, v.ToString("0"), Tema.Fuente(9F), Color.FromArgb(160, 174, 192),
                        new RectangleF(r.X + 18, y - 10, 34, 20), StringAlignment.Far, StringAlignment.Center);
                }
            }

            // Columnas
            float anchoCat = plot.Width / _datos.Count;
            float anchoBarra = Math.Min(anchoCat * 0.55F, 60F);
            for (int i = 0; i < _datos.Count; i++)
            {
                var d = _datos[i];
                float x = plot.X + i * anchoCat + (anchoCat - anchoBarra) / 2F;
                float hA = (float)(d.Primario * k), hB = (float)(d.Secundario * k);

                if (hA > 0.5F)
                {
                    var rA = new RectangleF(x, plot.Bottom - hA, anchoBarra, hA);
                    RellenarBarra(g, rA, Tema.Menta, redondear: hB <= 0.5F);
                }
                if (hB > 0.5F)
                {
                    var rB = new RectangleF(x, plot.Bottom - hA - hB, anchoBarra, hB);
                    RellenarBarra(g, rB, Tema.Cielo, redondear: true);
                }

                Dibujo.Texto(g, d.Etiqueta, Tema.Fuente(8.5F), Color.FromArgb(160, 174, 192),
                    new RectangleF(plot.X + i * anchoCat, plot.Bottom + 8, anchoCat, 20),
                    StringAlignment.Center, StringAlignment.Near);
            }

            // Leyenda
            float ly = r.Bottom - 34;
            float w1 = Dibujo.Ancho(g, NombrePrimario, Tema.Fuente(10F));
            float w2 = Dibujo.Ancho(g, NombreSecundario, Tema.Fuente(10F));
            float total = 10 + 6 + w1 + 24 + 10 + 6 + w2;
            float lx = r.X + (r.Width - total) / 2F;
            using (var b = new SolidBrush(Tema.Menta)) g.FillEllipse(b, lx, ly + 7, 10, 10);
            Dibujo.Texto(g, NombrePrimario, Tema.Fuente(10F), Tema.TextoSuave, new RectangleF(lx + 16, ly, w1 + 6, 24), StringAlignment.Near, StringAlignment.Center);
            lx += 16 + w1 + 24;
            using (var b = new SolidBrush(Tema.Cielo)) g.FillEllipse(b, lx, ly + 7, 10, 10);
            Dibujo.Texto(g, NombreSecundario, Tema.Fuente(10F), Tema.TextoSuave, new RectangleF(lx + 16, ly, w2 + 6, 24), StringAlignment.Near, StringAlignment.Center);
        }

        private static void RellenarBarra(Graphics g, RectangleF rect, Color color, bool redondear)
        {
            using var br = new SolidBrush(color);
            if (redondear)
            {
                using var path = Dibujo.RedondoSuperior(rect, Math.Min(6F, rect.Height));
                g.FillPath(br, path);
            }
            else
            {
                g.FillRectangle(br, rect);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var plot = AreaPlot();
            int i = -1;
            if (_datos.Count > 0 && plot.Width > 0 && e.X >= plot.X && e.X <= plot.Right && e.Y >= plot.Y && e.Y <= plot.Bottom + 30)
                i = Math.Min(_datos.Count - 1, (int)((e.X - plot.X) / (plot.Width / _datos.Count)));

            if (i != _indiceHover)
            {
                _indiceHover = i;
                if (i < 0) _tip.SetToolTip(this, string.Empty);
                else
                {
                    var d = _datos[i];
                    _tip.SetToolTip(this, $"{d.Etiqueta}\n{NombrePrimario}: {d.Primario:0}\n{NombreSecundario}: {d.Secundario:0}");
                }
            }
            base.OnMouseMove(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _tip.Dispose();
            base.Dispose(disposing);
        }
    }
}
