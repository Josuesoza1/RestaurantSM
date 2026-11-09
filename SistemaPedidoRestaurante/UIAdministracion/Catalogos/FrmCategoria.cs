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
        private string filtroAplicado = "";

        public FrmCategoria()
        {
            InitializeComponent();
            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            VistaGestion.PermitirOrden(dgvCategorias);
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }
        private void CargarDatos(int idSeleccionado = 0, string nuevoFiltro = null)
        {
            string filtro = nuevoFiltro ?? filtroAplicado;
            Categorias resultados = _categoriaBLL.BuscarCategorias(filtro);
            dgvCategorias.AutoGenerateColumns = false;
            dgvCategorias.Columns[0].DataPropertyName = "IdCategoria";
            dgvCategorias.Columns[1].DataPropertyName = "Nombre";
            dgvCategorias.Columns[2].DataPropertyName = "Descripcion";
            dgvCategorias.Columns[3].DataPropertyName = "Activa";
            VistaGestion.VincularGestion(dgvCategorias, resultados);
            filtroAplicado = filtro;
            grpListadoCliente.Text = VistaGestion.TituloListado("categorías", resultados.Count, filtro);
            btnQuitarFiltro.Enabled = filtro.Length > 0;
            if (modoActual == ModoFormulario.Neutral)
                RestaurarSeleccion(idSeleccionado);
        }

        private void RestaurarSeleccion(int id)
        {
            dgvCategorias.ClearSelection();
            categoriaSeleccionada = id > 0
                ? VistaGestion.SeleccionarRegistro<Categoria>(dgvCategorias, categoria => categoria.IdCategoria == id)
                : null;
            if (categoriaSeleccionada == null)
                LimpiarCampos();
            else
                CargarCategoriaEnFormulario(categoriaSeleccionada);
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

            ActiveControl = txtTextoBuscar;

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
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.Gainsboro;
                    break;
                case ModoFormulario.Nuevo:
                    lblOperacion.Text = "Operación actual: Nueva categoría";
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.LightGreen;
                    break;
                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar categoría «" + categoriaSeleccionada.Nombre + "»";
                    btnAccion.Text = "Guardar cambios";
                    panel1.BackColor = Color.LightSkyBlue;
                    break;
                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar categoría «" + categoriaSeleccionada.Nombre + "»";
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
            nuevoToolStripMenuItem.Enabled = editarToolStripMenuItem.Enabled = !hayOperacion;
            eliminarToolStripMenuItem.Enabled = desactivarToolStripMenuItem.Enabled = !hayOperacion;
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
            int id = categoriaSeleccionada?.IdCategoria ?? 0;
            CambiarModo(ModoFormulario.Neutral);
            RestaurarSeleccion(id);
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
            categoriaSeleccionada = VistaGestion.RegistroSeleccionado<Categoria>(dgvCategorias);
            if (categoriaSeleccionada == null)
            {
                lblOperacion.Text = "Seleccione una categoría del listado.";
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

                int id = categoriaSeleccionada?.IdCategoria ?? 0;
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
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos(id);
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

                int id = categoriaSeleccionada.IdCategoria;
                _categoriaBLL.DesactivarCategoria(id);
                categoriaSeleccionada = null;
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos(id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }
    }
}
