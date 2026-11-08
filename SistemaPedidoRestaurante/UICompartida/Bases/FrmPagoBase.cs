using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmPagoBase : Form
    {
        private readonly int rolPermitido;
        private readonly bool desdeMostrador;
        private readonly PagoBLL servicio = new PagoBLL();
        private readonly Guid solicitud = Guid.NewGuid();
        private int pedido;
        private decimal total;
        private bool pagado;
        public FrmPagoBase() : this(0, 0)
        {
        }

        protected FrmPagoBase(int rol, int pedido, bool desdeMostrador = false)
        {
            this.pedido = pedido;
            this.desdeMostrador = desdeMostrador;
            rolPermitido = rol;
            InitializeComponent();
        }

        private void FrmPagoBase_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            if (!AccesoFormulario.PermitirOperacion(this, rolPermitido, Permisos.PuedeCobrar))
            {
                Close();
                return;
            }

            try
            {
                if (pedido == 0)
                {
                    using (FrmBandejaOperaciones elegir = PantallasPersonal.CrearBandeja("Pedidos", true))
                    {
                        if (elegir.ShowDialog(this) != DialogResult.OK)
                        {
                            Close();
                            return;
                        }

                        pedido = elegir.PedidoSeleccionado;
                    }
                }

                DataRow cotizacion = servicio.Cotizar(pedido, desdeMostrador);
                total = Convert.ToDecimal(cotizacion["Total"]);
                txtMontoTotal.Text = total.ToString("N2");
                bool nombreOpcional = Convert.ToBoolean(cotizacion["PuedeNombrarComprobante"]);
                lblNombreComprobante.Visible = txtNombreComprobante.Visible = nombreOpcional;
                txtNombreComprobante.Enabled = nombreOpcional;
                Text = "Cobro de pedido " + pedido + " — " + Convert.ToString(cotizacion["Cliente"]);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
                btnRegistrarPago.Enabled = false;
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (pagado)
                return;
            try
            {
                string metodo = Convert.ToString(cmbTipoPedido.SelectedItem);
                string nombreComprobante = PagoValidacion.NombreParaComprobante(txtNombreComprobante.Text);
                using (FrmConfirmarCobro confirmar = new FrmConfirmarCobro(total, metodo))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                    btnRegistrarPago.Enabled = false;
                    DataRow resultado = servicio.Cobrar(pedido, metodo, confirmar.Recibido, total, solicitud, txtNotas.Text,
                        desdeMostrador, nombreComprobante);
                    pagado = true;
                    DialogResult verFactura = MessageBox.Show(this, "Pago registrado. Factura " + resultado["IdFactura"] + ". Cambio: C$ " + Convert.ToDecimal(resultado["Cambio"]).ToString("N2") + ". ¿Ver la factura de esta venta?",
                        "Cobro confirmado", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (verFactura == DialogResult.Yes)
                    {
                        try
                        {
                            var documento = new FacturaBLL().ObtenerDocumento(pedido, true, solicitud);
                            DocumentoPdf.MostrarVistaPrevia(this, documento);
                        }
                        catch (Exception error)
                        {
                            MessageBox.Show(this, "El cobro se guardó. No se pudo mostrar la factura: " + error.Message);
                        }
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                    btnRegistrarPago.Enabled = !pagado;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
