using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    public class TipoEstablecimientoRepositorio : RepositorioBase, ITipoEstablecimientoRepositorio
    {
        private const string Columnas =
            "TipoEstablecimientoId, Nombre, Descripcion, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion";

        public List<TipoEstablecimiento> Listar(bool incluirInactivos)
            => EjecutarLista<TipoEstablecimiento>(
                $"SELECT {Columnas} FROM dbo.Tipos_Establecimientos WHERE (@Inactivos = 1 OR Activo = 1) ORDER BY Nombre;",
                Mapear,
                P("@Inactivos", incluirInactivos));

        public TipoEstablecimiento? ObtenerPorId(int tipoId)
            => EjecutarUno<TipoEstablecimiento>(
                $"SELECT {Columnas} FROM dbo.Tipos_Establecimientos WHERE TipoEstablecimientoId = @Id;",
                Mapear,
                P("@Id", tipoId));

        public int Insertar(TipoEstablecimiento tipo, int usuarioId)
            => InsertarYObtenerId(
                "INSERT INTO dbo.Tipos_Establecimientos (Nombre, Descripcion, CreadoPor) VALUES (@Nombre, @Descripcion, @Usuario);",
                P("@Nombre", tipo.Nombre),
                P("@Descripcion", tipo.Descripcion),
                P("@Usuario", usuarioId));

        public bool Actualizar(TipoEstablecimiento tipo, int usuarioId)
            => EjecutarNoQuery(
                @"UPDATE dbo.Tipos_Establecimientos
                  SET Nombre = @Nombre, Descripcion = @Descripcion,
                      ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                  WHERE TipoEstablecimientoId = @Id;",
                P("@Nombre", tipo.Nombre),
                P("@Descripcion", tipo.Descripcion),
                P("@Usuario", usuarioId),
                P("@Id", tipo.TipoEstablecimientoId)) > 0;

        public bool CambiarEstado(int tipoId, bool activo, int usuarioId)
            => EjecutarNoQuery(
                @"UPDATE dbo.Tipos_Establecimientos
                  SET Activo = @Activo, ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                  WHERE TipoEstablecimientoId = @Id AND Activo <> @Activo;",
                P("@Activo", activo),
                P("@Usuario", usuarioId),
                P("@Id", tipoId)) > 0;

        public int ContarEstablecimientosActivos(int tipoId)
            => Convert.ToInt32(EjecutarEscalar(
                "SELECT COUNT(*) FROM dbo.Establecimientos WHERE TipoEstablecimientoId = @Id AND Activo = 1;",
                P("@Id", tipoId)));

        private static TipoEstablecimiento Mapear(SqlDataReader r)
        {
            TipoEstablecimiento t = new TipoEstablecimiento
            {
                TipoEstablecimientoId = r.Entero("TipoEstablecimientoId"),
                Nombre = r.Texto("Nombre"),
                Descripcion = r.TextoNulo("Descripcion")
            };
            r.LeerAuditoria(t);
            return t;
        }
    }
}
