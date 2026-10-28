using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmConfirmarCobro : Form
    {
        private readonly decimal total;

        public decimal Recibido
        {
            get
            {
                return monto.Value;
            }
        }

        public FrmConfirmarCobro() : this(0, "Efectivo")
        {
        }

        public FrmConfirmarCobro(decimal total, string metodo)
        {
            this.total = total;
            InitializeComponent();
            resumen.Text = metodo + " — Total: C$ " + total.ToString("N2");
            monto.Value = total;
            monto.Enabled = metodo == "Efectivo";
        }

        private void monto_ValueChanged(object sender, EventArgs e)
        {
            cambio.Text = "Cambio: C$ " + (monto.Value - total).ToString("N2");
        }

        private void confirmar_Click(object sender, EventArgs e)
        {
            if (monto.Value < total)
            {
                MessageBox.Show(this, "El importe recibido no cubre el total.");
                return;
            }

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
