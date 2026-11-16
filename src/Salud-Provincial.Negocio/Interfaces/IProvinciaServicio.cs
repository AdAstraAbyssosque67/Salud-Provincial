using System.Collections.Generic;
using Salud_Provincial.Entidades.Modelos;

namespace Salud_Provincial.Negocio.Interfaces
{
    public interface IProvinciaServicio
    {
        /// <summary>Administrador nacional: todas. Usuario provincial: solo la suya (y solo si está activa).</summary>
        List<Provincia> Listar(bool incluirInactivas = false);

        // Solo administrador nacional:
        Provincia Crear(string codigo, string nombre);
        void Actualizar(int provinciaId, string codigo, string nombre);

        /// <summary>Al desactivar una provincia, sus usuarios ya no pueden iniciar sesión.</summary>
        void Desactivar(int provinciaId);
        void Reactivar(int provinciaId);
    }
}
