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
            this.paneles.SuspendLayout();
            this.SuspendLayout();
            // 
            // titulo
            // 
            this.titulo.BackColor = System.Drawing.Color.Maroon;
            this.titulo.Text = "MESAS — EMPLEADO";
            // 
            // buscar
            // 
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
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
            // FrmMesasEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Name = "FrmMesasEmpleado";
            this.Text = "Mesas — Empleado";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
