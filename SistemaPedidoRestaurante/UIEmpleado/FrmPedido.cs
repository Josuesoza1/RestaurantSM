using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmPedido : Form
    {
        private readonly OperacionRestauranteBLL servicio = new OperacionRestauranteBLL();
        private readonly Guid solicitud = Guid.NewGuid();
        private int cliente;
        private bool guardado;
        public FrmPedido()
        {
            InitializeComponent();
            txtCliente.ReadOnly = txtCedula.ReadOnly = txtTelefono.ReadOnly = txtEmail.ReadOnly = true;
            cmbPlato.DropDownStyle = cmbBebida.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCantidadPlato.DropDownStyle = cmbCantidadBebida.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPrecioPlato.Enabled = cmbPrecioBebida.Enabled = false;
            btnAceptar.Text = "Guardar pedido";
            btnCancelar.Text = "Volver";
            btnRegistrarPago.Text = "Ver pedidos";
            lblPlato.Text = "Producto 1:";
            lblBebida.Text = "Producto 2:";
            cmbPlato.SelectedValueChanged += producto_SelectedValueChanged;
            cmbBebida.SelectedValueChanged += producto_SelectedValueChanged;
            editarToolStripMenuItem.Text = "Consultar pedidos";
            eliminarToolStripMenuItem.Text = "Cancelar pendiente";
            FormClosing += FrmPedido_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
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
                servicio.VerificarInstalacion();
                Productos productos = new ProductoBLL().ObtenerDisponiblesParaPedido();
                cmbPlato.DisplayMember = cmbBebida.DisplayMember = "Nombre";
                cmbPlato.DataSource = productos;
                // Cada combo tiene su propia lista para que las selecciones sean independientes.
                Productos segundaLista = new Productos();
                segundaLista.AddRange(productos);
                cmbBebida.DataSource = segundaLista;
                for (int cantidad = 1; cantidad <= 999; cantidad++)
                {
                    cmbCantidadPlato.Items.Add(cantidad);
                    cmbCantidadBebida.Items.Add(cantidad);
                }

                Limpiar();
            }
            catch (Exception error)
            {
                MostrarError(error);
                btnAceptar.Enabled = false;
            }
        }

        private void producto_SelectedValueChanged(object sender, EventArgs e)
        {
            MostrarPrecio(cmbPlato, cmbPrecioPlato);
            MostrarPrecio(cmbBebida, cmbPrecioBebida);
        }

        private void MostrarPrecio(ComboBox productos, ComboBox precios)
        {
            precios.Items.Clear();
            Producto producto = productos.SelectedItem as Producto;
            if (producto != null)
            {
                precios.Items.Add(producto.Precio.ToString("N2"));
                precios.SelectedIndex = 0;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (FrmSeleccionCliente elegir = new FrmSeleccionCliente())
            {
                if (elegir.ShowDialog(this) != DialogResult.OK)
                    return;
                DataRow fila = elegir.Cliente;
                cliente = Convert.ToInt32(fila["IdCliente"]);
                txtCliente.Text = Convert.ToString(fila["Nombre"]);
                txtCedula.Text = Convert.ToString(fila["Cedula"]);
                txtTelefono.Text = Convert.ToString(fila["Telefono"]);
                txtEmail.Text = Convert.ToString(fila["Email"]);
            }
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            if (guardado)
                return;
            try
            {
                PedidoBorrador pedido = new PedidoBorrador();
                if (cmbPlato.SelectedItem is Producto)
                    pedido.Agregar((Producto)cmbPlato.SelectedItem, Convert.ToInt32(cmbCantidadPlato.SelectedItem));
                if (cmbBebida.SelectedItem is Producto)
                    pedido.Agregar((Producto)cmbBebida.SelectedItem, Convert.ToInt32(cmbCantidadBebida.SelectedItem));
                if (!VistaGestion.Confirmar(this, "¿Registrar el pedido por C$ " + pedido.Total.ToString("N2") + "?"))
                    return;
                btnAceptar.Enabled = false;
                int numero = servicio.GuardarPedido(cliente, pedido, solicitud);
                guardado = true;
                MessageBox.Show(this, "Pedido " + numero + " registrado. Puede atenderlo en la bandeja de pedidos.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
            finally
            {
                if (!IsDisposed)
                    btnAceptar.Enabled = true;
            }
        }

        private void Limpiar()
        {
            cliente = 0;
            txtCliente.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cmbPlato.SelectedIndex = cmbBebida.SelectedIndex = -1;
            if (cmbCantidadPlato.Items.Count > 0)
                cmbCantidadPlato.SelectedIndex = cmbCantidadBebida.SelectedIndex = 0;
        }

        private void limpiar_Click(object sender, EventArgs e)
        {
            if ((cliente > 0 || cmbPlato.SelectedIndex >= 0 || cmbBebida.SelectedIndex >= 0) && !VistaGestion.Confirmar(this, "¿Descartar el pedido sin guardar?"))
                return;
            Limpiar();
        }

        private void FrmPedido_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!guardado && (cliente > 0 || cmbPlato.SelectedIndex >= 0 || cmbBebida.SelectedIndex >= 0))
                e.Cancel = !VistaGestion.Confirmar(this, "¿Salir y descartar el pedido sin guardar?");
        }

        private void volver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bandeja_Click(object sender, EventArgs e)
        {
            using (FrmBandejaOperaciones formulario = new FrmBandejaOperaciones("Pedidos"))
                formulario.ShowDialog(this);
        }

        private void ayuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Busque un cliente registrado. Seleccione uno o dos productos y sus cantidades. El servidor valida precio e inventario antes de guardar. Para mas productos, el cliente puede usar Mi pedido.");
        }

        private void MostrarError(Exception error)
        {
            MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void lblPnombre_Click(object sender, EventArgs e)
        {
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
