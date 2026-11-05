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
            try
            {
                cmbPersona.DisplayMember = "NombreCompleto";
                cmbPersona.DataSource = new PersonaBLL().ObtenerPersonas();
                cmbPersona.SelectedIndex = -1;
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnGuardar.Enabled = false;
            }
        }

        private void chkPersonaExistente_CheckedChanged(object sender, EventArgs e)
        {
            cmbPersona.Enabled = chkPersonaExistente.Checked;
            cmbPersona.SelectedIndex = -1;
            personaSeleccionada = null;
            LimpiarPersona();
            ActualizarCuenta();
        }

        private void cmbPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkPersonaExistente.Checked) return;
            personaSeleccionada = cmbPersona.SelectedItem as Persona;
            if (personaSeleccionada == null) { LimpiarPersona(); ActualizarCuenta(); return; }
            txtPrimerNombre.Text = personaSeleccionada.PrimerNombre;
            txtSegundoNombre.Text = personaSeleccionada.SegundoNombre;
            txtPrimerApellido.Text = personaSeleccionada.PrimerApellido;
            txtSegundoApellido.Text = personaSeleccionada.SegundoApellido;
            txtCedula.Text = personaSeleccionada.Cedula;
            txtTelefono.Text = personaSeleccionada.Telefono;
            txtCorreo.Text = personaSeleccionada.Email;
            cmbGenero.SelectedIndex = Array.IndexOf(new[] { "M", "F", "O" }, personaSeleccionada.Genero);
            foreach (TextBox campo in new[] { txtPrimerNombre, txtSegundoNombre, txtPrimerApellido,
                txtSegundoApellido, txtCedula, txtTelefono, txtCorreo })
                campo.ReadOnly = !string.IsNullOrWhiteSpace(campo.Text);
            cmbGenero.Enabled = cmbGenero.SelectedIndex < 0;
            ActualizarCuenta();
        }

        private void ActualizarCuenta()
        {
            cuentaExistente = null;
            int rol = ObtenerRol();
            if (personaSeleccionada != null && rol != 0 && SesionActual.TieneRol(1))
            {
                try { cuentaExistente = servicio.CuentaParaPerfil(personaSeleccionada.IdPersona, rol); }
                catch (Exception error)
                {
                    btnGuardar.Enabled = false;
                    MessageBox.Show(this, error.Message, Text);
                    return;
                }
            }
            grpCuenta.Enabled = rol != 0 && cuentaExistente == null;
            txtUsuario.Text = cuentaExistente == null ? "" : cuentaExistente.NombreUsuario;
            txtContrasena.Clear(); txtConfirmar.Clear();
            if (cuentaExistente != null)
            {
                lblIndicacion.Text = "Se agregara el perfil a la cuenta «" + cuentaExistente.NombreUsuario +
                    "». Se conserva su contraseña y su historial; no necesita otra cuenta.";
                lblCuenta.Text = "Cuenta existente. No se modifica su contraseña.";
            }
            else lblCuenta.Text = "Datos para ingresar con una cuenta nueva.";
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
                txtSegundoApellido, txtCedula, txtTelefono, txtCorreo })
            {
                campo.ReadOnly = false;
                campo.Clear();
            }
            cmbGenero.SelectedIndex = -1;
            cmbGenero.Enabled = true;
        }

        private int ObtenerRol()
        {
            switch (cmbTipo.SelectedIndex)
            {
                case 0: return RolesSistema.Cliente;
                case 1: return RolesSistema.Empleado;
                case 2: return RolesSistema.Administrador;
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
            bool cliente = rol == RolesSistema.Cliente;
            grpPersona.Enabled = grpCuenta.Enabled = btnGuardar.Enabled = rol != 0;
            lblCorreo.Visible = txtCorreo.Visible = cliente;
            lblCargo.Visible = cmbCargo.Visible = rol != 0 && !cliente;
            cmbCargo.Items.Clear();
            cmbCargo.Items.AddRange(CargosPersonal.ParaRol(rol));
            cmbCargo.SelectedIndex = -1;
            lblIndicacion.Text = "Seleccione el tipo de persona para comenzar. Los campos con * son obligatorios.";
            if (cliente)
                lblIndicacion.Text = "Cliente: podrá ingresar para gestionar sus propios pedidos, reservas y datos.";
            else if (rol == RolesSistema.Empleado)
                lblIndicacion.Text = "Empleado: seleccione el cargo que desempeña la persona.";
            else if (rol == RolesSistema.Administrador)
                lblIndicacion.Text = "Administrador: tendrá acceso completo a las gestiones y operaciones del restaurante.";
            ActualizarCuenta();
        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObtenerRol() != RolesSistema.Empleado) return;
            lblIndicacion.Text = cmbCargo.Text == CargosPersonal.Mesero
                ? "Mesero: registra y atiende pedidos, reservas, asignación y limpieza de mesas."
                : cmbCargo.Text == CargosPersonal.Cajero
                ? "Cajero: consulta pedidos, registra cobros y consulta o exporta facturas."
                : cmbCargo.Text == CargosPersonal.Repartidor
                ? "Repartidor: registra sus datos como personal. Las opciones de reparto quedan pendientes."
                : "Empleado: seleccione el cargo que desempeña la persona.";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (guardando) return;
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
                if (cuenta.IdRol == RolesSistema.Cliente)
                {
                    Cliente cliente = new Cliente
                    {
                        IdPersona = personaSeleccionada == null ? 0 : personaSeleccionada.IdPersona,
                        PrimerNombre = txtPrimerNombre.Text, SegundoNombre = txtSegundoNombre.Text,
                        PrimerApellido = txtPrimerApellido.Text, SegundoApellido = txtSegundoApellido.Text,
                        Genero = genero, Cedula = txtCedula.Text, Telefono = txtTelefono.Text,
                        Email = txtCorreo.Text
                    };
                    if (cuentaExistente != null)
                        servicio.HabilitarCliente(cuentaExistente.IdUsuario, cliente.Email, cliente.Telefono);
                    else servicio.RegistrarCliente(cliente, cuenta, txtConfirmar.Text);
                }
                else
                {
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
                }

                txtContrasena.Clear();
                txtConfirmar.Clear();
                MessageBox.Show(this, cuentaExistente != null
                    ? "Perfil agregado a la cuenta «" + cuenta.NombreUsuario + "». Se conserva su contraseña actual."
                    : "Persona y cuenta registradas correctamente. El usuario «" + cuenta.NombreUsuario +
                    "» ya puede iniciar sesión como " + RolesSistema.Nombre(cuenta.IdRol) + ".",
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
            chkPersonaExistente.Checked = false;
            personaSeleccionada = null;
            LimpiarPersona();
            cmbCargo.SelectedIndex = -1;
            txtUsuario.Clear(); txtContrasena.Clear(); txtConfirmar.Clear();
            cmbGenero.SelectedIndex = -1;
            chkMostrar.Checked = false;
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
    }
}
