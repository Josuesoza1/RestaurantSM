using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class PedidoBLL
    {
        private readonly PedidoDAL pedidoDAL = new PedidoDAL();
        public DataTable OpcionesEntrega(int pedido = 0)
        {
            if (!SesionActual.TieneRol(3)) Permisos.ExigirPersonal();
            try { return pedidoDAL.OpcionesEntrega(SesionActual.IdUsuario, pedido, SesionActual.TieneRol(3)); }
            catch (SqlException error) { throw OperacionDatos.Traducir(error, "No se pudieron consultar las opciones de entrega."); }
        }

        public EntregaPedido ObtenerEntrega(int pedido)
        {
            if (!SesionActual.TieneRol(3)) Permisos.ExigirPersonal();
            try
            {
                DataTable tabla = pedidoDAL.ObtenerEntrega(SesionActual.IdUsuario, pedido, SesionActual.TieneRol(3));
                if (tabla.Rows.Count != 1) throw new ArgumentException("No existe el pedido.");
                DataRow fila = tabla.Rows[0];
                return new EntregaPedido
                {
                    IdTipoEntrega = Convert.ToInt32(fila["IdTipoEntrega"]),
                    Direccion = Convert.ToString(fila["Direccion"]),
                    Referencia = Convert.ToString(fila["Referencia"]),
                    TelefonoContacto = Convert.ToString(fila["TelefonoContacto"]),
                    CostoEnvioEsperado = Convert.ToDecimal(fila["CostoEnvio"])
                };
            }
            catch (SqlException error) { throw OperacionDatos.Traducir(error, "No se pudo consultar la entrega."); }
        }
        public List<Pedido> ObtenerPedidosAbiertos()
        {
            Permisos.ExigirPersonal();
            return pedidoDAL.ListarAbiertos(SesionActual.IdUsuario);
        }

        public DataTable ListarPedidos()
        {
            Permisos.ExigirPersonal();
            try
            {
                return pedidoDAL.ListarPedidos(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }

        public DataTable Detalle(int pedido)
        {
            Permisos.ExigirPersonal();
            try
            {
                return pedidoDAL.Detalle(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el pedido.");
            }
        }

        public int GuardarPedido(int cliente, PedidoBorrador pedido, Guid solicitud, EntregaPedido entrega = null)
        {
            Permisos.ExigirAtencion();
            if (cliente <= 0)
                throw new ArgumentException("Seleccione un cliente.");
            PedidoValidacion.Contenido(pedido == null ? null : pedido.Lineas, solicitud);
            entrega = entrega ?? new EntregaPedido { IdTipoEntrega = EntregaPedido.EnMesa };
            PedidoValidacion.Entrega(entrega);
            try
            {
                return pedidoDAL.Guardar(SesionActual.IdUsuario, cliente, 0, solicitud, pedido.Lineas, entrega);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo registrar el pedido.");
            }
        }

        public void AvanzarPedido(int pedido, string estado)
        {
            Permisos.ExigirAtencion();
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
                pedidoDAL.CambiarEstadoPedido(SesionActual.IdUsuario, pedido, estado, nuevo);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cambiar el estado.");
            }
        }

        public void CancelarPedido(int pedido)
        {
            Permisos.ExigirAtencion();
            if (pedido <= 0)
                throw new ArgumentException("Seleccione un pedido.");
            try
            {
                pedidoDAL.CancelarPedido(SesionActual.IdUsuario, pedido);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo cancelar el pedido.");
            }
        }
    }
}
