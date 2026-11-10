using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UICompartida
{
    public class FrmPagoBase : Form
    {
        protected virtual ComboBox CmbTipoPedido => null;
        protected virtual Label LblGuiaPago => null;
        protected virtual TextBox TxtNotas => null;
        protected virtual Label LblNombreComprobante => null;
        protected virtual TextBox TxtNombreComprobante => null;
        protected virtual TextBox TxtMontoTotal => null;
        protected virtual Button BtnRegistrarPago => null;

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

        }

        protected void Procesar_FrmPagoBase_Load(object sender, EventArgs e)
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
                TxtMontoTotal.Text = total.ToString("N2");
                bool nombreOpcional = Convert.ToBoolean(cotizacion["PuedeNombrarComprobante"]);
                LblNombreComprobante.Visible = TxtNombreComprobante.Visible = nombreOpcional;
                TxtNombreComprobante.Enabled = nombreOpcional;
                Text = "Cobro de venta " + pedido + " — " + Convert.ToString(cotizacion["Cliente"]);
                ActualizarBotonPago();
                ActiveControl = CmbTipoPedido;
            }
            catch (Exception error)
            {
                total = 0;
                ActualizarBotonPago();
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo consultar el importe. Vuelva a Ventas abiertas y abra el cobro de nuevo."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void Procesar_cmbTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarBotonPago();
        }

        private void ActualizarBotonPago()
        {
            bool formaElegida = CmbTipoPedido.SelectedIndex >= 0;
            LblGuiaPago.Visible = !formaElegida;
            BtnRegistrarPago.Enabled = total > 0 && formaElegida && !pagado;
        }

        protected void Procesar_btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (!BtnRegistrarPago.Enabled)
                return;
            try
            {
                string metodo = Convert.ToString(CmbTipoPedido.SelectedItem);
                string nombreComprobante = PagoValidacion.NombreParaComprobante(TxtNombreComprobante.Text);
                using (FrmConfirmarCobro confirmar = new FrmConfirmarCobro(total, metodo))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                    BtnRegistrarPago.Enabled = false;
                    DataRow resultado = servicio.Cobrar(pedido, metodo, confirmar.Recibido, total, solicitud, TxtNotas.Text,
                        desdeMostrador, nombreComprobante);
                    pagado = true;
                    string mensaje = "Pago registrado para la venta " + pedido + ".";
                    if (metodo == "Efectivo")
                        mensaje += " Vuelto: C$ " + Convert.ToDecimal(resultado["Cambio"]).ToString("N2") + ".";
                    DialogResult verFactura = MessageBox.Show(this, mensaje + Environment.NewLine +
                        "¿Ver la factura de esta venta?",
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
                            MessageBox.Show(this, "El pago se guardó. " + VistaGestion.MensajeParaUsuario(error,
                                "Puede volver a abrir el documento desde Último comprobante vigente."),
                                Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo confirmar el pago. Revise el estado de la venta antes de volver a cobrar."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                    ActualizarBotonPago();
            }
        }

        protected void Procesar_btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
