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

        public DataTable ObtenerConfiguracion()
        {
            try
            {
                return configuracionDAL.ObtenerConfiguracion();
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar la configuración del restaurante.");
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
