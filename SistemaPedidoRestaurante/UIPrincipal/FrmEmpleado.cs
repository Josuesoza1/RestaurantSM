using System;
using System.Windows.Forms;
using ModeloSistRestaurante.BObj;

namespace ModeloSistRestaurante.UI
{
    public partial class FrmEmpleado : Form
    {
        private readonly FrmLogin frmLogin;

        public FrmEmpleado(FrmLogin frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            dgvMesas.Columns.Add("colNumero", "Número");
            dgvMesas.Columns.Add("colCapacidad", "Capacidad");
            dgvMesas.Columns.Add("colEstado", "Estado");

            dgvPedidos.Columns.Add("colCliente", "Cliente");
            dgvPedidos.Columns.Add("colMesa", "Mesa");
            dgvPedidos.Columns.Add("colTipo", "Tipo");
            dgvPedidos.Columns.Add("colTotal", "Total");

            lblBienvenida.Text = $"Bienvenido, {Sesion.EmpleadoActual.PNombreEmp} {Sesion.EmpleadoActual.PApellidoEmp}";

            CargarMesas();
            CargarPedidosAbiertos();
        }

        private void CargarMesas()
        {
            Mesas mesas = new Mesas();
            mesas.Listar();

            dgvMesas.Rows.Clear();
            foreach (Mesa m in mesas)
            {
                dgvMesas.Rows.Add(m.Numero, m.Capacidad, m.Estado);
            }
        }

        private void CargarPedidosAbiertos()
        {
            Pedidos pedidos = new Pedidos();
            pedidos.Listar();

            dgvPedidos.Rows.Clear();
            foreach (Pedido p in pedidos)
            {
                if (!p.Estado.Equals("Abierto", StringComparison.OrdinalIgnoreCase))
                    continue;

                Cliente cliente = new Cliente();
                string nombreCliente = cliente.Buscar(p.IdCliente)
                    ? $"{cliente.PNombreCl} {cliente.PApellidoCl}"
                    : "(no encontrado)";

                string mesaTexto = "Para llevar";
                if (p.IdMesa.HasValue)
                {
                    Mesa mesa = new Mesa();
                    mesaTexto = mesa.Buscar(p.IdMesa.Value) ? "Mesa " + mesa.Numero : "Mesa " + p.IdMesa.Value;
                }

                dgvPedidos.Rows.Add(nombreCliente, mesaTexto, p.TipoPedido, p.Total.ToString("C2"));
            }
        }

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            FrmPedido frmPedido = new FrmPedido();
            frmPedido.ShowDialog();

            CargarMesas();
            CargarPedidosAbiertos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMesas();
            CargarPedidosAbiertos();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Sesion.CerrarSesion();
            frmLogin.LimpiarCampos();
            frmLogin.Show();
            this.Close();
        }
    }
}
