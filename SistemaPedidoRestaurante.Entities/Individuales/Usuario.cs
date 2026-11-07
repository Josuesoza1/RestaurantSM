
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
        public string EstadoAcceso
        {
            get { return RolesSistema.Nombre(IdRol) + (AccesoPersonal ? "" : " inactivo"); }
        }
    }
}
