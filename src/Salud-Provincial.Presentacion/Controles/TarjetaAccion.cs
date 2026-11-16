using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Tarjeta con título, texto descriptivo y botón píldora inferior.</summary>
    public class TarjetaAccion : TarjetaBase
    {
        private string _titulo = "", _texto = "", _textoBoton = "";
        private bool _hoverBoton;

        public event EventHandler? Accion;

        [DefaultValue("")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [DefaultValue("")] public string Texto { get => _texto; set { _texto = value; Invalidate(); } }
        [DefaultValue("")] public string TextoBoton { get => _textoBoton; set { _textoBoton = value; Invalidate(); } }

        private RectangleF RectBoton()
        {
            var r = AreaTarjeta;
            return new RectangleF(r.X + 22, r.Bottom - 22 - 40, r.Width - 44, 40);
        }

        protected override void DibujarContenido(Graphics g, RectangleF r)
        {
            Dibujo.Texto(g, Titulo, Tema.Fuente(15F, true), Tema.Texto, new RectangleF(r.X + 22, r.Y + 20, r.Width - 44, 30));

            var rb = RectBoton();
            float yTexto = r.Y + 56;
            Dibujo.Texto(g, Texto, Tema.Fuente(10F), Gradiente ? Color.FromArgb(45, 55, 72) : Tema.TextoSuave,
                new RectangleF(r.X + 22, yTexto, r.Width - 44, Math.Max(0, rb.Y - yTexto - 8)),
                StringAlignment.Near, StringAlignment.Near, true);

            Color fondo = Gradiente
                ? (_hoverBoton ? Color.FromArgb(235, 255, 255) : Color.White)
                : (_hoverBoton ? Color.FromArgb(26, 32, 44) : Tema.Oscuro);
            using (var path = Dibujo.Redondo(rb, rb.Height / 2F))
            using (var br = new SolidBrush(fondo))
                g.FillPath(br, path);

            Dibujo.Texto(g, TextoBoton, Tema.Fuente(10F, true), Gradiente ? Tema.Texto : Color.White,
                rb, StringAlignment.Center, StringAlignment.Center);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool h = RectBoton().Contains(e.Location);
            if (h != _hoverBoton)
            {
                _hoverBoton = h;
                Cursor = h ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_hoverBoton) { _hoverBoton = false; Cursor = Cursors.Default; Invalidate(); }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && RectBoton().Contains(e.Location))
                Accion?.Invoke(this, EventArgs.Empty);
            base.OnMouseClick(e);
        }
    }
}
