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
                throw new System.UnauthorizedAccessException("Este acceso corresponde a una cuenta de cliente.");
            return _clienteDAL.ObtenerPorUsuario(SesionActual.IdUsuario);
        }

        private readonly ClienteDAL _clienteDAL = new ClienteDAL();
        private readonly ClienteValidacion _validacion = new ClienteValidacion();

        public void InsertarCliente(Cliente cliente, Usuario usuario, string confirmarContrasena)
        {
            if (usuario == null) throw new System.ArgumentException("Ingrese los datos de la cuenta.");
            new UsuarioValidacion().ValidarCredenciales(usuario.NombreUsuario, usuario.Contrasena);
            usuario.NombreUsuario=CamposValidacion.Usuario(usuario.NombreUsuario);
            _validacion.ValidarRegistro(cliente, usuario.Contrasena, confirmarContrasena);
            _clienteDAL.InsertarCliente(cliente, usuario);
        }
    }
}
