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
            VistaGestion.Iniciar(this, 1, CargarUsuarios);
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
            VistaGestion.Ejecutar(this, () =>
            {
                dgvUsuarios.DataSource = servicio.BuscarUsuarios(txtBuscar.Text);
                dgvUsuarios.ClearSelection();
                ActualizarCantidad();
            });
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this, () => { txtBuscar.Clear(); CargarUsuarios(); });
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
