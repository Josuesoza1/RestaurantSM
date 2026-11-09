namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmPedidosEmpleado
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
            this.titulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titulo.Size = new System.Drawing.Size(1028, 53);
            this.titulo.Text = "PEDIDOS — EMPLEADO";
            // 
            // buscar
            // 
            this.buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buscar.Location = new System.Drawing.Point(0, 53);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Size = new System.Drawing.Size(319, 20);
            // 
            // paneles
            // 
            this.paneles.Location = new System.Drawing.Point(0, 98);
            this.paneles.Size = new System.Drawing.Size(1028, 411);
            this.paneles.SplitterDistance = 246;
            // 
            // FrmPedidosEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(528, 372);
            this.Name = "FrmPedidosEmpleado";
            this.Text = "Pedidos — Empleado";
            this.buscar.ResumeLayout(false);
            this.buscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paneles)).EndInit();
            this.paneles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
