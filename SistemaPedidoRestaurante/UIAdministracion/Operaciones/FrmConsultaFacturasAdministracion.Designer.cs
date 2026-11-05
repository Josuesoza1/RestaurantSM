namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmConsultaFacturasAdministracion
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
            this.Name = "FrmConsultaFacturasAdministracion";
            this.Text = "Facturas — Administración";
            this.titulo.Text = "FACTURAS — ADMINISTRACIÓN";
            this.paneles.Panel2Collapsed = true;
            this.btnSeleccionar.Visible = false;
            this.btnNuevo.Visible = false;
            this.btnMesa.Visible = false;
            this.btnMesas.Visible = false;
            this.btnAvanzar.Visible = false;
            this.btnCobrar.Visible = false;
            this.btnAtendida.Visible = false;
            this.btnAusencia.Visible = false;
            this.btnCancelar.Visible = false;
            this.btnPedidoPdf.Visible = false;
            this.btnFacturaPdf.Visible = true;
            this.btnLimpiarMesa.Visible = false;
            this.btnRecargar.Visible = true;
            this.btnVolver.Visible = true;
            this.btnFacturaPdf.Text = "Guardar PDF";
            this.ResumeLayout(false);
        }
    }
}
