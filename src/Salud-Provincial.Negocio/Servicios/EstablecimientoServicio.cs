using System.Collections.Generic;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Validaciones;

namespace Salud_Provincial.Negocio.Servicios
{
    /// <summary>
    /// Aquí se decide el aislamiento por provincia: el alcance sale SIEMPRE de la sesión
    /// (Alcance()), nunca de un valor que envíe el formulario.
    /// </summary>
    public class EstablecimientoServicio : ServicioBase, IEstablecimientoServicio
    {
        private const string MensajeDuplicado = "Ya existe un establecimiento con ese nombre en la provincia.";
        private const string MensajeNoEncontrado = "El establecimiento no existe o no pertenece a su provincia.";

        private readonly IEstablecimientoRepositorio _establecimientos;
        private readonly ITipoEstablecimientoRepositorio _tipos;
        private readonly IProvinciaRepositorio _provincias;

        public EstablecimientoServicio()
            : this(new EstablecimientoRepositorio(), new TipoEstablecimientoRepositorio(), new ProvinciaRepositorio())
        {
        }

        public EstablecimientoServicio(IEstablecimientoRepositorio establecimientos,
                                       ITipoEstablecimientoRepositorio tipos,
                                       IProvinciaRepositorio provincias)
        {
            _establecimientos = establecimientos;
            _tipos = tipos;
            _provincias = provincias;
        }

        public List<Establecimiento> Listar(bool incluirInactivos = false)
        {
            AlcanceProvincia alcance = Alcance();
            return Ejecutar(() => _establecimientos.Listar(alcance, incluirInactivos));
        }

        public Establecimiento? ObtenerPorId(int establecimientoId)
        {
            AlcanceProvincia alcance = Alcance();
            return Ejecutar(() => _establecimientos.ObtenerPorId(establecimientoId, alcance));
        }

        public Establecimiento Crear(Establecimiento datos)
        {
            AlcanceProvincia alcance = Alcance();
            Establecimiento nuevo = Normalizar(datos);

            if (Sesion.EsAdminNacional)
            {
                if (nuevo.ProvinciaId <= 0)
                    throw new NegocioException("Seleccione la provincia del establecimiento.");
            }
            else
            {
                // Un usuario provincial siempre registra en SU provincia, diga lo que diga el formulario.
                nuevo.ProvinciaId = alcance.ProvinciaId!.Value;
            }

            return Ejecutar(() =>
            {
                Provincia? provincia = _provincias.ObtenerPorId(nuevo.ProvinciaId);
                if (provincia == null || !provincia.Activo)
                    throw new NegocioException("La provincia seleccionada no existe o está desactivada.");

                ValidarTipo(nuevo.TipoEstablecimientoId);

                nuevo.EstablecimientoId = _establecimientos.Insertar(nuevo, Sesion.UsuarioId, alcance);
                return nuevo;
            }, MensajeDuplicado);
        }

        public void Actualizar(Establecimiento datos)
        {
            AlcanceProvincia alcance = Alcance();

            if (datos.EstablecimientoId <= 0)
                throw new NegocioException("Seleccione el establecimiento que desea modificar.");

            Establecimiento cambios = Normalizar(datos);

            Ejecutar(() =>
            {
                Establecimiento existente = _establecimientos.ObtenerPorId(datos.EstablecimientoId, alcance)
                    ?? throw new NegocioException(MensajeNoEncontrado);

                if (!existente.Activo)
                    throw new NegocioException("El establecimiento está desactivado. Reactívelo antes de modificarlo.");

                ValidarTipo(cambios.TipoEstablecimientoId);

                existente.Nombre = cambios.Nombre;
                existente.Direccion = cambios.Direccion;
                existente.Telefono = cambios.Telefono;
                existente.TipoEstablecimientoId = cambios.TipoEstablecimientoId;
                // La provincia NO se toca: un establecimiento no cambia de provincia.

                if (!_establecimientos.Actualizar(existente, Sesion.UsuarioId, alcance))
                    throw new NegocioException(MensajeNoEncontrado);
            }, MensajeDuplicado);
        }

        public void Desactivar(int establecimientoId) => CambiarEstado(establecimientoId, false);

        public void Reactivar(int establecimientoId) => CambiarEstado(establecimientoId, true);

        private void CambiarEstado(int establecimientoId, bool activo)
        {
            AlcanceProvincia alcance = Alcance();

            Ejecutar(() =>
            {
                if (_establecimientos.ObtenerPorId(establecimientoId, alcance) == null)
                    throw new NegocioException(MensajeNoEncontrado);

                if (!_establecimientos.CambiarEstado(establecimientoId, activo, Sesion.UsuarioId, alcance))
                    throw new NegocioException(activo ? "El establecimiento ya está activo." : "El establecimiento ya está desactivado.");
            });
        }

        private void ValidarTipo(int tipoId)
        {
            TipoEstablecimiento? tipo = _tipos.ObtenerPorId(tipoId);
            if (tipo == null || !tipo.Activo)
                throw new NegocioException("El tipo de establecimiento no existe o está desactivado.");
        }

        /// <summary>Valida y limpia los datos recibidos; devuelve un objeto nuevo.</summary>
        private static Establecimiento Normalizar(Establecimiento datos)
        {
            if (datos.TipoEstablecimientoId <= 0)
                throw new NegocioException("Seleccione el tipo de establecimiento.");

            return new Establecimiento
            {
                EstablecimientoId = datos.EstablecimientoId,
                Nombre = Validador.TextoRequerido(datos.Nombre, "El nombre", 150),
                Direccion = Validador.TextoOpcional(datos.Direccion, "La dirección", 250),
                Telefono = Validador.Telefono(datos.Telefono),
                TipoEstablecimientoId = datos.TipoEstablecimientoId,
                ProvinciaId = datos.ProvinciaId
            };
        }
    }
}
