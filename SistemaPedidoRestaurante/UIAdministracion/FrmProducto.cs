using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoBLL _productoBLL = new ProductoBLL();
        private readonly CategoriaBLL _categoriaBLL = new CategoriaBLL();
        private List<Subcategoria> subcategorias = new List<Subcategoria>();
        private ModoFormulario modoActual;
        private Producto productoSeleccionado;

        public FrmProducto()
        {
            InitializeComponent();
            dgvProductos.ReadOnly = true; dgvProductos.AllowUserToAddRows = false;
            dgvProductos.MultiSelect = false; dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.CellClick += (sender, e) => { if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral) SeleccionarProducto(); };
            cmbCategoria.SelectedValueChanged += (sender, e) => CargarSubcategorias();
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }
        private void CargarDatos()
        {
            dgvProductos.AutoGenerateColumns = false;
            string[] campos = { "IdProducto", "CategoriaNombre", "Nombre", "Precio", "Codigo", "Disponible", "SubcategoriaNombre" };
            for (int i = 0; i < campos.Length; i++) dgvProductos.Columns[i].DataPropertyName = campos[i];
            dgvProductos.Columns[3].DefaultCellStyle.Format = "N2";
            dgvProductos.DataSource = _productoBLL.ObtenerProductos();
        }
        private void CargarListas()
        {
            subcategorias = _productoBLL.ObtenerSubcategorias();
            cmbCategoria.DisplayMember = "Nombre"; cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = _categoriaBLL.ObtenerCategorias()
                .Select(c => new { c.IdCategoria, Nombre = c.Nombre + (c.Activa ? "" : " (inactiva)") }).ToList();
            cmbCategoria.SelectedIndex = -1;
            CargarSubcategorias();
        }
        private void CargarSubcategorias()
        {
            int id = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0;
            var lista = new List<Subcategoria> { new Subcategoria { IdSubcategoria = 0, Nombre = "Sin subcategoría" } };
            lista.AddRange(subcategorias.Where(s => s.IdCategoria == id));
            cmbSubcategoria.DisplayMember = "Nombre"; cmbSubcategoria.ValueMember = "IdSubcategoria";
            cmbSubcategoria.DataSource = lista;
            cmbSubcategoria.SelectedValue = 0;
        }
        private void FrmProducto_Load_1(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (!SesionActual.TieneRol(1)) { Close(); return; }
            try { CargarListas(); CargarDatos(); } catch (Exception ex) { MostrarError(ex); }
        }
        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e) { }
        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;
            bool editar = nuevoModo == ModoFormulario.Nuevo || nuevoModo == ModoFormulario.Editar;
            bool operacion = nuevoModo != ModoFormulario.Neutral;
            switch (nuevoModo)
            {
                case ModoFormulario.Neutral: lblOperacion.Text = "Operación actual: Seleccione una operación"; btnAccion.Text = "Acción"; panel1.BackColor = Color.Gainsboro; break;
                case ModoFormulario.Nuevo: lblOperacion.Text = "Operación actual: Nuevo producto"; btnAccion.Text = "Guardar"; panel1.BackColor = Color.LightGreen; break;
                case ModoFormulario.Editar: lblOperacion.Text = "Operación actual: Editar producto"; btnAccion.Text = "Guardar cambios"; panel1.BackColor = Color.LightSkyBlue; break;
                case ModoFormulario.Eliminar: lblOperacion.Text = "Operación actual: Retirar producto"; btnAccion.Text = "Retirar"; panel1.BackColor = Color.LightCoral; break;
            }
            cmbCategoria.Enabled = cmbSubcategoria.Enabled = txtDescripcion.Enabled = editar;
            txtNombre.Enabled = txtPrecio.Enabled = txtCodigo.Enabled = chkDisponible.Enabled = btnLimpiar.Enabled = editar;
            btnAccion.Enabled = btnCancelar.Enabled = operacion;
            dgvProductos.Enabled = txtTextoBuscar.Enabled = btnBuscar.Enabled = !operacion;
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e) { if (SeleccionarProducto()) CambiarModo(ModoFormulario.Editar); }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) { if (SeleccionarProducto()) CambiarModo(ModoFormulario.Eliminar); }
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productoSeleccionado = null; LimpiarCampos(); CambiarModo(ModoFormulario.Nuevo);
        }
        private void VolverAlMenu() { Close(); }
        private void volverAlMenúPríncipalToolStripMenuItem_Click(object sender, EventArgs e) { VolverAlMenu(); }
        private void LimpiarCampos()
        {
            cmbCategoria.SelectedIndex = -1; CargarSubcategorias();
            txtNombre.Clear(); txtPrecio.Clear(); txtCodigo.Clear(); txtDescripcion.Clear(); chkDisponible.Checked = true;
        }
        private void btnLimpiar_Click(object sender, EventArgs e) { LimpiarCampos(); }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            productoSeleccionado = null; LimpiarCampos(); CambiarModo(ModoFormulario.Neutral);
        }
        private Producto ObtenerProductoDesdeFormulario()
        {
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text.Trim().Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out precio))
                throw new ArgumentException("Ingrese un precio numérico sin separadores de miles, por ejemplo 150.50.");
            int subcategoria = cmbSubcategoria.SelectedValue is int ? (int)cmbSubcategoria.SelectedValue : 0;
            return new Producto { IdProducto = productoSeleccionado?.IdProducto ?? 0,
                IdCategoria = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0,
                IdSubcategoria = subcategoria == 0 ? (int?)null : subcategoria, Nombre = txtNombre.Text.Trim(), Precio = precio,
                Codigo = txtCodigo.Text.Trim(), Descripcion = txtDescripcion.Text.Trim(), Disponible = chkDisponible.Checked };
        }
        private bool SeleccionarProducto()
        {
            productoSeleccionado = dgvProductos.CurrentRow?.DataBoundItem as Producto;
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto del listado.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            CargarProductoEnFormulario(productoSeleccionado);
            return true;
        }
        private void CargarProductoEnFormulario(Producto producto)
        {
            cmbCategoria.SelectedValue = producto.IdCategoria;
            CargarSubcategorias();
            cmbSubcategoria.SelectedValue = producto.IdSubcategoria ?? 0;
            txtNombre.Text = producto.Nombre; txtPrecio.Text = producto.Precio.ToString("0.00", CultureInfo.CurrentCulture);
            txtCodigo.Text = producto.Codigo; txtDescripcion.Text = producto.Descripcion; chkDisponible.Checked = producto.Disponible;
        }
        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Neutral) return;
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (productoSeleccionado == null) return;
                    if (MessageBox.Show("¿Marcar como no disponible el producto " + productoSeleccionado.Nombre + "?",
                        "Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                    _productoBLL.DesactivarProducto(productoSeleccionado.IdProducto);
                }
                else _productoBLL.GuardarProducto(ObtenerProductoDesdeFormulario());
                productoSeleccionado = null;
                txtTextoBuscar.Clear(); LimpiarCampos(); CambiarModo(ModoFormulario.Neutral); CargarDatos();
                MessageBox.Show("Operación realizada correctamente.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try { dgvProductos.DataSource = _productoBLL.BuscarProductos(txtTextoBuscar.Text); }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void MostrarError(Exception ex) { MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
