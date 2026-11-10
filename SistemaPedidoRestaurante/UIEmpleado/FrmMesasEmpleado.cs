using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UICompartida;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmMesasEmpleado : Form
    {
        private readonly MesaBLL servicio = new MesaBLL();
        private DataTable mesas;

        public FrmMesasEmpleado()
        {
            InitializeComponent();
            VistaGestion.PermitirOrden(listado);
        }

        private void FrmMesasEmpleado_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                !AccesoFormulario.PermitirOperacion(this, RolesSistema.Empleado, Permisos.PuedeAtender))
                return;
            Recargar();
            ActiveControl = txtBuscar;
        }

        private void Recargar()
        {
            try
            {
                mesas = servicio.ListarMesas();
                MostrarMesas();
            }
            catch (Exception error)
            {
                mesas = null;
                listado.DataSource = null;
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudieron cargar las mesas. Pulse Recargar para volver a intentar."),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarMesas()
        {
            if (mesas == null) return;
            listado.DataSource = BusquedaLINQ.Filtrar(mesas, txtBuscar.Text);
            listado.Columns["IdMesa"].Visible = false;
            listado.Columns["Nombre"].Visible = false;
            listado.ClearSelection();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarMesas();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            Recargar();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
