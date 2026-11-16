using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Salud_Provincial.Presentacion
{
    /// <summary>Utilidades de dibujo GDI+ compartidas por los controles personalizados.</summary>
    internal static class Dibujo
    {
        public static void Preparar(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public static GraphicsPath Redondo(RectangleF r, float radio)
        {
            var p = new GraphicsPath();
            float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));
            if (d <= 0) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        /// <summary>Solo las esquinas superiores redondeadas (barras del gráfico).</summary>
        public static GraphicsPath RedondoSuperior(RectangleF r, float radio)
        {
            var p = new GraphicsPath();
            float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height * 2));
            if (d <= 0) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddLine(r.Right, r.Bottom, r.X, r.Bottom);
            p.CloseFigure();
            return p;
        }

        public static LinearGradientBrush Degradado(RectangleF r)
            => new LinearGradientBrush(r, Tema.Menta, Tema.Cielo, 35F);

        public static void Texto(Graphics g, string? s, Font f, Color c, RectangleF r,
            StringAlignment h = StringAlignment.Near, StringAlignment v = StringAlignment.Near, bool ajustar = false)
        {
            if (string.IsNullOrEmpty(s) || r.Width <= 0 || r.Height <= 0) return;
            using var sf = new StringFormat(StringFormat.GenericDefault)
            {
                Alignment = h,
                LineAlignment = v,
                Trimming = StringTrimming.EllipsisCharacter
            };
            if (!ajustar) sf.FormatFlags |= StringFormatFlags.NoWrap;
            using var b = new SolidBrush(c);
            g.DrawString(s, f, b, r, sf);
        }

        public static float Ancho(Graphics g, string? s, Font f)
            => string.IsNullOrEmpty(s) ? 0 : g.MeasureString(s, f).Width;
    }
}
