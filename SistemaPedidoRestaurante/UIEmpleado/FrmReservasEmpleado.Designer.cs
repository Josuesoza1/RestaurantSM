namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmReservasEmpleado
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "FrmReservasEmpleado";
            this.Text = "Reservas — Empleado";
            this.titulo.Text = "RESERVAS — EMPLEADO";
            this.paneles.Panel2Collapsed = true;
            this.btnSeleccionar.Visible = false;
            this.btnNuevo.Visible = false;
            this.btnMesa.Visible = false;
            this.btnMesas.Visible = true;
            this.btnAvanzar.Visible = false;
            this.btnCobrar.Visible = false;
            this.btnAtendida.Visible = true;
            this.btnAusencia.Visible = true;
            this.btnCancelar.Visible = true;
            this.btnPedidoPdf.Visible = false;
            this.btnFacturaPdf.Visible = false;
            this.btnLimpiarMesa.Visible = false;
            this.btnRecargar.Visible = true;
            this.btnVolver.Visible = true;
            this.btnCancelar.Text = "Cancelar reserva";
            this.acciones.Controls.SetChildIndex(this.btnMesas, 9);
            this.ResumeLayout(false);
        }
    }
}
