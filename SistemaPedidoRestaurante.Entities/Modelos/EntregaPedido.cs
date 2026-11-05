namespace SistemaPedidoRestaurante.Entities
{
    
    public class EntregaPedido
    {
        public const int EnMesa = 1;
        public const int Recoger = 2;
        public const int Domicilio = 3;

        public int IdTipoEntrega { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string TelefonoContacto { get; set; }
        public decimal CostoEnvioEsperado { get; set; }
    }
}
