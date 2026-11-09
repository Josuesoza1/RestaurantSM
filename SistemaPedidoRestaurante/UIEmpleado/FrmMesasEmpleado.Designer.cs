namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmMesasEmpleado
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
            this.titulo.BackColor = System.Drawing.Color.Maroon;
            this.titulo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.titulo.Size = new System.Drawing.Size(1371, 98);
            this.titulo.Text = "MESAS — EMPLEADO";
            // 
            // buscar
            // 
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buscar.Location = new System.Drawing.Point(0, 98);
            this.buscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buscar.Padding = new System.Windows.Forms.Padding(12, 7, 12, 7);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(250, 7);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.txtBuscar.Size = new System.Drawing.Size(433, 22);
            // 
            // etiquetaBuscar
            // 
            this.etiquetaBuscar.Location = new System.Drawing.Point(163, 7);
            this.etiquetaBuscar.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.etiquetaBuscar.Size = new System.Drawing.Size(87, 41);
            // 
            // paneles
            // 
            this.paneles.Location = new System.Drawing.Point(0, 187);
            this.paneles.Margin = new System.Windows.Forms.Padding(5);
            this.paneles.Panel2Collapsed = true;
            this.paneles.Size = new System.Drawing.Size(1371, 439);
            this.paneles.SplitterDistance = 239;
            this.paneles.SplitterWidth = 6;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(12, 7);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5);
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.btnNuevo.Size = new System.Drawing.Size(151, 41);
            this.btnNuevo.Visible = false;
            // 
            // FrmMesasEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1371, 750);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(1229, 654);
            this.Name = "FrmMesasEmpleado";
            this.Text = "Mesas — Empleado";
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
