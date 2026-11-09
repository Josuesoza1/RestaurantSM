using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMesas : Form
    {
        private readonly MesaBLL servicio = new MesaBLL();
        private DataTable mesas;
        private int id;
        private bool activa;
        private ModoFormulario modoActual;
        public FrmMesas()
        {
            InitializeComponent();
            ValidacionEntradaUI.Enteros(txtNumeroMesas, txtCapacidadMesas);
            VistaGestion.Listado(dgvMesas);
            dgvMesas.Columns[0].DataPropertyName = "Numero";
            dgvMesas.Columns[1].DataPropertyName = "Capacidad";
            dgvMesas.Columns[2].DataPropertyName = "Estado";
            dgvMesas.CellClick += dgvMesas_CellClick;
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
            Load += FrmMesas_CargarDatos;
        }

        private void Recargar()
        {
            mesas = servicio.Listar();
            VistaGestion.MostrarListadoGestion(dgvMesas, servicio.Buscar(mesas, txtBuscarMesas.Text));
        }

        private void Guardar()
        {
            int numero = CamposValidacion.Entero(txtNumeroMesas.Text, "número de mesa", 1, GestionValidacion.NumeroMesaMaximo);
            int capacidad = CamposValidacion.Entero(txtCapacidadMesas.Text, "capacidad", 1, GestionValidacion.CapacidadMesaMaxima);
            string estado = cmbEstadoMesas.SelectedItem as string;
            servicio.Guardar(id, numero, capacidad, estado, activa || estado == "Disponible");
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
            activa = (bool)f["Activa"];
        }

        private void Nuevo()
        {
            id = 0;
            txtNumeroMesas.Clear();
            txtCapacidadMesas.Clear();
            cmbEstadoMesas.SelectedItem = "Disponible";
            activa = true;
        }

        private void volverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardarMesas_Click(object s, EventArgs e)
        {
            if (modoActual == ModoFormulario.Neutral) return;
            try
            {
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (!VistaGestion.Confirmar(this, "¿Eliminar permanentemente la mesa? Debe carecer de pedidos y reservas.")) return;
                    servicio.Eliminar(id);
                }
                else Guardar();
                Recargar(); Nuevo(); CambiarModo(ModoFormulario.Neutral);
            }
            catch (Exception error) { MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            int anterior = id;
            bool estabaActiva = activa;
            object estado = cmbEstadoMesas.SelectedItem;
            Nuevo();
            if (modoActual == ModoFormulario.Editar)
            {
                id = anterior;
                activa = estabaActiva;
                cmbEstadoMesas.SelectedItem = estado;
            }
        }

        private void btnCancelarMesas_Click(object s, EventArgs e)
        {
            Nuevo(); CambiarModo(ModoFormulario.Neutral);
        }

        private void btnBuscarMesas_Click(object s, EventArgs e)
        {
            try
            {
                VistaGestion.MostrarListadoGestion(dgvMesas, servicio.Buscar(mesas, txtBuscarMesas.Text));
                if (modoActual == ModoFormulario.Neutral)
                    Nuevo();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Eliminar);
        }

        private void agregarToolStripMenuItem_Click(object s, EventArgs e)
        {
            Nuevo(); CambiarModo(ModoFormulario.Nuevo);
        }

        private void editarToolStripMenuItem_Click(object s, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Editar);
        }

        private void volverToolStripMenuItem1_Click(object s, EventArgs e)
        {
            Close();
        }

        private void ayudaToolStripMenuItem_Click(object s, EventArgs e)
        {
            MessageBox.Show(this, "Use un número de mesa del 1 al " + GestionValidacion.NumeroMesaMaximo +
                " y una capacidad de 1 a " + GestionValidacion.CapacidadMesaMaxima +
                " personas. Para impedir nuevas ventas en una mesa, elija No disponible. " +
                "Una mesa con una venta abierta se libera automáticamente al cobrar o cancelar la última venta.", "Mesas");
        }

        private void dgvMesas_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
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

            ActiveControl = txtBuscarMesas;

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

        private void IniciarSeleccion(ModoFormulario modo)
        {
            if (VistaGestion.Fila(dgvMesas) == null)
            {
                lblOperacion.Text = "Seleccione una mesa del listado.";
                return;
            }
            Seleccionar();
            CambiarModo(modo);
        }
        private void CambiarModo(ModoFormulario modo)
        {
            modoActual = modo;
            bool edicion = modo == ModoFormulario.Nuevo || modo == ModoFormulario.Editar;
            bool operacion = modo != ModoFormulario.Neutral;
            string mesa = modo == ModoFormulario.Editar || modo == ModoFormulario.Eliminar
                ? "mesa " + txtNumeroMesas.Text : "mesa";
            VistaGestion.Modo(lblOperacion, pnlOperacion, btnGuardarMesas, modo, mesa);
            groupBox1.Enabled = edicion;
            btnGuardarMesas.Enabled = btnCancelarMesas.Enabled = operacion;
            btnLimpiar.Enabled = edicion;
            agregarToolStripMenuItem.Enabled = editarToolStripMenuItem.Enabled = eliminarToolStripMenuItem.Enabled = !operacion;
        }
    }
}
