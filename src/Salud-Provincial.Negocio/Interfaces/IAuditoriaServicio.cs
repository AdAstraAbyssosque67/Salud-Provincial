using System;
using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    public interface IAuditoriaServicio
    {
        /// <summary>
        /// Bitácora de cambios. Cada provincia ve solo la suya; el administrador nacional, todas.
        /// </summary>
        /// <param name="hasta">Fecha final INCLUIDA (el servicio suma el día para cubrirlo completo).</param>
        /// <param name="tabla">Opcional: Provincias, Tipos_Establecimientos, Usuarios o Establecimientos.</param>
        List<AuditoriaRegistro> ConsultarCambios(DateTime? desde, DateTime? hasta, string? tabla, int maximo = 200);

        /// <summary>Bitácora de inicios de sesión. Solo administrador nacional.</summary>
        List<AccesoRegistro> ConsultarAccesos(int maximo = 200);
    }
}
