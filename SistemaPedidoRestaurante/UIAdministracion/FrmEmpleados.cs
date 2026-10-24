using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmEmpleados : System.Windows.Forms.Form
    {
        private readonly EmpleadoBLL _empleadoBLL;
        private readonly RolBLL _rolBLL;
        private ModoFormulario modoActual;
        private Empleado empleadoSeleccionado;
        public FrmEmpleados()
        {
            InitializeComponent();
            ValidacionEntradaUI.Nombres(txtPnombre, txtSnombre, txtPapellido, txtSapellido);
            ValidacionEntradaUI.Telefonos(txtTelefono);
            ValidacionEntradaUI.Cedulas(txtCedula);
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            _empleadoBLL = new EmpleadoBLL();
            _rolBLL = new RolBLL();
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.CellClick += dgvEmpleados_CellClick;
            CambiarModo(ModoFormulario.Neutral);
            TemaUI.Aplicar(this);
        }

        private void CargarDatos()
        {
            dgvEmpleados.AutoGenerateColumns = false;
            string[] propiedades =

            {
                "IdEmpleado",
                "PrimerNombre",
                "SegundoNombre",
                "PrimerApellido",
                "SegundoApellido",
                "Cargo",
                "Genero",
                "Cedula",
                "Telefono",
                "Activo"
            };
            for (int i = 0; i < propiedades.Length; i++)
            {
                dgvEmpleados.Columns[i].DataPropertyName = propiedades[i];
            }

            dgvEmpleados.DataSource = _empleadoBLL.ObtenerEmpleados();
        }

        private void CargarRoles()
        {
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "IdRol";
            cmbRol.DataSource = _rolBLL.ObtenerRoles().Where(rol => rol.IdRol == 1 || rol.IdRol == 2).Select(rol => new { rol.IdRol, rol.Nombre }).OrderBy(rol => rol.Nombre).ToList();
            cmbRol.SelectedValue = 2;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            if (!SesionActual.TieneRol(1))
            {
                this.Close();
                return;
            }

            try
            {
                CargarRoles();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void VolverAlMenu()
        {
            this.Close();
        }

        private void volverAlMenúPríncipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
        }

        private void CambiarModo(ModoFormulario nuevoModo)
        {
            modoActual = nuevoModo;
            bool puedeEditar = nuevoModo == ModoFormulario.Nuevo || nuevoModo == ModoFormulario.Editar;
            bool hayOperacion = nuevoModo != ModoFormulario.Neutral;
            switch (nuevoModo)
            {
                case ModoFormulario.Neutral:
                    lblOperacion.Text = "Operación actual: Seleccione una operación";
                    btnAccion.Text = "Acción"; panel1.BackColor = Color.Gainsboro; break;
                case ModoFormulario.Nuevo:
                    lblOperacion.Text = "Operación actual: Nuevo empleado";
                    btnAccion.Text = "Guardar"; panel1.BackColor = Color.LightGreen; break;
                case ModoFormulario.Editar:
                    lblOperacion.Text = "Operación actual: Editar empleado";
                    btnAccion.Text = "Guardar cambios"; panel1.BackColor = Color.LightSkyBlue; break;
                case ModoFormulario.Eliminar:
                    lblOperacion.Text = "Operación actual: Eliminar empleado";
                    btnAccion.Text = "Eliminar"; panel1.BackColor = Color.LightCoral; break;
            }
            txtPnombre.Enabled = txtSnombre.Enabled = txtPapellido.Enabled = txtSapellido.Enabled = puedeEditar;
            txtTelefono.Enabled = txtCargo.Enabled = txtCedula.Enabled = puedeEditar;
            cmbGenero.Enabled = cmbRol.Enabled = puedeEditar;
            txtUsuario.Enabled = txtContrasena.Enabled = chkActivo.Enabled = puedeEditar;
            btnAccion.Enabled = btnCancelar.Enabled = hayOperacion;
            btnLimpiar.Enabled = puedeEditar;
            dgvEmpleados.Enabled = btnBuscar.Enabled = txtTextoBuscar.Enabled = !hayOperacion;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarEmpleado())
            {
                CambiarModo(ModoFormulario.Editar);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SeleccionarEmpleado())
            {
                CambiarModo(ModoFormulario.Eliminar);
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            empleadoSeleccionado = null;
            LimpiarCampos();
            CambiarModo(ModoFormulario.Nuevo);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtPnombre.Clear();
            txtSnombre.Clear();
            txtPapellido.Clear();
            txtSapellido.Clear();
            txtTelefono.Clear();
            txtCargo.Clear();
            txtCedula.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            cmbGenero.SelectedIndex = -1;
            if (cmbRol.DataSource != null)
            {
                cmbRol.SelectedValue = 2;
            }

            chkActivo.Checked = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            empleadoSeleccionado = null;
            LimpiarCampos();
            CambiarModo(ModoFormulario.Neutral);
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (modoActual == ModoFormulario.Neutral)
                {
                    return;
                }

                if (modoActual == ModoFormulario.Eliminar)
                {
                    if (empleadoSeleccionado == null)
                    {
                        return;
                    }

                    if (MessageBox.Show("¿Eliminar permanentemente a " + empleadoSeleccionado.PrimerNombre + " " + empleadoSeleccionado.PrimerApellido + "? También se eliminará su cuenta de acceso asociada si no tiene historial.", "Eliminar empleado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    _empleadoBLL.EliminarEmpleado(empleadoSeleccionado.IdEmpleado);
                }
                else
                {
                    Empleado empleado = LeerEmpleado();
                    Usuario usuario = new Usuario
                    {
                        NombreUsuario = txtUsuario.Text.Trim(),
                        Contrasena = txtContrasena.Text.Trim(),
                        IdRol = cmbRol.SelectedValue == null ? 0 : Convert.ToInt32(cmbRol.SelectedValue)
                    };
                    _empleadoBLL.GuardarEmpleado(empleado, usuario, modoActual == ModoFormulario.Nuevo);
                    if (empleado.IdUsuario == SesionActual.IdUsuario)
                    {
                        usuario.IdUsuario = SesionActual.IdUsuario;
                        usuario.NombreRol = "Administrador";
                        SesionActual.Iniciar(usuario);
                    }
                }

                MessageBox.Show("Operación realizada correctamente.", "Empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                empleadoSeleccionado = null;
                txtTextoBuscar.Clear();
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private Empleado LeerEmpleado()
        {
            return new Empleado
            {
                IdEmpleado = empleadoSeleccionado?.IdEmpleado ?? 0,
                IdUsuario = empleadoSeleccionado?.IdUsuario,
                PrimerNombre = txtPnombre.Text.Trim(),
                SegundoNombre = txtSnombre.Text.Trim(),
                PrimerApellido = txtPapellido.Text.Trim(),
                SegundoApellido = txtSapellido.Text.Trim(),
                Cargo = txtCargo.Text.Trim(),
                Cedula = txtCedula.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Genero = cmbGenero.SelectedIndex < 0 ? null : new[]
                {
                    "M",
                    "F",
                    "O"
                }[cmbGenero.SelectedIndex],
                Activo = chkActivo.Checked
            };
        }

        private bool SeleccionarEmpleado()
        {
            empleadoSeleccionado = dgvEmpleados.CurrentRow?.DataBoundItem as Empleado;
            if (empleadoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un empleado del listado.", "Empleados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            MostrarEmpleado(empleadoSeleccionado);
            return true;
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && modoActual == ModoFormulario.Neutral)
            {
                SeleccionarEmpleado();
            }
        }

        private void MostrarEmpleado(Empleado empleado)
        {
            txtPnombre.Text = empleado.PrimerNombre;
            txtSnombre.Text = empleado.SegundoNombre;
            txtPapellido.Text = empleado.PrimerApellido;
            txtSapellido.Text = empleado.SegundoApellido;
            txtTelefono.Text = empleado.Telefono;
            txtCargo.Text = empleado.Cargo;
            txtCedula.Text = empleado.Cedula;
            cmbGenero.SelectedIndex = Array.IndexOf(new[]
            {
                "M",
                "F",
                "O"
            }, empleado.Genero);
            cmbRol.SelectedValue = empleado.IdRol ?? 2;
            txtUsuario.Text = empleado.NombreUsuario;
            txtContrasena.Clear();
            chkActivo.Checked = empleado.Activo;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEmpleados.DataSource = _empleadoBLL.BuscarEmpleados(txtTextoBuscar.Text);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Empleados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SeleccionarEmpleado())
                {
                    return;
                }

                if (MessageBox.Show("¿Desactivar al empleado conservando su cuenta e historial?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                _empleadoBLL.DesactivarEmpleado(empleadoSeleccionado.IdEmpleado);
                empleadoSeleccionado = null;
                LimpiarCampos();
                CambiarModo(ModoFormulario.Neutral);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }
    }
}
