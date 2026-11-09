using System;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    public static class DocumentoPdf
    {

        public static void MostrarVistaPrevia(Form propietario, DocumentoVenta documento)
        {
            var exportacion = Preparar(documento);
            IDocument pdf = Crear(documento, exportacion);
            using (var vista = new FrmVistaPrevia(pdf, documento.Numero, exportacion.GuardarExcel,
                exportacion.Titulo, exportacion.Detalle, exportacion.DatosVista(), exportacion.ImportesVista()))
                vista.ShowDialog(propietario);
        }

        public static void Guardar(DocumentoVenta documento, string archivo)
        {
            if (string.IsNullOrWhiteSpace(archivo))
                throw new ArgumentException("Seleccione un archivo PDF.");

            IDocument pdf = Crear(documento, Preparar(documento));
            ArchivoExportado.Guardar(archivo, ruta => pdf.GeneratePdf(ruta));
        }

        private static DocumentoVentaExportacion Preparar(DocumentoVenta datos)
        {
            if (datos == null || datos.Detalle == null || datos.Detalle.Count == 0)
                throw new ArgumentException("El documento no contiene productos.");

            return new DocumentoVentaExportacion(datos);
        }

        private static IDocument Crear(DocumentoVenta datos, DocumentoVentaExportacion exportacion)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.Letter);
                    pagina.Margin(36);
                    pagina.DefaultTextStyle(estilo => estilo.FontSize(10));
                    pagina.Header().Column(cabecera =>
                    {
                        cabecera.Item().Text(exportacion.Titulo).FontSize(21).Bold();
                        if (!string.IsNullOrWhiteSpace(datos.Restaurante))
                            cabecera.Item().PaddingTop(4).Text(datos.Restaurante).FontSize(12);
                    });
                    pagina.Content().PaddingVertical(16).Column(contenido =>
                    {
                        contenido.Spacing(7);
                        foreach (var campo in exportacion.Datos)
                            contenido.Item().Text(DocumentoVentaExportacion.TextoDato(campo));
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
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Precio unitario").Bold();
                                encabezado.Cell().Background(Colors.Grey.Lighten3).Padding(6).AlignRight().Text("Importe").Bold();
                            });
                            foreach (DetalleDocumento linea in datos.Detalle)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(linea.Producto);
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(linea.Cantidad.ToString());
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text(exportacion.Dinero(linea.PrecioUnitario));
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text(exportacion.Dinero(linea.Subtotal));
                            }
                        });
                        foreach (var campo in exportacion.Importes)
                        {
                            var texto = contenido.Item().AlignRight().Text(campo.Key + ": " + exportacion.Dinero((decimal)campo.Value));
                            if (campo.Key == "Total")
                                texto.FontSize(15).Bold();
                        }
                        if (exportacion.DatosAdicionales.Count > 0)
                            contenido.Item().PaddingTop(6).Text("Datos adicionales").Bold();
                        foreach (var campo in exportacion.DatosAdicionales)
                            contenido.Item().Text(DocumentoVentaExportacion.TextoDato(campo));
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

    }
}
