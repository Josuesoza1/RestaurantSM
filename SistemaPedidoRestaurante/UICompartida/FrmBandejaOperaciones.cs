using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    public partial class FrmBandejaOperaciones : Form
    {
        private readonly PedidoBLL pedidos = new PedidoBLL();
        private readonly ReservaBLL reservas = new ReservaBLL();
        private readonly PagoBLL pagos = new PagoBLL();
        private readonly FacturaBLL facturas = new FacturaBLL();
        private readonly InventarioBLL inventario = new InventarioBLL();
        private readonly MesaBLL mesas = new MesaBLL();
        private readonly int rolPermitido;
        private readonly string tipo;
        private readonly bool seleccionar;
        private DataTable registros;
        private bool actualizandoListado;

        public int PedidoSeleccionado { get; private set; }

        private bool TieneFiltroMensual => rolPermitido == RolesSistema.Administrador && tipo == "Pedidos" && !seleccionar;

        public FrmBandejaOperaciones() : this(0, "Pedidos")
        {
        }

        protected FrmBandejaOperaciones(int rol, string tipo, bool seleccionar = false, string filtro = "")
        {
            rolPermitido = rol;
            this.tipo = tipo;
            this.seleccionar = seleccionar;
            InitializeComponent();
            VistaGestion.PermitirOrden(listado);
            VistaGestion.PermitirOrden(detalle);
            Text = seleccionar ? "Seleccione un registro" : "Bandeja de " + tipo;
            titulo.Text = Text.ToUpperInvariant();
            txtBuscar.Text = filtro;
            ConfigurarOpciones();
            listado.SelectionChanged += listado_SelectionChanged;
            ActualizarAcciones();
        }

        protected void ConfigurarOpciones()
        {
            if (seleccionar)
            {
                Text = "Seleccione un registro";
                titulo.Text = Text.ToUpperInvariant();
            }
            bool pedidos = !seleccionar && tipo == "Pedidos";
            bool reservas = !seleccionar && tipo == "Reservas";
            filtroMes.Visible = TieneFiltroMensual;
            btnResumenMensual.Visible = TieneFiltroMensual;
            btnSeleccionar.Visible = seleccionar;
            btnNuevo.Visible = pedidos;
            btnMesa.Visible = pedidos;
            btnMesas.Visible = pedidos || reservas;
            btnAvanzar.Visible = pedidos;
            btnAvanzar.Text = "Despachar";
            btnCobrar.Visible = pedidos;
            btnCancelar.Visible = pedidos || reservas;
            btnCancelar.Text = reservas ? "Cancelar reserva" : "Cancelar pendiente";
            btnPedidoPdf.Visible = pedidos;
            btnFacturaPdf.Visible = pedidos || (!seleccionar && tipo == "Facturas");
            btnAtendida.Visible = reservas;
            btnAusencia.Visible = reservas;
            btnLimpiarMesa.Visible = !seleccionar && tipo == "Mesas";
            acciones.Controls.SetChildIndex(btnMesas, reservas ? 9 : 3);
            paneles.Panel2Collapsed = tipo != "Pedidos";
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                btnNuevo.Visible = btnMesa.Visible = btnAvanzar.Visible = pedidos && Permisos.PuedeAtender;
                btnMesas.Visible = (pedidos || reservas) && Permisos.PuedeAtender;
                btnCancelar.Visible = (pedidos || reservas) && Permisos.PuedeAtender;
                btnCobrar.Visible = pedidos && Permisos.PuedeCobrar;
                btnPedidoPdf.Visible = pedidos && Permisos.PuedeVerHistorial;
                btnFacturaPdf.Visible = (pedidos || (!seleccionar && tipo == "Facturas")) && Permisos.PuedeVerHistorial;
                if (SesionActual.TieneRol(2) && pedidos)
                {
                    Text = "Ventas abiertas";
                    titulo.Text = "VENTAS ABIERTAS";
                }
            }
        }

        private void FrmBandeja_Load(object sender, EventArgs e)
        {
            if (DesignMode || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            bool permitido = tipo == "Pedidos" ? Permisos.PuedeConsultarPedidos :
                tipo == "Mesas" ? Permisos.PuedeAtender : Permisos.PuedeVerHistorial;
            if (!AccesoFormulario.PermitirOperacion(this, rolPermitido, permitido)) return;
            ConfigurarOpciones();
            try
            {
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void Recargar(int idPedido = 0)
        {
            if (tipo == "Pedidos")
                registros = pedidos.ListarPedidos(seleccionar);
            else if (tipo == "Reservas")
                registros = reservas.ListarReservas();
            else if (tipo == "Facturas")
                registros = facturas.ListarFacturas();
            else if (tipo == "Mesas")
                registros = mesas.ListarMesas();
            else
                registros = inventario.ListarMovimientos();
            Filtrar();
            if (idPedido > 0)
            {
                foreach (DataGridViewRow fila in listado.Rows)
                {
                    var venta = fila.DataBoundItem as DataRowView;
                    if (venta != null && Convert.ToInt32(venta["IdPedido"]) == idPedido)
                    {
                        listado.CurrentCell = fila.Cells["IdPedido"];
                        fila.Selected = true;
                        break;
                    }
                }
            }
        }

        private void Filtrar()
        {
            if (registros == null)
                return;
            actualizandoListado = true;
            try
            {
                listado.DataSource = TieneFiltroMensual && chkFiltrarMes.Checked
                    ? pedidos.FiltrarHistorialPorMes(registros, txtBuscar.Text, mesVenta.Value)
                    : BusquedaLINQ.Filtrar(registros, txtBuscar.Text);
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

        private void filtro_Changed(object sender, EventArgs e)
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

        private void chkFiltrarMes_CheckedChanged(object sender, EventArgs e)
        {
            mesVenta.Enabled = chkFiltrarMes.Checked;
            filtro_Changed(sender, e);
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
            bool puedeReabrir = PuedeVolverAPendiente(fila);
            bool reservaAbierta = estado == "Pendiente" || estado == "Confirmada";
            btnSeleccionar.Enabled = fila != null;
            btnMesa.Enabled = fila != null && puedeAvanzar && (!fila.Table.Columns.Contains("IdTipoEntrega") || Convert.ToInt32(fila["IdTipoEntrega"]) <= 2);
            btnAvanzar.Text = puedeReabrir ? "Volver a pendiente" : "Despachar";
            btnAvanzar.Enabled = puedeAvanzar || puedeReabrir;
            btnCobrar.Enabled = fila != null && estado == "Entregado";
            btnCancelar.Enabled = fila != null && (tipo == "Reservas" ? reservaAbierta : estado == "Pendiente");
            btnPedidoPdf.Enabled = fila != null;
            btnFacturaPdf.Enabled = fila != null && (tipo == "Facturas" || estado == "Pagado" ||
                tipo == "Pedidos" && Convert.ToString(fila["Pago"]) == "Anulado");
            btnAtendida.Enabled = btnAusencia.Enabled = fila != null && reservaAbierta;
            btnLimpiarMesa.Enabled = fila != null && (estado == "Ocupada" || estado == "En limpieza");
        }

        private bool PuedeVolverAPendiente(DataRow fila)
        {
            if (tipo != "Pedidos" || seleccionar || !Permisos.PuedeVerHistorial || fila == null)
                return false;

            int tipoEntrega = Convert.ToInt32(fila["IdTipoEntrega"]);
            return Convert.ToString(fila["Estado"]) == "Entregado"
                && Convert.ToString(fila["Origen"]) == "En el local"
                && (tipoEntrega == 1 || tipoEntrega == 2)
                && Convert.ToString(fila["Pago"]) == "Sin pagar";
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
            try
            {
                using (FrmPedidoLocalBase formulario = PantallasPersonal.CrearPedido())
                {
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                    {
                        chkFiltrarMes.Checked = false;
                        txtBuscar.Clear();
                        Recargar(formulario.PedidoRegistrado);
                    }
                }
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                string estado = Convert.ToString(fila["Estado"]);
                bool reabrir = PuedeVolverAPendiente(fila);
                string mensaje = reabrir ? "¿Volver esta venta a pendiente?" : "¿Confirmar el despacho del pedido?";
                if (!VistaGestion.Confirmar(this, mensaje))
                    return;
                int idPedido = Convert.ToInt32(fila["IdPedido"]);
                if (reabrir)
                    pedidos.VolverAPendiente(idPedido);
                else
                    pedidos.DespacharPedido(idPedido, estado);
                txtBuscar.Clear();
                Recargar(idPedido);
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
                using (Form formulario = PantallasPersonal.CrearCobro(pedido))
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
                DocumentoPdf.MostrarVistaPrevia(this, facturas.ObtenerDocumento(pedido, factura));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnResumenMensual_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime mes = chkFiltrarMes.Checked ? mesVenta.Value : DateTime.Today;
                var reporte = new ResumenMensualDocumento(mes, facturas.ConsultarResumenMensual(mes));
                using (var vista = new FrmVistaPrevia(reporte.CrearPdf(), reporte.NombreArchivo, reporte.GuardarExcel))
                    vista.ShowDialog(this);
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
            using (FrmBandejaOperaciones formulario = PantallasPersonal.CrearBandeja("Mesas"))
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
                if (columna.Name == "IdMesa" || columna.Name == "IdCategoria" || columna.Name == "IdTipoEntrega")
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
