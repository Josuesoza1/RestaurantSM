using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using ClosedXML.Excel;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal sealed class DocumentoVentaExportacion
    {
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-NI");
        private readonly List<KeyValuePair<string, object>> datos = new List<KeyValuePair<string, object>>();
        private readonly List<KeyValuePair<string, object>> importes = new List<KeyValuePair<string, object>>();
        private readonly string moneda;

        internal string Titulo { get; }
        internal DataTable Detalle { get; } = new DataTable();

        internal DocumentoVentaExportacion(DocumentoVenta venta)
        {
            moneda = venta.Moneda;
            Titulo = (venta.EsFactura ? "Factura " : "Comprobante de pedido ") + venta.Numero;
            Agregar(datos, "Restaurante", venta.Restaurante);
            Agregar(datos, "Fecha", venta.Fecha);
            Agregar(datos, "Estado", venta.Estado);
            Agregar(datos, "Motivo de anulación", venta.MotivoAnulacion);
            Agregar(datos, "Cliente", venta.Cliente);
            Agregar(datos, "Cédula", venta.Cedula);
            Agregar(datos, "Mesa", venta.Mesa);
            Agregar(datos, "Origen", venta.OrigenPedido);
            Agregar(datos, "Entrega", venta.TipoPedido);
            Agregar(datos, "Registrado por", venta.RegistradoPor);
            Agregar(datos, "Atendido por", venta.AtendidoPor);
            Agregar(datos, "Dirección", venta.DireccionEntrega);
            Agregar(datos, "Referencia", venta.ReferenciaEntrega);
            Agregar(datos, "Contacto", venta.TelefonoContacto);
            Agregar(datos, "Moneda", moneda);
            if (venta.EsFactura)
            {
                Agregar(datos, "Cobrado por", venta.CobradoPor);
                Agregar(datos, "Medio de pago", venta.Metodo);
            }
            else
                Agregar(datos, "Uso del documento", "Comprobante de pedido. No acredita un pago.");

            Agregar(importes, "Total", venta.Total);
            Agregar(importes, venta.EsFactura ? "Base sin impuesto" : "Productos", venta.Subtotal);
            if (venta.EsFactura)
            {
                string etiquetaImpuesto = venta.TasaImpuesto < 0 ? "Impuesto registrado" :
                    "Impuesto incluido (" + venta.TasaImpuesto.ToString("0.##", Cultura) + " %)";
                Agregar(importes, etiquetaImpuesto, venta.Impuestos);
                if (venta.Descuento != 0)
                    Agregar(importes, "Descuento", venta.Descuento);
                Agregar(importes, "Recibido", venta.Recibido);
                Agregar(importes, "Cambio", venta.Cambio);
            }
            if (venta.CostoEnvio != 0)
                Agregar(importes, "Envío", venta.CostoEnvio);

            Detalle.Columns.Add("Producto", typeof(string));
            Detalle.Columns.Add("Cantidad", typeof(int));
            Detalle.Columns.Add("Precio unitario", typeof(decimal));
            Detalle.Columns.Add("Importe", typeof(decimal));
            foreach (DetalleDocumento linea in venta.Detalle)
                Detalle.Rows.Add(linea.Producto ?? "", linea.Cantidad, linea.PrecioUnitario, linea.Subtotal);
        }

        internal string DatosVista()
        {
            return string.Join(Environment.NewLine, datos.Select(campo => campo.Key + ": " +
                (campo.Value is DateTime fecha ? fecha.ToString("dd/MM/yyyy HH:mm", Cultura) : campo.Value.ToString())));
        }

        internal string ImportesVista()
        {
            return string.Join(Environment.NewLine, importes.Select(campo => campo.Key + ": " + moneda + " " +
                ((decimal)campo.Value).ToString("N2", Cultura)));
        }

        internal void GuardarExcel(string archivo)
        {
            using (var libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add("Documento de venta");
                hoja.Column(1).Width = 42;
                hoja.Column(2).Width = 14;
                hoja.Columns(3, 4).Width = 21;
                hoja.Range("A1:D1").Merge().Value = Titulo;
                hoja.Cell("A1").Style.Font.Bold = true;
                hoja.Cell("A1").Style.Font.FontSize = 17;
                hoja.Row(1).Height = 27;

                int numero = 3;
                foreach (var campo in datos)
                {
                    hoja.Cell(numero, 1).Value = campo.Key;
                    hoja.Range(numero, 2, numero, 4).Merge();
                    var celda = hoja.Cell(numero, 2);
                    celda.Value = XLCellValue.FromObject(campo.Value, Cultura);
                    if (campo.Value is DateTime)
                        celda.Style.DateFormat.Format = "dd/mm/yyyy hh:mm";
                    else if (campo.Value.ToString().Length > 50)
                    {
                        celda.Style.Alignment.WrapText = true;
                        hoja.Row(numero).Height = 15 * (1 + campo.Value.ToString().Length / 50);
                    }
                    numero++;
                }

                numero++;
                foreach (DataColumn columna in Detalle.Columns)
                    hoja.Cell(numero, columna.Ordinal + 1).Value = columna.ColumnName;
                var encabezado = hoja.Range(numero, 1, numero, 4);
                encabezado.Style.Font.Bold = true;
                encabezado.Style.Fill.BackgroundColor = XLColor.FromHtml("#D8E3F0");
                numero++;

                foreach (DataRow linea in Detalle.Rows)
                {
                    foreach (DataColumn columna in Detalle.Columns)
                        hoja.Cell(numero, columna.Ordinal + 1).Value = XLCellValue.FromObject(linea[columna], Cultura);
                    hoja.Range(numero, 3, numero, 4).Style.NumberFormat.Format = "#,##0.00";
                    hoja.Cell(numero, 1).Style.Alignment.WrapText = true;
                    int largoProducto = Convert.ToString(linea[0]).Length;
                    if (largoProducto > 28)
                        hoja.Row(numero).Height = 15 * (1 + largoProducto / 28);
                    numero++;
                }

                numero++;
                hoja.Range(numero, 1, numero, 4).Style.Font.Bold = true;
                foreach (var campo in importes)
                {
                    hoja.Cell(numero, 1).Value = campo.Key;
                    hoja.Cell(numero, 4).Value = (decimal)campo.Value;
                    hoja.Cell(numero, 4).Style.NumberFormat.Format = "#,##0.00";
                    numero++;
                }
                libro.SaveAs(archivo);
            }
        }

        private static void Agregar(List<KeyValuePair<string, object>> campos, string nombre, object valor)
        {
            if (valor != null && !string.IsNullOrWhiteSpace(valor.ToString()))
                campos.Add(new KeyValuePair<string, object>(nombre, valor));
        }
    }
}
