using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Enums;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
    {
        // Filtro de provincia para sentencias UPDATE (sin alias de tabla).
        private const string FiltroAlcance = "(@Todas = 1 OR ProvinciaId = @ProvinciaId)";

        // El hash de la clave solo se trae cuando hace falta (login).
        private static string Seleccion(bool conClave) => $@"
SELECT u.UsuarioId, u.NombreUsuario, {(conClave ? "u.ClaveHash," : "")} u.NombreCompleto, u.Rol,
       u.ProvinciaId, p.Nombre AS NombreProvincia,
       u.Activo, u.CreadoPor, u.FechaCreacion, u.ModificadoPor, u.FechaModificacion
FROM dbo.Usuarios u
LEFT JOIN dbo.Provincias p ON p.ProvinciaId = u.ProvinciaId";

        public Usuario? ObtenerParaLogin(string nombreUsuario)
            => EjecutarUno<Usuario>(
                Seleccion(true) + " WHERE u.NombreUsuario = @NombreUsuario;",
                r => Mapear(r, true),
                P("@NombreUsuario", nombreUsuario));

        public List<Usuario> Listar(AlcanceProvincia alcance, bool incluirInactivos)
            => EjecutarLista<Usuario>(
                Seleccion(false) +
                @" WHERE (@Todas = 1 OR u.ProvinciaId = @ProvinciaId)
                     AND (@Inactivos = 1 OR u.Activo = 1)
                   ORDER BY u.NombreUsuario;",
                r => Mapear(r, false),
                Unir(ParametrosAlcance(alcance), P("@Inactivos", incluirInactivos)));

        public Usuario? ObtenerPorId(int usuarioId, AlcanceProvincia alcance)
            => EjecutarUno<Usuario>(
                Seleccion(false) +
                " WHERE u.UsuarioId = @Id AND (@Todas = 1 OR u.ProvinciaId = @ProvinciaId);",
                r => Mapear(r, false),
                Unir(ParametrosAlcance(alcance), P("@Id", usuarioId)));

        public int Insertar(Usuario usuario, int usuarioId, AlcanceProvincia alcance)
        {
            // Defensa adicional: quien no es admin nacional solo puede crear usuarios de su provincia.
            if (!alcance.VerTodas &&
                (usuario.ProvinciaId == null || !alcance.Permite(usuario.ProvinciaId.Value)))
                throw new UnauthorizedAccessException("No puede crear usuarios fuera de su provincia.");

            return InsertarYObtenerId(
                @"INSERT INTO dbo.Usuarios (NombreUsuario, ClaveHash, NombreCompleto, Rol, ProvinciaId, CreadoPor)
                  VALUES (@NombreUsuario, @ClaveHash, @NombreCompleto, @Rol, @ProvinciaId, @Usuario);",
                P("@NombreUsuario", usuario.NombreUsuario),
                P("@ClaveHash", SqlDbType.VarBinary, usuario.ClaveHash),
                P("@NombreCompleto", usuario.NombreCompleto),
                P("@Rol", usuario.Rol.ToString()),
                P("@ProvinciaId", SqlDbType.Int, usuario.ProvinciaId),
                P("@Usuario", usuarioId));
        }

        public bool Actualizar(Usuario usuario, int usuarioId, AlcanceProvincia alcance)
            => EjecutarNoQuery(
                $@"UPDATE dbo.Usuarios
                   SET NombreCompleto = @NombreCompleto, Rol = @Rol, ProvinciaId = @NuevaProvincia,
                       ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                   WHERE UsuarioId = @Id AND {FiltroAlcance};",
                Unir(ParametrosAlcance(alcance),
                     P("@NombreCompleto", usuario.NombreCompleto),
                     P("@Rol", usuario.Rol.ToString()),
                     P("@NuevaProvincia", SqlDbType.Int, usuario.ProvinciaId),
                     P("@Usuario", usuarioId),
                     P("@Id", usuario.UsuarioId))) > 0;

        public bool CambiarClave(int usuarioObjetivoId, byte[] nuevoHash, int usuarioId, AlcanceProvincia alcance)
            => EjecutarNoQuery(
                $@"UPDATE dbo.Usuarios
                   SET ClaveHash = @Clave, ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                   WHERE UsuarioId = @Id AND {FiltroAlcance};",
                Unir(ParametrosAlcance(alcance),
                     P("@Clave", SqlDbType.VarBinary, nuevoHash),
                     P("@Usuario", usuarioId),
                     P("@Id", usuarioObjetivoId))) > 0;

        public bool CambiarEstado(int usuarioObjetivoId, bool activo, int usuarioId, AlcanceProvincia alcance)
            => EjecutarNoQuery(
                $@"UPDATE dbo.Usuarios
                   SET Activo = @Activo, ModificadoPor = @Usuario, FechaModificacion = SYSDATETIME()
                   WHERE UsuarioId = @Id AND Activo <> @Activo AND {FiltroAlcance};",
                Unir(ParametrosAlcance(alcance),
                     P("@Activo", activo),
                     P("@Usuario", usuarioId),
                     P("@Id", usuarioObjetivoId))) > 0;

        private static Usuario Mapear(SqlDataReader r, bool conClave)
        {
            Usuario u = new Usuario
            {
                UsuarioId = r.Entero("UsuarioId"),
                NombreUsuario = r.Texto("NombreUsuario"),
                ClaveHash = conClave ? r.Bytes("ClaveHash") : Array.Empty<byte>(),
                NombreCompleto = r.Texto("NombreCompleto"),
                Rol = Enum.Parse<RolUsuario>(r.Texto("Rol")),
                ProvinciaId = r.EnteroNulo("ProvinciaId"),
                NombreProvincia = r.TextoNulo("NombreProvincia")
            };
            r.LeerAuditoria(u);
            return u;
        }
    }
}
