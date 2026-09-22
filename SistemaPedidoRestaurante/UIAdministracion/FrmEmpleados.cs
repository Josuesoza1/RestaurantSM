using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmEmpleados : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new EmpleadoBLL();

        public FrmEmpleados()
        {
            InitializeComponent();
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FrmMenuAdministracion menu = new FrmMenuAdministracion();
            menu.Show();
            this.Close();
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmEmpleados_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}