using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    // 
    // Solo lectura de la bitácora de cambios (la llenan los triggers) y escritura de la
    // bitácora de accesos. Nunca modifica ni borra: las tablas son de solo inserción.
    // 
    public class AuditoriaRepositorio : RepositorioBase, IAuditoriaRepositorio
    {
        public List<AuditoriaRegistro> Listar(AlcanceProvincia alcance, DateTime? desde, DateTime? hastaExclusivo,
                                              string? tabla, int maximo = 200)
            => EjecutarLista<AuditoriaRegistro>(
                @"SELECT TOP (@Maximo)
                         a.AuditoriaId, a.FechaHora, a.Tabla, a.RegistroId, a.Accion,
                         a.UsuarioId, u.NombreUsuario, a.ProvinciaId,
                         a.ValoresAnteriores, a.ValoresNuevos, a.Equipo
                  FROM dbo.Auditoria a
                  LEFT JOIN dbo.Usuarios u ON u.UsuarioId = a.UsuarioId
                  WHERE (@Todas = 1 OR a.ProvinciaId = @ProvinciaId)
                    AND (@Desde IS NULL OR a.FechaHora >= @Desde)
                    AND (@Hasta IS NULL OR a.FechaHora < @Hasta)
                    AND (@Tabla IS NULL OR a.Tabla = @Tabla)
                  ORDER BY a.AuditoriaId DESC;",
                MapearAuditoria,
                Unir(ParametrosAlcance(alcance),
                     P("@Maximo", maximo),
                     P("@Desde", SqlDbType.DateTime2, desde),
                     P("@Hasta", SqlDbType.DateTime2, hastaExclusivo),
                     P("@Tabla", SqlDbType.NVarChar, tabla)));

        public void RegistrarAcceso(AccesoRegistro acceso)
            => EjecutarNoQuery(
                @"INSERT INTO dbo.BitacoraAccesos (NombreUsuario, UsuarioId, Exitoso, Motivo)
                  VALUES (@NombreUsuario, @UsuarioId, @Exitoso, @Motivo);",
                P("@NombreUsuario", acceso.NombreUsuario),
                P("@UsuarioId", SqlDbType.Int, acceso.UsuarioId),
                P("@Exitoso", acceso.Exitoso),
                P("@Motivo", SqlDbType.NVarChar, acceso.Motivo));

        public List<AccesoRegistro> ListarAccesos(int maximo = 200)
            => EjecutarLista<AccesoRegistro>(
                @"SELECT TOP (@Maximo) AccesoId, FechaHora, NombreUsuario, UsuarioId, Exitoso, Motivo, Equipo
                  FROM dbo.BitacoraAccesos
                  ORDER BY AccesoId DESC;",
                MapearAcceso,
                P("@Maximo", maximo));

        private static AuditoriaRegistro MapearAuditoria(SqlDataReader r) => new AuditoriaRegistro
        {
            AuditoriaId = r.Largo("AuditoriaId"),
            FechaHora = r.Fecha("FechaHora"),
            Tabla = r.Texto("Tabla"),
            RegistroId = r.Entero("RegistroId"),
            Accion = r.Texto("Accion"),
            UsuarioId = r.EnteroNulo("UsuarioId"),
            NombreUsuario = r.TextoNulo("NombreUsuario"),
            ProvinciaId = r.EnteroNulo("ProvinciaId"),
            ValoresAnteriores = r.TextoNulo("ValoresAnteriores"),
            ValoresNuevos = r.TextoNulo("ValoresNuevos"),
            Equipo = r.TextoNulo("Equipo")
        };

        private static AccesoRegistro MapearAcceso(SqlDataReader r) => new AccesoRegistro
        {
            AccesoId = r.Largo("AccesoId"),
            FechaHora = r.Fecha("FechaHora"),
            NombreUsuario = r.Texto("NombreUsuario"),
            UsuarioId = r.EnteroNulo("UsuarioId"),
            Exitoso = r.Booleano("Exitoso"),
            Motivo = r.TextoNulo("Motivo"),
            Equipo = r.TextoNulo("Equipo")
        };
    }
}
