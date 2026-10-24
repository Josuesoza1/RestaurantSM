using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioBLL servicio = new UsuarioBLL();
        public FrmUsuarios()
        {
            InitializeComponent();
            VistaGestion.Listado(dgvUsuarios);
            TemaUI.Aplicar(this);
            Load += FrmUsuarios_CargarDatos;
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = servicio.ObtenerTodosLosUsuarios();
            dgvUsuarios.ClearSelection();
            ActualizarCantidad();
        }

        private void ActualizarCantidad()
        {
            lblCantidad.Text = "Usuarios mostrados: " + dgvUsuarios.Rows.Count;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvUsuarios.DataSource = servicio.BuscarUsuarios(txtBuscar.Text);
                dgvUsuarios.ClearSelection();
                ActualizarCantidad();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            try
            {
                txtBuscar.Clear();
                CargarUsuarios();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmUsuarios_CargarDatos(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.Site?.DesignMode == true)
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
                CargarUsuarios();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
