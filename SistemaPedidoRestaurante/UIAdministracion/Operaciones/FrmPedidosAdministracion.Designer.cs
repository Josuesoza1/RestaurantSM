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
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.Text = "PEDIDOS — ADMINISTRACIÓN";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Size = new System.Drawing.Size(319, 20);
            // 
            // paneles
            // 
            this.paneles.Size = new System.Drawing.Size(1028, 399);
            this.paneles.SplitterDistance = 239;
            // 
            // FrmPedidosAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Name = "FrmPedidosAdministracion";
            this.Text = "Pedidos — Administración";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
