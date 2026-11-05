namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmMesasAdministracion
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
            this.Name = "FrmMesasAdministracion";
            this.Text = "Mesas — Administración";
            this.titulo.Text = "MESAS — ADMINISTRACIÓN";
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
            this.btnFacturaPdf.Visible = false;
            this.btnLimpiarMesa.Visible = true;
            this.btnRecargar.Visible = true;
            this.btnVolver.Visible = true;
            this.ResumeLayout(false);
        }
    }
}
