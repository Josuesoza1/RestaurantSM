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
            this.buscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).BeginInit();
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.Text = "RESERVAS ANTERIORES";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Size = new System.Drawing.Size(319, 20);
            // 
            // paneles
            // 
            this.paneles.Panel2Collapsed = true;
            this.paneles.Size = new System.Drawing.Size(1028, 399);
            this.paneles.SplitterDistance = 239;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Visible = false;
            // 
            // FrmReservasAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Name = "FrmReservasAdministracion";
            this.Text = "Reservas anteriores";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
