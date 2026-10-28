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
        private ModoFormulario modoActual;
        private Productos catalogo;
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
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
            Load += FrmInventario_CargarDatos;
        }

        private void Recargar()
        {
            cargando = true;
            try
            {
                catalogo = new ProductoBLL().ObtenerProductos();
                cmbProductoInventario.DataSource = catalogo;
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
            cmbProductoInventario.DataSource = catalogo;
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
                if (dgvProductoInventario.SelectedRows.Count == 0 ||
                    !(dgvProductoInventario.CurrentRow?.DataBoundItem is InventarioProducto))
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
            Productos sinRegistro = new Productos();
            if (catalogo != null && inventario != null)
                sinRegistro.AddRange(catalogo.Where(p => !inventario.Any(i => i.IdProducto == p.IdProducto)));
            if (sinRegistro.Count == 0) { MessageBox.Show(this, "Todos los productos ya tienen inventario, o no hay productos registrados. Use Editar o registre otro producto."); return; }
            cargando = true;
            cmbProductoInventario.DataSource = sinRegistro;
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
            MessageBox.Show(this, "Seleccione un producto existente. Unidad y precio vienen de Productos. Guardar establece las unidades disponibles; los pedidos las descuentan. Solo se venden productos con inventario activo y existencias. Use Ajustar existencias para entradas o salidas con motivo y Movimientos para consultar el historial. Use Recargar si otra operación cambió el stock.", "Inventario");
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
            if (dgvProductoInventario.SelectedRows.Count == 0) { MessageBox.Show(this, "Seleccione un registro de inventario."); return; }
            Seleccionar(); CambiarModo(modo);
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
        }
    }
}
