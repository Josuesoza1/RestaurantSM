using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal partial class FrmVistaPrevia : Form
    {
        private readonly IDocument documento;
        private readonly string nombreArchivo;
        private readonly Action<string> guardarExcel;

        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        internal FrmVistaPrevia(IDocument documento, string nombreArchivo, Action<string> guardarExcel,
            string titulo, DataTable filas, string datos = "", string importes = "") : this()
        {
            this.documento = documento;
            this.nombreArchivo = nombreArchivo;
            this.guardarExcel = guardarExcel;
            Text = "Vista previa - " + nombreArchivo;
            lblTitulo.Text = titulo;
            txtDatos.Text = datos;
            txtImportes.Text = importes;
            txtImportes.Visible = importes.Length > 0;
            panelDatos.SetColumnSpan(txtDatos, importes.Length > 0 ? 1 : 2);
            if (importes.Length == 0)
                panelDatos.Height = txtDatos.Font.Height * 5 + panelDatos.Padding.Vertical + txtDatos.Margin.Vertical;
            panelDatos.Visible = datos.Length > 0 || importes.Length > 0;
            VistaGestion.PermitirOrden(tablaDetalle);
            tablaDetalle.DataSource = filas;
        }

        private void tablaDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn columna in tablaDetalle.Columns)
            {
                if (columna.ValueType == typeof(decimal))
                {
                    columna.DefaultCellStyle.Format = "N2";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (columna.Name == "Producto" || columna.Name == "Medio de pago")
                    columna.FillWeight = 210;
            }
            tablaDetalle.ClearSelection();
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

                    string destino = Path.GetFullPath(elegir.FileName);
                    ArchivoExportado.Guardar(destino, generar);
                    MessageBox.Show(this, "El archivo se guardó en la siguiente ubicación." +
                        Environment.NewLine + Environment.NewLine + destino, "Exportación completada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "No se pudo guardar el archivo. " + error.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
