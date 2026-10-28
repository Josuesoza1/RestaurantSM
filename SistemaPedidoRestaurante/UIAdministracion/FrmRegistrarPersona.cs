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
            AccesoFormulario.Permitir(this, RolesSistema.Administrador);
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
                lblIndicacion.Text = "Empleado: seleccione Mesero para atención o Cajero para cobros y facturas.";
            else if (rol == RolesSistema.Administrador)
                lblIndicacion.Text = "Administrador: tendrá acceso completo a las gestiones y operaciones del restaurante.";
        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObtenerRol() != RolesSistema.Empleado) return;
            lblIndicacion.Text = cmbCargo.Text == CargosPersonal.Mesero
                ? "Mesero: registra y atiende pedidos, reservas, asignación y limpieza de mesas."
                : cmbCargo.Text == CargosPersonal.Cajero
                ? "Cajero: consulta pedidos, registra cobros y consulta o exporta facturas."
                : "Empleado: seleccione Mesero para atención o Cajero para cobros y facturas.";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (guardando) return;
            guardando = true;
            btnGuardar.Enabled = false;
            try
            {
                Usuario cuenta = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contrasena = txtContrasena.Text,
                    IdRol = ObtenerRol()
                };
                string genero = cmbGenero.SelectedIndex < 0 ? null : new[] { "M", "F", "O" }[cmbGenero.SelectedIndex];
                if (cuenta.IdRol == RolesSistema.Cliente)
                {
                    Cliente cliente = new Cliente
                    {
                        PrimerNombre = txtPrimerNombre.Text, SegundoNombre = txtSegundoNombre.Text,
                        PrimerApellido = txtPrimerApellido.Text, SegundoApellido = txtSegundoApellido.Text,
                        Genero = genero, Cedula = txtCedula.Text, Telefono = txtTelefono.Text,
                        Email = txtCorreo.Text
                    };
                    servicio.RegistrarCliente(cliente, cuenta, txtConfirmar.Text);
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
                        PrimerNombre = txtPrimerNombre.Text, SegundoNombre = txtSegundoNombre.Text,
                        PrimerApellido = txtPrimerApellido.Text, SegundoApellido = txtSegundoApellido.Text,
                        Genero = genero, Cedula = txtCedula.Text, Telefono = txtTelefono.Text,
                        Cargo = cmbCargo.Text.Trim()
                    };
                    servicio.RegistrarPersonal(empleado, cuenta, txtConfirmar.Text);
                }

                txtContrasena.Clear();
                txtConfirmar.Clear();
                MessageBox.Show(this, "Persona y cuenta registradas correctamente. El usuario «" + cuenta.NombreUsuario +
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
            txtPrimerNombre.Clear(); txtSegundoNombre.Clear();
            txtPrimerApellido.Clear(); txtSegundoApellido.Clear();
            txtCedula.Clear(); txtTelefono.Clear(); txtCorreo.Clear(); cmbCargo.SelectedIndex = -1;
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
