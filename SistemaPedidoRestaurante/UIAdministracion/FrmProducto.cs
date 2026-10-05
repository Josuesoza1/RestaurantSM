using SistemaPedidoRestaurante.BLL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmProducto : Form
    {
        private readonly ProductoBLL _productoBLL = new ProductoBLL();

        private ModoFormulario modoActual;

        public FrmProducto()
        {
            InitializeComponent();
            CambiarModo(ModoFormulario.Neutral);
        }

        private void CargarDatos()
        {
            try
            {
                dgvProductos.AutoGenerateColumns = false;

                dgvProductos.Columns[0].DataPropertyName = "IdProducto";
                dgvProductos.Columns[1].DataPropertyName = "CategoriaNombre";
                dgvProductos.Columns[2].DataPropertyName = "Nombre";
                dgvProductos.Columns[3].DataPropertyName = "Precio";
                dgvProductos.Columns[4].DataPropertyName = "Codigo";
                dgvProductos.Columns[5].DataPropertyName = "Disponible";

                dgvProductos.DataSource = _productoBLL.ObtenerProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmProducto_Load_1(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;

            bool puedeEditar = modoActual == ModoFormulario.Nuevo ||
                               modoActual == ModoFormulario.Editar;

            bool hayOperacion = modoActual != ModoFormulario.Neutral;

            switch (modoActual)
            {
                case ModoFormulario.Neutral:
                    lblOperacion.Text = "Operación actual: Seleccione una operación";
                    btnAccion.Text = "Acción";
                    panel1.BackColor = Color.Gainsboro;
                    break;

                case ModoFormulario.Nuevo:
                    lblOperacion.Text = "Operación actual: Nuevo producto";
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.LightGreen;
                    break;

                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar producto";
                    btnAccion.Text = "Guardar cambios";
                    panel1.BackColor = Color.LightSkyBlue;
                    break;

                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar producto";
                    btnAccion.Text = "Eliminar";
                    panel1.BackColor = Color.LightCoral;
                    break;
            }

            cmbCategoria.Enabled = puedeEditar;
            txtNombre.Enabled = puedeEditar;
            txtPrecio.Enabled = puedeEditar;
            txtCodigo.Enabled = puedeEditar;
            chkDisponible.Enabled = puedeEditar;

            btnAccion.Enabled = hayOperacion;
            btnLimpiar.Enabled = puedeEditar;
            btnCancelar.Enabled = hayOperacion;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Editar);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Eliminar);
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Nuevo);
        }

        private void VolverAlMenu()
        {
            FrmMenuAdministracion menu = new FrmMenuAdministracion();
            menu.Show();
            this.Hide();
        }
        private void volverAlMenúPríncipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbCategoria.SelectedIndex = -1;
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCodigo.Clear();
            chkDisponible.Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Neutral);
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            switch (modoActual)
            {
                case ModoFormulario.Nuevo:
                    break;

                case ModoFormulario.Editar:
                    break;

                case ModoFormulario.Eliminar:
                    break;
            }
        }
    }
}