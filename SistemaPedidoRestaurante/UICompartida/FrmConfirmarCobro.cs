using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmConfirmarCobro : Form
    {
        private readonly decimal total;
        private readonly bool esEfectivo;

        public decimal Recibido { get; private set; }

        public FrmConfirmarCobro() : this(0, "Efectivo")
        {
        }

        public FrmConfirmarCobro(decimal total, string metodo)
        {
            this.total = total;
            esEfectivo = metodo == "Efectivo";
            InitializeComponent();
            resumen.Text = metodo + " — Total: C$ " + total.ToString("N2");
            monto.ReadOnly = !esEfectivo;
            monto.TabStop = esEfectivo;
            pagoExacto.Visible = esEfectivo;
            if (!esEfectivo)
            {
                lblRecibido.Text = "Monto por verificar";
                monto.AccessibleName = "Monto por verificar";
                monto.Text = total.ToString("F2");
                cambio.Text = metodo == "Tarjeta"
                    ? "Cobre con el datáfono. Registre el pago cuando el datáfono lo apruebe."
                    : "Verifique en su banco que recibió la transferencia antes de registrar el pago.";
                confirmar.Text = "Registrar pago recibido";
                AcceptButton = null;
                ActiveControl = cancelar;
                Recibido = total;
                confirmar.Enabled = true;
            }
        }

        private void monto_TextChanged(object sender, EventArgs e)
        {
            if (esEfectivo)
                ActualizarCambio();
        }

        private void pagoExacto_Click(object sender, EventArgs e)
        {
            monto.Text = total.ToString("F2");
        }

        private void ActualizarCambio()
        {
            Recibido = 0;
            confirmar.Enabled = false;
            if (string.IsNullOrWhiteSpace(monto.Text))
            {
                cambio.Text = "Ingrese el monto recibido o elija Pago exacto.";
                return;
            }

            if (!PagoValidacion.IntentarLeerRecibido(monto.Text, out decimal recibido))
            {
                cambio.Text = PagoValidacion.AvisoRangoEfectivo(total);
                return;
            }

            if (recibido < total)
            {
                cambio.Text = "Faltan C$ " + (total - recibido).ToString("N2") + "." + Environment.NewLine +
                    PagoValidacion.AvisoRangoEfectivo(total);
                return;
            }

            decimal vuelto = recibido - total;
            if (vuelto > PagoValidacion.VueltoMaximo)
            {
                cambio.Text = "El vuelto máximo es C$ " + PagoValidacion.VueltoMaximo.ToString("N2") + "." +
                    Environment.NewLine + PagoValidacion.AvisoRangoEfectivo(total);
                return;
            }

            Recibido = recibido;
            cambio.Text = "Vuelto: C$ " + vuelto.ToString("N2");
            confirmar.Enabled = true;
        }

        private void confirmar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void cancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
