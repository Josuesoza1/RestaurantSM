using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPago : FrmPagoBase
    {
        public FrmPago() : this(0) { }
        public FrmPago(int pedido, bool desdeMostrador = false) : base(2, pedido, desdeMostrador)
        {
            InitializeComponent();
        }

        protected override ComboBox CmbTipoPedido => cmbTipoPedido;
        protected override Label LblGuiaPago => lblGuiaPago;
        protected override TextBox TxtNotas => txtNotas;
        protected override Label LblNombreComprobante => lblNombreComprobante;
        protected override TextBox TxtNombreComprobante => txtNombreComprobante;
        protected override TextBox TxtMontoTotal => txtMontoTotal;
        protected override Button BtnRegistrarPago => btnRegistrarPago;

        private void FrmPago_Load(object sender, EventArgs e)
        {
            Procesar_FrmPagoBase_Load(sender, e);
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            Procesar_btnRegistrarPago_Click(sender, e);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Procesar_btnVolver_Click(sender, e);
        }

        private void cmbTipoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            Procesar_cmbTipoPedido_SelectedIndexChanged(sender, e);
        }
    }
}
