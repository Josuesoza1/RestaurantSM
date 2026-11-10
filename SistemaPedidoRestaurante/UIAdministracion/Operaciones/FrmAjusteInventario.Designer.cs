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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAjusteInventario));
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.lblProducto = new System.Windows.Forms.Label();
            this.productos = new System.Windows.Forms.ComboBox();
            this.existencias = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.tipo = new System.Windows.Forms.ComboBox();
            this.lblUnidades = new System.Windows.Forms.Label();
            this.cantidad = new System.Windows.Forms.TextBox();
            this.lblRango = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.motivo = new System.Windows.Forms.TextBox();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.guardar = new System.Windows.Forms.Button();
            this.volver = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel.Controls.Add(this.lblProducto, 0, 0);
            this.panel.Controls.Add(this.productos, 0, 1);
            this.panel.Controls.Add(this.existencias, 0, 2);
            this.panel.Controls.Add(this.lblTipo, 0, 3);
            this.panel.Controls.Add(this.tipo, 0, 4);
            this.panel.Controls.Add(this.lblUnidades, 0, 5);
            this.panel.Controls.Add(this.cantidad, 0, 6);
            this.panel.Controls.Add(this.lblRango, 0, 7);
            this.panel.Controls.Add(this.lblMotivo, 0, 8);
            this.panel.Controls.Add(this.motivo, 0, 9);
            this.panel.Controls.Add(this.acciones, 0, 10);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.RowCount = 11;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
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
            this.productos.AccessibleName = "Producto con inventario activo";
            this.productos.DisplayMember = "Producto";
            this.productos.Dock = System.Windows.Forms.DockStyle.Top;
            this.productos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productos.Location = new System.Drawing.Point(23, 36);
            this.productos.Name = "productos";
            this.productos.Size = new System.Drawing.Size(574, 21);
            this.productos.TabIndex = 1;
            this.productos.SelectedValueChanged += new System.EventHandler(this.datosMovimiento_Cambiados);
            // 
            // existencias
            // 
            this.existencias.AutoSize = true;
            this.existencias.Location = new System.Drawing.Point(23, 60);
            this.existencias.Name = "existencias";
            this.existencias.Size = new System.Drawing.Size(239, 13);
            this.existencias.TabIndex = 2;
            this.existencias.Text = "Seleccione un producto para ver sus existencias.";
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(23, 73);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(99, 13);
            this.lblTipo.TabIndex = 3;
            this.lblTipo.Text = "Tipo de movimiento";
            // 
            // tipo
            // 
            this.tipo.AccessibleName = "Tipo de movimiento";
            this.tipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipo.Items.AddRange(new object[] {
            "Entrada",
            "Salida"});
            this.tipo.Location = new System.Drawing.Point(23, 89);
            this.tipo.Name = "tipo";
            this.tipo.Size = new System.Drawing.Size(574, 21);
            this.tipo.TabIndex = 4;
            this.tipo.SelectedIndexChanged += new System.EventHandler(this.datosMovimiento_Cambiados);
            // 
            // lblUnidades
            // 
            this.lblUnidades.AutoSize = true;
            this.lblUnidades.Location = new System.Drawing.Point(23, 113);
            this.lblUnidades.Name = "lblUnidades";
            this.lblUnidades.Size = new System.Drawing.Size(110, 13);
            this.lblUnidades.TabIndex = 4;
            this.lblUnidades.Text = "Cantidad en unidades";
            // 
            // cantidad
            // 
            this.cantidad.AccessibleName = "Cantidad en unidades";
            this.cantidad.Enabled = false;
            this.cantidad.Location = new System.Drawing.Point(23, 129);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(120, 20);
            this.cantidad.TabIndex = 6;
            this.cantidad.TextChanged += new System.EventHandler(this.datosMovimiento_Cambiados);
            // 
            // lblRango
            // 
            this.lblRango.AutoSize = true;
            this.lblRango.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRango.Location = new System.Drawing.Point(23, 152);
            this.lblRango.Name = "lblRango";
            this.lblRango.Size = new System.Drawing.Size(574, 13);
            this.lblRango.TabIndex = 7;
            this.lblRango.Text = "Elija Entrada para sumar unidades o Salida para retirarlas.";
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(23, 165);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(96, 13);
            this.lblMotivo.TabIndex = 6;
            this.lblMotivo.Text = "Motivo (obligatorio)";
            // 
            // motivo
            // 
            this.motivo.AccessibleName = "Motivo del movimiento";
            this.motivo.Dock = System.Windows.Forms.DockStyle.Top;
            this.motivo.Location = new System.Drawing.Point(23, 181);
            this.motivo.MaxLength = 200;
            this.motivo.Name = "motivo";
            this.motivo.Size = new System.Drawing.Size(574, 20);
            this.motivo.TabIndex = 9;
            this.motivo.TextChanged += new System.EventHandler(this.datosMovimiento_Cambiados);
            this.acciones.AutoSize = true;
            this.acciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.acciones.Controls.Add(this.guardar);
            this.acciones.Controls.Add(this.volver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acciones.Margin = new System.Windows.Forms.Padding(0);
            this.acciones.Name = "acciones";
            this.acciones.TabIndex = 10;
            // 
            // guardar
            // 
            this.guardar.AutoSize = true;
            this.guardar.Enabled = false;
            this.guardar.Location = new System.Drawing.Point(3, 3);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(111, 23);
            this.guardar.TabIndex = 0;
            this.guardar.Text = "Guardar movimiento";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.volver.AutoSize = true;
            this.volver.Location = new System.Drawing.Point(120, 3);
            this.volver.Name = "volver";
            this.volver.Size = new System.Drawing.Size(75, 23);
            this.volver.TabIndex = 1;
            this.volver.Text = "Volver";
            this.volver.Click += new System.EventHandler(this.volver_Click);
            // 
            // FrmAjusteInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.volver;
            this.ClientSize = new System.Drawing.Size(620, 340);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(636, 379);
            this.Name = "FrmAjusteInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Entrada o salida de inventario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAjusteInventario_FormClosing);
            this.Load += new System.EventHandler(this.cargar);
            this.acciones.ResumeLayout(false);
            this.acciones.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox productos;
        private System.Windows.Forms.Label existencias;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox tipo;
        private System.Windows.Forms.Label lblRango;
        private System.Windows.Forms.Label lblUnidades;
        private System.Windows.Forms.TextBox cantidad;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox motivo;
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button guardar;
        private System.Windows.Forms.Button volver;
    }
}
