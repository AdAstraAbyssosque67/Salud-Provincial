using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    /// <summary>
    /// Aquí vive el aislamiento por provincia (Patrón Pool): TODA consulta incluye el filtro
    /// (@Todas = 1 OR ProvinciaId = @ProvinciaId) según el AlcanceProvincia recibido.
    /// </summary>
    public class EstablecimientoRepositorio : RepositorioBase, IEstablecimientoRepositorio
    {
        private const string Seleccion = @"
SELECT e.EstablecimientoId, e.Nombre, e.Direccion, e.Telefono,
       e.TipoEstablecimientoId, t.Nombre AS NombreTipo,
       e.ProvinciaId, p.Nombre AS NombreProvincia,
       e.Activo, e.CreadoPor, e.FechaCreacion, e.ModificadoPor, e.FechaModificacion
FROM dbo.Establecimientos e
INNER JOIN dbo.Tipos_Establecimientos t ON t.TipoEstablecimientoId = e.TipoEstablecimientoId
INNER JOIN dbo.Provincias p ON p.ProvinciaId = e.ProvinciaId";

        // Filtro para SELECT (con alias e) y para UPDATE (sin alias).
        private const string FiltroSelect = "(@Todas = 1 OR e.ProvinciaId = @ProvinciaId)";
        private const string FiltroUpdate = "(@Todas = 1 OR ProvinciaId = @ProvinciaId)";

        public List<Establecimiento> Listar(AlcanceProvincia alcance, bool incluirInactivos)
            => EjecutarLista<Establecimiento>(
                $"{Seleccion} WHERE {FiltroSelect} AND (@Inactivos = 1 OR e.Activo = 1) ORDER BY p.Nombre, e.Nombre;",
                Mapear,
                Unir(ParametrosAlcance(alcance), P("@Inactivos", incluirInactivos)));

        public Establecimiento? ObtenerPorId(int establecimientoId, AlcanceProvincia alcance)
            => EjecutarUno<Establecimiento>(
                $"{Seleccion} WHERE e.EstablecimientoId = @Id AND {FiltroSelect};",
                Mapear,
                Unir(ParametrosAlcance(alcance), P("@Id", establecimientoId)));

        public int Insertar(Establecimiento establecimiento, int usuarioId, AlcanceProvincia alcance)
        {
            // Defensa adicional: aunque Negocio ya lo valida, el repositorio no inserta fuera del alcance.
            if (!alcance.Permite(establecimiento.ProvinciaId))
                throw new UnauthorizedAccessException("No puede registrar establecimientos en otra provincia.");

            return InsertarYObtenerId(
                @"INSERT INTO dbo.Establecimientos
                      (Nombre, Direccion, Telefono, TipoEstablecimientoId, ProvinciaId, CreadoPor)
                  VALUES (@Nombre, @Direccion, @Telefono, @Tipo, @Provincia, @Usuario);",
                P("@Nombre", establecimiento.Nombre),
                P("@Direccion", establecimiento.Direccion),
                P("@Telefono", establecimiento.Telefono),
                P("@Tipo", establecimiento.TipoEstablecimientoId),
                P("@Provincia", establecimiento.ProvinciaId),
                P("@Usuario", usuarioId));
        }

        public bool Actualizar(Establecimiento establecimiento, int usuarioId, AlcanceProvincia alcance)
            => EjecutarNoQuery(
                $@"UPDATE dbo.Establecimientos
                   SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono,
                       TipoEstablecimientoId = @Tipo,
                       ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                   WHERE EstablecimientoId = @Id AND {FiltroUpdate};",
                Unir(ParametrosAlcance(alcance),
                     P("@Nombre", establecimiento.Nombre),
                     P("@Direccion", establecimiento.Direccion),
                     P("@Telefono", establecimiento.Telefono),
                     P("@Tipo", establecimiento.TipoEstablecimientoId),
                     P("@Usuario", usuarioId),
                     P("@Id", establecimiento.EstablecimientoId))) > 0;

        public bool CambiarEstado(int establecimientoId, bool activo, int usuarioId, AlcanceProvincia alcance)
            => EjecutarNoQuery(
                $@"UPDATE dbo.Establecimientos
                   SET Activo = @Activo, ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                   WHERE EstablecimientoId = @Id AND Activo <> @Activo AND {FiltroUpdate};",
                Unir(ParametrosAlcance(alcance),
                     P("@Activo", activo),
                     P("@Usuario", usuarioId),
                     P("@Id", establecimientoId))) > 0;

        private static Establecimiento Mapear(SqlDataReader r)
        {
            Establecimiento e = new Establecimiento
            {
                EstablecimientoId = r.Entero("EstablecimientoId"),
                Nombre = r.Texto("Nombre"),
                Direccion = r.TextoNulo("Direccion"),
                Telefono = r.TextoNulo("Telefono"),
                TipoEstablecimientoId = r.Entero("TipoEstablecimientoId"),
                NombreTipo = r.Texto("NombreTipo"),
                ProvinciaId = r.Entero("ProvinciaId"),
                NombreProvincia = r.Texto("NombreProvincia")
            };
            r.LeerAuditoria(e);
            return e;
        }
    }
}
