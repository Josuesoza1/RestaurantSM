using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class FacturaBLL
    {
        private readonly FacturaDAL facturaDAL = new FacturaDAL();

        public void AnularVenta(int pedido, string motivo, bool reponerInventario)
        {
            Permisos.ExigirRol(1);
            if (pedido <= 0)
                throw new ArgumentException("Seleccione una factura.");
            motivo = CamposValidacion.Texto(motivo, 200, "motivo de anulación", true);
            try
            {
                facturaDAL.AnularVenta(SesionActual.IdUsuario, pedido, motivo, reponerInventario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo anular la venta.");
            }
        }

        public DataTable ListarFacturas()
        {
            Permisos.ExigirRol(1);
            try
            {
                return facturaDAL.ListarFacturas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ConsultarResumenMensual(DateTime mes)
        {
            DateTime inicio = new DateTime(mes.Year, mes.Month, 1);
            return ConsultarResumen(inicio, inicio.AddMonths(1));
        }

        public DataTable ConsultarResumenDiario(DateTime dia)
        {
            DateTime inicio = dia.Date;
            return ConsultarResumen(inicio, inicio.AddDays(1));
        }

        private DataTable ConsultarResumen(DateTime inicio, DateTime fin)
        {
            Permisos.ExigirRol(RolesSistema.Administrador);
            try
            {
                return facturaDAL.ConsultarResumen(SesionActual.IdUsuario, inicio, fin);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el resumen de caja.");
            }
        }

        public DocumentoVenta ObtenerUltimoComprobantePropio()
        {
            Permisos.ExigirCobro();
            try
            {
                DataSet tablas = facturaDAL.ObtenerUltimoComprobantePropio(SesionActual.IdUsuario);
                return tablas.Tables.Count == 0 ? null : LeerDocumento(tablas, true);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el último comprobante.");
            }
        }

        public DocumentoVenta ObtenerDocumento(int pedido, bool factura, Guid? solicitudCobro = null)
        {
            Permisos.ExigirPersonal();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido para consultar el documento.");
            if (!Permisos.PuedeVerHistorial && (!factura || !solicitudCobro.HasValue))
                throw new UnauthorizedAccessException("El historial de ventas está reservado al administrador.");

            DataSet tablas;
            try
            {
                tablas = facturaDAL.ObtenerDocumento(SesionActual.IdUsuario, pedido, factura, solicitudCobro);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el documento.");
            }

            return LeerDocumento(tablas, factura);
        }

        private static DocumentoVenta LeerDocumento(DataSet tablas, bool factura)
        {
            if (tablas.Tables.Count < 2 || tablas.Tables[0].Rows.Count != 1 || tablas.Tables[1].Rows.Count == 0)
                throw new ArgumentException("El documento no tiene cabecera o detalle completo.");
            DataRow fila = tablas.Tables[0].Rows[0];
            DocumentoVenta documento = new DocumentoVenta();
            documento.EsFactura = factura;
            documento.ImpuestoAgregado = Convert.ToBoolean(fila["ImpuestoAgregado"]);
            documento.Restaurante = Convert.ToString(fila["Restaurante"]);
            documento.Moneda = Convert.ToString(fila["Moneda"]);
            documento.Numero = Convert.ToString(fila["Numero"]);
            documento.Fecha = Convert.ToDateTime(fila["Fecha"]);
            documento.Cliente = Convert.ToString(fila["Cliente"]);
            documento.Cedula = Convert.ToString(fila["Cedula"]);
            documento.Estado = Convert.ToString(fila["Estado"]);
            if (factura)
                documento.MotivoAnulacion = Convert.ToString(fila["MotivoAnulacion"]);
            documento.Mesa = Convert.ToString(fila["Mesa"]);
            documento.TipoPedido = Convert.ToString(fila["TipoPedido"]);
            documento.OrigenPedido = Convert.ToString(fila["OrigenPedido"]);
            documento.RegistradoPor = Convert.ToString(fila["RegistradoPor"]);
            documento.AtendidoPor = Convert.ToString(fila["AtendidoPor"]);
            documento.CobradoPor = Convert.ToString(fila["CobradoPor"]);
            documento.DireccionEntrega = Convert.ToString(fila["DireccionEntrega"]);
            documento.ReferenciaEntrega = Convert.ToString(fila["ReferenciaEntrega"]);
            documento.TelefonoContacto = Convert.ToString(fila["TelefonoContacto"]);

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
