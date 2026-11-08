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
        private readonly FacturaBLL facturas = new FacturaBLL();
        private readonly InventarioBLL inventario = new InventarioBLL();
        private readonly MesaBLL mesas = new MesaBLL();
        private readonly int rolPermitido;
        private readonly string tipo;
        private readonly bool seleccionar;
        private readonly bool historial;
        private DataTable registros;
        private bool actualizandoListado;

        public int PedidoSeleccionado { get; private set; }

        private bool TieneHistorialVentas => historial && rolPermitido == RolesSistema.Administrador && tipo == "Pedidos" && !seleccionar;

        public FrmBandejaOperaciones() : this(0, "Pedidos")
        {
        }

        protected FrmBandejaOperaciones(int rol, string tipo, bool seleccionar = false, string filtro = "", bool historial = false)
        {
            rolPermitido = rol;
            this.tipo = tipo;
            this.seleccionar = seleccionar;
            this.historial = historial;
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
            bool pedidos = !seleccionar && tipo == "Pedidos" && !historial;
            if (!seleccionar && tipo == "Pedidos")
            {
                Text = historial ? "Historial de ventas" : "Ventas abiertas";
                titulo.Text = Text.ToUpperInvariant();
            }
            filtroMes.Visible = TieneHistorialVentas;
            btnResumenMensual.Visible = TieneHistorialVentas;
            panelResumenDiario.Visible = TieneHistorialVentas;
            btnSeleccionar.Visible = seleccionar;
            btnNuevo.Visible = pedidos;
            btnMesa.Visible = pedidos;
            btnAvanzar.Visible = pedidos;
            btnAvanzar.Text = "Despachar";
            btnCobrar.Visible = pedidos;
            btnCancelar.Visible = pedidos;
            btnPedidoPdf.Visible = TieneHistorialVentas;
            btnFacturaPdf.Visible = TieneHistorialVentas || (!seleccionar && tipo == "Facturas");
            btnLimpiarMesa.Visible = !seleccionar && tipo == "Mesas";
            paneles.Panel2Collapsed = tipo != "Pedidos";
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                btnNuevo.Visible = btnMesa.Visible = btnAvanzar.Visible = pedidos && Permisos.PuedeAtender;
                btnCancelar.Visible = pedidos && Permisos.PuedeAtender;
                btnCobrar.Visible = pedidos && Permisos.PuedeCobrar;
                btnUltimoComprobante.Visible = pedidos && Permisos.PuedeCobrar;
                btnResumenMensual.Visible = panelResumenDiario.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                btnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial;
                btnFacturaPdf.Visible = (TieneHistorialVentas || (!seleccionar && tipo == "Facturas")) && Permisos.PuedeVerHistorial;
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
                registros = pedidos.ListarPedidos(seleccionar, soloAbiertas: !historial);
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
                listado.DataSource = TieneHistorialVentas && chkFiltrarMes.Checked
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
            bool puedeDeshacer = PuedeDeshacerDespacho(fila);
            bool cobroMostrador = PuedeCobrarEnMostrador(fila);
            btnSeleccionar.Enabled = fila != null;
            btnMesa.Enabled = fila != null && puedeAvanzar && (!fila.Table.Columns.Contains("IdTipoEntrega") || Convert.ToInt32(fila["IdTipoEntrega"]) <= 2);
            btnAvanzar.Text = puedeDeshacer ? "Deshacer despacho" : "Despachar";
            btnAvanzar.Enabled = puedeAvanzar || puedeDeshacer;
            btnCobrar.Text = cobroMostrador ? "Cobrar ahora" : "Cobrar";
            btnCobrar.Enabled = Permisos.PuedeCobrar && (estado == "Entregado" || cobroMostrador);
            btnCancelar.Enabled = estado == "Pendiente";
            btnPedidoPdf.Enabled = fila != null;
            btnFacturaPdf.Enabled = fila != null && (tipo == "Facturas" || estado == "Pagado" ||
                tipo == "Pedidos" && Convert.ToString(fila["Pago"]) == "Anulado");
            btnLimpiarMesa.Enabled = fila != null && (estado == "Ocupada" || estado == "En limpieza");
            bool ventaActiva = !seleccionar && !historial && tipo == "Pedidos" && Permisos.PuedeAtender;
            btnEditarVenta.Visible = ventaActiva;
            btnEditarVenta.Enabled = ventaActiva && estado == "Pendiente";
            btnMesa.Visible = ventaActiva && btnMesa.Enabled;
            btnAvanzar.Visible = ventaActiva && btnAvanzar.Enabled;
            btnCobrar.Visible = ventaActiva && btnCobrar.Enabled;
            btnCancelar.Visible = ventaActiva && btnCancelar.Enabled;
            btnPedidoPdf.Visible = TieneHistorialVentas && Permisos.PuedeVerHistorial && btnPedidoPdf.Enabled;
            btnFacturaPdf.Visible = !seleccionar && (TieneHistorialVentas || tipo == "Facturas") &&
                Permisos.PuedeVerHistorial && btnFacturaPdf.Enabled;
        }

        private static bool PuedeCobrarEnMostrador(DataRow fila)
        {
            return fila != null && fila.Table.Columns.Contains("Entrega") &&
                Convert.ToString(fila["Entrega"]) == "Mostrador" &&
                Convert.ToString(fila["Estado"]) == "Pendiente" &&
                fila.IsNull("IdMesa");
        }

        private bool PuedeDeshacerDespacho(DataRow fila)
        {
            if (tipo != "Pedidos" || seleccionar || historial || !Permisos.PuedeAtender || fila == null)
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

        private void btnEditarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow fila = Seleccion();
                int id = Convert.ToInt32(fila["IdPedido"]);
                using (FrmPedidoLocalBase formulario = PantallasPersonal.CrearPedido())
                {
                    formulario.PrepararEdicion(id, Convert.ToString(fila["Cliente"]));
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                        Recargar(id);
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
                bool deshacer = PuedeDeshacerDespacho(fila);
                int idPedido = Convert.ToInt32(fila["IdPedido"]);
                if (deshacer)
                {
                    if (MessageBox.Show(this, "¿Deshacer el despacho de la venta " + idPedido + "? Volverá a quedar pendiente.",
                        Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                    pedidos.DeshacerDespacho(idPedido);
                }
                else
                {
                    if (!VistaGestion.Confirmar(this, "¿Confirmar el despacho del pedido?"))
                        return;
                    pedidos.DespacharPedido(idPedido, estado);
                }
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
                DataRow fila = Seleccion();
                int pedido = Convert.ToInt32(fila["IdPedido"]);
                using (Form formulario = PantallasPersonal.CrearCobro(pedido, PuedeCobrarEnMostrador(fila)))
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
            if (tipo != "Pedidos" || seleccionar || historial)
                return;
            try
            {
                DataRow fila = Seleccion();
                if (!VistaGestion.Confirmar(this, "¿Cancelar la venta pendiente seleccionada?"))
                    return;
                pedidos.CancelarPedido(Convert.ToInt32(fila["IdPedido"]));
                Recargar();
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnUltimoComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentoVenta documento = facturas.ObtenerUltimoComprobantePropio();
                if (documento == null)
                {
                    MessageBox.Show(this, "Esta cuenta aún no tiene comprobantes de ventas locales vigentes.", Text);
                    return;
                }

                DocumentoPdf.MostrarVistaPrevia(this, documento);
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
            try
            {
                int pedido = Convert.ToInt32(Seleccion()["IdPedido"]);
                using (var formulario = new UIAdministracion.FrmFacturasAdministracion(pedido))
                    formulario.ShowDialog(this);
                Recargar(pedido);
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
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
                MostrarResumen(new ResumenVentasDocumento(mes, facturas.ConsultarResumenMensual(mes), diario: false));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void btnResumenDiario_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dia = diaCaja.Value.Date;
                MostrarResumen(new ResumenVentasDocumento(dia, facturas.ConsultarResumenDiario(dia), diario: true));
            }
            catch (Exception error)
            {
                MostrarError(error);
            }
        }

        private void MostrarResumen(ResumenVentasDocumento reporte)
        {
            using (var vista = new FrmVistaPrevia(reporte.CrearPdf(), reporte.NombreArchivo, reporte.GuardarExcel,
                reporte.TituloVista, reporte.DetalleVista(), ResumenVentasDocumento.NotaFechas))
                vista.ShowDialog(this);
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            RecargarListado();
        }

        private void RecargarListado()
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
                bool datosDeHistorial = columna.Name == "Origen" || columna.Name == "RegistradoPor" ||
                    columna.Name == "AtendidoPor" || columna.Name == "Pago";
                if (columna.Name == "IdMesa" || columna.Name == "IdCategoria" || columna.Name == "IdTipoEntrega" ||
                    columna.Name == "Envio" || columna.Name == "Direccion" || columna.Name == "Contacto" ||
                    tipo == "Pedidos" && !historial && datosDeHistorial)
                    columna.Visible = false;
                if (tipo == "Mesas" && columna.Name == "Nombre")
                    columna.Visible = false;
                if (columna.ValueType == typeof(decimal))
                    columna.DefaultCellStyle.Format = "N2";
                if (columna.ValueType == typeof(DateTime))
                    columna.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (columna.Name == "IdPedido")
                    columna.HeaderText = "Venta";
                columna.MinimumWidth = columna.ValueType == typeof(DateTime) ? 132 : 70;
            }
        }
    }
}
