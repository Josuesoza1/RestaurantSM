using SistemaPedidoRestaurante.UIPrincipal;
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
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }
        private void CargarDatos()
        {
            dgvCategorias.AutoGenerateColumns = false;
            dgvCategorias.Columns[0].DataPropertyName = "IdCategoria";
            dgvCategorias.Columns[1].DataPropertyName = "Nombre";
            dgvCategorias.Columns[2].DataPropertyName = "Descripcion";
            dgvCategorias.Columns[3].DataPropertyName = "Activa";
            dgvCategorias.DataSource = _categoriaBLL.ObtenerCategorias();
        }
        private void FrmCategoria_Load(object sender, EventArgs e)
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

            try
            {
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
            {
                SeleccionarCategoria();
            }
        }

        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;
            bool puedeEditar = nuevoModo == ModoFormulario.Nuevo || nuevoModo == ModoFormulario.Editar;
            bool hayOperacion = nuevoModo != ModoFormulario.Neutral;
            switch (nuevoModo)
            {
                case ModoFormulario.Neutral:
                    lblOperacion.Text = "Operación actual: Seleccione una operación";
                    btnAccion.Text = "Acción";
                    panel1.BackColor = Color.Gainsboro;
                    break;
                case ModoFormulario.Nuevo:
                    lblOperacion.Text = "Operación actual: Nueva categoría";
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.LightGreen;
                    break;
                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar categoría";
                    btnAccion.Text = "Guardar cambios";
                    panel1.BackColor = Color.LightSkyBlue;
                    break;
                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar categoría";
                    btnAccion.Text = "Eliminar";
                    panel1.BackColor = Color.LightCoral;
                    break;
            }

            txtNombre.Enabled = puedeEditar;
            txtDescripcion.Enabled = puedeEditar;
            chkActiva.Enabled = puedeEditar;
            btnLimpiar.Enabled = puedeEditar;
            btnAccion.Enabled = hayOperacion;
            btnCancelar.Enabled = hayOperacion;
            dgvCategorias.Enabled = !hayOperacion;
            txtTextoBuscar.Enabled = !hayOperacion;
            btnBuscar.Enabled = !hayOperacion;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarCategoria())
            {
                CambiarModo(ModoFormulario.Editar);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarCategoria())
            {
                CambiarModo(ModoFormulario.Eliminar);
            }
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categoriaSeleccionada = null;
            LimpiarCampos();
            CambiarModo(ModoFormulario.Nuevo);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            categoriaSeleccionada = null;
            LimpiarCampos();
            CambiarModo(ModoFormulario.Neutral);
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            chkActiva.Checked = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private Categoria ObtenerCategoriaDesdeFormulario()
        {
            Categoria categoria = new Categoria();

            if (categoriaSeleccionada != null)
            {
                categoria.IdCategoria = categoriaSeleccionada.IdCategoria;
            }

            categoria.Nombre = txtNombre.Text.Trim();
            categoria.Descripcion = txtDescripcion.Text.Trim();
            categoria.Activa = chkActiva.Checked;

            return categoria;
        }
        private bool SeleccionarCategoria()
        {
            categoriaSeleccionada = null;
            if (dgvCategorias.CurrentRow != null)
            {
                categoriaSeleccionada = dgvCategorias.CurrentRow.DataBoundItem as Categoria;
            }
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
            txtNombre.Text = categoria.Nombre;
            txtDescripcion.Text = categoria.Descripcion;
            chkActiva.Checked = categoria.Activa;
        }
        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Neutral)
                {
                    return;
                }

                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (categoriaSeleccionada == null)
                    {
                        return;
                    }

                    DialogResult respuesta = MessageBox.Show(
                        "¿Eliminar permanentemente la categoría " + categoriaSeleccionada.Nombre + "?",
                        "Categorías", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta != DialogResult.Yes)
                    {
                        return;
                    }

                    _categoriaBLL.EliminarCategoria(categoriaSeleccionada.IdCategoria);
                }
                else
                {
                    Categoria categoria = ObtenerCategoriaDesdeFormulario();
                    _categoriaBLL.GuardarCategoria(categoria);
                }

                categoriaSeleccionada = null;
                txtTextoBuscar.Clear();
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos();
                MessageBox.Show("Operación realizada correctamente.", "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string filtro = txtTextoBuscar.Text;
                Categorias resultado = _categoriaBLL.BuscarCategorias(filtro);
                dgvCategorias.DataSource = resultado;
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Categorías", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarCategoria())
                {
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Desactivar la categoría conservando sus relaciones?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                {
                    return;
                }

                _categoriaBLL.DesactivarCategoria(categoriaSeleccionada.IdCategoria);
                categoriaSeleccionada = null;
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }
    }
}
