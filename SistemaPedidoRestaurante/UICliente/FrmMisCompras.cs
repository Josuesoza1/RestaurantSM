using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMisCompras : Form
    {
        private readonly ComprasClienteBLL servicio = new ComprasClienteBLL();
        private DataTable pedidos;
        public FrmMisCompras()
        {
            InitializeComponent();
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            dgvPedidos.CellClick += dgvPedidos_CellClick;
            TemaUI.Aplicar(this);
            Load += FrmMisCompras_CargarDatos;
        }

        private void Recargar()
        {
            pedidos = servicio.Listar();
            dgvPedidos.DataSource = pedidos;
            dgvDetalle.DataSource = null;
            dgvPedidos.ClearSelection();
            if (dgvPedidos.Columns.Contains("Total"))
            {
                dgvPedidos.Columns["Total"].DefaultCellStyle.Format = "N2";
            }
        }

        private DataRow Seleccionar()
        {
            var f = VistaGestion.Fila(dgvPedidos);
            if (f == null)
            {
                throw new ArgumentException("Seleccione un pedido.");
            }

            if (f["Estado"].ToString() != "Pendiente")
            {
                throw new ArgumentException("Solo puede cambiar pedidos pendientes.");
            }

            return f;
        }

        private void Detalle()
        {
            var f = VistaGestion.Fila(dgvPedidos);
            if (f == null)
            {
                return;
            }

            dgvDetalle.DataSource = servicio.Detalle((int)f["IdPedido"]);
            foreach (string c in new[]
            {
                "IdProducto",
                "IdCategoria"
            }

            )
            {
                if (dgvDetalle.Columns.Contains(c))
                {
                    dgvDetalle.Columns[c].Visible = false;
                }
            }

            foreach (string c in new[]
            {
                "PrecioUnitario",
                "Subtotal"
            }

            )
            {
                if (dgvDetalle.Columns.Contains(c))
                {
                    dgvDetalle.Columns[c].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void btnVolver_Click(object s, EventArgs e)
        {
            Close();
        }

        private void btnActualizar_Click(object s, EventArgs e)
        {
            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object s, EventArgs e)
        {
            try
            {
                var fila = Seleccionar();
                using (FrmPedidoCliente f = new FrmPedidoCliente((int)fila["IdPedido"]))
                {
                    f.ShowDialog(this);
                }

                Recargar();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelarPedido_Click(object s, EventArgs e)
        {
            try
            {
                var f = Seleccionar();
                if (VistaGestion.Confirmar(this, "¿Cancelar el pedido y devolver las existencias?"))
                {
                    servicio.Cancelar((int)f["IdPedido"]);
                    Recargar();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtBuscar_TextChanged(object s, EventArgs e)
        {
            try
            {
                dgvPedidos.DataSource = servicio.Buscar(pedidos, txtBuscar.Text);
                dgvDetalle.DataSource = null;
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvPedidos_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    Detalle();
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void FrmMisCompras_CargarDatos(object sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime || this.Site?.DesignMode == true)
            {
                return;
            }

            if (!SesionActual.TieneRol(3))
            {
                Close();
                return;
            }

            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
