namespace SistemaPedidoRestaurante.UICliente
{
    partial class FrmMenuCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpAdmin = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hacerPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reservarMesaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verComprasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpAdmin.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAdmin
            // 
            this.grpAdmin.BackColor = System.Drawing.Color.LightSteelBlue;
            this.grpAdmin.Controls.Add(this.menuStrip1);
            this.grpAdmin.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpAdmin.Location = new System.Drawing.Point(12, 98);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAdmin.Size = new System.Drawing.Size(649, 316);
            this.grpAdmin.TabIndex = 13;
            this.grpAdmin.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hacerPedidoToolStripMenuItem,
            this.reservarMesaToolStripMenuItem,
            this.verComprasToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 19);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(641, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hacerPedidoToolStripMenuItem
            // 
            this.hacerPedidoToolStripMenuItem.Name = "hacerPedidoToolStripMenuItem";
            this.hacerPedidoToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.hacerPedidoToolStripMenuItem.Text = "Hacer Pedido";
            // 
            // reservarMesaToolStripMenuItem
            // 
            this.reservarMesaToolStripMenuItem.Name = "reservarMesaToolStripMenuItem";
            this.reservarMesaToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.reservarMesaToolStripMenuItem.Text = "Reservar Mesa";
            // 
            // verComprasToolStripMenuItem
            // 
            this.verComprasToolStripMenuItem.Name = "verComprasToolStripMenuItem";
            this.verComprasToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.verComprasToolStripMenuItem.Text = "Ver Historial";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Location = new System.Drawing.Point(12, 3);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(649, 86);
            this.pnlEncabezado.TabIndex = 12;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(217, 26);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(163, 35);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "CLIENTES";
            // 
            // FrmMenuCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(672, 421);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.pnlEncabezado);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmMenuCliente";
            this.Text = "Menú para Clientes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMenuCliente_FormClosed);
            this.grpAdmin.ResumeLayout(false);
            this.grpAdmin.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAdmin;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ToolStripMenuItem hacerPedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reservarMesaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verComprasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}