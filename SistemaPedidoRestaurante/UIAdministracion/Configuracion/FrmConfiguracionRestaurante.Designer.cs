namespace SistemaPedidoRestaurante.UIAdministracion
{
    partial class FrmConfiguracionRestaurante
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracionRestaurante));
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.tasa = new System.Windows.Forms.NumericUpDown();
            this.explicacion = new System.Windows.Forms.Label();
            this.guardar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasa)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 480F));
            this.panel.Controls.Add(this.lblNombre, 0, 0);
            this.panel.Controls.Add(this.nombre, 0, 1);
            this.panel.Controls.Add(this.lblImpuesto, 0, 2);
            this.panel.Controls.Add(this.tasa, 0, 3);
            this.panel.Controls.Add(this.explicacion, 0, 4);
            this.panel.Controls.Add(this.guardar, 0, 5);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.RowCount = 6;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.Size = new System.Drawing.Size(520, 400);
            this.panel.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(23, 20);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(117, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre del restaurante";
            // 
            // nombre
            // 
            this.nombre.Dock = System.Windows.Forms.DockStyle.Top;
            this.nombre.Location = new System.Drawing.Point(23, 36);
            this.nombre.MaxLength = 120;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(474, 20);
            this.nombre.TabIndex = 1;
            // 
            // lblImpuesto
            // 
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Location = new System.Drawing.Point(23, 59);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(106, 13);
            this.lblImpuesto.TabIndex = 2;
            this.lblImpuesto.Text = "Impuesto incluido (%)";
            // 
            // tasa
            // 
            this.tasa.DecimalPlaces = 2;
            this.tasa.Location = new System.Drawing.Point(23, 75);
            this.tasa.Name = "tasa";
            this.tasa.Size = new System.Drawing.Size(120, 20);
            this.tasa.TabIndex = 3;
            // 
            // explicacion
            // 
            this.explicacion.AutoSize = true;
            this.explicacion.Location = new System.Drawing.Point(23, 98);
            this.explicacion.MaximumSize = new System.Drawing.Size(460, 0);
            this.explicacion.Name = "explicacion";
            this.explicacion.Size = new System.Drawing.Size(447, 26);
            this.explicacion.TabIndex = 4;
            this.explicacion.Text = "Los precios del menu son finales. La tasa se desglosa dentro de ese precio y se a" +
    "plica solo a nuevas facturas.";
            // 
            // guardar
            // 
            this.guardar.AutoSize = true;
            this.guardar.Location = new System.Drawing.Point(23, 127);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(122, 23);
            this.guardar.TabIndex = 5;
            this.guardar.Text = "Guardar configuracion";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // FrmConfiguracionRestaurante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 400);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(536, 439);
            this.Name = "FrmConfiguracionRestaurante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuracion del restaurante";
            this.Load += new System.EventHandler(this.cargar);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasa)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.Label lblImpuesto;
        private System.Windows.Forms.NumericUpDown tasa;
        private System.Windows.Forms.Label explicacion;
        private System.Windows.Forms.Button guardar;
    }
}
