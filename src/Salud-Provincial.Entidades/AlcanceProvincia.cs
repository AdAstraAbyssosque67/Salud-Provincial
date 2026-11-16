using System;

namespace Salud_Provincial.Entidades.Base
{
    // 
    // Define qué provincias puede ver o modificar quien hace la operación.
    // La capa de Negocio la construye a partir de la sesión y la pasa a los repositorios.
    // No tiene constructor público: hay que pedir explícitamente "Todas" o una provincia concreta,
    // así un null olvidado nunca se convierte en "ver todo".
    // 
    public sealed class AlcanceProvincia
    {
        public bool VerTodas { get; }
        public int? ProvinciaId { get; }

        private AlcanceProvincia(bool verTodas, int? provinciaId)
        {
            VerTodas = verTodas;
            ProvinciaId = provinciaId;
        }

        // Solo para el administrador nacional.
        public static AlcanceProvincia Todas() => new AlcanceProvincia(true, null);

        // Para usuarios provinciales: solo su provincia.
        public static AlcanceProvincia De(int provinciaId)
        {
            if (provinciaId <= 0)
                throw new ArgumentOutOfRangeException(nameof(provinciaId), "La provincia no es válida.");
            return new AlcanceProvincia(false, provinciaId);
        }

        // Indica si este alcance incluye a la provincia indicada.
        public bool Permite(int provinciaId) => VerTodas || ProvinciaId == provinciaId;
    }
}
