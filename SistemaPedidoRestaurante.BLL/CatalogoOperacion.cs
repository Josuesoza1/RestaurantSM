using System;
using SistemaPedidoRestaurante.DAL;

namespace SistemaPedidoRestaurante.BLL
{
    internal static class CatalogoOperacion
    {
        public static void Ejecutar(Action operacion, string mensajeDuplicado)
        {
            Permisos.ExigirRol(1);
            OperacionDatos.Ejecutar(operacion,mensajeDuplicado);
        }
    }
}
