using System;
using System.Collections.Generic;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;

namespace Salud_Provincial.Negocio.Servicios
{
    public class AuditoriaServicio : ServicioBase, IAuditoriaServicio
    {
        private static readonly string[] TablasAuditadas =
            { "Provincias", "Tipos_Establecimientos", "Usuarios", "Establecimientos" };

        private readonly IAuditoriaRepositorio _auditoria;

        public AuditoriaServicio() : this(new AuditoriaRepositorio())
        {
        }

        public AuditoriaServicio(IAuditoriaRepositorio auditoria)
        {
            _auditoria = auditoria;
        }

        public List<AuditoriaRegistro> ConsultarCambios(DateTime? desde, DateTime? hasta, string? tabla, int maximo = 200)
        {
            ExigirSesion();

            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
                throw new NegocioException("La fecha inicial no puede ser posterior a la fecha final.");

            if (!string.IsNullOrWhiteSpace(tabla) && Array.IndexOf(TablasAuditadas, tabla) < 0)
                throw new NegocioException("La tabla indicada no es válida.");

            string? tablaFiltro = string.IsNullOrWhiteSpace(tabla) ? null : tabla;
            DateTime? desdeFiltro = desde?.Date;
            DateTime? hastaExclusivo = hasta?.Date.AddDays(1); // incluye el día final completo

            return Ejecutar(() => _auditoria.Listar(Alcance(), desdeFiltro, hastaExclusivo, tablaFiltro, LimitarMaximo(maximo)));
        }

        public List<AccesoRegistro> ConsultarAccesos(int maximo = 200)
        {
            ExigirAdminNacional();
            return Ejecutar(() => _auditoria.ListarAccesos(LimitarMaximo(maximo)));
        }

        private static int LimitarMaximo(int maximo) => Math.Clamp(maximo, 1, 1000);
    }
}
