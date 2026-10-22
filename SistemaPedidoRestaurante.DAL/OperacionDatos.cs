using System;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{
    public static class OperacionDatos
    {
        public static T Ejecutar<T>(Func<T> operacion, string duplicado = "Ya existe un registro con ese código, nombre o dato único.")
        {
            try { return operacion(); }
            catch (SqlException e) when (e.Number == 2601 || e.Number == 2627)
            { throw new ArgumentException(duplicado, e); }
            catch (SqlException e) when (e.Number == 1205)
            { throw new ArgumentException("Otra operación modificó estos datos al mismo tiempo. Recargue e inténtelo de nuevo.", e); }
            catch (SqlException e) when (e.Number == 547)
            { throw new ArgumentException("El registro tiene relaciones o valores incompatibles. No se realizó el cambio.", e); }
            catch (SqlException e) when (e.Number == 2812)
            { throw new ArgumentException("Falta un procedimiento en la base. Instale las actualizaciones SQL del proyecto.", e); }
            catch (SqlException e) when (e.Number >= 50000)
            { throw new ArgumentException(e.Message, e); }
        }
        public static void Ejecutar(Action operacion, string duplicado = "Ya existe un registro con ese código, nombre o dato único.")
        { Ejecutar(() => { operacion(); return 0; }, duplicado); }
    }
}
