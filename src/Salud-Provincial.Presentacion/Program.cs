namespace Salud_Provincial.Presentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetDefaultFont(new Font("Segoe UI", 10F));

            // Ciclo: login -> principal -> (si cerró sesión) login otra vez.
            while (true)
            {
                using (var login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        break;
                }

                using var principal = new FrmPrincipal();
                Application.Run(principal);

                if (!principal.CerrarSesionSolicitada)
                    break;
            }
        }
    }
}
