using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.Entities
{
    
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string Cargo { get; set; }
        public bool AccesoPersonal { get; set; }
        public bool TienePerfilCliente { get; set; }
        public bool ClienteActivo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string PerfilesDisponibles
        {
            get
            {
                string personal = IdRol == 1 || IdRol == 2 ? RolesSistema.Nombre(IdRol) + (AccesoPersonal ? "" : " inactivo") : "";
                string cliente = ClienteActivo ? "Cliente" : TienePerfilCliente ? "Cliente inactivo" : "";
                return personal.Length > 0 && cliente.Length > 0 ? personal + " / " + cliente : personal + cliente;
            }
        }

    }
}
