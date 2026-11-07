using System;
using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmFacturasBase : Form
    {
        private readonly int rolPermitido;
        private readonly FacturaBLL servicio = new FacturaBLL();
        private DocumentoVenta documento;
        private int pedidoConsultado;
        private readonly int pedidoInicial;
        public FrmFacturasBase() : this(0, 0)
        {
        }

        protected FrmFacturasBase(int rol, int pedido)
        {
            pedidoInicial = pedido;
            rolPermitido = rol;
            InitializeComponent();
            txtClienteFactura.ReadOnly = txtEmpleadoFactura.ReadOnly = txtMesaFactura.ReadOnly = true;
            txtNumeroFactura.ReadOnly = txtSubtotalFactura.ReadOnly = txtTotalFactura.ReadOnly = true;
            txtImpuestoFactura.ReadOnly = txtDescuentoFactura.ReadOnly = txtMetodoPago.ReadOnly = true;
            cmbTipoPedido.Enabled = dtpFechaFactura.Enabled = false;
            dtpFechaFactura.Format = DateTimePickerFormat.Custom;
            dtpFechaFactura.CustomFormat = "dd/MM/yy";
            VistaGestion.Listado(dataGridView1);
            dataGridView1.Columns[0].DataPropertyName = "Producto";
            dataGridView1.Columns[1].DataPropertyName = "Cantidad";
            dataGridView1.Columns[2].DataPropertyName = "PrecioUnitario";
            dataGridView1.Columns[3].DataPropertyName = "Subtotal";
            Load += FrmFacturasBase_Load;
        }

        private void FrmFacturasBase_Load(object sender, EventArgs e)
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

        private void consultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmBandejaOperaciones elegir = PantallasPersonal.CrearBandeja("Facturas", true, txtBuscarFactura.Text))
                {
                    if (elegir.ShowDialog(this) != DialogResult.OK)
                        return;
                    CargarDocumento(elegir.PedidoSeleccionado);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarDocumento(int pedido)
        {
            documento = servicio.ObtenerDocumento(pedido, true);
            pedidoConsultado = pedido;
            Text = "Factura " + documento.Numero + " — " + documento.Estado;
            txtNumeroFactura.Text = documento.Numero;
            txtClienteFactura.Text = documento.Cliente;
            txtEmpleadoFactura.Text = documento.CobradoPor;
            txtMesaFactura.Text = string.IsNullOrEmpty(documento.Mesa) ? "Sin mesa" : documento.Mesa;
            dtpFechaFactura.Value = documento.Fecha;
            cmbTipoPedido.Items.Clear();
            cmbTipoPedido.Items.Add(documento.TipoPedido);
            cmbTipoPedido.SelectedIndex = 0;
            txtSubtotalFactura.Text = documento.Subtotal.ToString("N2");
            txtImpuestoFactura.Text = documento.Impuestos.ToString("N2");
            txtDescuentoFactura.Text = documento.Descuento.ToString("N2");
            txtTotalFactura.Text = documento.Total.ToString("N2");
            txtMetodoPago.Text = documento.Metodo;
            VistaGestion.Vincular(dataGridView1, documento.Detalle);
            btnGuardarProducto.Enabled = true;
            btnAnular.Enabled = Permisos.PuedeVerHistorial && documento.Estado == "Pagada"
                && documento.OrigenPedido == "En el local";
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (!btnAnular.Enabled || documento == null)
                return;

            try
            {
                using (var confirmar = new FrmAnularVenta(documento))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                    servicio.AnularVenta(pedidoConsultado, confirmar.Motivo, confirmar.ReponerInventario);
                }
                btnAnular.Enabled = false;
                MessageBox.Show(this, "La venta quedó anulada y se conservó su comprobante.", Text);
                CargarDocumento(pedidoConsultado);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            documento = null;
            pedidoConsultado = 0;
            btnAnular.Enabled = false;
            btnGuardarProducto.Enabled = false;
            txtNumeroFactura.Clear();
            txtClienteFactura.Clear();
            txtEmpleadoFactura.Clear();
            txtMesaFactura.Clear();
            txtSubtotalFactura.Clear();
            txtImpuestoFactura.Clear();
            txtDescuentoFactura.Clear();
            txtTotalFactura.Clear();
            txtMetodoPago.Clear();
            dataGridView1.DataSource = null;
        }

        private void volver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ayuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Busque una factura por número, cliente o fecha. Si la venta está pagada, puede registrar una anulación. Las facturas anuladas siguen disponibles para consulta y PDF.");
        }
    }
}
