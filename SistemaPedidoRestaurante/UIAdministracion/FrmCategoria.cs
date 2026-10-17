using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmCategoria : Form
    {
        private readonly CategoriaBLL _categoriaBLL = new CategoriaBLL();
        private ModoFormulario modoActual;
        private Categoria categoriaSeleccionada;

        public FrmCategoria()
        {
            InitializeComponent();
            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.CellClick += (sender, e) => { if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral) SeleccionarCategoria(); };
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }
        private void CargarDatos()
        {
            dgvCategorias.AutoGenerateColumns = false;
            string[] campos = { "IdCategoria", "Nombre", "Descripcion", "Activa" };
            for (int i = 0; i < campos.Length; i++) dgvCategorias.Columns[i].DataPropertyName = campos[i];
            dgvCategorias.DataSource = _categoriaBLL.ObtenerCategorias();
        }
        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (!SesionActual.TieneRol(1)) { Close(); return; }
            try { CargarDatos(); } catch (Exception ex) { MostrarError(ex); }
        }
        private void VolverAlMenu() { Close(); }
        private void volverToolStripMenuItem_Click(object sender, EventArgs e) { VolverAlMenu(); }

        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;
            bool editar = nuevoModo == ModoFormulario.Nuevo || nuevoModo == ModoFormulario.Editar;
            bool operacion = nuevoModo != ModoFormulario.Neutral;
            switch (nuevoModo)
            {
                case ModoFormulario.Neutral: lblOperacion.Text = "Operación actual: Seleccione una operación"; btnAccion.Text = "Acción"; panel1.BackColor = Color.Gainsboro; break;
                case ModoFormulario.Nuevo: lblOperacion.Text = "Operación actual: Nueva categoría"; btnAccion.Text = "Guardar"; panel1.BackColor = Color.LightGreen; break;
                case ModoFormulario.Editar: lblOperacion.Text = "Operación actual: Editar categoría"; btnAccion.Text = "Guardar cambios"; panel1.BackColor = Color.LightSkyBlue; break;
                case ModoFormulario.Eliminar: lblOperacion.Text = "Operación actual: Desactivar categoría"; btnAccion.Text = "Desactivar"; panel1.BackColor = Color.LightCoral; break;
            }
            txtNombre.Enabled = txtDescripcion.Enabled = chkActiva.Enabled = btnLimpiar.Enabled = editar;
            btnAccion.Enabled = btnCancelar.Enabled = operacion;
            dgvCategorias.Enabled = txtTextoBuscar.Enabled = btnBuscar.Enabled = !operacion;
        }
        private void editarToolStripMenuItem_Click(object sender, EventArgs e) { if (SeleccionarCategoria()) CambiarModo(ModoFormulario.Editar); }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e) { if (SeleccionarCategoria()) CambiarModo(ModoFormulario.Eliminar); }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoriaSeleccionada = null; LimpiarCampos(); CambiarModo(ModoFormulario.Nuevo);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            categoriaSeleccionada = null; LimpiarCampos(); CambiarModo(ModoFormulario.Neutral);
        }
        private void LimpiarCampos() { txtNombre.Clear(); txtDescripcion.Clear(); chkActiva.Checked = true; }
        private void btnLimpiar_Click(object sender, EventArgs e) { LimpiarCampos(); }

        private Categoria ObtenerCategoriaDesdeFormulario()
        {
            return new Categoria { IdCategoria = categoriaSeleccionada?.IdCategoria ?? 0,
                Nombre = txtNombre.Text.Trim(), Descripcion = txtDescripcion.Text.Trim(), Activa = chkActiva.Checked };
        }
        private bool SeleccionarCategoria()
        {
            categoriaSeleccionada = dgvCategorias.CurrentRow?.DataBoundItem as Categoria;
            if (categoriaSeleccionada == null)
            {
                MessageBox.Show("Seleccione una categoría del listado.", "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            CargarCategoriaEnFormulario(categoriaSeleccionada);
            return true;
        }
        private void CargarCategoriaEnFormulario(Categoria categoria)
        {
            txtNombre.Text = categoria.Nombre; txtDescripcion.Text = categoria.Descripcion; chkActiva.Checked = categoria.Activa;
        }
        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Neutral) return;
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (categoriaSeleccionada == null) return;
                    if (MessageBox.Show("¿Desactivar la categoría " + categoriaSeleccionada.Nombre + "?",
                        "Categorías", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                    _categoriaBLL.DesactivarCategoria(categoriaSeleccionada.IdCategoria);
                }
                else _categoriaBLL.GuardarCategoria(ObtenerCategoriaDesdeFormulario());
                categoriaSeleccionada = null;
                txtTextoBuscar.Clear(); LimpiarCampos(); CambiarModo(ModoFormulario.Neutral); CargarDatos();
                MessageBox.Show("Operación realizada correctamente.", "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try { dgvCategorias.DataSource = _categoriaBLL.BuscarCategorias(txtTextoBuscar.Text); }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void MostrarError(Exception ex) { MessageBox.Show(ex.Message, "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}
