using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ConfiguracionRestauranteBLL
    {
        private readonly ConfiguracionRestauranteDAL configuracionDAL = new ConfiguracionRestauranteDAL();

        public DataTable VerificarInstalacion()
        {
            try
            {
                return configuracionDAL.VerificarInstalacion();
            }
            catch (SqlException error)
            {
                if (error.Number == 2812 || error.Number == 208)
                    throw new ArgumentException("La base necesita actualizarse. Ejecute sql/ACTUALIZAR_TODO_RESTAURANTSM.sql completo en SQL Server.", error);
                throw OperacionDatos.Traducir(error, "No se pudo verificar la instalacion.");
            }
        }

        public void GuardarConfiguracion(string nombre, decimal tasa)
        {
            Permisos.ExigirRol(1);
            nombre = ConfiguracionValidacion.Validar(nombre, tasa);
            try
            {
                configuracionDAL.GuardarConfiguracion(SesionActual.IdUsuario, nombre, tasa);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo guardar la configuracion.");
            }
        }
    }
}
