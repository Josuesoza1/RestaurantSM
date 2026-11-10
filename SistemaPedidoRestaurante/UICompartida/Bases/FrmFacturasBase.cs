using System;
using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public class FrmFacturasBase : Form
    {
        protected virtual Label LblTitulo => null;
        protected virtual Button BtnGuardarProducto => null;
        protected virtual Button BtnAnular => null;
        protected virtual DataGridView DataGridView1 => null;
        protected virtual Label LblTipoPedido => null;
        protected virtual Label LblEmpleadoFactura => null;
        protected virtual Label LblMesaFactura => null;
        protected virtual Label Label3 => null;
        protected virtual TextBox TxtClienteFactura => null;
        protected virtual TextBox TxtEmpleadoFactura => null;
        protected virtual TextBox TxtMesaFactura => null;
        protected virtual TextBox TxtImpuestoFactura => null;
        protected virtual TextBox TxtDescuentoFactura => null;
        protected virtual TextBox TxtNumeroFactura => null;
        protected virtual Label LblMetodoPagoFactura => null;
        protected virtual TextBox TxtMetodoPago => null;
        protected virtual TextBox TxtTotalFactura => null;
        protected virtual TextBox TxtSubtotalFactura => null;
        protected virtual ComboBox CmbTipoPedido => null;
        protected virtual DateTimePicker DtpFechaFactura => null;

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

        }

        protected void PrepararVista()
        {
            TxtClienteFactura.ReadOnly = TxtEmpleadoFactura.ReadOnly = TxtMesaFactura.ReadOnly = true;
            TxtNumeroFactura.ReadOnly = TxtSubtotalFactura.ReadOnly = TxtTotalFactura.ReadOnly = true;
            TxtImpuestoFactura.ReadOnly = TxtDescuentoFactura.ReadOnly = TxtMetodoPago.ReadOnly = true;
            CmbTipoPedido.Enabled = DtpFechaFactura.Enabled = false;
            VistaGestion.Listado(DataGridView1);
            DataGridView1.Columns[0].DataPropertyName = "Producto";
            DataGridView1.Columns[1].DataPropertyName = "Cantidad";
            DataGridView1.Columns[2].DataPropertyName = "PrecioUnitario";
            DataGridView1.Columns[3].DataPropertyName = "Subtotal";
        }

        protected void Procesar_FrmFacturasBase_Load(object sender, EventArgs e)
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

        private void CargarDocumento(int pedido)
        {
            documento = servicio.ObtenerDocumento(pedido, true);
            pedidoConsultado = pedido;
            Text = "Factura " + documento.Numero + " — " + documento.Estado;
            LblTitulo.Text = Text;
            TxtNumeroFactura.Text = documento.Numero;
            TxtClienteFactura.Text = documento.Cliente;
            TxtEmpleadoFactura.Text = documento.CobradoPor;
            LblEmpleadoFactura.Visible = TxtEmpleadoFactura.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.CobradoPor);
            TxtMesaFactura.Text = documento.Mesa;
            LblMesaFactura.Visible = TxtMesaFactura.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.Mesa);
            DtpFechaFactura.Value = documento.Fecha;
            DtpFechaFactura.CustomFormat = "dd/MM/yyyy";
            CmbTipoPedido.Items.Clear();
            bool tieneEntrega = DocumentoVentaExportacion.TieneDatoHistorico(documento.TipoPedido);
            if (tieneEntrega)
            {
                CmbTipoPedido.Items.Add(documento.TipoPedido);
                CmbTipoPedido.SelectedIndex = 0;
            }
            LblTipoPedido.Visible = CmbTipoPedido.Visible = tieneEntrega;
            TxtSubtotalFactura.Text = documento.Subtotal.ToString("N2");
            TxtImpuestoFactura.Text = documento.Impuestos.ToString("N2");
            TxtDescuentoFactura.Text = documento.Descuento.ToString("N2");
            Label3.Visible = TxtDescuentoFactura.Visible = documento.Descuento != 0;
            TxtTotalFactura.Text = (documento.Moneda + " " + documento.Total.ToString("N2")).Trim();
            TxtMetodoPago.Text = documento.Metodo;
            LblMetodoPagoFactura.Visible = TxtMetodoPago.Visible = DocumentoVentaExportacion.TieneDatoHistorico(documento.Metodo);
            VistaGestion.Vincular(DataGridView1, documento.Detalle);
            BtnGuardarProducto.Enabled = true;
            BtnAnular.Enabled = Permisos.PuedeVerHistorial && documento.Estado == "Pagada"
                && documento.OrigenPedido == "En el local";
            BtnAnular.Visible = BtnAnular.Enabled;
        }

        protected void Procesar_btnAnular_Click(object sender, EventArgs e)
        {
            if (!BtnAnular.Enabled || documento == null)
                return;

            try
            {
                int venta = pedidoConsultado;
                using (var confirmar = new FrmAnularVenta(documento,
                    (motivo, reponer) => RegistrarDevolucion(venta, motivo, reponer)))
                {
                    if (confirmar.ShowDialog(this) != DialogResult.OK)
                        return;
                }
                BtnAnular.Enabled = false;
                BtnAnular.Visible = false;
                MessageBox.Show(this, "La venta quedó anulada y se conservó su comprobante.", Text);
                CargarDocumento(pedidoConsultado);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RegistrarDevolucion(int venta, string motivo, bool reponer)
        {
            var vigente = servicio.ObtenerDocumento(venta, true);
            if (!string.Equals(vigente.Estado, "Anulada", StringComparison.OrdinalIgnoreCase))
                servicio.AnularVenta(venta, motivo, reponer);
        }

        protected void Procesar_guardarPdf_Click(object sender, EventArgs e)
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

        protected void Procesar_volver_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
