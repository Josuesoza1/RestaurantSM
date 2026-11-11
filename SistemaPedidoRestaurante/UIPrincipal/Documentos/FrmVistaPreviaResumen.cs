using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal partial class FrmVistaPreviaResumen : Form
    {
        private readonly IDocument documento;
        private readonly string nombreArchivo;
        private readonly Action<string> guardarExcel;

        public FrmVistaPreviaResumen()
        {
            InitializeComponent();
        }

        internal FrmVistaPreviaResumen(IDocument documento, string nombreArchivo, Action<string> guardarExcel,
            string titulo, DataTable filas, string datos = "") : this()
        {
            this.documento = documento;
            this.nombreArchivo = nombreArchivo;
            this.guardarExcel = guardarExcel;
            Text = "Vista previa - " + nombreArchivo;
            lblTitulo.Text = titulo;
            lblNotas.Text = datos;
            tablaDetalle.DataSource = filas;
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (documento == null) return;
            dialogoPdf.FileName = nombreArchivo + ".pdf";
            Guardar(dialogoPdf, "pdf", documento.GeneratePdf);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (guardarExcel == null) return;
            dialogoExcel.FileName = nombreArchivo + ".xlsx";
            Guardar(dialogoExcel, "xlsx", guardarExcel);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tablaDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tablaDetalle.ClearSelection();
        }

        private void Guardar(SaveFileDialog elegir, string extension, Action<string> generar)
        {
            try
            {
                if (elegir.ShowDialog(this) != DialogResult.OK) return;
                if (!string.Equals(Path.GetExtension(elegir.FileName), "." + extension, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Use la extensión ." + extension + " para este formato.");
                string destino = Path.GetFullPath(elegir.FileName);
                ArchivoExportado.Guardar(destino, generar);
                MessageBox.Show(this, "El archivo se guardó en la siguiente ubicación." +
                    Environment.NewLine + Environment.NewLine + destino, "Exportación completada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "No se pudo guardar el archivo. " + error.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
