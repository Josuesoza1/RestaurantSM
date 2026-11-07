using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Image = System.Drawing.Image;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal partial class FrmVistaPrevia : Form
    {
        private readonly IDocument documento;
        private readonly string nombreArchivo;
        private readonly Action<string> guardarExcel;
        private readonly byte[][] paginas;
        private int pagina;
        private int paginaImpresion;

        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        internal FrmVistaPrevia(IDocument documento, string nombreArchivo, Action<string> guardarExcel = null) : this()
        {
            this.documento = documento;
            this.nombreArchivo = nombreArchivo;
            this.guardarExcel = guardarExcel;
            paginas = documento.GenerateImages(new ImageGenerationSettings { RasterDpi = 216 }).ToArray();
            Text = "Vista previa - " + nombreArchivo;
            btnExcel.Visible = guardarExcel != null;
            MostrarPagina();
        }

        private void MostrarPagina()
        {
            foto.Image?.Dispose();
            using (var flujo = new MemoryStream(paginas[pagina]))
            using (var imagen = Image.FromStream(flujo))
                foto.Image = new Bitmap(imagen);

            panelPagina.AutoScrollPosition = Point.Empty;
            lblPagina.Text = $"Página {pagina + 1} de {paginas.Length}";
            btnAnterior.Enabled = pagina > 0;
            btnSiguiente.Enabled = pagina < paginas.Length - 1;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            pagina--;
            MostrarPagina();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            pagina++;
            MostrarPagina();
        }

        private void chkAmpliar_CheckedChanged(object sender, EventArgs e)
        {
            foto.Dock = chkAmpliar.Checked ? DockStyle.None : DockStyle.Fill;
            foto.SizeMode = chkAmpliar.Checked ? PictureBoxSizeMode.AutoSize : PictureBoxSizeMode.Zoom;
            panelPagina.AutoScrollPosition = Point.Empty;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            Guardar("pdf", "Documento PDF (*.pdf)|*.pdf", documento.GeneratePdf);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Guardar("xlsx", "Libro de Excel (*.xlsx)|*.xlsx", guardarExcel);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Guardar(string extension, string filtro, Action<string> generar)
        {
            try
            {
                using (var elegir = new SaveFileDialog())
                {
                    elegir.Filter = filtro;
                    elegir.DefaultExt = extension;
                    elegir.FileName = nombreArchivo + "." + extension;
                    if (elegir.ShowDialog(this) != DialogResult.OK)
                        return;

                    if (!string.Equals(Path.GetExtension(elegir.FileName), "." + extension, StringComparison.OrdinalIgnoreCase))
                        throw new ArgumentException("Use la extensión ." + extension + " para este formato.");

                    ArchivoExportado.Guardar(elegir.FileName, generar);
                    MessageBox.Show(this, "Archivo guardado correctamente.", Text);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "No se pudo guardar el archivo. " + error.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (PrinterSettings.InstalledPrinters.Count == 0)
                {
                    MessageBox.Show(this, "Windows no tiene una impresora disponible.", Text);
                    return;
                }

                using (PrintDocument trabajo = CrearImpresion())
                using (var elegir = new PrintDialog { Document = trabajo, UseEXDialog = true, AllowPrintToFile = false })
                {
                    if (elegir.ShowDialog(this) == DialogResult.OK)
                        trabajo.Print();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "No se pudo imprimir. " + error.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private PrintDocument CrearImpresion()
        {
            var trabajo = new PrintDocument { DocumentName = nombreArchivo };
            trabajo.BeginPrint += trabajo_BeginPrint;
            trabajo.PrintPage += trabajo_PrintPage;
            return trabajo;
        }

        private void trabajo_BeginPrint(object sender, PrintEventArgs e)
        {
            paginaImpresion = 0;
        }

        private void trabajo_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (var flujo = new MemoryStream(paginas[paginaImpresion]))
            using (var imagen = Image.FromStream(flujo))
            {
                RectangleF area = e.PageSettings.PrintableArea;
                float escala = Math.Min(area.Width / imagen.Width, area.Height / imagen.Height);
                float ancho = imagen.Width * escala;
                float alto = imagen.Height * escala;
                e.Graphics.DrawImage(imagen, (area.Width - ancho) / 2, (area.Height - alto) / 2, ancho, alto);
            }

            paginaImpresion++;
            e.HasMorePages = paginaImpresion < paginas.Length;
        }
    }
}
