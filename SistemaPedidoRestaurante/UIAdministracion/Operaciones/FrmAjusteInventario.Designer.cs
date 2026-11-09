namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmAjusteInventario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Codigo generado por el disenador de Windows Forms
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.lblProducto = new System.Windows.Forms.Label();
            this.productos = new System.Windows.Forms.ComboBox();
            this.existencias = new System.Windows.Forms.Label();
            this.tipo = new System.Windows.Forms.ComboBox();
            this.lblUnidades = new System.Windows.Forms.Label();
            this.cantidad = new System.Windows.Forms.NumericUpDown();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.motivo = new System.Windows.Forms.TextBox();
            this.guardar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 580F));
            this.panel.Controls.Add(this.lblProducto, 0, 0);
            this.panel.Controls.Add(this.productos, 0, 1);
            this.panel.Controls.Add(this.existencias, 0, 2);
            this.panel.Controls.Add(this.tipo, 0, 3);
            this.panel.Controls.Add(this.lblUnidades, 0, 4);
            this.panel.Controls.Add(this.cantidad, 0, 5);
            this.panel.Controls.Add(this.lblMotivo, 0, 6);
            this.panel.Controls.Add(this.motivo, 0, 7);
            this.panel.Controls.Add(this.guardar, 0, 8);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.RowCount = 9;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.Size = new System.Drawing.Size(620, 340);
            this.panel.TabIndex = 0;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(23, 20);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(138, 13);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto con control activo";
            // 
            // productos
            // 
            this.productos.DisplayMember = "Producto";
            this.productos.Dock = System.Windows.Forms.DockStyle.Top;
            this.productos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productos.Location = new System.Drawing.Point(23, 36);
            this.productos.Name = "productos";
            this.productos.Size = new System.Drawing.Size(574, 21);
            this.productos.TabIndex = 1;
            this.productos.SelectedValueChanged += new System.EventHandler(this.productos_SelectedValueChanged);
            // 
            // existencias
            // 
            this.existencias.AutoSize = true;
            this.existencias.Location = new System.Drawing.Point(23, 60);
            this.existencias.Name = "existencias";
            this.existencias.Size = new System.Drawing.Size(91, 13);
            this.existencias.TabIndex = 2;
            this.existencias.Text = "Sin registro activo";
            // 
            // tipo
            // 
            this.tipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipo.Items.AddRange(new object[] {
            "Entrada",
            "Salida"});
            this.tipo.Location = new System.Drawing.Point(23, 76);
            this.tipo.Name = "tipo";
            this.tipo.Size = new System.Drawing.Size(574, 21);
            this.tipo.TabIndex = 3;
            // 
            // lblUnidades
            // 
            this.lblUnidades.AutoSize = true;
            this.lblUnidades.Location = new System.Drawing.Point(23, 100);
            this.lblUnidades.Name = "lblUnidades";
            this.lblUnidades.Size = new System.Drawing.Size(52, 13);
            this.lblUnidades.TabIndex = 4;
            this.lblUnidades.Text = "Unidades";
            // 
            // cantidad
            // 
            this.cantidad.Location = new System.Drawing.Point(23, 116);
            this.cantidad.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.cantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(120, 20);
            this.cantidad.TabIndex = 5;
            this.cantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(23, 139);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(132, 13);
            this.lblMotivo.TabIndex = 6;
            this.lblMotivo.Text = "Motivo de entrada o salida";
            // 
            // motivo
            // 
            this.motivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.motivo.Location = new System.Drawing.Point(23, 155);
            this.motivo.MaxLength = 200;
            this.motivo.Name = "motivo";
            this.motivo.Size = new System.Drawing.Size(574, 20);
            this.motivo.TabIndex = 7;
            // 
            // guardar
            // 
            this.guardar.AutoSize = true;
            this.guardar.Location = new System.Drawing.Point(23, 181);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(111, 23);
            this.guardar.TabIndex = 8;
            this.guardar.Text = "Guardar movimiento";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // FrmAjusteInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 340);
            this.Controls.Add(this.panel);
            this.MinimumSize = new System.Drawing.Size(636, 379);
            this.Name = "FrmAjusteInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Entrada o salida de inventario";
            this.Load += new System.EventHandler(this.cargar);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantidad)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox productos;
        private System.Windows.Forms.Label existencias;
        private System.Windows.Forms.ComboBox tipo;
        private System.Windows.Forms.Label lblUnidades;
        private System.Windows.Forms.NumericUpDown cantidad;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox motivo;
        private System.Windows.Forms.Button guardar;
    }
}
