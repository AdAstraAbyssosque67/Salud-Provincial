using System;
using System.Collections.Generic;
using Salud_Provincial.AccesoDatos.Interfaces;
using Salud_Provincial.AccesoDatos.Repositorios;
using Salud_Provincial.Entidades.Base;
using Salud_Provincial.Entidades.Enums;
using Salud_Provincial.Entidades.Modelos;
using Salud_Provincial.Negocio.Excepciones;
using Salud_Provincial.Negocio.Interfaces;
using Salud_Provincial.Negocio.Seguridad;
using Salud_Provincial.Negocio.Validaciones;

namespace Salud_Provincial.Negocio.Servicios
{
    /// <summary>Gestión de usuarios: solo el administrador nacional.</summary>
    public class UsuarioServicio : ServicioBase, IUsuarioServicio
    {
        private const string MensajeDuplicado = "Ya existe un usuario con ese nombre de usuario.";

        private readonly IUsuarioRepositorio _usuarios;
        private readonly IProvinciaRepositorio _provincias;

        public UsuarioServicio() : this(new UsuarioRepositorio(), new ProvinciaRepositorio())
        {
        }

        public UsuarioServicio(IUsuarioRepositorio usuarios, IProvinciaRepositorio provincias)
        {
            _usuarios = usuarios;
            _provincias = provincias;
        }

        public List<Usuario> Listar(bool incluirInactivos = false)
        {
            ExigirAdminNacional();
            return Ejecutar(() => _usuarios.Listar(AlcanceProvincia.Todas(), incluirInactivos));
        }

        public Usuario Crear(string nombreUsuario, string nombreCompleto, string clave, RolUsuario rol, int? provinciaId)
        {
            ExigirAdminNacional();

            string usuarioLimpio = Validador.NombreUsuario(nombreUsuario);
            string nombreLimpio = Validador.TextoRequerido(nombreCompleto, "El nombre completo", 150);
            Validador.Clave(clave);

            return Ejecutar(() =>
            {
                int? provinciaFinal = ResolverProvincia(rol, provinciaId);

                Usuario nuevo = new Usuario
                {
                    NombreUsuario = usuarioLimpio,
                    NombreCompleto = nombreLimpio,
                    ClaveHash = HashClave.Calcular(clave),
                    Rol = rol,
                    ProvinciaId = provinciaFinal
                };

                nuevo.UsuarioId = _usuarios.Insertar(nuevo, Sesion.UsuarioId, AlcanceProvincia.Todas());

                nuevo.ClaveHash = Array.Empty<byte>(); // el hash no sale de esta capa
                return nuevo;
            }, MensajeDuplicado);
        }

        public void Actualizar(int usuarioId, string nombreCompleto, RolUsuario rol, int? provinciaId)
        {
            ExigirAdminNacional();

            string nombreLimpio = Validador.TextoRequerido(nombreCompleto, "El nombre completo", 150);

            Ejecutar(() =>
            {
                Usuario usuario = ObtenerExistente(usuarioId);

                // Evita que el administrador se quite a sí mismo el rol y deje el sistema sin administración.
                if (usuarioId == Sesion.UsuarioId && rol != usuario.Rol)
                    throw new NegocioException("No puede cambiar su propio rol.");

                usuario.NombreCompleto = nombreLimpio;
                usuario.Rol = rol;
                usuario.ProvinciaId = ResolverProvincia(rol, provinciaId);

                if (!_usuarios.Actualizar(usuario, Sesion.UsuarioId, AlcanceProvincia.Todas()))
                    throw new NegocioException("No se pudo actualizar el usuario.");
            });
        }

        public void RestablecerClave(int usuarioId, string nuevaClave)
        {
            ExigirAdminNacional();
            Validador.Clave(nuevaClave);

            Ejecutar(() =>
            {
                ObtenerExistente(usuarioId);

                if (!_usuarios.CambiarClave(usuarioId, HashClave.Calcular(nuevaClave),
                                            Sesion.UsuarioId, AlcanceProvincia.Todas()))
                    throw new NegocioException("No se pudo restablecer la contraseña.");
            });
        }

        public void Desactivar(int usuarioId)
        {
            ExigirAdminNacional();

            if (usuarioId == Sesion.UsuarioId)
                throw new NegocioException("No puede desactivar su propio usuario.");

            CambiarEstado(usuarioId, false);
        }

        public void Reactivar(int usuarioId)
        {
            ExigirAdminNacional();
            CambiarEstado(usuarioId, true);
        }

        private void CambiarEstado(int usuarioId, bool activo)
        {
            Ejecutar(() =>
            {
                ObtenerExistente(usuarioId);

                if (!_usuarios.CambiarEstado(usuarioId, activo, Sesion.UsuarioId, AlcanceProvincia.Todas()))
                    throw new NegocioException(activo ? "El usuario ya está activo." : "El usuario ya está desactivado.");
            });
        }

        private Usuario ObtenerExistente(int usuarioId)
            => _usuarios.ObtenerPorId(usuarioId, AlcanceProvincia.Todas())
               ?? throw new NegocioException("El usuario no existe.");

        /// <summary>
        /// Aplica la regla rol/provincia: el administrador nacional no pertenece a ninguna provincia;
        /// un usuario provincial debe pertenecer a una provincia existente y activa.
        /// </summary>
        private int? ResolverProvincia(RolUsuario rol, int? provinciaId)
        {
            if (rol == RolUsuario.AdminNacional)
                return null;

            if (provinciaId == null || provinciaId <= 0)
                throw new NegocioException("Seleccione la provincia del usuario.");

            Provincia? provincia = _provincias.ObtenerPorId(provinciaId.Value);
            if (provincia == null || !provincia.Activo)
                throw new NegocioException("La provincia seleccionada no existe o está desactivada.");

            return provinciaId;
        }
    }
}
