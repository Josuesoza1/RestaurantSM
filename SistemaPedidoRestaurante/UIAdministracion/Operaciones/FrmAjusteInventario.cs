using System;
using System.Globalization;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmAjusteInventario : Form
    {

        private readonly InventarioBLL servicio = new InventarioBLL();
        private readonly int productoInicial;
        public int ProductoAjustado { get; private set; }

        public FrmAjusteInventario() : this(0)
        {
        }

        internal FrmAjusteInventario(int productoInicial)
        {
            this.productoInicial = productoInicial;
            InitializeComponent();
        }

        private void cargar(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            if (!SesionActual.TieneRol(RolesSistema.Administrador))
            {
                Close();
                return;
            }
            try
            {
                InventarioProductos activos = new InventarioProductos();
                foreach (InventarioProducto producto in servicio.Listar())
                    if (producto.Activo)
                        activos.Add(producto);
                productos.DataSource = activos;
                productos.SelectedIndex = activos.FindIndex(producto => producto.IdProducto == productoInicial);
                ActualizarMovimiento();
                ActiveControl = productos.SelectedIndex < 0 ? (Control)productos : tipo;
            }
            catch (Exception error)
            {
                guardar.Enabled = false;
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudieron consultar las existencias. Cierre esta ventana y vuelva a abrirla."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void datosMovimiento_Cambiados(object sender, EventArgs e)
        {
            ActualizarMovimiento();
        }

        private void ActualizarMovimiento()
        {
            InventarioProducto producto = productos.SelectedItem as InventarioProducto;
            string movimiento = tipo.SelectedItem as string;
            guardar.Enabled = false;
            cantidad.Enabled = false;
            existencias.Text = producto == null
                ? (productos.Items.Count == 0 ? "No hay productos con inventario activo." : "Seleccione un producto para ver sus existencias.")
                : "Existencias actuales: " + producto.StockActual + " unidades.";
            if (producto == null || movimiento == null)
            {
                lblRango.Text = "Elija Entrada para sumar unidades o Salida para retirarlas.";
                return;
            }

            int maximo = GestionValidacion.MaximoMovimientoInventario(producto.StockActual, movimiento);
            if (maximo == 0)
            {
                lblRango.Text = movimiento == "Salida"
                    ? "Hay 0 unidades disponibles para retirar."
                    : "El producto ya tiene el máximo de " + int.MaxValue + " unidades.";
                return;
            }

            cantidad.Enabled = true;
            lblRango.Text = "Ingrese una cantidad entera de 1 a " + maximo + " unidades.";
            if (int.TryParse(cantidad.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out int unidades)
                && unidades >= 1 && unidades <= maximo)
            {
                int nuevoStock = movimiento == "Entrada"
                    ? producto.StockActual + unidades
                    : producto.StockActual - unidades;
                lblRango.Text += " Quedarán " + nuevoStock + " unidades.";
                guardar.Enabled = !string.IsNullOrWhiteSpace(motivo.Text);
            }
        }

        private void FrmAjusteInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool hayCambios = DialogResult != DialogResult.OK && (cantidad.TextLength > 0 || motivo.TextLength > 0);
            if (!VistaGestion.ConfirmarDescarte(this, hayCambios))
                e.Cancel = true;
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            if (!guardar.Enabled)
                return;

            try
            {
                var producto = (InventarioProducto)productos.SelectedItem;
                string movimiento = (string)tipo.SelectedItem;
                int maximo = GestionValidacion.MaximoMovimientoInventario(producto.StockActual, movimiento);
                int unidades = CamposValidacion.Entero(cantidad.Text, "cantidad", 1, maximo);
                guardar.Enabled = false;
                servicio.AjustarInventario(producto, unidades, movimiento, motivo.Text);
                ProductoAjustado = producto.IdProducto;
                MessageBox.Show(this, movimiento + " de " + unidades + " unidades registrada para «" + producto.Producto + "».",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo confirmar el movimiento. Revise el historial de inventario antes de volver a intentarlo."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                    ActualizarMovimiento();
            }
        }
    }
}
