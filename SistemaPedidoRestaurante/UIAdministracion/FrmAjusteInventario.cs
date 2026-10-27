using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmAjusteInventario : Form
    {

        private readonly InventarioBLL servicio = new InventarioBLL();
        public FrmAjusteInventario()
        {
            InitializeComponent();
        }

        private void cargar(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            try
            {
                new ConfiguracionRestauranteBLL().VerificarInstalacion();
                InventarioProductos activos = new InventarioProductos();
                foreach (InventarioProducto producto in new InventarioBLL().Listar())
                    if (producto.Activo)
                        activos.Add(producto);
                productos.DataSource = activos;
                guardar.Enabled = activos.Count > 0;
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
                guardar.Enabled = false;
            }
        }

        private void productos_SelectedValueChanged(object sender, EventArgs e)
        {
            InventarioProducto producto = productos.SelectedItem as InventarioProducto;
            existencias.Text = producto == null ? "Sin registro activo" : "Unidades actuales: " + producto.StockActual;
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            try
            {
                guardar.Enabled = false;
                servicio.AjustarInventario(productos.SelectedItem as InventarioProducto, (int)cantidad.Value, Convert.ToString(tipo.SelectedItem), motivo.Text);
                MessageBox.Show(this, "Movimiento registrado.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message);
            }
            finally
            {
                if (!IsDisposed)
                    guardar.Enabled = true;
            }
        }
    }
}
