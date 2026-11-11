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

        public void GuardarConfiguracion(string nombre, decimal tasa, bool descuentoActivo = false,
            decimal porcentajeDescuento = 0, DateTime? fechaDescuento = null)
        {
            Permisos.ExigirRol(1);
            nombre = ConfiguracionValidacion.Validar(nombre, tasa);
            if (porcentajeDescuento < 0 || porcentajeDescuento > 100 ||
                decimal.Round(porcentajeDescuento, 2) != porcentajeDescuento ||
                descuentoActivo && (porcentajeDescuento == 0 || !fechaDescuento.HasValue))
                throw new ArgumentException("Indique un descuento mayor que 0 y hasta 100 %, con hasta dos decimales y una fecha de aplicación.");
            try
            {
                configuracionDAL.GuardarConfiguracion(SesionActual.IdUsuario, nombre, tasa,
                    descuentoActivo, porcentajeDescuento, fechaDescuento);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo guardar la configuracion.");
            }
        }
    }
}
