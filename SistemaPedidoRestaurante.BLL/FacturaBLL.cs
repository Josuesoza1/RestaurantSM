using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.BLL
{
    public class FacturaBLL
    {
        private readonly FacturaDAL facturaDAL = new FacturaDAL();

        public DataTable ListarFacturas()
        {
            Permisos.ExigirPersonal();
            try
            {
                return facturaDAL.ListarFacturas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DocumentoVenta ObtenerDocumento(int pedido, bool factura)
        {
            if (!SesionActual.EstaAutenticado)
                throw new UnauthorizedAccessException("Inicie sesion para consultar documentos.");
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido para consultar el documento.");
            DataSet tablas;
            try
            {
                tablas = facturaDAL.ObtenerDocumento(SesionActual.IdUsuario, pedido, factura);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el documento.");
            }

            if (tablas.Tables.Count < 2 || tablas.Tables[0].Rows.Count != 1 || tablas.Tables[1].Rows.Count == 0)
                throw new ArgumentException("El documento no tiene cabecera o detalle completo.");
            DataRow fila = tablas.Tables[0].Rows[0];
            DocumentoVenta documento = new DocumentoVenta();
            documento.EsFactura = factura;
            documento.Restaurante = Convert.ToString(fila["Restaurante"]);
            documento.Moneda = Convert.ToString(fila["Moneda"]);
            documento.Numero = Convert.ToString(fila["Numero"]);
            documento.Fecha = Convert.ToDateTime(fila["Fecha"]);
            documento.Cliente = Convert.ToString(fila["Cliente"]);
            documento.Cedula = Convert.ToString(fila["Cedula"]);
            documento.Empleado = Convert.ToString(fila["Empleado"]);
            documento.Estado = Convert.ToString(fila["Estado"]);
            documento.Mesa = Convert.ToString(fila["Mesa"]);
            documento.TipoPedido = Convert.ToString(fila["TipoPedido"]);
            documento.Metodo = Convert.ToString(fila["Metodo"]);
            documento.Subtotal = Convert.ToDecimal(fila["Subtotal"]);
            documento.CostoEnvio = Convert.ToDecimal(fila["CostoEnvio"]);
            documento.Impuestos = Convert.ToDecimal(fila["Impuestos"]);
            documento.Descuento = Convert.ToDecimal(fila["Descuento"]);
            documento.Total = Convert.ToDecimal(fila["Total"]);
            documento.Recibido = Convert.ToDecimal(fila["Recibido"]);
            documento.Cambio = Convert.ToDecimal(fila["Cambio"]);
            documento.TasaImpuesto = Convert.ToDecimal(fila["TasaImpuesto"]);
            foreach (DataRow linea in tablas.Tables[1].Rows)
            {
                DetalleDocumento detalle = new DetalleDocumento();
                detalle.Producto = Convert.ToString(linea["Producto"]);
                detalle.Cantidad = Convert.ToInt32(linea["Cantidad"]);
                detalle.PrecioUnitario = Convert.ToDecimal(linea["PrecioUnitario"]);
                detalle.Subtotal = Convert.ToDecimal(linea["Subtotal"]);
                documento.Detalle.Add(detalle);
            }

            return documento;
        }
    }
}
