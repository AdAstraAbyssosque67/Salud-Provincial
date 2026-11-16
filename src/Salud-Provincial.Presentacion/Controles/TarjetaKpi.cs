using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Tarjeta de indicador: título, cifra grande, detalle y botón circular con flecha.</summary>
    public class TarjetaKpi : TarjetaBase
    {
        private string _titulo = "", _valor = "", _detalle = "";
        private bool _hoverFlecha;

        public event EventHandler? FlechaClick;

        [DefaultValue("")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [DefaultValue("")] public string Valor { get => _valor; set { _valor = value; Invalidate(); } }
        [DefaultValue("")] public string Detalle { get => _detalle; set { _detalle = value; Invalidate(); } }

        private RectangleF RectFlecha()
        {
            var r = AreaTarjeta;
            return new RectangleF(r.Right - 22 - 34, r.Y + 16, 34, 34);
        }

        protected override void DibujarContenido(Graphics g, RectangleF r)
        {
            var colorTitulo = Gradiente ? Tema.Texto : Color.FromArgb(74, 85, 104);
            Dibujo.Texto(g, Titulo, Tema.Fuente(11F), colorTitulo,
                new RectangleF(r.X + 22, r.Y + 20, r.Width - 100, 26));

            // Botón circular con flecha diagonal
            var fr = RectFlecha();
            if (Gradiente)
            {
                using var b = new SolidBrush(_hoverFlecha ? Color.FromArgb(235, 255, 255) : Color.White);
                g.FillEllipse(b, fr);
            }
            else
            {
                if (_hoverFlecha)
                {
                    using var b = new SolidBrush(Tema.GrisHover);
                    g.FillEllipse(b, fr);
                }
                using var p = new Pen(Color.FromArgb(203, 213, 224), 1.2F);
                g.DrawEllipse(p, fr);
            }
            Iconos.Dibujar(g, TipoIcono.FlechaDiagonal, new RectangleF(fr.X + 9, fr.Y + 9, 16, 16), Tema.Texto, 1.8F);

            // Cifra + detalle
            var fv = Tema.Fuente(30F, true);
            float x = r.X + 22;
            Dibujo.Texto(g, Valor, fv, Tema.Texto,
                new RectangleF(x, r.Bottom - 80, r.Width - 44, 60), StringAlignment.Near, StringAlignment.Far);

            float w = Dibujo.Ancho(g, Valor, fv);
            var colorDetalle = Gradiente ? Color.FromArgb(45, 55, 72) : Tema.TextoSuave;
            Dibujo.Texto(g, Detalle, Tema.Fuente(9.5F), colorDetalle,
                new RectangleF(x + w + 2, r.Bottom - 80, Math.Max(0, r.Width - 44 - w - 2), 56),
                StringAlignment.Near, StringAlignment.Far);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool h = RectFlecha().Contains(e.Location);
            if (h != _hoverFlecha)
            {
                _hoverFlecha = h;
                Cursor = h ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_hoverFlecha) { _hoverFlecha = false; Cursor = Cursors.Default; Invalidate(); }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && RectFlecha().Contains(e.Location))
                FlechaClick?.Invoke(this, EventArgs.Empty);
            base.OnMouseClick(e);
        }
    }
}
