using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMesas : Form
    {
        private readonly MesasGestionBLL servicio = new MesasGestionBLL();
        private DataTable mesas;
        private int id;
        public FrmMesas()
        {
            InitializeComponent();
            ValidacionEntradaUI.Enteros(txtNumeroMesas, txtCapacidadMesas);
            VistaGestion.Listado(dgvMesas);
            dgvMesas.Columns[0].DataPropertyName = "Numero";
            dgvMesas.Columns[1].DataPropertyName = "Capacidad";
            dgvMesas.Columns[2].DataPropertyName = "Estado";
            VistaGestion.Columna(dgvMesas, "Activa", "Activa");
            cmbEstadoMesas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstadoMesas.Items.Clear();
            cmbEstadoMesas.Items.AddRange(new object[]
            {
                "Disponible",
                "Ocupada",
                "Reservada",
                "En limpieza"
            });
            dgvMesas.CellClick += dgvMesas_CellClick;
            TemaUI.Aplicar(this);
            Load += FrmMesas_CargarDatos;
        }

        private void Recargar()
        {
            mesas = servicio.Listar();
            dgvMesas.DataSource = mesas;
        }

        private void Guardar(bool activa)
        {
            int numero = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtNumeroMesas.Text, "número de mesa", 1);
            int capacidad = SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtCapacidadMesas.Text, "capacidad", 1, 20);
            servicio.Guardar(id, numero, capacidad, cmbEstadoMesas.SelectedItem as string, activa);
        }

        private void Seleccionar()
        {
            var f = VistaGestion.Fila(dgvMesas);
            if (f == null)
            {
                return;
            }

            id = (int)f["IdMesa"];
            txtNumeroMesas.Text = f["Numero"].ToString();
            txtCapacidadMesas.Text = f["Capacidad"].ToString();
            cmbEstadoMesas.SelectedItem = f["Estado"].ToString();
            chkActiva.Checked = (bool)f["Activa"];
        }

        private void Nuevo()
        {
            id = 0;
            txtNumeroMesas.Clear();
            txtCapacidadMesas.Clear();
            cmbEstadoMesas.SelectedItem = "Disponible";
            chkActiva.Checked = true;
            dgvMesas.ClearSelection();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardarMesas_Click(object s, EventArgs e)
        {
            try
            {
                Guardar(chkActiva.Checked);
                Recargar();
                Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnCancelarMesas_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscarMesas_Click(object s, EventArgs e)
        {
            try
            {
                dgvMesas.DataSource = servicio.Buscar(mesas, txtBuscarMesas.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fila = VistaGestion.Fila(dgvMesas);
                if (fila == null)
                {
                    throw new ArgumentException("Seleccione una mesa del listado.");
                }

                if (VistaGestion.Confirmar(this, "¿Eliminar permanentemente esta mesa? No debe tener pedidos ni reservas en su historial."))
                {
                    servicio.Eliminar((int)fila["IdMesa"]);
                    Recargar();
                    Nuevo();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (VistaGestion.Fila(dgvMesas) == null)
                {
                    throw new ArgumentException("Seleccione una mesa del listado.");
                }

                Seleccionar();
                if (VistaGestion.Confirmar(this, "¿Desactivar esta mesa conservando su historial?"))
                {
                    Guardar(false);
                    Recargar();
                    Nuevo();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void agregarToolStripMenuItem_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void editarToolStripMenuItem_Click(object s, EventArgs e)
        {
            Seleccionar();
        }

        private void volverToolStripMenuItem1_Click(object s, EventArgs e)
        {
            Close();
        }

        private void ayudaToolStripMenuItem_Click(object s, EventArgs e)
        {
            MessageBox.Show(this, "Agregue o seleccione una mesa. Para reactivarla, marque Activa y guarde. Capacidad: 1 a 20 personas.", "Mesas");
        }

        private void dgvMesas_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Seleccionar();
            }
        }

        private void FrmMesas_CargarDatos(object sender, EventArgs e)
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
                Recargar();
                Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
