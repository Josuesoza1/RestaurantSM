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
            this.components = new System.ComponentModel.Container();
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
            this.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cantidad)).BeginInit();
            this.panel.Name = "panel";
            this.lblProducto.Name = "lblProducto";
            this.productos.Name = "productos";
            this.existencias.Name = "existencias";
            this.tipo.Name = "tipo";
            this.lblUnidades.Name = "lblUnidades";
            this.cantidad.Name = "cantidad";
            this.lblMotivo.Name = "lblMotivo";
            this.motivo.Name = "motivo";
            this.guardar.Name = "guardar";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 9;
            this.lblProducto.Text = "Producto con control activo";
            this.lblProducto.AutoSize = true;
            this.productos.Dock = System.Windows.Forms.DockStyle.Top;
            this.productos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productos.DisplayMember = "Producto";
            this.productos.SelectedValueChanged += new System.EventHandler(this.productos_SelectedValueChanged);
            this.existencias.AutoSize = true;
            this.existencias.Text = "Sin registro activo";
            this.tipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipo.Items.AddRange(new object[] { "Entrada", "Salida" });
            this.tipo.SelectedIndex = 0;
            this.lblUnidades.Text = "Unidades";
            this.lblUnidades.AutoSize = true;
            this.cantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.cantidad.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
            this.cantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.lblMotivo.Text = "Motivo de entrada o salida";
            this.lblMotivo.AutoSize = true;
            this.motivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.motivo.MaxLength = 200;
            this.guardar.Text = "Guardar movimiento";
            this.guardar.AutoSize = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.panel.Controls.Add(this.lblProducto, 0, 0);
            this.panel.Controls.Add(this.productos, 0, 1);
            this.panel.Controls.Add(this.existencias, 0, 2);
            this.panel.Controls.Add(this.tipo, 0, 3);
            this.panel.Controls.Add(this.lblUnidades, 0, 4);
            this.panel.Controls.Add(this.cantidad, 0, 5);
            this.panel.Controls.Add(this.lblMotivo, 0, 6);
            this.panel.Controls.Add(this.motivo, 0, 7);
            this.panel.Controls.Add(this.guardar, 0, 8);
            this.Controls.Add(this.panel);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 340);
            this.MinimumSize = new System.Drawing.Size(636, 379);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmAjusteInventario";
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
