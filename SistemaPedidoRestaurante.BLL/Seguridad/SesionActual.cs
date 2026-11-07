using System;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public static class SesionActual
    {
        public static int IdUsuario { get; private set; }
        public static int IdPersona { get; private set; }
        public static string NombreUsuario { get; private set; }
        public static int IdRol { get; private set; }
        public static string NombreRol { get; private set; }
        public static string Cargo { get; private set; }
        public static bool EstaAutenticado => IdUsuario > 0;

        public static void Iniciar(Usuario usuario)
        {
            if (usuario == null || usuario.IdUsuario <= 0 || !usuario.AccesoPersonal ||
                (usuario.IdRol != RolesSistema.Administrador && usuario.IdRol != RolesSistema.Empleado))
            {
                throw new UnauthorizedAccessException("El acceso está reservado al personal del local.");
            }

            string cargo = CargosPersonal.Normalizar(usuario.Cargo);
            if (usuario.IdRol == RolesSistema.Empleado && cargo != CargosPersonal.Despachador)
                throw new UnauthorizedAccessException("La cuenta necesita el cargo de Despachador.");

            IdUsuario = usuario.IdUsuario;
            IdPersona = usuario.IdPersona;
            NombreUsuario = usuario.NombreUsuario;
            IdRol = usuario.IdRol;
            NombreRol = RolesSistema.Nombre(usuario.IdRol);
            Cargo = cargo;
        }

        public static bool TieneRol(int idRol)
        {
            return EstaAutenticado && IdRol == idRol;
        }

        public static void Cerrar()
        {
            IdUsuario = 0;
            IdPersona = 0;
            NombreUsuario = null;
            IdRol = 0;
            NombreRol = null;
            Cargo = null;
        }
    }
}
