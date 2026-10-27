using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIEmpleado
{
    public partial class FrmBandejaOperaciones : Form
    {
        private readonly PedidoBLL pedidos = new PedidoBLL();
        private readonly ReservaBLL reservas = new ReservaBLL();
        private readonly PagoBLL pagos = new PagoBLL();
        private readonly FacturaBLL facturas = new FacturaBLL();
        private readonly InventarioBLL inventario = new InventarioBLL();
        private readonly MesaBLL mesas = new MesaBLL();
        private readonly string tipo;
        private readonly bool seleccionar;
        private DataTable registros;
        private bool actualizandoListado;

        public int PedidoSeleccionado { get; private set; }

        public FrmBandejaOperaciones() : this("Pedidos")
        {
        }

        public FrmBandejaOperaciones(string tipo, bool seleccionar = false, string filtro = "")
        {
            this.tipo = tipo;
            this.seleccionar = seleccionar;
            InitializeComponent();
            Text = seleccionar ? "Seleccione un registro" : "Bandeja de " + tipo;
            titulo.Text = Text.ToUpperInvariant();
            txtBuscar.Text = filtro;
            ConfigurarOpciones();
            listado.SelectionChanged += listado_SelectionChanged;
            ActualizarAcciones();
        }

        private void ConfigurarOpciones()
        {
            bool pedidos = !seleccionar && tipo == "Pedidos";
            bool reservas = !seleccionar && tipo == "Reservas";
            btnSeleccionar.Visible = seleccionar;
            btnNuevo.Visible = pedidos;
            btnMesa.Visible = pedidos;
            btnMesas.Visible = pedidos || reservas;
            btnAvanzar.Visible = pedidos;
            btnCobrar.Visible = pedidos;
            btnCancelar.Visible = pedidos || reservas;
            btnCancelar.Text = reservas ? "Cancelar reserva" : "Cancelar pendiente";
            btnPedidoPdf.Visible = pedidos;
            btnFacturaPdf.Visible = pedidos || (!seleccionar && tipo == "Facturas");
            btnFacturaPdf.Text = tipo == "Facturas" ? "Guardar PDF" : "PDF de factura";
            btnAtendida.Visible = reservas;
            btnAusencia.Visible = reservas;
            btnLimpiarMesa.Visible = !seleccionar && tipo == "Mesas";
            acciones.Controls.SetChildIndex(btnMesas, reservas ? 9 : 3);
            paneles.Panel2Collapsed = tipo != "Pedidos";
        }

        private void FrmBandeja_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            try
            {
                new ConfiguracionRestauranteBLL().VerificarInstalacion();
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void Recargar()
        {
            if (tipo == "Pedidos")
                registros = pedidos.ListarPedidos();
            else if (tipo == "Reservas")
                registros = reservas.ListarReservas();
            else if (tipo == "Facturas")
                registros = facturas.ListarFacturas();
            else if (tipo == "Mesas")
                registros = mesas.ListarMesas();
            else
                registros = inventario.ListarMovimientos();
            Filtrar();
        }

        private void Filtrar()
        {
            if (registros == null)
                return;
            actualizandoListado = true;
            try
            {
                listado.DataSource = BusquedaLINQ.Filtrar(registros, txtBuscar.Text);
                FormatearColumnas(listado);
                listado.ClearSelection();
                detalle.DataSource = null;
            }
            finally
            {
                actualizandoListado = false;
                ActualizarAcciones();
            }
        }

        private DataRow Seleccion()
        {
            DataRow fila = VistaGestion.Fila(listado);
            if (fila == null)
                throw new ArgumentException("Seleccione una fila del listado.");
            return fila;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Filtrar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void listado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActualizarAcciones();
        }

        private void listado_SelectionChanged(object sender, EventArgs e)
        {
            if (actualizandoListado)
                return;
            ActualizarAcciones();
            if (tipo != "Pedidos" || DesignMode ||
                System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            try
            {
                DataRow fila = VistaGestion.Fila(listado);
                detalle.DataSource = fila == null ? null : pedidos.Detalle(Convert.ToInt32(fila["IdPedido"]));
                if (fila != null)
                    FormatearColumnas(detalle);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void ActualizarAcciones()
        {
            DataRow fila = VistaGestion.Fila(listado);
            string estado = fila != null && fila.Table.Columns.Contains("Estado") ? Convert.ToString(fila["Estado"]) : "";
            bool puedeAvanzar = estado == "Pendiente" || estado == "En preparación" || estado == "Listo";
            bool reservaAbierta = estado == "Pendiente" || estado == "Confirmada";
            btnSeleccionar.Enabled = fila != null;
            btnMesa.Enabled = fila != null && puedeAvanzar;
            btnAvanzar.Enabled = fila != null && puedeAvanzar;
            btnCobrar.Enabled = fila != null && estado == "Entregado";
            btnCancelar.Enabled = fila != null && (tipo == "Reservas" ? reservaAbierta : estado == "Pendiente");
            btnPedidoPdf.Enabled = fila != null;
            btnFacturaPdf.Enabled = fila != null && (tipo == "Facturas" || estado == "Pagado");
            btnAtendida.Enabled = btnAusencia.Enabled = fila != null && reservaAbierta;
            btnLimpiarMesa.Enabled = fila != null && (estado == "Ocupada" || estado == "En limpieza");
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                PedidoSeleccionado = Convert.ToInt32(Seleccion()["IdPedido"]);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (FrmSeleccionCliente elegir = new FrmSeleccionCliente())
            {
                if (elegir.ShowDialog(this) != DialogResult.OK)
                    return;
                int cliente = Convert.ToInt32(elegir.Cliente["IdCliente"]);
                using (SistemaPedidoRestaurante.UICliente.FrmPedidoCliente formulario = new SistemaPedidoRestaurante.UICliente.FrmPedidoCliente(0, cliente))
                    formulario.ShowDialog(this);
            }

            btnRecargar_Click(sender, e);
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                string estado = Convert.ToString(fila["Estado"]);
                if (!VistaGestion.Confirmar(this, "¿Avanzar el pedido desde " + estado + " al siguiente estado?"))
                    return;
                pedidos.AvanzarPedido(Convert.ToInt32(fila["IdPedido"]), estado);
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                int pedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                pagos.Cotizar(pedido);
                using (FrmPago formulario = new FrmPago(pedido))
                    formulario.ShowDialog(this);
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                if (!VistaGestion.Confirmar(this, "¿Confirmar la cancelacion del registro seleccionado?"))
                    return;
                if (tipo == "Reservas")
                    reservas.CambiarReserva(Convert.ToInt32(fila["IdReserva"]), Convert.ToString(fila["Estado"]), "Cancelada");
                else
                    pedidos.CancelarPedido(Convert.ToInt32(fila["IdPedido"]));
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void CambiarReserva(string estado)
        {
            DataRow fila = Seleccion();
            if (!VistaGestion.Confirmar(this, "¿Marcar la reserva como " + estado + "?"))
                return;
            reservas.CambiarReserva(Convert.ToInt32(fila["IdReserva"]), Convert.ToString(fila["Estado"]), estado);
            Recargar();
        }

        private void btnAtendida_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarReserva("Atendida");
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnAusencia_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarReserva("No asistio");
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnPedidoPdf_Click(object sender, EventArgs e)
        {
            Exportar(false);
        }

        private void btnFacturaPdf_Click(object sender, EventArgs e)
        {
            Exportar(true);
        }

        private void Exportar(bool factura)
        {
            try
            {
                int pedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                DocumentoPdf.GuardarConDialogo(this, facturas.ObtenerDocumento(pedido, factura));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MostrarError(Exception error)
        {
            MessageBox.Show(this, error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnMesa_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmElegirMesa formulario = new FrmElegirMesa(Convert.ToInt32(Seleccion()["IdPedido"])))
                    formulario.ShowDialog(this);
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            using (FrmBandejaOperaciones formulario = new FrmBandejaOperaciones("Mesas"))
            {
                formulario.ShowDialog(this);
            }

            btnRecargar_Click(sender, e);
        }

        private void btnLimpiarMesa_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                string anterior = Convert.ToString(fila["Estado"]);
                string mensaje = anterior == "Ocupada" ? "¿Iniciar la limpieza de esta mesa?" : "¿Confirmar que la mesa ya esta limpia y disponible?";
                if (!VistaGestion.Confirmar(this, mensaje))
                    return;
                mesas.CambiarMesa(Convert.ToInt32(fila["IdMesa"]), anterior);
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void FormatearColumnas(DataGridView grid)
        {
            foreach (DataGridViewColumn columna in grid.Columns)
            {
                if (columna.Name == "IdMesa" || columna.Name == "IdCategoria")
                    columna.Visible = false;
                if (tipo == "Mesas" && columna.Name == "Nombre")
                    columna.Visible = false;
                if (columna.ValueType == typeof(decimal))
                    columna.DefaultCellStyle.Format = "N2";
                if (columna.ValueType == typeof(DateTime))
                    columna.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                columna.MinimumWidth = 70;
            }
        }
    }
}
