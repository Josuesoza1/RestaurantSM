using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.Entities
{
    
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }

        public int IdRol { get; set; }
        public string NombreRol { get; set; }

    }
}
