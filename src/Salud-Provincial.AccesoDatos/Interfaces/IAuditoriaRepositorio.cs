using System;
using System.Collections.Generic;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Interfaces
{
    public interface IAuditoriaRepositorio
    {
        /// <param name="hastaExclusivo">Límite superior excluyente (para incluir un día completo, enviar el día siguiente).</param>
        List<AuditoriaRegistro> Listar(AlcanceProvincia alcance, DateTime? desde, DateTime? hastaExclusivo,
                                       string? tabla, int maximo = 200);

        /// <summary>Registra un intento de inicio de sesión (exitoso o fallido).</summary>
        void RegistrarAcceso(AccesoRegistro acceso);

        List<AccesoRegistro> ListarAccesos(int maximo = 200);
    }
}
