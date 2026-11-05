namespace SistemaPedidoRestaurante.Entities
{
    public class Empleado : Persona
    {
        public int IdEmpleado { get; set; }
        public int? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int? IdRol { get; set; }
        public string NombreRol { get; set; }
        public string Cargo { get; set; }
        public bool Activo { get; set; }
    }
}
