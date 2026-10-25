using System;
using System.Collections.Generic;

namespace SistemaPedidoRestaurante.Entities
{
    public class DocumentoVenta
    {
        public bool EsFactura { get; set; }
        public string Restaurante { get; set; }
        public string Moneda { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Cedula { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }
        public string Mesa { get; set; }
        public string TipoPedido { get; set; }
        public string Metodo { get; set; }
        public decimal Subtotal { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Recibido { get; set; }
        public decimal Cambio { get; set; }
        public decimal TasaImpuesto { get; set; }
        public List<DetalleDocumento> Detalle { get; set; } = new List<DetalleDocumento>();
    }

    public class DetalleDocumento
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
