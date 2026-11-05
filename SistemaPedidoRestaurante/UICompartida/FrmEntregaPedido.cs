using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmEntregaPedido : Form
    {
        private readonly decimal subtotal;
        private readonly int pedido;
        private readonly string telefono;
        private readonly EntregaPedido anterior;
        public EntregaPedido Entrega { get; private set; }

        public FrmEntregaPedido() : this(0, 0, "", null) { }

        public FrmEntregaPedido(decimal subtotal, int pedido, string telefono, EntregaPedido anterior)
        {
            this.subtotal = subtotal;
            this.pedido = pedido;
            this.telefono = telefono;
            this.anterior = anterior;
            InitializeComponent();
        }

        private void FrmEntregaPedido_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            try
            {
                PedidoBLL servicio = new PedidoBLL();
                EntregaPedido actual = anterior;
                if (actual == null && pedido > 0) actual = servicio.ObtenerEntrega(pedido);
                DataTable opciones = servicio.OpcionesEntrega(pedido);
                modalidad.DisplayMember = "Nombre";
                modalidad.ValueMember = "IdTipoEntrega";
                modalidad.DataSource = opciones;
                modalidad.SelectedValue = actual != null && actual.IdTipoEntrega > 0 ? actual.IdTipoEntrega :
                    SesionActual.TieneRol(3) ? EntregaPedido.Recoger : EntregaPedido.EnMesa;
                direccion.Text = actual == null ? "" : actual.Direccion;
                referencia.Text = actual == null ? "" : actual.Referencia;
                contacto.Text = actual == null || string.IsNullOrWhiteSpace(actual.TelefonoContacto) ? telefono : actual.TelefonoContacto;
                resumenOrigen.Text = pedido > 0 ? "Se conserva el origen del pedido registrado." :
                    SesionActual.TieneRol(3) ? "Origen: En linea (cuenta de cliente)" : "Origen: En el local (personal)";
                ActualizarTotal();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void modalidad_SelectedValueChanged(object sender, EventArgs e) { ActualizarTotal(); }

        private void ActualizarTotal()
        {
            DataRowView seleccion = modalidad.SelectedItem as DataRowView;
            bool domicilio = seleccion != null && Convert.ToInt32(seleccion["IdTipoEntrega"]) == EntregaPedido.Domicilio;
            direccion.Enabled = referencia.Enabled = contacto.Enabled = domicilio;
            decimal envio = seleccion == null ? 0 : Convert.ToDecimal(seleccion["CostoEnvio"]);
            string moneda = seleccion == null ? "" : Convert.ToString(seleccion["Moneda"]) + " ";
            importes.Text = "Productos: " + moneda + subtotal.ToString("N2") + "    Envio: " + moneda + envio.ToString("N2") +
                "\r\nTotal a pagar: " + moneda + (subtotal + envio).ToString("N2");
            confirmar.Enabled = seleccion != null;
        }

        private void confirmar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView seleccion = modalidad.SelectedItem as DataRowView;
                if (seleccion == null) throw new ArgumentException("Seleccione una modalidad.");
                EntregaPedido datos = new EntregaPedido
                {
                    IdTipoEntrega = Convert.ToInt32(seleccion["IdTipoEntrega"]),
                    CostoEnvioEsperado = Convert.ToDecimal(seleccion["CostoEnvio"]),
                    Direccion = direccion.Text, Referencia = referencia.Text, TelefonoContacto = contacto.Text
                };
                PedidoValidacion.Entrega(datos);
                if (subtotal + datos.CostoEnvioEsperado > 99999999.99m) throw new ArgumentException("El total supera el importe permitido.");
                Entrega = datos;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException error) { MessageBox.Show(this, error.Message, Text); }
        }

        private void cancelar_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
    }
}
