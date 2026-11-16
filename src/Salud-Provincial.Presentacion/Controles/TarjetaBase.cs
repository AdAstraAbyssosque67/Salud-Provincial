using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    // Tarjeta Bento: fondo blanco (o degradado), esquinas de 20 px y sombra muy suave.
    // Puede contener otros controles.
    public class TarjetaBase : Panel
    {
        private bool _gradiente;

        public TarjetaBase()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;   // lo heredan los controles hijos
            Padding = new Padding(24);
        }

        [DefaultValue(20)]
        public int Radio { get; set; } = 20;

        [DefaultValue(false)]
        public bool Gradiente
        {
            get => _gradiente;
            set { _gradiente = value; Invalidate(); }
        }

        // Rectángulo de la tarjeta (deja margen para la sombra).
        protected RectangleF AreaTarjeta => new RectangleF(4, 2, Math.Max(0, Width - 9), Math.Max(0, Height - 9));

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Las esquinas redondeadas muestran el color del contenedor.
            e.Graphics.Clear(Parent?.BackColor ?? Tema.Fondo);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Dibujo.Preparar(g);
            var r = AreaTarjeta;

            if (r.Width >= 10 && r.Height >= 10)
            {
                for (int i = 4; i >= 1; i--)
                {
                    var rs = new RectangleF(r.X - i * 0.8F, r.Y - i * 0.8F + 2, r.Width + i * 1.6F, r.Height + i * 1.6F);
                    using var sp = Dibujo.Redondo(rs, Radio + i * 0.8F);
                    using var sb = new SolidBrush(Color.FromArgb(6, 0, 0, 0));
                    g.FillPath(sb, sp);
                }

                using (var path = Dibujo.Redondo(r, Radio))
                {
                    if (Gradiente)
                    {
                        using var br = Dibujo.Degradado(r);
                        g.FillPath(br, path);
                    }
                    else
                    {
                        using var br = new SolidBrush(Color.White);
                        g.FillPath(br, path);
                    }
                }

                DibujarContenido(g, r);
            }
            base.OnPaint(e);
        }

        protected virtual void DibujarContenido(Graphics g, RectangleF area) { }
    }
}
