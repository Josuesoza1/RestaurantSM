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
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titulo.Size = new System.Drawing.Size(1028, 53);
            this.titulo.Text = "FACTURAS — ADMINISTRACIÓN";
            // 
            // buscar
            // 
            this.buscar.Location = new System.Drawing.Point(0, 53);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBuscar.Size = new System.Drawing.Size(319, 20);
            // 
            // paneles
            // 
            this.paneles.Location = new System.Drawing.Point(0, 98);
            this.paneles.Panel2Collapsed = true;
            this.paneles.Size = new System.Drawing.Size(1028, 411);
            this.paneles.SplitterDistance = 239;
            // 
            // FrmConsultaFacturasAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(528, 372);
            this.Name = "FrmConsultaFacturasAdministracion";
            this.Text = "Facturas — Administración";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
