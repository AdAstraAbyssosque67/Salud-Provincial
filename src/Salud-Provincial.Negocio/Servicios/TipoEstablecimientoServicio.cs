using System.Collections.Generic;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Validaciones;

namespace Salud_Provincial.Negocio.Servicios
{
    public class TipoEstablecimientoServicio : ServicioBase, ITipoEstablecimientoServicio
    {
        private const string MensajeDuplicado = "Ya existe un tipo de establecimiento con ese nombre.";

        private readonly ITipoEstablecimientoRepositorio _tipos;

        public TipoEstablecimientoServicio() : this(new TipoEstablecimientoRepositorio())
        {
        }

        public TipoEstablecimientoServicio(ITipoEstablecimientoRepositorio tipos)
        {
            _tipos = tipos;
        }

        public List<TipoEstablecimiento> Listar(bool incluirInactivos = false)
        {
            ExigirSesion();

            // Los inactivos solo le sirven al administrador nacional (para reactivarlos).
            bool conInactivos = incluirInactivos && Sesion.EsAdminNacional;
            return Ejecutar(() => _tipos.Listar(conInactivos));
        }

        public TipoEstablecimiento Crear(string nombre, string? descripcion)
        {
            ExigirAdminNacional();

            TipoEstablecimiento nuevo = new TipoEstablecimiento
            {
                Nombre = Validador.TextoRequerido(nombre, "El nombre", 100),
                Descripcion = Validador.TextoOpcional(descripcion, "La descripción", 250)
            };

            return Ejecutar(() =>
            {
                nuevo.TipoEstablecimientoId = _tipos.Insertar(nuevo, Sesion.UsuarioId);
                return nuevo;
            }, MensajeDuplicado);
        }

        public void Actualizar(int tipoId, string nombre, string? descripcion)
        {
            ExigirAdminNacional();

            string nombreLimpio = Validador.TextoRequerido(nombre, "El nombre", 100);
            string? descripcionLimpia = Validador.TextoOpcional(descripcion, "La descripción", 250);

            Ejecutar(() =>
            {
                TipoEstablecimiento tipo = _tipos.ObtenerPorId(tipoId)
                    ?? throw new NegocioException("El tipo de establecimiento no existe.");

                tipo.Nombre = nombreLimpio;
                tipo.Descripcion = descripcionLimpia;

                if (!_tipos.Actualizar(tipo, Sesion.UsuarioId))
                    throw new NegocioException("No se pudo actualizar el tipo de establecimiento.");
            }, MensajeDuplicado);
        }

        public void Desactivar(int tipoId)
        {
            ExigirAdminNacional();

            Ejecutar(() =>
            {
                if (_tipos.ObtenerPorId(tipoId) == null)
                    throw new NegocioException("El tipo de establecimiento no existe.");

                int enUso = _tipos.ContarEstablecimientosActivos(tipoId);
                if (enUso > 0)
                    throw new NegocioException(
                        $"No se puede desactivar: hay {enUso} establecimiento(s) activo(s) de este tipo.");

                if (!_tipos.CambiarEstado(tipoId, false, Sesion.UsuarioId))
                    throw new NegocioException("El tipo de establecimiento ya está desactivado.");
            });
        }

        public void Reactivar(int tipoId)
        {
            ExigirAdminNacional();

            Ejecutar(() =>
            {
                if (_tipos.ObtenerPorId(tipoId) == null)
                    throw new NegocioException("El tipo de establecimiento no existe.");

                if (!_tipos.CambiarEstado(tipoId, true, Sesion.UsuarioId))
                    throw new NegocioException("El tipo de establecimiento ya está activo.");
            });
        }
    }
}
