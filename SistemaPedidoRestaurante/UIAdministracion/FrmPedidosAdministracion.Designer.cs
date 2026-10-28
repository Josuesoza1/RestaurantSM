namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmPedidosAdministracion
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
            this.Name = "FrmPedidosAdministracion";
            this.Text = "Pedidos — Administración";
            this.titulo.Text = "PEDIDOS — ADMINISTRACIÓN";
            this.paneles.Panel2Collapsed = false;
            this.btnSeleccionar.Visible = false;
            this.btnNuevo.Visible = true;
            this.btnMesa.Visible = true;
            this.btnMesas.Visible = true;
            this.btnAvanzar.Visible = true;
            this.btnCobrar.Visible = true;
            this.btnAtendida.Visible = false;
            this.btnAusencia.Visible = false;
            this.btnCancelar.Visible = true;
            this.btnPedidoPdf.Visible = true;
            this.btnFacturaPdf.Visible = true;
            this.btnLimpiarMesa.Visible = false;
            this.btnRecargar.Visible = true;
            this.btnVolver.Visible = true;
            this.ResumeLayout(false);
        }
    }
}
