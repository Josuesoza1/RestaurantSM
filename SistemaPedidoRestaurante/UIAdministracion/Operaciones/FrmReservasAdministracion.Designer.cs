namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmReservasAdministracion
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
            this.Name = "FrmReservasAdministracion";
            this.Text = "Reservas anteriores";
            this.titulo.Text = "RESERVAS ANTERIORES";
            this.paneles.Panel2Collapsed = true;
            this.btnSeleccionar.Visible = false;
            this.btnNuevo.Visible = false;
            this.btnMesa.Visible = false;
            this.btnAvanzar.Visible = false;
            this.btnCobrar.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnPedidoPdf.Visible = false;
            this.btnFacturaPdf.Visible = false;
            this.btnLimpiarMesa.Visible = false;
            this.btnRecargar.Visible = true;
            this.btnVolver.Visible = true;
            this.ResumeLayout(false);
        }
    }
}
