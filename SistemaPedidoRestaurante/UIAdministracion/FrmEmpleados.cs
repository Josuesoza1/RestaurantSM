using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using System.Drawing;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmEmpleados : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new EmpleadoBLL();

        private ModoFormulario modoActual;

        public FrmEmpleados()
        {
            InitializeComponent();
            CambiarModo(ModoFormulario.Neutral);
        } 

        private void CargarDatos()
        {
            try
            {
                dgvEmpleados.AutoGenerateColumns = false;
                dgvEmpleados.Columns[0].DataPropertyName = "IdEmpleado";
                dgvEmpleados.Columns[1].DataPropertyName = "PrimerNombre";
                dgvEmpleados.Columns[2].DataPropertyName = "SegundoNombre";
                dgvEmpleados.Columns[3].DataPropertyName = "PrimerApellido";
                dgvEmpleados.Columns[4].DataPropertyName = "SegundoApellido";
                dgvEmpleados.Columns[5].DataPropertyName = "Cargo";
                dgvEmpleados.Columns[6].DataPropertyName = "Genero";
                dgvEmpleados.Columns[7].DataPropertyName = "Cedula";
                dgvEmpleados.Columns[8].DataPropertyName = "Telefono";
                dgvEmpleados.DataSource = _empleadoBLL.ObtenerEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmEmpleados_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
                    lblOperacion.Text = "Operación actual: Nuevo empleado";
                    btnAccion.Text = "Guardar";
                    panel1.BackColor = Color.LightGreen;
                    break;

                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar empleado";
                    btnAccion.Text = "Guardar cambios";
                    panel1.BackColor = Color.LightSkyBlue;
                    break;

                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar empleado";
                    btnAccion.Text = "Eliminar";
                    panel1.BackColor = Color.LightCoral;
                    break;
            }

            txtPnombre.Enabled = puedeEditar;
            txtSnombre.Enabled = puedeEditar;
            txtPapellido.Enabled = puedeEditar;
            txtSapellido.Enabled = puedeEditar;
            txtTelefono.Enabled = puedeEditar;
            cmbRol.Enabled = puedeEditar;
            txtUsuario.Enabled = puedeEditar;
            txtContrasena.Enabled = puedeEditar;
            chkActivo.Enabled = puedeEditar;

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPnombre.Clear();
            txtSnombre.Clear();
            txtPapellido.Clear();
            txtSapellido.Clear();
            txtTelefono.Clear();
            cmbRol.SelectedIndex = -1;
            txtUsuario.Clear();
            txtContrasena.Clear();
            chkActivo.Checked = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CambiarModo(ModoFormulario.Neutral);
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            switch (modoActual)
            {
                case ModoFormulario.Nuevo: // Inserta empleado
                    break;

                case ModoFormulario.Editar: // Actualiza empleado
                    break;

                case ModoFormulario.Eliminar: // Elimina empleado
                    break;
            }
        }
    }
}