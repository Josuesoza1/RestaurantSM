using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.BLL
{
    public class MesasGestionBLL
    {
        private readonly MesaDAL mesaDAL = new MesaDAL();
        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 0)
            {
                throw new ArgumentException("Seleccione una mesa para eliminar.");
            }

            try
            {
                mesaDAL.Eliminar(id, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public DataTable Listar(bool cliente = false)
        {
            Permisos.ExigirRol(cliente ? 3 : 1);
            DataTable mesas = mesaDAL.ListarGestion();
            if (!cliente)
            {
                return mesas;
            }

            var disponibles = mesas.AsEnumerable().Where(mesa =>
                mesa.Field<bool>("Activa") &&
                (mesa.Field<string>("Estado") == "Disponible" ||
                 mesa.Field<string>("Estado") == "Reservada")).ToArray();

            if (disponibles.Length == 0)
            {
                return mesas.Clone();
            }
            return disponibles.CopyToDataTable();
        }

        public DataTable Buscar(DataTable tabla, string filtro)
        {
            Permisos.ExigirRol(1);
            return BusquedaLINQ.Filtrar(tabla, filtro);
        }

        public int Guardar(int id, int numero, int capacidad, string estado, bool activa)
        {
            Permisos.ExigirRol(1);
            GestionValidacion.Mesa(numero, capacidad, estado);
            try
            {
                return mesaDAL.Guardar(id, numero, capacidad, estado, activa, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }
    }
}
