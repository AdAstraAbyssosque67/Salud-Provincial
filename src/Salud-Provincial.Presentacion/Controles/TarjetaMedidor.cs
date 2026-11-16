using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Medidor semicircular (0-100 %) con degradado azul → menta.</summary>
    public class TarjetaMedidor : TarjetaBase
    {
        private string _titulo = "", _detalle = "";
        private double _porcentaje;

        [DefaultValue("")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [DefaultValue("")] public string Detalle { get => _detalle; set { _detalle = value; Invalidate(); } }
        [DefaultValue(0.0)]
        public double Porcentaje
        {
            get => _porcentaje;
            set { _porcentaje = Math.Max(0, Math.Min(100, value)); Invalidate(); }
        }

        protected override void DibujarContenido(Graphics g, RectangleF r)
        {
            Dibujo.Texto(g, Titulo, Tema.Fuente(13F), Tema.Texto,
                new RectangleF(r.X + 22, r.Y + 18, r.Width - 44, 30));

            float alto = r.Height - 64 - 52;                       // alto disponible para el semicírculo
            float d = Math.Min(r.Width - 70, alto * 2);
            if (d < 60) return;

            float cx = r.X + r.Width / 2F;
            float baseY = r.Y + 64 + alto;                         // línea base del semicírculo
            float grosor = d * 0.2F;
            var caja = new RectangleF(cx - d / 2F + grosor / 2F, baseY - d / 2F + grosor / 2F, d - grosor, d - grosor);

            using (var pista = new Pen(Tema.GrisHover, grosor))
                g.DrawArc(pista, caja, 180, 180);

            float barrido = (float)(180 * Porcentaje / 100.0);
            if (barrido > 0.5F)
            {
                var limites = new RectangleF(cx - d / 2F, baseY - d / 2F, d, d / 2F);
                using var br = new LinearGradientBrush(limites, Tema.Cielo, Tema.Menta, LinearGradientMode.Horizontal);
                using var pen = new Pen(br, grosor);
                g.DrawArc(pen, caja, 180, barrido);
            }

            Dibujo.Texto(g, $"{Porcentaje:0}%", Tema.Fuente(28F, true), Tema.Texto,
                new RectangleF(cx - d / 4F, baseY - 52, d / 2F, 52), StringAlignment.Center, StringAlignment.Far);
            Dibujo.Texto(g, Detalle, Tema.Fuente(10F), Tema.TextoSuave,
                new RectangleF(r.X + 10, baseY + 10, r.Width - 20, 24), StringAlignment.Center, StringAlignment.Near);
        }
    }
}
