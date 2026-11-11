using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SistemaPedidoRestaurante.Entities;

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

        internal FrmVistaPrevia(IDocument documento, DocumentoVenta venta, Action<string> guardarExcel) : this()
        {
            if (venta == null) throw new ArgumentException("No se recibió la venta para mostrar.");
            this.documento = documento;
            nombreArchivo = venta.Numero;
            this.guardarExcel = guardarExcel;
            Text = "Vista previa - " + venta.Numero;
            lblTitulo.Text = (venta.EsFactura ? "Factura " : "Comprobante de venta ") + venta.Numero;
            txtNumero.Text = venta.Numero;
            txtFecha.Text = venta.Fecha.ToString("dd/MM/yyyy HH:mm");
            txtCliente.Text = venta.Cliente;
            txtMetodo.Text = string.IsNullOrWhiteSpace(venta.Metodo) ? "Sin cobrar" : venta.Metodo;
            txtMesa.Text = string.IsNullOrWhiteSpace(venta.Mesa) ? "No aplica" : venta.Mesa;
            txtAtendido.Text = string.IsNullOrWhiteSpace(venta.AtendidoPor) ? "Sin registrar" : venta.AtendidoPor;
            txtEstado.Text = venta.Estado;
            txtRestaurante.Text = venta.Restaurante;
            txtRegistrado.Text = venta.RegistradoPor;
            decimal subtotal = venta.ImpuestoAgregado ? venta.Subtotal : venta.Detalle.Sum(linea => linea.Subtotal);
            txtSubtotal.Text = Dinero(venta, subtotal);
            txtDescuento.Text = Dinero(venta, venta.Descuento);
            txtBaseImpuesto.Text = Dinero(venta, venta.ImpuestoAgregado ? venta.Subtotal - venta.Descuento : venta.Subtotal);
            txtImpuesto.Text = Dinero(venta, venta.Impuestos) +
                (venta.TasaImpuesto >= 0 ? " (" + venta.TasaImpuesto.ToString("0.##") + " %)" : "") +
                (venta.EsFactura && !venta.ImpuestoAgregado ? " incluido" : "");
            txtTotal.Text = Dinero(venta, venta.Total);
            txtRecibido.Text = venta.EsFactura ? Dinero(venta, venta.Recibido) : "Sin cobrar";
            txtVuelto.Text = venta.EsFactura ? Dinero(venta, venta.Cambio) : "Sin cobrar";
            VistaGestion.Vincular(tablaDetalle, venta.Detalle);
        }

        private static string Dinero(DocumentoVenta venta, decimal importe)
        {
            return venta.Moneda + " " + importe.ToString("N2");
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
