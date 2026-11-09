using SistemaPedidoRestaurante.UIPrincipal;
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
        private string filtroAplicado = "";
        private (object Categoria, object Subcategoria, string Nombre, string Precio, string Codigo,
            string Descripcion, bool Disponible) datosIniciales;
        public FrmProducto()
        {
            InitializeComponent();
            ValidacionEntradaUI.Precios(txtPrecio);
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            VistaGestion.PermitirOrden(dgvProductos);
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }

        private void CargarDatos(int idSeleccionado = 0, string nuevoFiltro = null)
        {
            string filtro = nuevoFiltro ?? filtroAplicado;
            Productos resultados = _productoBLL.BuscarProductos(filtro);
            dgvProductos.AutoGenerateColumns = false;
            string[] campos =

            {
                "IdProducto",
                "CategoriaNombre",
                "Nombre",
                "Precio",
                "Codigo",
                "EstadoVenta",
                "SubcategoriaNombre"
            };
            for (int i = 0; i < campos.Length; i++)
            {
                dgvProductos.Columns[i].DataPropertyName = campos[i];
            }

            dgvProductos.Columns[3].DefaultCellStyle.Format = "N2";
            VistaGestion.VincularGestion(dgvProductos, resultados);
            filtroAplicado = filtro;
            grpListadoCliente.Text = VistaGestion.TituloListado("productos", resultados.Count, filtro);
            btnQuitarFiltro.Enabled = filtro.Length > 0;
            if (modoActual == ModoFormulario.Neutral)
                RestaurarSeleccion(idSeleccionado);
        }

        private void RestaurarSeleccion(int id)
        {
            dgvProductos.ClearSelection();
            productoSeleccionado = id > 0
                ? VistaGestion.SeleccionarRegistro<Producto>(dgvProductos, producto => producto.IdProducto == id)
                : null;
            if (productoSeleccionado == null)
                LimpiarCampos();
            else
                CargarProductoEnFormulario(productoSeleccionado);
        }

        private void CargarListas()
        {
            subcategorias = _productoBLL.ObtenerSubcategorias();
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "IdCategoria";
            cmbCategoria.DataSource = _categoriaBLL.ObtenerCategorias().Select(c => new { c.IdCategoria, Nombre = c.Nombre + (c.Activa ? "" : " (inactiva)") }).ToList();
            cmbCategoria.SelectedIndex = -1;
            CargarSubcategorias();
        }

        private void CargarSubcategorias()
        {
            int id = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0;
            List<Subcategoria> lista = new List<Subcategoria>
            {
                new Subcategoria
                {
                    IdSubcategoria = 0,
                    Nombre = "Sin subcategoría"
                }
            };
            lista.AddRange(subcategorias.Where(s => s.IdCategoria == id));
            cmbSubcategoria.DisplayMember = "Nombre";
            cmbSubcategoria.ValueMember = "IdSubcategoria";
            cmbSubcategoria.DataSource = lista;
            cmbSubcategoria.SelectedValue = 0;
        }

        private void FrmProducto_Load_1(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!SesionActual.TieneRol(1))
            {
                Close();
                return;
            }

            ActiveControl = txtTextoBuscar;

            try
            {
                CargarListas();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;
            bool editar = nuevoModo == ModoFormulario.Nuevo || nuevoModo == ModoFormulario.Editar;
            if (editar)
                datosIniciales = DatosIngresados();
            bool operacion = nuevoModo != ModoFormulario.Neutral;
            switch (nuevoModo)
            {
                case ModoFormulario.Neutral: lblOperacion.Text = "Operación actual: Seleccione una operación"; btnAccion.Text = "Guardar"; panel1.BackColor = Color.Gainsboro; break;
                case ModoFormulario.Nuevo: lblOperacion.Text = "Operación actual: Nuevo producto. El código se genera al guardar."; btnAccion.Text = "Guardar"; panel1.BackColor = Color.LightGreen; break;
                case ModoFormulario.Editar: lblOperacion.Text = "Operación actual: Editar producto «" + productoSeleccionado.Nombre + "»"; btnAccion.Text = "Guardar cambios"; panel1.BackColor = Color.LightSkyBlue; break;
                case ModoFormulario.Eliminar: lblOperacion.Text = "Operación actual: Eliminar producto «" + productoSeleccionado.Nombre + "»"; btnAccion.Text = "Eliminar"; panel1.BackColor = Color.LightCoral; break;
            }
            cmbCategoria.Enabled = cmbSubcategoria.Enabled = txtDescripcion.Enabled = editar;
            txtNombre.Enabled = txtPrecio.Enabled = txtCodigo.Enabled = chkDisponible.Enabled = btnLimpiar.Enabled = editar;
            txtCodigo.ReadOnly = nuevoModo == ModoFormulario.Nuevo;
            btnAccion.Enabled = btnCancelar.Enabled = operacion;
            agregarToolStripMenuItem.Enabled = editarToolStripMenuItem.Enabled = eliminarToolStripMenuItem.Enabled = !operacion;
            ActualizarAccionesDisponibilidad();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarProducto())
            {
                CambiarModo(ModoFormulario.Editar);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarProducto())
            {
                CambiarModo(ModoFormulario.Eliminar);
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productoSeleccionado = null;
            LimpiarCampos();
            CambiarModo(ModoFormulario.Nuevo);
        }

        private void VolverAlMenu()
        {
            Close();
        }

        private (object Categoria, object Subcategoria, string Nombre, string Precio, string Codigo,
            string Descripcion, bool Disponible) DatosIngresados()
        {
            return (cmbCategoria.SelectedValue, cmbSubcategoria.SelectedValue, txtNombre.Text, txtPrecio.Text,
                txtCodigo.Text, txtDescripcion.Text, chkDisponible.Checked);
        }

        private bool HayCambios()
        {
            return (modoActual == ModoFormulario.Nuevo || modoActual == ModoFormulario.Editar)
                && !datosIniciales.Equals(DatosIngresados());
        }

        private void FrmProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HayCambios())
                e.Cancel = !VistaGestion.Confirmar(this, "¿Volver sin guardar los cambios del producto?");
        }

        private void volverAlMenúPríncipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
        }

        private void LimpiarCampos()
        {
            cmbCategoria.SelectedIndex = -1;
            CargarSubcategorias();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            chkDisponible.Checked = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!VistaGestion.Confirmar(this, "¿Borrar los datos de los campos de este formulario?"))
                return;
            LimpiarCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (HayCambios() && !VistaGestion.Confirmar(this, "¿Descartar los cambios del producto?"))
                return;
            int id = productoSeleccionado?.IdProducto ?? 0;
            CambiarModo(ModoFormulario.Neutral);
            RestaurarSeleccion(id);
        }

        private Producto ObtenerProductoDesdeFormulario()
        {
            decimal precio = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Precio(txtPrecio.Text);
            int subcategoria = cmbSubcategoria.SelectedValue is int ? (int)cmbSubcategoria.SelectedValue : 0;
            return new Producto
            {
                IdProducto = productoSeleccionado?.IdProducto ?? 0,
                IdCategoria = cmbCategoria.SelectedValue is int ? (int)cmbCategoria.SelectedValue : 0,
                IdSubcategoria = subcategoria == 0 ? (int? )null : subcategoria,
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Codigo = modoActual == ModoFormulario.Nuevo ? null : txtCodigo.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                Disponible = chkDisponible.Checked
            };
        }

        private bool SeleccionarProducto()
        {
            productoSeleccionado = VistaGestion.RegistroSeleccionado<Producto>(dgvProductos);
            if (productoSeleccionado == null)
            {
                lblOperacion.Text = "Seleccione un producto del listado.";
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
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString("0.00", CultureInfo.CurrentCulture);
            txtCodigo.Text = producto.Codigo;
            txtDescripcion.Text = producto.Descripcion;
            chkDisponible.Checked = producto.Disponible;
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Neutral)
                {
                    return;
                }

                int id = productoSeleccionado?.IdProducto ?? 0;
                string mensaje = "Operación realizada correctamente.";
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (productoSeleccionado == null)
                    {
                        return;
                    }

                    if (!VistaGestion.Confirmar(this, "¿Eliminar permanentemente el producto " + productoSeleccionado.Nombre + "?"))
                    {
                        return;
                    }

                    _productoBLL.EliminarProducto(productoSeleccionado.IdProducto);
                }
                else
                {
                    Producto producto = ObtenerProductoDesdeFormulario();
                    _productoBLL.GuardarProducto(producto);
                    if (modoActual == ModoFormulario.Nuevo)
                    {
                        mensaje = "Producto guardado con el código " + producto.Codigo + ".";
                        if (producto.Disponible)
                            mensaje += " En Inventario, seleccione el producto y use Registrar o editar existencias para que pueda venderse en Caja.";
                    }
                }

                productoSeleccionado = null;
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos(id);
                MessageBox.Show(mensaje, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(nuevoFiltro: txtTextoBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void txtTextoBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnBuscar.PerformClick();
            }
        }

        private void btnQuitarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(nuevoFiltro: "");
                txtTextoBuscar.Clear();
                txtTextoBuscar.Focus();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ActualizarAccionesDisponibilidad()
        {
            var producto = VistaGestion.RegistroSeleccionado<Producto>(dgvProductos);
            bool puedeCambiar = modoActual == ModoFormulario.Neutral && producto != null;
            activarToolStripMenuItem.Enabled = puedeCambiar && !producto.Disponible;
            desactivarToolStripMenuItem.Enabled = puedeCambiar && producto.Disponible;
            existenciasToolStripMenuItem.Enabled = puedeCambiar;
        }

        private void existenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var producto = VistaGestion.RegistroSeleccionado<Producto>(dgvProductos);
            if (producto == null)
                return;
            try
            {
                using (var formulario = new FrmInventario(producto.IdProducto))
                    formulario.ShowDialog(this);
                CargarDatos(producto.IdProducto);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarAccionesDisponibilidad();
        }

        private void activarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarProducto())
                    return;

                if (!VistaGestion.Confirmar(this, "¿Volver a ofrecer «" + productoSeleccionado.Nombre + "» en Caja?"))
                    return;

                Producto producto = _productoBLL.ActivarProducto(productoSeleccionado.IdProducto);
                CargarDatos(producto.IdProducto);

                string mensaje = producto.PuedeVenderse
                    ? "Producto habilitado y disponible en Caja."
                    : "Producto habilitado. Motivo por el que sigue fuera de Caja: " + producto.EstadoVenta + ". Revise Inventario.";
                MessageBox.Show(this, mensaje, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarProducto())
                {
                    return;
                }

                if (!VistaGestion.Confirmar(this, "¿Dejar de ofrecer «" + productoSeleccionado.Nombre +
                    "» en Caja? Se conservará su historial."))
                {
                    return;
                }

                int id = productoSeleccionado.IdProducto;
                _productoBLL.DesactivarProducto(id);
                productoSeleccionado = null;
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos(id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
            {
                SeleccionarProducto();
            }
        }

        private void cmbCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            CargarSubcategorias();
        }
    }
}
