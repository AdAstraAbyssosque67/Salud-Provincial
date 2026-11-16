using Salud_Provincial.Negocio.Excepciones;

namespace Salud_Provincial.Presentacion
{
    /// <summary>Muestra errores al usuario sin exponer detalles técnicos.</summary>
    internal static class ManejadorErrores
    {
        private const string Titulo = "Salud-Provincial";

        public static void Mostrar(Exception ex, IWin32Window? propietario = null)
        {
            if (ex is NegocioException) // incluye PermisoDenegadoException
                MessageBox.Show(propietario, ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show(propietario, "Ocurrió un error inesperado.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
