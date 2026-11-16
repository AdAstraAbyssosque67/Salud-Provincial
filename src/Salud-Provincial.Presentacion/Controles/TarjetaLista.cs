using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Salud_Provincial.Presentacion.Controles
{
    public class ItemLista
    {
        public string Titulo { get; set; } = "";
        public string Subtitulo { get; set; } = "";
        public string Derecha { get; set; } = "";
        public string DerechaDetalle { get; set; } = "";
        public Color ColorDerecha { get; set; } = Color.Gray;
        public bool Atenuado { get; set; }
    }

    // Lista vertical con avatar de iniciales, dos líneas de texto y estado a la derecha.
    public class TarjetaLista : TarjetaBase
    {
        private string _titulo = "";
        private string _vacio = "No hay registros para mostrar.";
        private List<ItemLista> _items = new();
        private int _total;

        [DefaultValue("")] public string Titulo { get => _titulo; set { _titulo = value; Invalidate(); } }
        [DefaultValue("No hay registros para mostrar.")] public string TextoVacio { get => _vacio; set { _vacio = value; Invalidate(); } }

        public void Establecer(IList<ItemLista> items, int totalReal = -1)
        {
            _items = items.ToList();
            _total = totalReal < 0 ? _items.Count : totalReal;
            Invalidate();
        }

        private static string Iniciales(string s)
        {
            var partes = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length == 0) return "?";
            return partes.Length == 1
                ? partes[0].Substring(0, 1).ToUpper()
                : (partes[0].Substring(0, 1) + partes[1].Substring(0, 1)).ToUpper();
        }

        protected override void DibujarContenido(Graphics g, RectangleF r)
        {
            Dibujo.Texto(g, Titulo, Tema.Fuente(15F, true), Tema.Texto, new RectangleF(r.X + 22, r.Y + 18, r.Width - 44, 32));

            float y0 = r.Y + 66, filaAlto = 58;
            if (_items.Count == 0)
            {
                Dibujo.Texto(g, TextoVacio, Tema.Fuente(10.5F), Tema.TextoSuave,
                    new RectangleF(r.X + 22, y0, r.Width - 44, 60), StringAlignment.Center, StringAlignment.Near, true);
                return;
            }

            int maxFilas = (int)Math.Floor((r.Bottom - 16 - y0) / filaAlto);
            bool recortado = _total > Math.Min(_items.Count, maxFilas);
            if (recortado) maxFilas = Math.Max(1, maxFilas - 1);
            int n = Math.Min(_items.Count, Math.Max(0, maxFilas));

            float anchoDer = 112;
            for (int i = 0; i < n; i++)
            {
                var it = _items[i];
                float y = y0 + i * filaAlto;

                // avatar
                var av = new RectangleF(r.X + 22, y + 6, 42, 42);
                if (it.Atenuado)
                {
                    using var b = new SolidBrush(Tema.GrisHover);
                    g.FillEllipse(b, av);
                }
                else
                {
                    using var b = new LinearGradientBrush(av, Tema.Menta, Tema.Cielo, 45F);
                    g.FillEllipse(b, av);
                }
                Dibujo.Texto(g, Iniciales(it.Titulo), Tema.Fuente(10F, true),
                    it.Atenuado ? Tema.TextoSuave : Color.White, av, StringAlignment.Center, StringAlignment.Center);

                float xt = av.Right + 12;
                float anchoTxt = r.Right - 22 - anchoDer - xt;
                var colorTit = it.Atenuado ? Tema.TextoSuave : Tema.Texto;
                Dibujo.Texto(g, it.Titulo, Tema.Fuente(11F), colorTit, new RectangleF(xt, y + 6, anchoTxt, 22));
                Dibujo.Texto(g, it.Subtitulo, Tema.Fuente(9.5F), Color.FromArgb(160, 174, 192), new RectangleF(xt, y + 28, anchoTxt, 20));

                var rd = new RectangleF(r.Right - 22 - anchoDer, y + 6, anchoDer, 22);
                Dibujo.Texto(g, it.Derecha, Tema.Fuente(10.5F), it.ColorDerecha, rd, StringAlignment.Far, StringAlignment.Near);
                Dibujo.Texto(g, it.DerechaDetalle, Tema.Fuente(9.5F), Color.FromArgb(160, 174, 192),
                    new RectangleF(rd.X, y + 28, rd.Width, 20), StringAlignment.Far, StringAlignment.Near);
            }

            if (recortado)
            {
                int resto = _total - n;
                Dibujo.Texto(g, $"y {resto} más en Establecimientos…", Tema.Fuente(9.5F), Tema.TextoSuave,
                    new RectangleF(r.X + 22, y0 + n * filaAlto + 2, r.Width - 44, 22), StringAlignment.Near, StringAlignment.Center);
            }
        }
    }
}
