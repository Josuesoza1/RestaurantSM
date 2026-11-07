using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL clienteDAL = new ClienteDAL();
        public DataTable Listar()
        {
            Permisos.ExigirRol(1);
            return clienteDAL.ListarGestion(SesionActual.IdUsuario);
        }

        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 0)
            {
                throw new ArgumentException("Seleccione un cliente para eliminar.");
            }

            try
            {
                clienteDAL.Eliminar(id, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public void Desactivar(int id)
        {
            Permisos.ExigirRol(1);
            var fila = Listar().AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == id);
            if (fila == null)
            {
                throw new ArgumentException("Seleccione un cliente existente.");
            }

            Guardar(new Cliente
            {
                IdCliente = id,
                PrimerNombre = Convert.ToString(fila["PrimerNombre"]),
                SegundoNombre = Convert.ToString(fila["SegundoNombre"]),
                PrimerApellido = Convert.ToString(fila["PrimerApellido"]),
                SegundoApellido = Convert.ToString(fila["SegundoApellido"]),
                Genero = Convert.ToString(fila["Genero"]),
                Cedula = Convert.ToString(fila["Cedula"]),
                Telefono = Convert.ToString(fila["Telefono"]),
                Email = Convert.ToString(fila["Email"])
            }, false);
        }

        public DataTable Buscar(DataTable tabla, string filtro)
        {
            Permisos.ExigirRol(1);
            return BusquedaLINQ.Filtrar(tabla, filtro);
        }

        public int Guardar(Cliente cliente, bool activo)
        {
            Permisos.ExigirRol(1);
            return GuardarValidado(cliente, activo);
        }

        private int GuardarValidado(Cliente cliente, bool activo)
        {
            GestionValidacion.Cliente(cliente);
            cliente.Activo = activo;
            try
            {
                return clienteDAL.GuardarGestion(cliente, activo, SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "Ya existe un registro con ese código, nombre o dato único.");
            }
        }

        public DataTable ListarClientes()
        {
            Permisos.ExigirAtencion();
            try
            {
                return clienteDAL.ListarClientes(SesionActual.IdUsuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "No se pudo consultar el listado.");
            }
        }
    }
}
