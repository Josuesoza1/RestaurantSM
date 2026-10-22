using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ComprasClienteBLL
    {
        private readonly ComprasClienteDAL comprasDAL = new ComprasClienteDAL();
        public DataTable Listar() { Permisos.ExigirRol(3); return comprasDAL.Listar(SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable t, string filtro) { Permisos.ExigirRol(3); return BusquedaLINQ.Filtrar(t, filtro); }
        public DataTable Detalle(int pedido) { Permisos.ExigirRol(3); return comprasDAL.Detalle(SesionActual.IdUsuario, pedido); }
        public int Guardar(PedidoBorrador pedido, Guid solicitud, int id = 0)
        {
            Permisos.ExigirRol(3);
            if (pedido == null || pedido.Lineas.Count == 0 || pedido.Lineas.Count > 200 || solicitud == Guid.Empty)
                throw new ArgumentException("Agregue entre 1 y 200 productos diferentes antes de guardar.");
            return OperacionDatos.Ejecutar(() => comprasDAL.Guardar(SesionActual.IdUsuario,id,solicitud,pedido.Lineas));
        }
        public void Cancelar(int id)
        {
            Permisos.ExigirRol(3);
            OperacionDatos.Ejecutar(() => comprasDAL.Cancelar(SesionActual.IdUsuario,id));
        }
    }
}
