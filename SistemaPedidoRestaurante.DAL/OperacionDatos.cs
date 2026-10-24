using System;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public static class OperacionDatos
    {
        // Recibe el error ocurrido, en lugar de recibir una funcion para ejecutarla.
        public static Exception Traducir(SqlException error, string mensajeDuplicado)
        {
            switch (error.Number)
            {
                case 2601:
                case 2627:
                    return new ArgumentException(mensajeDuplicado, error);
                case 1205:
                    return new ArgumentException("Otra operación modificó estos datos al mismo tiempo. Recargue e inténtelo de nuevo.", error);
                case 547:
                    return new ArgumentException("El registro tiene relaciones o valores incompatibles. No se realizó el cambio.", error);
                case 2812:
                    return new ArgumentException("Falta un procedimiento en la base. Instale las actualizaciones SQL del proyecto.", error);
                default:
                    if (error.Number >= 50000)
                    {
                        return new ArgumentException(error.Message, error);
                    }

                    return error;
            }
        }
    }
}
