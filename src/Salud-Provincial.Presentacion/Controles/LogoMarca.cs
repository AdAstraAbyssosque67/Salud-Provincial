using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Logotipo: cruz médica en un cuadrado redondeado + nombre de la aplicación.</summary>
    public class LogoMarca : Control
    {
        public LogoMarca()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            Size = new Size(210, 44);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Dibujo.Preparar(g);

            var caja = new RectangleF(3, 3, 38, 38);
            using (var path = Dibujo.Redondo(caja, 11))
            using (var pen = new Pen(Tema.Oscuro, 2F))
                g.DrawPath(pen, path);

            using (var pen = new Pen(Tema.Oscuro, 2.4F) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round })
            {
                g.DrawLine(pen, 22, 12, 22, 32);
                g.DrawLine(pen, 12, 22, 32, 22);
            }

            Dibujo.Texto(g, "Salud-Provincial", Tema.Fuente(13.5F, true), Tema.Texto,
                new RectangleF(52, 0, Width - 52, Height), StringAlignment.Near, StringAlignment.Center);
        }
    }
}
