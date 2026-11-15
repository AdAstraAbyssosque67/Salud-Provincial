using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    public class ProvinciaRepositorio : RepositorioBase, IProvinciaRepositorio
    {
        private const string Columnas =
            "ProvinciaId, Codigo, Nombre, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion";

        public List<Provincia> Listar(bool incluirInactivas)
            => EjecutarLista<Provincia>(
                $"SELECT {Columnas} FROM dbo.Provincias WHERE (@Inactivas = 1 OR Activo = 1) ORDER BY Nombre;",
                Mapear,
                P("@Inactivas", incluirInactivas));

        public Provincia? ObtenerPorId(int provinciaId)
            => EjecutarUno<Provincia>(
                $"SELECT {Columnas} FROM dbo.Provincias WHERE ProvinciaId = @Id;",
                Mapear,
                P("@Id", provinciaId));

        public int Insertar(Provincia provincia, int usuarioId)
            => InsertarYObtenerId(
                "INSERT INTO dbo.Provincias (Codigo, Nombre, CreadoPor) VALUES (@Codigo, @Nombre, @Usuario);",
                P("@Codigo", provincia.Codigo),
                P("@Nombre", provincia.Nombre),
                P("@Usuario", usuarioId));

        public bool Actualizar(Provincia provincia, int usuarioId)
            => EjecutarNoQuery(
                @"UPDATE dbo.Provincias
                  SET Codigo = @Codigo, Nombre = @Nombre,
                      ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                  WHERE ProvinciaId = @Id;",
                P("@Codigo", provincia.Codigo),
                P("@Nombre", provincia.Nombre),
                P("@Usuario", usuarioId),
                P("@Id", provincia.ProvinciaId)) > 0;

        public bool CambiarEstado(int provinciaId, bool activo, int usuarioId)
            => EjecutarNoQuery(
                @"UPDATE dbo.Provincias
                  SET Activo = @Activo, ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                  WHERE ProvinciaId = @Id AND Activo <> @Activo;",
                P("@Activo", activo),
                P("@Usuario", usuarioId),
                P("@Id", provinciaId)) > 0;

        private static Provincia Mapear(SqlDataReader r)
        {
            Provincia p = new Provincia
            {
                ProvinciaId = r.Entero("ProvinciaId"),
                Codigo = r.Texto("Codigo"),
                Nombre = r.Texto("Nombre")
            };
            r.LeerAuditoria(p);
            return p;
        }
    }
}
