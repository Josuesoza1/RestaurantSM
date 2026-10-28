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
            if (!AccesoFormulario.Permitir(this, rolPermitido))
            {
                Close();
                return;
            }

            try
            {
                new ConfiguracionRestauranteBLL().VerificarInstalacion();
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
            txtNumeroFactura.Text = documento.Numero;
            txtClienteFactura.Text = documento.Cliente;
            txtEmpleadoFactura.Text = documento.Empleado;
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
            dataGridView1.DataSource = documento.Detalle;
            btnGuardarProducto.Enabled = true;
        }

        private void guardarPdf_Click(object sender, EventArgs e)
        {
            try
            {
                if (documento != null)
                    DocumentoPdf.GuardarConDialogo(this, documento);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            documento = null;
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
            MessageBox.Show(this, "Busque una factura pagada por numero, cliente o fecha. Sus importes y productos se conservan como fueron emitidos. Guardar PDF permite generar otra copia.");
        }
    }
}
