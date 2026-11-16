using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Opción de la barra lateral: icono lineal + texto, con estado activo y hover.</summary>
    public class ItemNavegacion : Control
    {
        private bool _hover, _activo;
        private TipoIcono _icono;

        public ItemNavegacion()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Cursor = Cursors.Hand;
            Size = new Size(232, 46);
        }

        [DefaultValue(TipoIcono.Inicio)]
        public TipoIcono Icono { get => _icono; set { _icono = value; Invalidate(); } }

        [DefaultValue(false)]
        public bool Activo { get => _activo; set { _activo = value; Invalidate(); } }

        protected override void OnTextChanged(EventArgs e) { Invalidate(); base.OnTextChanged(e); }
        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { _hover = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }
        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                OnClick(EventArgs.Empty);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent?.BackColor ?? Color.White);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Dibujo.Preparar(g);
            var rect = new RectangleF(0, 0, Width - 1, Height - 1);

            if (Activo || _hover)
            {
                using var path = Dibujo.Redondo(rect, 12);
                using var br = new SolidBrush(Activo ? Tema.MentaClaro : Tema.Fondo);
                g.FillPath(br, path);
            }
            if (Focused)
            {
                using var path = Dibujo.Redondo(RectangleF.Inflate(rect, -1, -1), 11);
                using var pen = new Pen(Color.FromArgb(160, Tema.Cielo), 1.5F);
                g.DrawPath(pen, path);
            }

            Color color = Activo ? Tema.MentaOscuro : (_hover ? Tema.Texto : Tema.TextoSuave);
            Iconos.Dibujar(g, Icono, new RectangleF(18, (Height - 20) / 2F, 20, 20), color);
            Dibujo.Texto(g, Text, Tema.Fuente(10.5F, Activo), color,
                new RectangleF(52, 0, Width - 60, Height), StringAlignment.Near, StringAlignment.Center);
        }
    }
}
