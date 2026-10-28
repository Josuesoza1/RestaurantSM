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
        public List<Pedido> ObtenerPedidosAbiertos()
        {
            Permisos.ExigirPersonal();
            return pedidoDAL.ListarAbiertos();
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

        public int GuardarPedido(int cliente, PedidoBorrador pedido, Guid solicitud)
        {
            Permisos.ExigirAtencion();
            if (cliente <= 0)
                throw new ArgumentException("Seleccione un cliente.");
            PedidoValidacion.Contenido(pedido == null ? null : pedido.Lineas, solicitud);
            try
            {
                return pedidoDAL.Guardar(SesionActual.IdUsuario, cliente, 0, solicitud, pedido.Lineas);
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
