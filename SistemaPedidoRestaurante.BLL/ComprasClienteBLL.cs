using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class ComprasClienteBLL
    {
        private readonly ComprasClienteDAL comprasDAL = new ComprasClienteDAL();
        public DataTable Listar()
        {
            Permisos.ExigirRol(3);
            return comprasDAL.Listar(SesionActual.IdUsuario);
        }

        public DataTable Buscar(DataTable t, string filtro)
        {
            Permisos.ExigirRol(3);
            return BusquedaLINQ.Filtrar(t, filtro);
        }

        public DataTable Detalle(int pedido)
        {
            Permisos.ExigirRol(3);
            return comprasDAL.Detalle(SesionActual.IdUsuario, pedido);
        }

        public int Guardar(PedidoBorrador pedido, Guid solicitud, int id = 0)
        {
            Permisos.ExigirRol(3);
            PedidoValidacion.Contenido(pedido == null ? null : pedido.Lineas, solicitud);

            try
            {
                return comprasDAL.Guardar(SesionActual.IdUsuario, id, solicitud, pedido.Lineas);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public void Cancelar(int id)
        {
            Permisos.ExigirRol(3);
            try
            {
                comprasDAL.Cancelar(SesionActual.IdUsuario, id);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }
    }
}
