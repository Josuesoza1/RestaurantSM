using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDAL = new ClienteDAL();
        private readonly ClienteValidacion _validacion = new ClienteValidacion();

        public void InsertarCliente(Cliente cliente, Usuario usuario, string confirmarContrasena)
        {
            _validacion.ValidarRegistro(cliente, usuario.Contrasena, confirmarContrasena);
            _clienteDAL.InsertarCliente(cliente, usuario);
        }
    }
}