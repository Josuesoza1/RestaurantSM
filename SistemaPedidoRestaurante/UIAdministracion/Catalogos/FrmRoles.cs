using System;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using System.Drawing;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmRoles : Form
    {
        private readonly RolBLL _rolBLL = new RolBLL();
        private ModoFormulario modoActual;
        private int idSeleccionado;
        public FrmRoles()
        {
            InitializeComponent();
            txtNombre.MaxLength = 50;
            txtDescripcion.MaxLength = 150;
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
            VistaGestion.Listado(dgvRoles);
            dgvRoles.CellClick += dgvRoles_CellClick;
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
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!SesionActual.TieneRol(1))
            {
                Close();
                return;
            }

            CargarDatos();
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

            txtNombre.Enabled = puedeEditar && (modoActual==ModoFormulario.Nuevo || idSeleccionado>3);
            txtDescripcion.Enabled = puedeEditar;

            btnAccion.Enabled = hayOperacion;
            btnLimpiar.Enabled = puedeEditar;
            btnCancelar.Enabled = hayOperacion;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Seleccionar())
            {
                CambiarModo(ModoFormulario.Editar);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Seleccionar())
            {
                CambiarModo(ModoFormulario.Eliminar);
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            CambiarModo(ModoFormulario.Nuevo);
        }

        private void VolverAlMenu()
        {
            this.Close();
        }

        private void volverAlMenúPríncipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (!VistaGestion.Confirmar(this, "¿Eliminar este rol adicional? Los roles del sistema no se pueden eliminar."))
                    {
                        return;
                    }

                    _rolBLL.Eliminar(idSeleccionado);
                }
                else if (modoActual == ModoFormulario.Nuevo || modoActual == ModoFormulario.Editar)
                {
                    _rolBLL.Guardar(idSeleccionado, txtNombre.Text, txtDescripcion.Text);
                }

                CargarDatos();
                idSeleccionado = 0;
                txtNombre.Clear();
                txtDescripcion.Clear();
                CambiarModo(ModoFormulario.Neutral);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool Seleccionar()
        {
            var rol = dgvRoles.CurrentRow?.DataBoundItem as Rol;
            if (rol == null)
            {
                return false;
            }

            idSeleccionado = rol.IdRol;
            txtNombre.Text = rol.Nombre;
            txtDescripcion.Text = rol.Descripcion;
            return true;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvRoles.DataSource = _rolBLL.BuscarRoles(txtTextoBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRoles_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Seleccionar();
            }
        }
    }
}
