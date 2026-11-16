using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Salud_Provincial.Presentacion.Controles
{
    /// <summary>Panel con el degradado diagonal menta → azul cielo.</summary>
    public class PanelDegradado : Panel
    {
        public PanelDegradado()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0) return;
            using var br = new LinearGradientBrush(ClientRectangle, Tema.Menta, Tema.Cielo, 55F);
            e.Graphics.FillRectangle(br, ClientRectangle);
        }
    }
}
