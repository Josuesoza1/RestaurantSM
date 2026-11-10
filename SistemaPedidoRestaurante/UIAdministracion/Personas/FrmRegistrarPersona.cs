using System;
using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UICompartida;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmRegistrarPersona : Form
    {
        private readonly UsuarioBLL servicio = new UsuarioBLL();
        private bool guardando;
        private bool restaurandoModoPersona;
        private Persona personaSeleccionada;
        private Usuario cuentaExistente;

        public FrmRegistrarPersona()
        {
            InitializeComponent();
            ValidacionEntradaUI.Nombres(txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido);
            ValidacionEntradaUI.Cedulas(txtCedula);
            ValidacionEntradaUI.Telefonos(txtTelefono);
            TemaUI.Aplicar(this);
            CambiarTipo();
        }

        private void FrmRegistrarPersona_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;
            if (!AccesoFormulario.Permitir(this, RolesSistema.Administrador)) return;
            cmbTipo.SelectedIndex = 0;
        }

        private void chkPersonaExistente_CheckedChanged(object sender, EventArgs e)
        {
            if (restaurandoModoPersona)
                return;
            if (!VistaGestion.ConfirmarDescarte(this, HayCambios()))
            {
                restaurandoModoPersona = true;
                chkPersonaExistente.Checked = !chkPersonaExistente.Checked;
                restaurandoModoPersona = false;
                return;
            }

            personaSeleccionada = null;
            LimpiarPersona();
            LimpiarCuenta();
            if (chkPersonaExistente.Checked)
            {
                try
                {
                    cmbPersona.DisplayMember = "NombreCompleto";
                    cmbPersona.DataSource = new PersonaBLL().ObtenerPersonasParaAlta();
                }
                catch (Exception error)
                {
                    MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPersona.DataSource = null;
                }
            }

            cmbPersona.Enabled = chkPersonaExistente.Checked;
            cmbPersona.SelectedIndex = -1;
            ActualizarCuenta();
        }

        private void cmbPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkPersonaExistente.Checked || !cmbPersona.Enabled) return;
            personaSeleccionada = cmbPersona.SelectedItem as Persona;
            LimpiarCuenta();
            if (personaSeleccionada == null)
            {
                LimpiarPersona();
                ActualizarCuenta();
                return;
            }
            txtPrimerNombre.Text = personaSeleccionada.PrimerNombre;
            txtSegundoNombre.Text = personaSeleccionada.SegundoNombre;
            txtPrimerApellido.Text = personaSeleccionada.PrimerApellido;
            txtSegundoApellido.Text = personaSeleccionada.SegundoApellido;
            txtCedula.Text = personaSeleccionada.Cedula;
            txtTelefono.Text = personaSeleccionada.Telefono;
            cmbGenero.SelectedIndex = Array.IndexOf(new[] { "M", "F", "O" }, personaSeleccionada.Genero);
            foreach (TextBox campo in new[] { txtPrimerNombre, txtSegundoNombre, txtPrimerApellido,
                txtSegundoApellido, txtCedula, txtTelefono })
                campo.ReadOnly = !string.IsNullOrWhiteSpace(campo.Text);
            cmbGenero.Enabled = cmbGenero.SelectedIndex < 0;
            ActualizarCuenta();
        }

        private void ActualizarCuenta()
        {
            cuentaExistente = null;
            int rol = ObtenerRol();
            bool puedeGuardar = rol != 0 && (!chkPersonaExistente.Checked || personaSeleccionada != null);
            grpPersona.Enabled = grpCuenta.Enabled = btnGuardar.Enabled = puedeGuardar;
            lblIndicacion.Text = rol == RolesSistema.Administrador
                ? "Administrador: acceso a todas las gestiones."
                : "Despachador: registra, despacha y cobra ventas locales.";
            if (chkPersonaExistente.Checked && personaSeleccionada == null)
            {
                lblIndicacion.Text = cmbPersona.Items.Count > 0
                    ? "Seleccione la persona a la que desea dar acceso."
                    : "No hay personas disponibles para vincular.";
            }

            if (personaSeleccionada != null && puedeGuardar && SesionActual.TieneRol(RolesSistema.Administrador))
            {
                try { cuentaExistente = servicio.CuentaParaPerfil(personaSeleccionada.IdPersona, rol); }
                catch (Exception error)
                {
                    btnGuardar.Enabled = false;
                    MessageBox.Show(this, error.Message, Text);
                    return;
                }
            }
            txtUsuario.ReadOnly = cuentaExistente != null;
            txtContrasena.Enabled = txtConfirmar.Enabled = chkMostrar.Enabled = cuentaExistente == null;
            lblCuenta.Text = "Datos para ingresar con una cuenta nueva.";
            if (cuentaExistente != null)
            {
                txtUsuario.Text = cuentaExistente.NombreUsuario;
                txtContrasena.Clear();
                txtConfirmar.Clear();
                chkMostrar.Checked = false;
                lblCuenta.Text = "Se conserva la cuenta existente, su contraseña y su historial.";
            }
        }

        private void cmbPersona_Format(object sender, ListControlConvertEventArgs e)
        {
            Persona persona = e.ListItem as Persona;
            if (persona != null)
                e.Value = persona.NombreCompleto + " — " + (string.IsNullOrWhiteSpace(persona.Cedula) ? "Sin cédula" : persona.Cedula);
        }

        private void LimpiarPersona()
        {
            foreach (TextBox campo in new[] { txtPrimerNombre, txtSegundoNombre, txtPrimerApellido,
                txtSegundoApellido, txtCedula, txtTelefono })
            {
                campo.ReadOnly = false;
                campo.Clear();
            }
            cmbGenero.SelectedIndex = -1;
            cmbGenero.Enabled = true;
        }

        private void LimpiarCuenta()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtConfirmar.Clear();
            chkMostrar.Checked = false;
        }

        private int ObtenerRol()
        {
            switch (cmbTipo.SelectedIndex)
            {
                case 0: return RolesSistema.Empleado;
                case 1: return RolesSistema.Administrador;
                default: return 0;
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiarTipo();
        }

        private void CambiarTipo()
        {
            int rol = ObtenerRol();
            cmbCargo.Items.Clear();
            cmbCargo.Items.AddRange(CargosPersonal.ParaRol(rol));
            cmbCargo.SelectedIndex = cmbCargo.Items.Count > 0 ? 0 : -1;
            cmbCargo.Enabled = rol == RolesSistema.Administrador;
            ActualizarCuenta();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (guardando || !btnGuardar.Enabled) return;
            guardando = true;
            btnGuardar.Enabled = false;
            try
            {
                if (chkPersonaExistente.Checked && personaSeleccionada == null)
                    throw new ArgumentException("Seleccione la persona a la que agregará el perfil.");
                Usuario cuenta = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contrasena = cuentaExistente == null ? txtContrasena.Text : null,
                    IdUsuario = cuentaExistente == null ? 0 : cuentaExistente.IdUsuario,
                    IdRol = ObtenerRol()
                };
                string genero = cmbGenero.SelectedIndex < 0 ? null : new[] { "M", "F", "O" }[cmbGenero.SelectedIndex];
                if (cuenta.IdRol == RolesSistema.Administrador && MessageBox.Show(this,
                    "Esta cuenta podrá administrar usuarios, personal, inventario y configuración. ¿Desea otorgarle acceso de administrador?",
                    "Confirmar acceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                Empleado empleado = new Empleado
                {
                    IdPersona = personaSeleccionada == null ? 0 : personaSeleccionada.IdPersona,
                    PrimerNombre = txtPrimerNombre.Text, SegundoNombre = txtSegundoNombre.Text,
                    PrimerApellido = txtPrimerApellido.Text, SegundoApellido = txtSegundoApellido.Text,
                    Genero = genero, Cedula = txtCedula.Text, Telefono = txtTelefono.Text,
                    Cargo = cmbCargo.Text.Trim()
                };
                if (cuentaExistente != null) servicio.AgregarPerfilPersonal(empleado, cuenta);
                else servicio.RegistrarPersonal(empleado, cuenta, txtConfirmar.Text);

                txtContrasena.Clear();
                txtConfirmar.Clear();
                MessageBox.Show(this, cuentaExistente != null
                    ? "Perfil agregado a la cuenta «" + cuenta.NombreUsuario + "». Se conserva su contraseña actual."
                    : "El usuario «" + cuenta.NombreUsuario + "» ya puede iniciar sesión como " +
                    RolesSistema.Nombre(cuenta.IdRol) + ".",
                    "Registro completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                guardando = false;
                btnGuardar.Enabled = ObtenerRol() != 0;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!VistaGestion.ConfirmarDescarte(this, HayCambios()))
                return;

            personaSeleccionada = null;
            LimpiarPersona();
            LimpiarCuenta();
            chkPersonaExistente.Checked = false;
            CambiarTipo();
            txtPrimerNombre.Focus();
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtContrasena.UseSystemPasswordChar = txtConfirmar.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool HayCambios()
        {
            return personaSeleccionada != null || txtPrimerNombre.TextLength > 0 || txtSegundoNombre.TextLength > 0
                || txtPrimerApellido.TextLength > 0 || txtSegundoApellido.TextLength > 0 || txtCedula.TextLength > 0
                || txtTelefono.TextLength > 0 || cmbGenero.SelectedIndex >= 0 || txtUsuario.TextLength > 0
                || txtContrasena.TextLength > 0 || txtConfirmar.TextLength > 0;
        }

        private void FrmRegistrarPersona_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK && !VistaGestion.ConfirmarDescarte(this, HayCambios()))
            {
                e.Cancel = true;
                DialogResult = DialogResult.None;
            }
        }
    }
}
