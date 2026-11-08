using System;
using System.Linq;
using System.Drawing;
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
        private ModoFormulario modoActual;
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
            dgvProductoInventario.CellFormatting += dgvProductoInventario_CellFormatting;
            txtPrecioVenta.ReadOnly = txtUnidad.ReadOnly = true;
            cmbCategoriaInventario.Enabled = false;
            cmbProductoInventario.DisplayMember = "Producto";
            cmbProductoInventario.ValueMember = "IdProducto";
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }

        private void Recargar()
        {
            cargando = true;
            try
            {
                inventario = servicio.Listar(incluirSinControl: true);
                cmbProductoInventario.DataSource = inventario;
                int cantidadBajos = inventario.Count(item => item.StockBajo);
                bajos.Text = "Solo existencias bajas (" + cantidadBajos + ")";
                Text = cantidadBajos > 0 ? "Inventario — " + cantidadBajos + " con stock bajo" : "Inventario";
                Filtrar();
            }
            finally
            {
                cargando = false;
            }
        }

        private void dgvProductoInventario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var item = dgvProductoInventario.Rows[e.RowIndex].DataBoundItem as InventarioProducto;
            if (item != null && item.StockBajo)
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.DarkRed;
            }
        }

        private void Filtrar()
        {
            try
            {
                VistaGestion.Vincular(dgvProductoInventario, servicio.Buscar(inventario, textBox2.Text, bajos.Checked));
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ElegirProducto()
        {
            var p = cmbProductoInventario.SelectedItem as InventarioProducto;
            seleccionado = 0;
            revision = 0;
            txtStockActual.Text = "0";
            textBox1.Text = "0";
            chkControlActivo.Checked = true;
            txtUnidad.Text = "Unidad";
            txtPrecioVenta.Text = p == null ? "" : p.PrecioVenta.ToString("N2");
            cmbCategoriaInventario.Items.Clear();
            if (p != null)
            {
                cmbCategoriaInventario.Items.Add(p.Categoria);
                cmbCategoriaInventario.SelectedIndex = 0;
                if (p.TieneControl)
                    CargarFila(p);
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
            ElegirProducto();
        }

        private void Guardar(bool activo)
        {
            var p = cmbProductoInventario.SelectedItem as InventarioProducto;
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

        private void btnGuardarProducto_Click(object s, EventArgs e)
        {
            if (modoActual == ModoFormulario.Neutral) return;
            try
            {
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (!VistaGestion.Confirmar(this, "¿Eliminar este registro de inventario? Requiere stock cero y no haber sido usado en pedidos.")) return;
                    servicio.Eliminar(seleccionado, revision);
                }
                else Guardar(chkControlActivo.Checked);
                Recargar(); Nuevo(); CambiarModo(ModoFormulario.Neutral);
            }
            catch (Exception error) { MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            txtStockActual.Clear(); textBox1.Clear();
        }

        private void btnCancelar_Click(object s, EventArgs e)
        {
            cargando = true;
            cmbProductoInventario.DataSource = inventario;
            cargando = false;
            Nuevo(); CambiarModo(ModoFormulario.Neutral);
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            Filtrar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Eliminar);
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarConControl())
                    return;
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
            var producto = dgvProductoInventario.CurrentRow?.DataBoundItem as InventarioProducto;
            InventarioProductos sinRegistro = new InventarioProductos();
            if (inventario != null)
                sinRegistro.AddRange(inventario.Where(p => !p.TieneControl));
            if (sinRegistro.Count == 0) { MessageBox.Show(this, "Todos los productos ya tienen inventario, o no hay productos registrados. Use Editar o registre otro producto."); return; }
            cargando = true;
            cmbProductoInventario.DataSource = sinRegistro;
            if (producto != null && !producto.TieneControl)
                cmbProductoInventario.SelectedValue = producto.IdProducto;
            cargando = false;
            Nuevo(); CambiarModo(ModoFormulario.Nuevo);
        }

        private void editarToolStripMenuItem_Click(object s, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Editar);
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
            MessageBox.Show(this, "La columna En caja indica si el producto se puede vender. Para los productos sin inventario, use Agregar control. Use Entrada o salida para ajustar existencias con motivo, e Historial para consultar los movimientos.", "Inventario");
        }

        private void dgvProductoInventario_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
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
            using (FrmMovimientosAdministracion formulario = new FrmMovimientosAdministracion()) formulario.ShowDialog(this);
        }

        private void IniciarSeleccion(ModoFormulario modo)
        {
            if (SeleccionarConControl())
                CambiarModo(modo);
        }

        private bool SeleccionarConControl()
        {
            if (dgvProductoInventario.SelectedRows.Count == 0 ||
                !(dgvProductoInventario.CurrentRow?.DataBoundItem is InventarioProducto producto))
            {
                MessageBox.Show(this, "Seleccione un producto del listado.");
                return false;
            }
            if (!producto.TieneControl)
            {
                MessageBox.Show(this, "Este producto todavía no tiene inventario. Use Agregar control.");
                return false;
            }
            Seleccionar();
            return true;
        }
        private void CambiarModo(ModoFormulario modo)
        {
            modoActual = modo;
            bool edicion = modo == ModoFormulario.Nuevo || modo == ModoFormulario.Editar;
            bool operacion = modo != ModoFormulario.Neutral;
            VistaGestion.Modo(lblOperacion, pnlOperacion, btnGuardarProducto, modo, "inventario");
            grpAgregarProducto.Enabled = edicion;
            cmbProductoInventario.Enabled = modo == ModoFormulario.Nuevo;
            dgvProductoInventario.Enabled = textBox2.Enabled = btnBuscar.Enabled = !operacion;
            btnGuardarProducto.Enabled = btnCancelar.Enabled = operacion;
            btnLimpiar.Enabled = edicion;
            agregarToolStripMenuItem.Enabled = editarToolStripMenuItem.Enabled = eliminarToolStripMenuItem.Enabled = desactivarToolStripMenuItem.Enabled = !operacion;
            movimientos.Enabled = bajos.Enabled = actualizar.Enabled = !operacion;
            registro.Enabled = !operacion;
        }
    }
}
