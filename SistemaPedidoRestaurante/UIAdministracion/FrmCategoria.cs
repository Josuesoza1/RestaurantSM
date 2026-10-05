using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using System.Drawing;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmCategoria : Form
    {
        private readonly CategoriaBLL _categoriaBLL = new CategoriaBLL();

        private ModoFormulario modoActual;

        public FrmCategoria()
        {
            InitializeComponent();
            CambiarModo(ModoFormulario.Neutral);
        }

        private void CargarDatos()
        {
            try
            {
                dgvCategorias.AutoGenerateColumns = false;
                dgvCategorias.Columns[0].DataPropertyName = "IdCategoria";
                dgvCategorias.Columns[1].DataPropertyName = "Nombre";
                dgvCategorias.Columns[2].DataPropertyName = "Descripcion";
                dgvCategorias.DataSource = _categoriaBLL.ObtenerCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    

    // Aquí inician los cambios en el código:
     // Como eliminé el botón volver reutilicé el código y lo volví un método para el menustrip
    private void VolverAlMenu()
        {
            FrmMenuAdministracion menu = new FrmMenuAdministracion();
            menu.Show();
            this.Hide();
        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
        }

        // Cambios en el código para hacer que funcionen los modes

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

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Nuevo);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Neutral);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            switch (modoActual)
            {
                case ModoFormulario.Nuevo: // Inserta categoría
                    break;

                case ModoFormulario.Editar: // Actualiza categoría
                    break;

                case ModoFormulario.Eliminar: // Elimina categoría
                    break;
            }
        }
    }
}