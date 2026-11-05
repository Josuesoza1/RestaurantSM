using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmElegirPerfil : Form
    {
        public Usuario Cuenta { get; private set; }
        public int RolElegido { get; private set; }

        public FrmElegirPerfil() { InitializeComponent(); }

        public FrmElegirPerfil(Usuario cuenta) : this()
        {
            Cuenta = cuenta;
            ValidacionEntradaUI.Telefonos(txtTelefono);
        }

        private void FrmElegirPerfil_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            if (Cuenta == null || Cuenta.IdUsuario != SesionActual.IdUsuario) { Close(); return; }
            ActualizarOpciones();
        }

        private void ActualizarOpciones()
        {
            lblCuenta.Text = "Cuenta: " + Cuenta.NombreUsuario;
            btnTrabajo.Text = "Ingresar como " + RolesSistema.Nombre(Cuenta.IdRol);
            btnTrabajo.Enabled = Cuenta.AccesoPersonal;
            btnCliente.Enabled = Cuenta.ClienteActivo;
            grpAltaCliente.Visible = Cuenta.AccesoPersonal && !Cuenta.TienePerfilCliente;
            txtCorreo.Text = Cuenta.Email;
            txtTelefono.Text = Cuenta.Telefono;
            txtCorreo.ReadOnly = !string.IsNullOrWhiteSpace(Cuenta.Email);
            txtTelefono.ReadOnly = !string.IsNullOrWhiteSpace(Cuenta.Telefono);
            lblIndicacion.Text = Cuenta.ClienteActivo
                ? "Elija el perfil para esta sesion. Como cliente vera sus pedidos personales y reservas."
                : Cuenta.TienePerfilCliente
                ? "Su perfil cliente esta inactivo. Solicite su revision al administrador. Su acceso de trabajo se mantiene independiente."
                : "Puede habilitar su perfil cliente completando el contacto. Usara esta misma cuenta y sus datos de Persona.";
            AcceptButton = Cuenta.AccesoPersonal ? btnTrabajo : btnCliente;
        }

        private void btnTrabajo_Click(object sender, EventArgs e)
        {
            if (Cuenta == null || !Cuenta.AccesoPersonal) return;
            RolElegido = Cuenta.IdRol;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            if (Cuenta == null || !Cuenta.ClienteActivo) return;
            RolElegido = RolesSistema.Cliente;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            btnHabilitar.Enabled = false;
            try
            {
                UsuarioBLL servicio = new UsuarioBLL();
                servicio.HabilitarCliente(Cuenta.IdUsuario, txtCorreo.Text, txtTelefono.Text);
                Cuenta = servicio.ObtenerAccesosActuales();
                ActualizarOpciones();
                lblIndicacion.Text = "Perfil cliente habilitado. Ya puede ingresar con esta misma cuenta.";
                btnCliente.Focus();
            }
            catch (Exception error) { MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            finally { btnHabilitar.Enabled = true; }
        }

        private void btnVolver_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
    }
}
