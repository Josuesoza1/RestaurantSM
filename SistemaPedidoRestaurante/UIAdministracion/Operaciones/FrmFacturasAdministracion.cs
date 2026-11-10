using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;
using System.ComponentModel;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class FrmFacturasAdministracion : Form
    {
        public FrmFacturasAdministracion() : this(0) { }
        public FrmFacturasAdministracion(int pedido)
        {
            pedidoInicial = pedido;
            rolPermitido = 1;

            InitializeComponent();
            PrepararVista();
        }

        private readonly int rolPermitido;
        private readonly FacturaBLL servicio = new FacturaBLL();
        private DocumentoVenta documento;
        private int pedidoConsultado;
        private readonly int pedidoInicial;

        private void PrepararVista()
        {
            txtClienteFactura.ReadOnly = txtEmpleadoFactura.ReadOnly = txtMesaFactura.ReadOnly = true;
            txtNumeroFactura.ReadOnly = txtSubtotalFactura.ReadOnly = txtTotalFactura.ReadOnly = true;
            txtImpuestoFactura.ReadOnly = txtDescuentoFactura.ReadOnly = txtMetodoPago.ReadOnly = true;
            cmbTipoPedido.Enabled = dtpFechaFactura.Enabled = false;
            VistaGestion.Listado(dataGridView1);
            dataGridView1.Columns[0].DataPropertyName = "Producto";
            dataGridView1.Columns[1].DataPropertyName = "Cantidad";
            dataGridView1.Columns[2].DataPropertyName = "PrecioUnitario";
            dataGridView1.Columns[3].DataPropertyName = "Subtotal";
        }

        private void FrmFacturasAdministracion_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            if (!AccesoFormulario.PermitirOperacion(this, rolPermitido, Permisos.PuedeVerHistorial))
            {
                Close();
                return;
            }

            try
            {
                if (pedidoInicial > 0)
                    CargarDocumento(pedidoInicial);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }

        private void CargarDocumento(int pedido)
        {
            documento = servicio.ObtenerDocumento(pedido, true);
            pedidoConsultado = pedido;
            Text = "Factura " + documento.Numero + " — " + documento.Estado;
            lblTitulo.Text = Text;
            txtNumeroFactura.Text = documento.Numero;
            txtClienteFactura.Text = documento.Cliente;
            txtEmpleadoFactura.Text = documento.CobradoPor;
            lblEmpleadoFactura.Visible = txtEmpleadoFactura.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.CobradoPor);
            txtMesaFactura.Text = documento.Mesa;
            lblMesaFactura.Visible = txtMesaFactura.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.Mesa);
            dtpFechaFactura.Value = documento.Fecha;
            dtpFechaFactura.CustomFormat = "dd/MM/yyyy";
            cmbTipoPedido.Items.Clear();
            bool tieneEntrega = DocumentoVentaExportacion.TieneDatoHistorico(documento.TipoPedido);
            if (tieneEntrega)
            {
                cmbTipoPedido.Items.Add(documento.TipoPedido);
                cmbTipoPedido.SelectedIndex = 0;
            }
            lblTipoPedido.Visible = cmbTipoPedido.Visible = tieneEntrega;
            txtSubtotalFactura.Text = documento.Subtotal.ToString("N2");
            txtImpuestoFactura.Text = documento.Impuestos.ToString("N2");
            txtDescuentoFactura.Text = documento.Descuento.ToString("N2");
            label3.Visible = txtDescuentoFactura.Visible = documento.Descuento != 0;
            txtTotalFactura.Text = (documento.Moneda + " " + documento.Total.ToString("N2")).Trim();
            txtMetodoPago.Text = documento.Metodo;
            lblMetodoPagoFactura.Visible = txtMetodoPago.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.Metodo);
            VistaGestion.Vincular(dataGridView1, documento.Detalle);
            btnGuardarProducto.Enabled = true;
            btnAnular.Enabled = Permisos.PuedeVerHistorial && documento.Estado == "Pagada"
                && documento.OrigenPedido == "En el local";
            btnAnular.Visible = btnAnular.Enabled;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (!btnAnular.Enabled || documento == null)
                return;

            try
            {
                int venta = pedidoConsultado;
                using (var confirmar = new FrmAnularVenta(documento,
                    (motivo, reponer) => RegistrarDevolucion(venta, motivo, reponer)))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                }
                btnAnular.Enabled = false;
                btnAnular.Visible = false;
                MessageBox.Show(this, "La venta quedó anulada y se conservó su comprobante.", Text);
                CargarDocumento(pedidoConsultado);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarDevolucion(int venta, string motivo, bool reponer)
        {
            var vigente = servicio.ObtenerDocumento(venta, true);
            if (!string.Equals(vigente.Estado, "Anulada", StringComparison.OrdinalIgnoreCase))
                servicio.AnularVenta(venta, motivo, reponer);
        }

        private void guardarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (documento != null)
                {
                    CargarDocumento(pedidoConsultado);
                    DocumentoPdf.MostrarVistaPrevia(this, documento);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }

        private void volver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
