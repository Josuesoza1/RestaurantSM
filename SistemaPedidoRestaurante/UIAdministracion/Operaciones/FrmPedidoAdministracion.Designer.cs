namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmPedidoAdministracion
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuitar.Location = new System.Drawing.Point(12, 503);
            this.btnQuitar.Size = new System.Drawing.Size(139, 27);
            this.btnQuitar.UseVisualStyleBackColor = false;
            // 
            // btnVaciar
            // 
            this.btnVaciar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnVaciar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVaciar.Location = new System.Drawing.Point(15, 536);
            this.btnVaciar.Size = new System.Drawing.Size(136, 27);
            this.btnVaciar.UseVisualStyleBackColor = false;
            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEnviar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEnviar.Location = new System.Drawing.Point(513, 531);
            this.btnEnviar.Size = new System.Drawing.Size(130, 32);
            this.btnEnviar.UseVisualStyleBackColor = false;
            // 
            // btnCobrarAhora
            // 
            this.btnCobrarAhora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCobrarAhora.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCobrarAhora.Location = new System.Drawing.Point(649, 531);
            this.btnCobrarAhora.Size = new System.Drawing.Size(123, 32);
            this.btnCobrarAhora.UseVisualStyleBackColor = false;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // btnAceptar
            // 
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            // 
            // cmbPrecio
            // 
            this.cmbPrecio.Size = new System.Drawing.Size(160, 23);
            // 
            // cmbProducto
            // 
            this.cmbProducto.Size = new System.Drawing.Size(320, 23);
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Size = new System.Drawing.Size(320, 23);
            // 
            // FrmPedidoAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Name = "FrmPedidoAdministracion";
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
