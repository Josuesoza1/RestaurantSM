using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class OperacionRestauranteBLL
    {
        private readonly OperacionRestauranteDAL datos = new OperacionRestauranteDAL();
        public DataTable VerificarInstalacion()
        {
            try
            {
                return datos.VerificarInstalacion();
            }
            catch (SqlException error)
            {
                if (error.Number == 2812 || error.Number == 208)
                    throw new ArgumentException("La base necesita actualizarse. Ejecute sql/ACTUALIZAR_TODO_RESTAURANTSM.sql completo en SQL Server.", error);
                throw OperacionDatos.Traducir(error, "No se pudo verificar la instalacion.");
            }
        }

        public DataTable ListarPedidos()
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.ListarPedidos(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ListarClientes()
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.ListarClientes(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ListarReservas()
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.ListarReservas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ListarFacturas()
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.ListarFacturas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable ListarMovimientos()
        {
            Permisos.ExigirRol(1);
            try
            {
                return datos.ListarMovimientos(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable Buscar(DataTable tabla, string filtro)
        {
            Permisos.ExigirPersonal();
            return BusquedaLINQ.Filtrar(tabla, filtro);
        }

        public DataTable Detalle(int pedido)
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.Detalle(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el pedido.");
            }
        }

        public int GuardarPedido(int cliente, PedidoBorrador pedido, Guid solicitud)
        {
            Permisos.ExigirPersonal();
            if (cliente <= 0 || pedido == null || pedido.Lineas.Count == 0 || pedido.Lineas.Count > 200 || solicitud == Guid.Empty)
                throw new ArgumentException("Seleccione un cliente y agregue entre 1 y 200 productos.");
            try
            {
                return datos.GuardarPedido(SesionActual.IdUsuario, cliente, solicitud, pedido.Lineas);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo registrar el pedido.");
            }
        }

        public void AvanzarPedido(int pedido, string estado)
        {
            Permisos.ExigirPersonal();
            string nuevo;
            switch (estado)
            {
                case "Pendiente":
                    nuevo = "En preparación";
                    break;
                case "En preparación":
                    nuevo = "Listo";
                    break;
                case "Listo":
                    nuevo = "Entregado";
                    break;
                default:
                    throw new ArgumentException("El pedido no tiene una transicion pendiente.");
            }

            try
            {
                datos.CambiarEstadoPedido(SesionActual.IdUsuario, pedido, estado, nuevo);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cambiar el estado.");
            }
        }

        public void CancelarPedido(int pedido)
        {
            Permisos.ExigirPersonal();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido.");
            try
            {
                datos.CancelarPedido(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cancelar el pedido.");
            }
        }

        public DataRow Cotizar(int pedido)
        {
            Permisos.ExigirPersonal();
            try
            {
                DataTable tabla = datos.Cotizar(SesionActual.IdUsuario, pedido);
                if (tabla.Rows.Count != 1)
                    throw new ArgumentException("No se encontro el pedido.");
                return tabla.Rows[0];
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo obtener el importe.");
            }
        }

        public DataRow Cobrar(int pedido, string metodo, decimal recibido, decimal total, Guid solicitud, string notas)
        {
            Permisos.ExigirPersonal();
            if (pedido <= 0 || solicitud == Guid.Empty || total <= 0 || total > 99999999.99m || recibido < total || recibido > 99999999.99m || decimal.Round(recibido, 2) != recibido)
                throw new ArgumentException("Revise el pedido y el importe recibido.");
            if (metodo != "Efectivo" && metodo != "Tarjeta" && metodo != "Transferencia")
                throw new ArgumentException("Seleccione efectivo, tarjeta o transferencia.");
            if (metodo != "Efectivo" && recibido != total)
                throw new ArgumentException("El pago electronico debe coincidir con el total.");
            notas = CamposValidacion.Texto(notas, 200, "notas");
            try
            {
                DataTable tabla = datos.Cobrar(SesionActual.IdUsuario, pedido, metodo, recibido, total, solicitud, notas);
                return tabla.Rows[0];
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "La solicitud de cobro ya fue registrada.");
            }
        }

        public void CambiarReserva(int reserva, string anterior, string nuevo)
        {
            Permisos.ExigirPersonal();
            if (reserva <= 0)
                throw new ArgumentException("Seleccione una reserva.");
            try
            {
                datos.CambiarEstadoReserva(SesionActual.IdUsuario, reserva, anterior, nuevo);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo actualizar la reserva.");
            }
        }

        public void AjustarInventario(InventarioProducto producto, int cantidad, string tipo, string motivo)
        {
            Permisos.ExigirRol(1);
            if (producto == null || producto.Revision <= 0 || cantidad <= 0 || (tipo != "Entrada" && tipo != "Salida"))
                throw new ArgumentException("Seleccione un producto actualizado y una cantidad positiva.");
            motivo = CamposValidacion.Texto(motivo, 200, "motivo", true);
            try
            {
                datos.AjustarInventario(SesionActual.IdUsuario, producto.IdProducto, cantidad, tipo, motivo, producto.Revision);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo ajustar el inventario.");
            }
        }

        public DocumentoVenta ObtenerDocumento(int pedido, bool factura)
        {
            if (!SesionActual.EstaAutenticado)
                throw new UnauthorizedAccessException("Inicie sesion para consultar documentos.");
            DataSet tablas;
            try
            {
                tablas = datos.ObtenerDocumento(SesionActual.IdUsuario, pedido, factura);
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

        public void GuardarConfiguracion(string nombre, decimal tasa)
        {
            Permisos.ExigirRol(1);
            nombre = CamposValidacion.Texto(nombre, 120, "nombre del restaurante", true);
            if (tasa < 0 || tasa > 100 || decimal.Round(tasa, 2) != tasa)
                throw new ArgumentException("La tasa incluida debe estar entre 0 y 100, con hasta dos decimales.");
            try
            {
                datos.GuardarConfiguracion(SesionActual.IdUsuario, nombre, tasa);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo guardar la configuracion.");
            }
        }

        public DataTable ListarMesas()
        {
            Permisos.ExigirPersonal();
            try
            {
                return datos.ListarMesas(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudieron consultar las mesas.");
            }
        }

        public void CambiarMesa(int mesa, string anterior)
        {
            Permisos.ExigirPersonal();
            if (mesa <= 0 || (anterior != "Ocupada" && anterior != "En limpieza"))
                throw new ArgumentException("Seleccione una mesa ocupada o en limpieza.");
            try
            {
                datos.CambiarMesa(SesionActual.IdUsuario, mesa, anterior);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cambiar el estado de la mesa.");
            }
        }

        public void AsignarMesa(int pedido, int mesa)
        {
            Permisos.ExigirPersonal();
            if (pedido <= 0 || mesa <= 0)
                throw new ArgumentException("Seleccione pedido y mesa.");
            try
            {
                datos.AsignarMesa(SesionActual.IdUsuario, pedido, mesa);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo asignar la mesa.");
            }
        }
    }
}
