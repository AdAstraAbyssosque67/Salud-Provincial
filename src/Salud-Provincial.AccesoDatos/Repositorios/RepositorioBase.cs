using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Salud_Provincial.AccesoDatos.Ayudantes;
using Salud_Provincial.AccesoDatos.Conexion;
using Salud_Provincial.Entidades.Base;

namespace Salud_Provincial.AccesoDatos.Repositorios
{
    // 
    // Código común de todos los repositorios: abre la conexión, ejecuta con parámetros,
    // la cierra siempre (using) y traduce los errores de SQL Server.
    // El SQL de los repositorios solo usa constantes y parámetros: nunca se concatenan valores del usuario.
    // 
    public abstract class RepositorioBase
    {
        // ----- Parámetros -----
        protected static SqlParameter P(string nombre, object? valor)
            => new SqlParameter(nombre, valor ?? DBNull.Value);

        // Parámetro con tipo explícito (necesario cuando el valor puede ser NULL).
        protected static SqlParameter P(string nombre, SqlDbType tipo, object? valor)
            => new SqlParameter(nombre, tipo) { Value = valor ?? DBNull.Value };

        // Los dos parámetros que usa el filtro (@Todas = 1 OR ProvinciaId = @ProvinciaId).
        protected static SqlParameter[] ParametrosAlcance(AlcanceProvincia alcance) => new[]
        {
            P("@Todas", alcance.VerTodas),
            P("@ProvinciaId", SqlDbType.Int, alcance.ProvinciaId)
        };

        protected static SqlParameter[] Unir(SqlParameter[] basicos, params SqlParameter[] otros)
            => basicos.Concat(otros).ToArray();

        // ----- Ejecución -----
        protected int EjecutarNoQuery(string sql, params SqlParameter[] parametros)
        {
            try
            {
                using SqlConnection cn = ConexionBD.Instancia.CrearConexion();
                using SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddRange(parametros);
                cn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw SqlErrores.Traducir(ex);
            }
        }

        protected object? EjecutarEscalar(string sql, params SqlParameter[] parametros)
        {
            try
            {
                using SqlConnection cn = ConexionBD.Instancia.CrearConexion();
                using SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddRange(parametros);
                cn.Open();
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw SqlErrores.Traducir(ex);
            }
        }

        // 
        // INSERT + id generado. No se usa OUTPUT INSERTED porque SQL Server no lo permite en
        // tablas con triggers (y todas las tablas de SaludDB tienen triggers de auditoría).
        // SCOPE_IDENTITY solo ve el identity de esta sentencia, no el de la tabla Auditoria.
        // 
        protected int InsertarYObtenerId(string sqlInsert, params SqlParameter[] parametros)
        {
            object? id = EjecutarEscalar(sqlInsert + " SELECT CAST(SCOPE_IDENTITY() AS INT);", parametros);
            return Convert.ToInt32(id);
        }

        protected List<T> EjecutarLista<T>(string sql, Func<SqlDataReader, T> mapear, params SqlParameter[] parametros)
        {
            try
            {
                List<T> lista = new List<T>();
                using SqlConnection cn = ConexionBD.Instancia.CrearConexion();
                using SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddRange(parametros);
                cn.Open();
                using SqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                    lista.Add(mapear(lector));
                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrores.Traducir(ex);
            }
        }

        protected T? EjecutarUno<T>(string sql, Func<SqlDataReader, T> mapear, params SqlParameter[] parametros)
            where T : class
            => EjecutarLista(sql, mapear, parametros).FirstOrDefault();
    }
}
