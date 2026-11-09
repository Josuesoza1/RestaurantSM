using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal sealed class ResumenVentasDocumento
    {
        internal static readonly string Notas = string.Join(Environment.NewLine,
            "El historial filtra por fecha de registro de la venta. Este resumen cuenta los cobros y las anulaciones en la fecha en que ocurrieron.",
            "Recibido y vuelto se muestran para pagos en efectivo cuando se registraron esos importes.",
            "El neto es lo cobrado menos las devoluciones por anulación.");
        private const string SinMovimientos = "Sin cobros ni devoluciones registrados en este período.";
        private const string Azul = "#263650";
        private const string FondoTotal = "#E8EEF5";
        private static readonly string[] Encabezados =
            { "Moneda", "Medio de pago", "Efectivo recibido", "Vuelto", "Cobrado", "Devuelto por anulación", "Neto" };
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-NI");
        private readonly string titulo;
        private readonly string periodo;
        private readonly List<FilaResumen> filas = new List<FilaResumen>();

        internal string NombreArchivo { get; }
        internal string TituloVista => titulo + " - " + periodo + (filas.Count == 0 ? " (sin movimientos)" : "");

        internal ResumenVentasDocumento(DateTime fecha, DataTable resumen, bool diario)
        {
            titulo = diario ? "Resumen diario de caja" : "Resumen mensual de ventas";
            periodo = fecha.ToString(diario ? "dd/MM/yyyy" : "MMMM yyyy", Cultura);
            string formatoArchivo = diario ? "yyyy-MM-dd" : "yyyy-MM";
            NombreArchivo = "Resumen-ventas-" + fecha.ToString(formatoArchivo, CultureInfo.InvariantCulture);
            foreach (var moneda in resumen.AsEnumerable().GroupBy(fila => fila.Field<string>("Moneda")))
            {
                DataRow efectivo = moneda.FirstOrDefault(fila =>
                    string.Equals(fila.Field<string>("MedioPago"), "Efectivo", StringComparison.OrdinalIgnoreCase));
                filas.Add(new FilaResumen
                {
                    Moneda = moneda.Key,
                    Medio = "Total de la moneda",
                    EfectivoRecibido = efectivo?.Field<decimal?>("EfectivoRecibido"),
                    Vuelto = efectivo?.Field<decimal?>("Vuelto"),
                    Cobrado = moneda.Sum(fila => fila.Field<decimal>("Cobrado")),
                    Devuelto = moneda.Sum(fila => fila.Field<decimal>("Devuelto")),
                    Neto = moneda.Sum(fila => fila.Field<decimal>("Neto")),
                    EsTotal = true
                });
                foreach (DataRow fila in moneda)
                {
                    filas.Add(new FilaResumen
                    {
                        Moneda = moneda.Key,
                        Medio = fila.Field<string>("MedioPago"),
                        EfectivoRecibido = fila.Field<decimal?>("EfectivoRecibido"),
                        Vuelto = fila.Field<decimal?>("Vuelto"),
                        Cobrado = fila.Field<decimal>("Cobrado"),
                        Devuelto = fila.Field<decimal>("Devuelto"),
                        Neto = fila.Field<decimal>("Neto")
                    });
                }
            }
        }

        internal DataTable DetalleVista()
        {
            var tabla = new DataTable();
            tabla.Columns.Add(Encabezados[0], typeof(string));
            tabla.Columns.Add(Encabezados[1], typeof(string));
            for (int columna = 2; columna < Encabezados.Length; columna++)
                tabla.Columns.Add(Encabezados[columna], typeof(decimal));
            foreach (FilaResumen fila in filas)
                tabla.Rows.Add(fila.Moneda, fila.Medio, fila.EfectivoRecibido, fila.Vuelto, fila.Cobrado, fila.Devuelto, fila.Neto);
            return tabla;
        }

        internal IDocument CrearPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(contenedor => contenedor.Page(pagina =>
            {
                pagina.Size(PageSizes.Letter.Landscape());
                pagina.Margin(36);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(10));
                pagina.Header().Column(cabecera =>
                {
                    cabecera.Item().Text(titulo).FontSize(21).Bold().FontColor(Azul);
                    cabecera.Item().PaddingTop(5).Text(periodo).FontSize(13);
                    cabecera.Item().PaddingTop(5).Text(Notas);
                });
                if (filas.Count == 0)
                    pagina.Content().PaddingTop(18).Text(SinMovimientos);
                else
                    pagina.Content().PaddingTop(18).Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            columnas.RelativeColumn(1.1f);
                            columnas.RelativeColumn(2.7f);
                            columnas.RelativeColumn(1.7f);
                            columnas.RelativeColumn(1.5f);
                            columnas.RelativeColumn(1.6f);
                            columnas.RelativeColumn(1.9f);
                            columnas.RelativeColumn(1.6f);
                        });
                        tabla.Header(cabecera =>
                        {
                            foreach (string nombre in Encabezados)
                                cabecera.Cell().Background(Azul).Padding(7).Text(nombre).FontColor(Colors.White).Bold();
                        });
                        foreach (FilaResumen fila in filas)
                        {
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).Text(fila.Moneda ?? "");
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).Text(fila.Medio);
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).AlignRight().Text(fila.EfectivoRecibido?.ToString("N2", Cultura) ?? "");
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).AlignRight().Text(fila.Vuelto?.ToString("N2", Cultura) ?? "");
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).AlignRight().Text(fila.Cobrado.ToString("N2", Cultura));
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).AlignRight().Text(fila.Devuelto.ToString("N2", Cultura));
                            tabla.Cell().Element(celda => EstiloCelda(celda, fila.EsTotal)).AlignRight().Text(fila.Neto.ToString("N2", Cultura));
                        }
                    });
                pagina.Footer().AlignCenter().Text(texto =>
                {
                    texto.Span("Página ");
                    texto.CurrentPageNumber();
                    texto.Span(" de ");
                    texto.TotalPages();
                });
            }));
        }

        private static IContainer EstiloCelda(IContainer celda, bool total)
        {
            if (total)
                celda = celda.Background(FondoTotal).DefaultTextStyle(estilo => estilo.Bold());

            return celda.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(7);
        }

        internal void GuardarExcel(string archivo)
        {
            using (var libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add("Resumen de ventas");
                hoja.Range("A1:G1").Merge().Value = titulo;
                hoja.Cell("A1").Style.Font.Bold = true;
                hoja.Cell("A1").Style.Font.FontSize = 18;
                hoja.Cell("A1").Style.Font.FontColor = XLColor.FromHtml(Azul);
                hoja.Row(1).Height = 28;
                hoja.Range("A2:G2").Merge().Value = periodo;
                var notas = hoja.Range("A3:G3").Merge();
                notas.Value = Notas;
                notas.Style.Alignment.WrapText = true;
                hoja.Row(3).Height = 46;

                for (int columna = 0; columna < Encabezados.Length; columna++)
                    hoja.Cell(4, columna + 1).Value = Encabezados[columna];

                var encabezado = hoja.Range("A4:G4");
                encabezado.Style.Fill.BackgroundColor = XLColor.FromHtml("#D8E3F0");
                encabezado.Style.Font.FontColor = XLColor.FromHtml(Azul);
                encabezado.Style.Font.Bold = true;
                hoja.Row(4).Height = 21;
                hoja.Column(1).Width = 13;
                hoja.Column(2).Width = 30;
                hoja.Columns(3, 7).Width = 23;
                hoja.SheetView.FreezeRows(4);

                int numero = 5;
                foreach (FilaResumen fila in filas)
                {
                    hoja.Cell(numero, 1).Value = fila.Moneda ?? "";
                    hoja.Cell(numero, 2).Value = fila.Medio;
                    if (fila.EfectivoRecibido.HasValue)
                        hoja.Cell(numero, 3).Value = fila.EfectivoRecibido.Value;
                    if (fila.Vuelto.HasValue)
                        hoja.Cell(numero, 4).Value = fila.Vuelto.Value;
                    hoja.Cell(numero, 5).Value = fila.Cobrado;
                    hoja.Cell(numero, 6).Value = fila.Devuelto;
                    hoja.Cell(numero, 7).Value = fila.Neto;
                    hoja.Range(numero, 3, numero, 7).Style.NumberFormat.Format = "#,##0.00";
                    if (fila.EsTotal)
                    {
                        var total = hoja.Range(numero, 1, numero, 7);
                        total.Style.Fill.BackgroundColor = XLColor.FromHtml(FondoTotal);
                        total.Style.Font.Bold = true;
                    }
                    numero++;
                }

                if (filas.Count == 0)
                    hoja.Range("A5:G5").Merge().Value = SinMovimientos;

                libro.SaveAs(archivo);
            }
        }

        private sealed class FilaResumen
        {
            public string Moneda { get; set; }
            public string Medio { get; set; }
            public decimal? EfectivoRecibido { get; set; }
            public decimal? Vuelto { get; set; }
            public decimal Cobrado { get; set; }
            public decimal Devuelto { get; set; }
            public decimal Neto { get; set; }
            public bool EsTotal { get; set; }
        }
    }
}
