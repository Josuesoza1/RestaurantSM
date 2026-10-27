using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPago : Form
    {
        private readonly PagoBLL servicio = new PagoBLL();
        private readonly Guid solicitud = Guid.NewGuid();
        private int pedido;
        private decimal total;
        private bool pagado;
        public FrmPago() : this(0)
        {
        }

        public FrmPago(int pedido)
        {
            this.pedido = pedido;
            InitializeComponent();
            Load += FrmPago_Load;
        }

        private void FrmPago_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            if (!SesionActual.TieneRol(1) && !SesionActual.TieneRol(2))
            {
                Close();
                return;
            }

            try
            {
                new ConfiguracionRestauranteBLL().VerificarInstalacion();
                if (pedido == 0)
                {
                    using (FrmBandejaOperaciones elegir = new FrmBandejaOperaciones("Pedidos", true, "Entregado"))
                    {
                        if (elegir.ShowDialog(this) != DialogResult.OK)
                        {
                            Close();
                            return;
                        }

                        pedido = elegir.PedidoSeleccionado;
                    }
                }

                DataRow cotizacion = servicio.Cotizar(pedido);
                total = Convert.ToDecimal(cotizacion["Total"]);
                txtMontoTotal.Text = total.ToString("N2");
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
                using (FrmConfirmarCobro confirmar = new FrmConfirmarCobro(total, metodo))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                    btnRegistrarPago.Enabled = false;
                    DataRow resultado = servicio.Cobrar(pedido, metodo, confirmar.Recibido, total, solicitud, txtNotas.Text);
                    pagado = true;
                    MessageBox.Show(this, "Pago registrado. Factura " + resultado["IdFactura"] + ". Cambio: C$ " + Convert.ToDecimal(resultado["Cambio"]).ToString("N2") + ". Puede guardar el PDF desde Facturas.");
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
