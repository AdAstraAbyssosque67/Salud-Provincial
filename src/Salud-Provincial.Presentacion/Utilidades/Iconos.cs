using System.Drawing;
using System.Drawing.Drawing2D;

namespace Salud_Provincial.Presentacion
{
    public enum TipoIcono { Inicio, Edificio, Candado, Salir, FlechaDiagonal }

    // Iconos lineales finos dibujados con GDI+ (sin dependencias externas).
    public static class Iconos
    {
        public static void Dibujar(Graphics g, TipoIcono tipo, RectangleF r, Color color, float grosor = 1.7F)
        {
            var estado = g.Save();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var pen = new Pen(color, grosor)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };
            PointF P(float x, float y) => new PointF(r.X + x * r.Width, r.Y + y * r.Height);

            switch (tipo)
            {
                case TipoIcono.Inicio:
                    g.DrawLines(pen, new[] { P(.1f, .48f), P(.5f, .12f), P(.9f, .48f) });
                    g.DrawLines(pen, new[] { P(.2f, .42f), P(.2f, .88f), P(.8f, .88f), P(.8f, .42f) });
                    g.DrawLines(pen, new[] { P(.42f, .88f), P(.42f, .62f), P(.58f, .62f), P(.58f, .88f) });
                    break;

                case TipoIcono.Edificio:
                    g.DrawRectangle(pen, r.X + .2f * r.Width, r.Y + .1f * r.Height, .6f * r.Width, .8f * r.Height);
                    g.DrawLine(pen, P(.38f, .3f), P(.62f, .3f));
                    g.DrawLine(pen, P(.38f, .48f), P(.62f, .48f));
                    g.DrawLine(pen, P(.5f, .9f), P(.5f, .68f));
                    break;

                case TipoIcono.Candado:
                    g.DrawRectangle(pen, r.X + .2f * r.Width, r.Y + .46f * r.Height, .6f * r.Width, .44f * r.Height);
                    g.DrawArc(pen, r.X + .3f * r.Width, r.Y + .08f * r.Height, .4f * r.Width, .76f * r.Height, 180, 180);
                    break;

                case TipoIcono.Salir:
                    g.DrawLines(pen, new[] { P(.45f, .15f), P(.15f, .15f), P(.15f, .85f), P(.45f, .85f) });
                    g.DrawLine(pen, P(.35f, .5f), P(.88f, .5f));
                    g.DrawLines(pen, new[] { P(.72f, .34f), P(.88f, .5f), P(.72f, .66f) });
                    break;

                case TipoIcono.FlechaDiagonal:
                    g.DrawLine(pen, P(.25f, .75f), P(.75f, .25f));
                    g.DrawLines(pen, new[] { P(.3f, .25f), P(.75f, .25f), P(.75f, .7f) });
                    break;
            }
            g.Restore(estado);
        }
    }
}
