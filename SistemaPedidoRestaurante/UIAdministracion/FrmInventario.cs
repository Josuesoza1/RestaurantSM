using System;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioBLL servicio = new InventarioBLL();
        private InventarioProductos inventario;
        private int revision;
        private int seleccionado;
        private bool cargando;
        public FrmInventario()
        {
            InitializeComponent();
            ValidacionEntradaUI.Enteros(txtStockActual, textBox1);
            VistaGestion.Listado(dgvProductoInventario);
            string[] campos =

            {
                "Producto",
                "Categoria",
                "Unidad",
                "PrecioVenta",
                "StockActual",
                "StockMinimo"
            };
            for (int i = 0; i < campos.Length; i++)
            {
                dgvProductoInventario.Columns[i].DataPropertyName = campos[i];
            }

            dgvProductoInventario.Columns[3].DefaultCellStyle.Format = "N2";
            VistaGestion.Columna(dgvProductoInventario, "Activo", "Control activo");
            txtPrecioVenta.ReadOnly = txtUnidad.ReadOnly = true;
            cmbCategoriaInventario.Enabled = false;
            cmbProductoInventario.DisplayMember = "Nombre";
            cmbProductoInventario.ValueMember = "IdProducto";
            cmbProductoInventario.SelectedValueChanged += cmbProductoInventario_SelectedValueChanged;
            dgvProductoInventario.CellClick += dgvProductoInventario_CellClick;
            TemaUI.Aplicar(this);
            Load += FrmInventario_CargarDatos;
        }

        private void Recargar()
        {
            cargando = true;
            try
            {
                cmbProductoInventario.DataSource = new ProductoBLL().ObtenerProductos();
                inventario = servicio.Listar();
                dgvProductoInventario.DataSource = inventario;
            }
            finally
            {
                cargando = false;
            }
        }

        private void Filtrar()
        {
            try
            {
                dgvProductoInventario.DataSource = servicio.Buscar(inventario, textBox2.Text, bajos.Checked);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ElegirProducto()
        {
            var p = cmbProductoInventario.SelectedItem as Producto;
            seleccionado = 0;
            revision = 0;
            txtStockActual.Text = "0";
            textBox1.Text = "0";
            chkControlActivo.Checked = true;
            txtUnidad.Text = "Unidad";
            txtPrecioVenta.Text = p == null ? "" : p.Precio.ToString("N2");
            cmbCategoriaInventario.Items.Clear();
            if (p != null)
            {
                cmbCategoriaInventario.Items.Add(p.CategoriaNombre);
                cmbCategoriaInventario.SelectedIndex = 0;
                var registro = inventario?.FirstOrDefault(item => item.IdProducto == p.IdProducto);
                if (registro != null)
                {
                    CargarFila(registro);
                }
            }
        }

        private void CargarFila(InventarioProducto item)
        {
            seleccionado = item.IdProducto;
            revision = item.Revision;
            txtStockActual.Text = item.StockActual.ToString();
            textBox1.Text = item.StockMinimo.ToString();
            chkControlActivo.Checked = item.Activo;
        }

        private void Seleccionar()
        {
            var item = dgvProductoInventario.CurrentRow?.DataBoundItem as InventarioProducto;
            if (item == null)
            {
                return;
            }

            cmbProductoInventario.SelectedValue = item.IdProducto;
            CargarFila(item);
        }

        private void Guardar(bool activo)
        {
            var p = cmbProductoInventario.SelectedItem as Producto;
            int stock = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtStockActual.Text, "stock actual", 0);
            int minimo = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(textBox1.Text, "stock mínimo", 0);
            servicio.Guardar(new InventarioProducto
            {
                IdProducto = p == null ? 0 : p.IdProducto,
                StockActual = stock,
                StockMinimo = minimo,
                Activo = activo,
                Revision = revision
            });
        }

        private void Nuevo()
        {
            dgvProductoInventario.ClearSelection();
            ElegirProducto();
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {
        }

        private void btnGuardarProducto_Click(object s, EventArgs e)
        {
            try
            {
                Guardar(chkControlActivo.Checked);
                Recargar();
                Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnCancelar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            Filtrar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var registro = dgvProductoInventario.CurrentRow?.DataBoundItem as InventarioProducto;
                if (registro == null)
                {
                    throw new ArgumentException("Seleccione un registro de inventario.");
                }

                if (VistaGestion.Confirmar(this, "¿Eliminar permanentemente el registro de inventario? Requiere stock cero y no haber sido usado en pedidos. El producto se conserva."))
                {
                    servicio.Eliminar(registro.IdProducto, registro.Revision);
                    Recargar();
                    Nuevo();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(dgvProductoInventario.CurrentRow?.DataBoundItem is InventarioProducto))
                {
                    throw new ArgumentException("Seleccione un registro de inventario.");
                }

                Seleccionar();
                if (VistaGestion.Confirmar(this, "¿Retirar el control de existencias conservando el registro? Requiere stock cero y ningún pedido pendiente."))
                {
                    Guardar(false);
                    Recargar();
                    Nuevo();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbProductoInventario_SelectedValueChanged(object s, EventArgs e)
        {
            if (!cargando)
            {
                ElegirProducto();
            }
        }

        private void agregarToolStripMenuItem_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void editarToolStripMenuItem_Click(object s, EventArgs e)
        {
            Seleccionar();
        }

        private void volverToolStripMenuItem_Click(object s, EventArgs e)
        {
            Close();
        }

        private void bajos_CheckedChanged(object s, EventArgs e)
        {
            Filtrar();
        }

        private void actualizar_Click(object s, EventArgs e)
        {
            try
            {
                Recargar();
                Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ayudaToolStripMenuItem_Click(object s, EventArgs e)
        {
            MessageBox.Show(this, "Seleccione un producto existente. Unidad y precio vienen de Productos. Guardar establece las unidades disponibles; los pedidos las descuentan. Solo se venden productos con inventario activo y existencias. Use Ajustar existencias para entradas o salidas con motivo y Movimientos para consultar el historial. Use Recargar si otra operación cambió el stock.", "Inventario");
        }

        private void dgvProductoInventario_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Seleccionar();
            }
        }

        private void FrmInventario_CargarDatos(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.Site?.DesignMode == true)
            {
                return;
            }

            if (!SesionActual.TieneRol(1))
            {
                Close();
                return;
            }

            try
            {
                Recargar();
                Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void entradaSalida_Click(object sender, EventArgs e)
        {
            using (FrmAjusteInventario formulario = new FrmAjusteInventario()) formulario.ShowDialog(this);
            try { Recargar(); Nuevo(); } catch (Exception error) { MessageBox.Show(this, error.Message); }
        }
        private void historial_Click(object sender, EventArgs e)
        {
            using (SistemaPedidoRestaurante.UIEmpleado.FrmBandejaOperaciones formulario = new SistemaPedidoRestaurante.UIEmpleado.FrmBandejaOperaciones("Movimientos")) formulario.ShowDialog(this);
        }
    }
}
