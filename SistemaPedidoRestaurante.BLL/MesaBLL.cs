using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class MesaBLL
    {
        private readonly MesaDAL mesaDAL = new MesaDAL();
        public List<Mesa> ObtenerMesas()
        {
            if (!SesionActual.EstaAutenticado)
                throw new UnauthorizedAccessException("Inicie sesión para consultar mesas.");
            return mesaDAL.Listar();
        }

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

        public DataTable Listar()
        {
            Permisos.ExigirRol(1);
            return mesaDAL.ListarGestion();
        }

        public DataTable ListarParaReservar()
        {
            Permisos.ExigirRol(3);
            DataTable mesas = mesaDAL.ListarGestion();
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

        public DataTable ListarMesas()
        {
            Permisos.ExigirPersonal();
            try
            {
                return mesaDAL.ListarMesas(SesionActual.IdUsuario);
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
                mesaDAL.CambiarMesa(SesionActual.IdUsuario, mesa, anterior);
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
                mesaDAL.AsignarMesa(SesionActual.IdUsuario, pedido, mesa);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo asignar la mesa.");
            }
        }
    }
}
