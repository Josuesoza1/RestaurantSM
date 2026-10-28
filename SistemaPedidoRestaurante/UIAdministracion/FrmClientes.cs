using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteBLL servicio = new ClienteBLL();
        private DataTable clientes;
        private int id;
        private ModoFormulario modoActual;
        public FrmClientes()
        {
            InitializeComponent();
            ValidacionEntradaUI.Nombres(txtPnombre, txtSnombre, txtPapellido, txtSapellido);
            ValidacionEntradaUI.Telefonos(txtTelefono);
            ValidacionEntradaUI.Cedulas(txtCedula);
            txtEmail.MaxLength = 120;
            VistaGestion.Listado(dgvClientes);
            string[] campos =

            {
                "Id",
                "PrimerNombre",
                "SegundoNombre",
                "PrimerApellido",
                "SegundoApellido",
                "Cedula",
                "Telefono",
                "Email"
            };
            for (int i = 0; i < campos.Length; i++)
            {
                dgvClientes.Columns[i].DataPropertyName = campos[i];
            }

            VistaGestion.Columna(dgvClientes, "Genero", "Género");
            VistaGestion.Columna(dgvClientes, "Activo", "Activo");
            btnEliminar.Text = "Eliminar";
            btnEliminar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            dgvClientes.CellClick += dgvClientes_CellClick;
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
            Load += FrmClientes_CargarDatos;
        }

        private void Recargar()
        {
            clientes = servicio.Listar();
            dgvClientes.DataSource = clientes;
        }

        private Cliente Leer()
        {
            return new Cliente
            {
                Id = id,
                PrimerNombre = txtPnombre.Text,
                SegundoNombre = txtSnombre.Text,
                PrimerApellido = txtPapellido.Text,
                SegundoApellido = txtSapellido.Text,
                Cedula = txtCedula.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                Genero = cmbGenero.SelectedItem as string
            };
        }

        private void Mostrar()
        {
            var f = VistaGestion.Fila(dgvClientes);
            if (f == null)
            {
                return;
            }

            id = (int)f["Id"];
            txtPnombre.Text = Convert.ToString(f["PrimerNombre"]);
            txtSnombre.Text = Convert.ToString(f["SegundoNombre"]);
            txtPapellido.Text = Convert.ToString(f["PrimerApellido"]);
            txtSapellido.Text = Convert.ToString(f["SegundoApellido"]);
            txtCedula.Text = Convert.ToString(f["Cedula"]);
            txtTelefono.Text = Convert.ToString(f["Telefono"]);
            txtEmail.Text = Convert.ToString(f["Email"]);
            cmbGenero.SelectedItem = Convert.ToString(f["Genero"]);
            chkActivo.Checked = (bool)f["Activo"];

        }

        private void Nuevo()
        {
            id = 0;
            txtPnombre.Clear();
            txtSnombre.Clear();
            txtPapellido.Clear();
            txtSapellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cmbGenero.SelectedIndex = -1;
            chkActivo.Checked = true;
            dgvClientes.ClearSelection();

        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            int anterior = id;
            Nuevo();
            if (modoActual == ModoFormulario.Editar) id = anterior;
        }

        private void btnNuevo_Click(object s, EventArgs e)
        {
            Nuevo();
            CambiarModo(modoActual == ModoFormulario.Neutral ? ModoFormulario.Nuevo : ModoFormulario.Neutral);
        }

        private void btnGuardar_Click(object s, EventArgs e)
        {
            if (modoActual == ModoFormulario.Neutral) return;
            try
            {
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (!VistaGestion.Confirmar(this, "¿Eliminar este cliente y su cuenta asociada? Debe carecer de pedidos y reservas.")) return;
                    servicio.Eliminar(id);
                }
                else servicio.Guardar(Leer(), chkActivo.Checked);
                Recargar();
                Nuevo();
                CambiarModo(ModoFormulario.Neutral);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object s, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Eliminar);
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            try
            {
                dgvClientes.DataSource = servicio.Buscar(clientes, txtTextoBuscar.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException("Seleccione un cliente del listado.");
                }

                if (VistaGestion.Confirmar(this, "¿Desactivar al cliente conservando su historial?"))
                {
                    servicio.Desactivar(id);
                    Recargar();
                    Nuevo();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvClientes_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
            {
                Mostrar();
            }
        }

        private void FrmClientes_CargarDatos(object sender, EventArgs e)
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

        private void editarMenu_Click(object sender, EventArgs e)
        {
            IniciarSeleccion(ModoFormulario.Editar);
        }
        private void IniciarSeleccion(ModoFormulario modo)
        {
            if (VistaGestion.Fila(dgvClientes) == null) { MessageBox.Show(this, "Seleccione un cliente del listado."); return; }
            Mostrar();
            CambiarModo(modo);
        }
        private void CambiarModo(ModoFormulario modo)
        {
            modoActual = modo;
            bool edicion = modo == ModoFormulario.Nuevo || modo == ModoFormulario.Editar;
            bool operacion = modo != ModoFormulario.Neutral;
            VistaGestion.Modo(lblOperacion, pnlOperacion, btnGuardar, modo, "cliente");
            grpDatosClientes.Enabled = edicion;
            dgvClientes.Enabled = txtTextoBuscar.Enabled = btnBuscar.Enabled = !operacion;
            btnGuardar.Enabled = operacion;
            btnLimpiar.Enabled = edicion;
            btnNuevo.Text = operacion ? "Cancelar" : "Nuevo cliente";
            btnEliminar.Enabled = btnDesactivar.Enabled = !operacion;
            editarMenu.Enabled = eliminarMenu.Enabled = desactivarMenu.Enabled = agregarMenu.Enabled = !operacion;
        }
    }
}
