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
        public Cliente ObtenerMiPerfil()
        {
            if (!SesionActual.TieneRol(3))
            {
                throw new System.UnauthorizedAccessException("Este acceso corresponde a una cuenta de cliente.");
            }

            return clienteDAL.ObtenerPorUsuario(SesionActual.IdUsuario);
        }

        private readonly ClienteDAL clienteDAL = new ClienteDAL();
        private readonly ClienteValidacion validacion = new ClienteValidacion();
        public void InsertarCliente(Cliente cliente, Usuario usuario, string confirmarContrasena)
        {
            if (usuario == null)
            {
                throw new System.ArgumentException("Ingrese los datos de la cuenta.");
            }

            new UsuarioValidacion().ValidarCredenciales(usuario.NombreUsuario, usuario.Contrasena);
            usuario.NombreUsuario = CamposValidacion.Usuario(usuario.NombreUsuario);
            validacion.ValidarRegistro(cliente, usuario.Contrasena, confirmarContrasena);
            try
            {
                clienteDAL.InsertarCliente(cliente, usuario);
            }
            catch (SqlException error)
            {
                throw OperacionDatos.Traducir(error, "La cédula o el nombre de usuario ya están registrados. Revise la gestión de clientes y el listado de usuarios.");
            }
        }

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

            // Desactivar no debe guardar cambios que todavía estén sin confirmar en el formulario.
            Guardar(new Cliente
            {
                Id = id,
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

        public int ActualizarMiPerfil(Cliente cliente)
        {
            Permisos.ExigirRol(3);
            if (cliente == null)
                throw new ArgumentException("Ingrese los datos del cliente.");
            Cliente perfil = ObtenerMiPerfil();
            if (perfil == null)
                throw new ArgumentException("No existe un perfil asociado.");

            // El identificador siempre viene de la sesión, aunque se envíe otro desde el formulario.
            cliente.Id = perfil.Id;
            return GuardarValidado(cliente, true);
        }

        private int GuardarValidado(Cliente cliente, bool activo)
        {
            GestionValidacion.Cliente(cliente);
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
