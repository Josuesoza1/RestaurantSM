namespace SistemaPedidoRestaurante.Entities
{
    public class Cliente : Persona
    {
        public int IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public bool Activo { get; set; }
    }
}
