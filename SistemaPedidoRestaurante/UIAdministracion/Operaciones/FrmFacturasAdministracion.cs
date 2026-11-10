using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UICompartida;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmFacturasAdministracion : FrmFacturasBase
    {
        public FrmFacturasAdministracion() : this(0) { }
        public FrmFacturasAdministracion(int pedido) : base(1, pedido)
        {
            InitializeComponent();
            PrepararVista();
        }

        protected override Label LblTitulo => lblTitulo;
        protected override Button BtnGuardarProducto => btnGuardarProducto;
        protected override Button BtnAnular => btnAnular;
        protected override DataGridView DataGridView1 => dataGridView1;
        protected override Label LblTipoPedido => lblTipoPedido;
        protected override Label LblEmpleadoFactura => lblEmpleadoFactura;
        protected override Label LblMesaFactura => lblMesaFactura;
        protected override Label Label3 => label3;
        protected override TextBox TxtClienteFactura => txtClienteFactura;
        protected override TextBox TxtEmpleadoFactura => txtEmpleadoFactura;
        protected override TextBox TxtMesaFactura => txtMesaFactura;
        protected override TextBox TxtImpuestoFactura => txtImpuestoFactura;
        protected override TextBox TxtDescuentoFactura => txtDescuentoFactura;
        protected override TextBox TxtNumeroFactura => txtNumeroFactura;
        protected override Label LblMetodoPagoFactura => lblMetodoPagoFactura;
        protected override TextBox TxtMetodoPago => txtMetodoPago;
        protected override TextBox TxtTotalFactura => txtTotalFactura;
        protected override TextBox TxtSubtotalFactura => txtSubtotalFactura;
        protected override ComboBox CmbTipoPedido => cmbTipoPedido;
        protected override DateTimePicker DtpFechaFactura => dtpFechaFactura;

        private void FrmFacturasAdministracion_Load(object sender, EventArgs e)
        {
            Procesar_FrmFacturasBase_Load(sender, e);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Procesar_btnAnular_Click(sender, e);
        }

        private void guardarPdf_Click(object sender, EventArgs e)
        {
            Procesar_guardarPdf_Click(sender, e);
        }

        private void volver_Click(object sender, EventArgs e)
        {
            Procesar_volver_Click(sender, e);
        }
    }
}
