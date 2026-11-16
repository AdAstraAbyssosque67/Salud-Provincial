using System.Collections.Generic;
using System.Linq;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Validaciones;

namespace Salud_Provincial.Negocio.Servicios
{
    public class ProvinciaServicio : ServicioBase, IProvinciaServicio
    {
        private const string MensajeDuplicada = "Ya existe una provincia con ese código o nombre.";

        private readonly IProvinciaRepositorio _provincias;

        public ProvinciaServicio() : this(new ProvinciaRepositorio())
        {
        }

        public ProvinciaServicio(IProvinciaRepositorio provincias)
        {
            _provincias = provincias;
        }

        public List<Provincia> Listar(bool incluirInactivas = false)
        {
            ExigirSesion();

            return Ejecutar(() =>
            {
                if (Sesion.EsAdminNacional)
                    return _provincias.Listar(incluirInactivas);

                // Un usuario provincial solo ve su propia provincia.
                int? propia = Sesion.ProvinciaId;
                return _provincias.Listar(false).Where(p => p.ProvinciaId == propia).ToList();
            });
        }

        public Provincia Crear(string codigo, string nombre)
        {
            ExigirAdminNacional();

            Provincia nueva = new Provincia
            {
                Codigo = Validador.Codigo(codigo),
                Nombre = Validador.TextoRequerido(nombre, "El nombre", 100)
            };

            return Ejecutar(() =>
            {
                nueva.ProvinciaId = _provincias.Insertar(nueva, Sesion.UsuarioId);
                return nueva;
            }, MensajeDuplicada);
        }

        public void Actualizar(int provinciaId, string codigo, string nombre)
        {
            ExigirAdminNacional();

            string codigoLimpio = Validador.Codigo(codigo);
            string nombreLimpio = Validador.TextoRequerido(nombre, "El nombre", 100);

            Ejecutar(() =>
            {
                Provincia provincia = _provincias.ObtenerPorId(provinciaId)
                    ?? throw new NegocioException("La provincia no existe.");

                provincia.Codigo = codigoLimpio;
                provincia.Nombre = nombreLimpio;

                if (!_provincias.Actualizar(provincia, Sesion.UsuarioId))
                    throw new NegocioException("No se pudo actualizar la provincia.");
            }, MensajeDuplicada);
        }

        public void Desactivar(int provinciaId) => CambiarEstado(provinciaId, false);

        public void Reactivar(int provinciaId) => CambiarEstado(provinciaId, true);

        private void CambiarEstado(int provinciaId, bool activo)
        {
            ExigirAdminNacional();

            Ejecutar(() =>
            {
                if (_provincias.ObtenerPorId(provinciaId) == null)
                    throw new NegocioException("La provincia no existe.");

                if (!_provincias.CambiarEstado(provinciaId, activo, Sesion.UsuarioId))
                    throw new NegocioException(activo ? "La provincia ya está activa." : "La provincia ya está desactivada.");
            });
        }
    }
}
