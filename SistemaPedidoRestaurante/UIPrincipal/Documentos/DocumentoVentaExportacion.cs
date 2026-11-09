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
        private readonly List<KeyValuePair<string, object>> datosAdicionales = new List<KeyValuePair<string, object>>();
        private readonly List<KeyValuePair<string, object>> importes = new List<KeyValuePair<string, object>>();
        private readonly string moneda;
        private readonly string restaurante;

        internal string Titulo { get; }
        internal DataTable Detalle { get; } = new DataTable();
        internal IReadOnlyList<KeyValuePair<string, object>> Datos => datos;
        internal IReadOnlyList<KeyValuePair<string, object>> DatosAdicionales => datosAdicionales;
        internal IReadOnlyList<KeyValuePair<string, object>> Importes => importes;

        internal DocumentoVentaExportacion(DocumentoVenta venta)
        {
            moneda = venta.Moneda;
            restaurante = venta.Restaurante;
            Titulo = (venta.EsFactura ? "Factura " : "Comprobante de venta ") + venta.Numero;
            Agregar(datos, "Estado", venta.Estado);
            if (venta.EsFactura)
                Agregar(datos, "Forma de pago", venta.Metodo);
            Agregar(datos, venta.EsFactura ? "Fecha de emisión" : "Fecha de registro", venta.Fecha);
            Agregar(datos, "Motivo de anulación", venta.MotivoAnulacion);
            Agregar(datos, "Cliente", venta.Cliente);
            if (venta.EsFactura)
                AgregarDatoAdicional("Cobrado por", venta.CobradoPor);
            else
                Agregar(datos, "Uso del documento", "Constancia de los productos solicitados. Para acreditar un pago, use la factura.");

            AgregarDatoAdicional("Cédula", venta.Cedula);
            AgregarDatoAdicional("Mesa", venta.Mesa);
            AgregarDatoAdicional("Origen", venta.OrigenPedido);
            AgregarDatoAdicional("Entrega", venta.TipoPedido);
            AgregarDatoAdicional("Registrado por", venta.RegistradoPor);
            AgregarDatoAdicional("Atendido por", venta.AtendidoPor);
            AgregarDatoAdicional("Dirección", venta.DireccionEntrega);
            AgregarDatoAdicional("Referencia", venta.ReferenciaEntrega);
            AgregarDatoAdicional("Contacto", venta.TelefonoContacto);

            Agregar(importes, venta.EsFactura ? "Base sin impuesto" : "Productos", venta.Subtotal);
            if (venta.EsFactura)
            {
                string etiquetaImpuesto = venta.TasaImpuesto < 0 ? "Impuesto registrado" :
                    "Impuesto incluido (" + venta.TasaImpuesto.ToString("0.##", Cultura) + " %)";
                Agregar(importes, etiquetaImpuesto, venta.Impuestos);
                if (venta.Descuento != 0)
                    Agregar(importes, "Descuento", venta.Descuento);
            }
            if (venta.CostoEnvio != 0)
                Agregar(importes, "Envío", venta.CostoEnvio);
            Agregar(importes, "Total", venta.Total);
            if (venta.EsFactura && string.Equals(venta.Metodo?.Trim(), "Efectivo", StringComparison.OrdinalIgnoreCase)
                && venta.Recibido > 0)
            {
                Agregar(importes, "Efectivo recibido", venta.Recibido);
                Agregar(importes, "Vuelto", venta.Cambio);
            }

            Detalle.Columns.Add("Producto", typeof(string));
            Detalle.Columns.Add("Cantidad", typeof(int));
            Detalle.Columns.Add("Precio unitario", typeof(decimal));
            Detalle.Columns.Add("Importe", typeof(decimal));
            foreach (DetalleDocumento linea in venta.Detalle)
                Detalle.Rows.Add(linea.Producto ?? "", linea.Cantidad, linea.PrecioUnitario, linea.Subtotal);
        }

        internal string DatosVista()
        {
            return string.Join(Environment.NewLine, datos
                .Concat(new[] { new KeyValuePair<string, object>("Restaurante", restaurante) })
                .Concat(datosAdicionales)
                .Where(campo => !string.IsNullOrWhiteSpace(Convert.ToString(campo.Value)))
                .Select(TextoDato));
        }

        internal string ImportesVista()
        {
            return string.Join(Environment.NewLine, importes.OrderBy(campo => campo.Key == "Total" ? 0 : 1)
                .Select(campo => campo.Key + ": " + Dinero((decimal)campo.Value)));
        }

        internal static string TextoDato(KeyValuePair<string, object> campo)
        {
            return campo.Key + ": " + (campo.Value is DateTime fecha
                ? fecha.ToString("dd/MM/yyyy HH:mm", Cultura) : campo.Value.ToString());
        }

        internal string Dinero(decimal importe)
        {
            return moneda + " " + importe.ToString("N2", Cultura);
        }

        internal static bool TieneDatoHistorico(string valor)
        {
            return !string.IsNullOrWhiteSpace(valor) &&
                !string.Equals(valor.Trim(), "Sin registrar", StringComparison.OrdinalIgnoreCase);
        }

        private void AgregarDatoAdicional(string nombre, string valor)
        {
            if (TieneDatoHistorico(valor))
                Agregar(datosAdicionales, nombre, valor);
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
                hoja.Range("A2:D2").Merge().Value = restaurante ?? "";

                int numero = 3;
                foreach (var campo in datos)
                    EscribirDato(hoja, numero++, campo);

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
                foreach (var campo in importes)
                {
                    hoja.Cell(numero, 1).Value = campo.Key;
                    hoja.Cell(numero, 4).Value = (decimal)campo.Value;
                    hoja.Cell(numero, 4).Style.NumberFormat.Format = "#,##0.00";
                    if (campo.Key == "Total")
                        hoja.Range(numero, 1, numero, 4).Style.Font.Bold = true;
                    numero++;
                }
                EscribirDato(hoja, numero++, new KeyValuePair<string, object>("Moneda", moneda ?? ""));
                if (datosAdicionales.Count > 0)
                {
                    numero++;
                    foreach (var campo in datosAdicionales)
                        EscribirDato(hoja, numero++, campo);
                }
                libro.SaveAs(archivo);
            }
        }

        private static void EscribirDato(IXLWorksheet hoja, int numero, KeyValuePair<string, object> campo)
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
        }

        private static void Agregar(List<KeyValuePair<string, object>> campos, string nombre, object valor)
        {
            if (valor != null && !string.IsNullOrWhiteSpace(valor.ToString()))
                campos.Add(new KeyValuePair<string, object>(nombre, valor));
        }
    }
}
