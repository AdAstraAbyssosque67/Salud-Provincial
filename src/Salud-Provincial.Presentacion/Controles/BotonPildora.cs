using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    // Botón plano con esquinas redondeadas (estilo píldora).
    public class BotonPildora : Button
    {
        private bool _hover, _presionado;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorFondo { get; set; } = Tema.Oscuro;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorHover { get; set; } = Color.FromArgb(26, 32, 44);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorTexto { get; set; } = Color.White;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorBorde { get; set; } = Color.Transparent;

        [DefaultValue(14)]
        public int Redondeo { get; set; } = 14;

        public BotonPildora()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            UseVisualStyleBackColor = false;
            Cursor = Cursors.Hand;
            Font = Tema.Fuente(10F, true);
        }

        public void Estilo(Color fondo, Color hover, Color texto, Color borde)
        {
            ColorFondo = fondo;
            ColorHover = hover;
            ColorTexto = texto;
            ColorBorde = borde;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; _presionado = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { _presionado = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { _presionado = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            ButtonRenderer.DrawParentBackground(g, ClientRectangle, this);
            Dibujo.Preparar(g);

            var rect = new RectangleF(0.5F, 0.5F, Width - 2F, Height - 2F);
            Color fondo = !Enabled ? Color.FromArgb(237, 242, 247) : (_hover || _presionado ? ColorHover : ColorFondo);
            Color texto = !Enabled ? Color.FromArgb(160, 174, 192) : ColorTexto;
            Color borde = !Enabled ? Color.FromArgb(226, 232, 240) : ColorBorde;

            using (var path = Dibujo.Redondo(rect, Math.Min(Redondeo, Height / 2F)))
            {
                using (var br = new SolidBrush(fondo)) g.FillPath(br, path);
                if (borde.A > 0)
                    using (var pen = new Pen(borde, 1.2F)) g.DrawPath(pen, path);

                if (Focused && Enabled)
                {
                    var foco = RectangleF.Inflate(rect, -3, -3);
                    using var pf = Dibujo.Redondo(foco, Math.Min(Redondeo, foco.Height / 2F));
                    using var pen = new Pen(Color.FromArgb(160, Tema.Cielo), 1.5F);
                    g.DrawPath(pen, pf);
                }
            }

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
        }
    }
}
