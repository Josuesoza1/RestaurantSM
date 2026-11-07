namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmMovimientosAdministracion
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.Panel1.SuspendLayout();
            this.paneles.Panel2.SuspendLayout();
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.Text = "MOVIMIENTOS — ADMINISTRACIÓN";
            // 
            // paneles
            // 
            this.paneles.Panel2Collapsed = true;
            // 
            // FrmMovimientosAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1445, 814);
            this.Name = "FrmMovimientosAdministracion";
            this.Text = "Movimientos — Administración";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            this.paneles.Panel1.ResumeLayout(false);
            this.paneles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
