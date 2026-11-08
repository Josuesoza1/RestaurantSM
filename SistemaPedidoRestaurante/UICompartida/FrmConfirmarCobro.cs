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
            pagoExacto.Visible = esEfectivo;
            cambio.Visible = esEfectivo;
            if (!esEfectivo)
            {
                lblRecibido.Text = "Importe a cobrar";
                monto.Text = total.ToString("F2");
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
                cambio.Text = "Ingrese un monto válido, con hasta dos decimales.";
                return;
            }

            if (recibido < total)
            {
                cambio.Text = "Faltan C$ " + (total - recibido).ToString("N2");
                return;
            }

            Recibido = recibido;
            cambio.Text = "Cambio: C$ " + (recibido - total).ToString("N2");
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
