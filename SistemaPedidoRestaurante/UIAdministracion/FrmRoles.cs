using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using System.Drawing;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmRoles : Form
    {
        private readonly RolBLL _rolBLL = new RolBLL();
        private ModoFormulario modoActual;

        public FrmRoles()
        {
            InitializeComponent();
            CambiarModo(ModoFormulario.Neutral);
        }

        private void CargarDatos()
        {
            try
            {
                dgvRoles.AutoGenerateColumns = false;
                dgvRoles.Columns[0].DataPropertyName = "IdRol";
                dgvRoles.Columns[1].DataPropertyName = "Nombre";
                dgvRoles.Columns[2].DataPropertyName = "Descripcion";
                dgvRoles.DataSource = _rolBLL.ObtenerRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmRoles_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmRoles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
                    lblOperacion.Text = "Operación actual: Nuevo rol";
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.LightGreen;
                    break;

                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar rol";
                    btnAccion.Text = "Guardar cambios";
                    panel1.BackColor = Color.LightSkyBlue;
                    break;

                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar rol";
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Neutral);
        }
    }
}