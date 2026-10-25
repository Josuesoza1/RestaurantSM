using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    public static class DocumentoPdf
    {
        // La sintaxis fluida de QuestPDF queda aislada del CRUD y sus eventos.
        public static void GuardarConDialogo(Form propietario, DocumentoVenta documento)
        {
            using (SaveFileDialog elegir = new SaveFileDialog())
            {
                elegir.Filter = "Documento PDF (*.pdf)|*.pdf";
                elegir.DefaultExt = "pdf";
                elegir.FileName = documento.Numero + ".pdf";
                elegir.OverwritePrompt = true;
                if (elegir.ShowDialog(propietario) != DialogResult.OK)
                    return;
                Guardar(documento, elegir.FileName);
                MessageBox.Show(propietario, "PDF guardado correctamente.");
            }
        }

        public static void Guardar(DocumentoVenta documento, string archivo)
        {
            if (documento == null || documento.Detalle == null || documento.Detalle.Count == 0)
                throw new ArgumentException("El documento no contiene productos.");
            if (string.IsNullOrWhiteSpace(archivo))
                throw new ArgumentException("Seleccione un archivo PDF.");
            // Uso educativo: licencia Community conforme a la licencia de QuestPDF.
            QuestPDF.Settings.License = LicenseType.Community;
            string destino = Path.GetFullPath(archivo);
            string temporal = Path.Combine(Path.GetDirectoryName(destino), "." + Guid.NewGuid().ToString("N") + ".pdf");
            try
            {
                Crear(documento).GeneratePdf(temporal);
                // Solo reemplaza el archivo elegido cuando el PDF ya esta completo.
                if (File.Exists(destino))
                    File.Replace(temporal, destino, null);
                else
                    File.Move(temporal, destino);
            }
            finally
            {
                if (File.Exists(temporal))
                    File.Delete(temporal);
            }
        }

        private static IDocument Crear(DocumentoVenta datos)
        {
            return Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.Letter);
                    pagina.Margin(36);
                    pagina.DefaultTextStyle(estilo => estilo.FontSize(10));
                    pagina.Header().Column(cabecera =>
                    {
                        cabecera.Item().Text(datos.Restaurante).FontSize(21).Bold();
                        cabecera.Item().Text(datos.EsFactura ? "FACTURA PAGADA" : "COMPROBANTE DE PEDIDO").FontSize(15).Bold();
                        cabecera.Item().PaddingTop(5).Text(datos.Numero + "  |  " + datos.Fecha.ToString("dd/MM/yyyy HH:mm"));
                    });
                    pagina.Content().PaddingVertical(16).Column(contenido =>
                    {
                        contenido.Spacing(7);
                        contenido.Item().Text("Cliente: " + datos.Cliente);
                        contenido.Item().Text("Cedula: " + datos.Cedula);
                        contenido.Item().Text("Estado: " + datos.Estado);
                        if (!string.IsNullOrEmpty(datos.Mesa))
                            contenido.Item().Text("Mesa: " + datos.Mesa);
                        if (datos.EsFactura)
                            contenido.Item().Text("Atendido por: " + datos.Empleado + "  |  Pago: " + datos.Metodo);
                        else
                            contenido.Item().Text("Este comprobante registra el pedido; no acredita un pago.").Italic();
                        contenido.Item().PaddingTop(8).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.RelativeColumn(5);
                                columnas.RelativeColumn(1.2f);
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn(2);
                            });
                            tabla.Header(encabezado =>
                            {
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).Text("Producto").Bold();
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).Text("Cant.").Bold();
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Precio final").Bold();
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Importe").Bold();
                            });
                            foreach (DetalleDocumento linea in datos.Detalle)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(linea.Producto);
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(linea.Cantidad.ToString());
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text(Dinero(datos, linea.PrecioUnitario));
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text(Dinero(datos, linea.Subtotal));
                            }
                        });
                        if (datos.EsFactura)
                        {
                            contenido.Item().AlignRight().Text("Base sin impuesto: " + Dinero(datos, datos.Subtotal));
                            contenido.Item().AlignRight().Text((datos.TasaImpuesto < 0 ? "Impuesto registrado: " : "Impuesto incluido (" + datos.TasaImpuesto.ToString("0.##", CultureInfo.InvariantCulture) + " %): ") + Dinero(datos, datos.Impuestos));
                        }
                        else
                            contenido.Item().AlignRight().Text("Productos: " + Dinero(datos, datos.Subtotal));
                        if (datos.CostoEnvio != 0)
                            contenido.Item().AlignRight().Text("Envio: " + Dinero(datos, datos.CostoEnvio));
                        contenido.Item().AlignRight().Text("TOTAL: " + Dinero(datos, datos.Total)).FontSize(15).Bold();
                        if (datos.EsFactura)
                        {
                            contenido.Item().AlignRight().Text("Recibido: " + Dinero(datos, datos.Recibido));
                            contenido.Item().AlignRight().Text("Cambio: " + Dinero(datos, datos.Cambio));
                        }
                    });
                    pagina.Footer().AlignCenter().Text(texto =>
                    {
                        texto.Span("Página ");
                        texto.CurrentPageNumber();
                        texto.Span(" de ");
                        texto.TotalPages();
                    });
                });
            });
        }

        private static string Dinero(DocumentoVenta documento, decimal importe)
        {
            return documento.Moneda + " " + importe.ToString("N2", CultureInfo.GetCultureInfo("es-NI"));
        }
    }
}
