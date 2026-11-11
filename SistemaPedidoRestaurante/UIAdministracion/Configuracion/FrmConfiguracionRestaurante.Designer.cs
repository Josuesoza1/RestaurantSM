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
            this.descuentoActivo = new System.Windows.Forms.CheckBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.porcentajeDescuento = new System.Windows.Forms.NumericUpDown();
            this.lblFechaDescuento = new System.Windows.Forms.Label();
            this.fechaDescuento = new System.Windows.Forms.DateTimePicker();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasa)).BeginInit();
            this.SuspendLayout();
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel.Controls.Add(this.lblNombre, 0, 0);
            this.panel.Controls.Add(this.nombre, 0, 1);
            this.panel.Controls.Add(this.lblImpuesto, 0, 2);
            this.panel.Controls.Add(this.tasa, 0, 3);
            this.panel.Controls.Add(this.descuentoActivo, 0, 4);
            this.panel.Controls.Add(this.lblDescuento, 0, 5);
            this.panel.Controls.Add(this.porcentajeDescuento, 0, 6);
            this.panel.Controls.Add(this.lblFechaDescuento, 0, 7);
            this.panel.Controls.Add(this.fechaDescuento, 0, 8);
            this.panel.Controls.Add(this.explicacion, 0, 9);
            this.panel.Controls.Add(this.guardar, 0, 10);
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
            this.panel.Size = new System.Drawing.Size(520, 460);
            this.panel.TabIndex = 0;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(23, 20);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(117, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre del restaurante";
            this.nombre.Dock = System.Windows.Forms.DockStyle.Top;
            this.nombre.Location = new System.Drawing.Point(23, 36);
            this.nombre.MaxLength = 120;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(474, 20);
            this.nombre.TabIndex = 1;
            this.lblImpuesto.AutoSize = true;
            this.lblImpuesto.Location = new System.Drawing.Point(23, 59);
            this.lblImpuesto.Name = "lblImpuesto";
            this.lblImpuesto.Size = new System.Drawing.Size(106, 13);
            this.lblImpuesto.TabIndex = 2;
            this.lblImpuesto.Text = "Impuesto a sumar (%)";
            this.tasa.DecimalPlaces = 2;
            this.tasa.Location = new System.Drawing.Point(23, 75);
            this.tasa.Name = "tasa";
            this.tasa.Size = new System.Drawing.Size(120, 20);
            this.tasa.TabIndex = 3;
            this.explicacion.AutoSize = true;
            this.explicacion.Location = new System.Drawing.Point(23, 98);
            this.explicacion.MaximumSize = new System.Drawing.Size(460, 0);
            this.explicacion.Name = "explicacion";
            this.explicacion.Size = new System.Drawing.Size(447, 26);
            this.explicacion.TabIndex = 4;
            this.explicacion.Text = "Subtotal menos descuento, más impuesto sobre el subtotal descontado. El descuento solo se aplica a cuentas abiertas en la fecha elegida. Las cuentas abiertas conservan sus tasas.";
            this.guardar.AutoSize = true;
            this.guardar.Location = new System.Drawing.Point(23, 127);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(122, 23);
            this.guardar.TabIndex = 5;
            this.guardar.Text = "Guardar configuracion";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.descuentoActivo.AutoSize = true;
            this.descuentoActivo.Name = "descuentoActivo";
            this.descuentoActivo.Text = "Aplicar descuento en una fecha";
            this.descuentoActivo.CheckedChanged += new System.EventHandler(this.descuentoActivo_CheckedChanged);
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Text = "Descuento (%) de 0 a 100";
            this.lblDescuento.Name = "lblDescuento";
            this.porcentajeDescuento.DecimalPlaces = 2;
            this.porcentajeDescuento.Enabled = false;
            this.porcentajeDescuento.Name = "porcentajeDescuento";
            this.porcentajeDescuento.Size = new System.Drawing.Size(120, 20);
            this.lblFechaDescuento.AutoSize = true;
            this.lblFechaDescuento.Name = "lblFechaDescuento";
            this.lblFechaDescuento.Text = "Fecha de aplicación";
            this.fechaDescuento.Enabled = false;
            this.fechaDescuento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaDescuento.Name = "fechaDescuento";
            this.fechaDescuento.Size = new System.Drawing.Size(160, 20);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 460);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(536, 499);
            this.Name = "FrmConfiguracionRestaurante";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfiguracionRestaurante_FormClosing);
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
        private System.Windows.Forms.CheckBox descuentoActivo;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.NumericUpDown porcentajeDescuento;
        private System.Windows.Forms.Label lblFechaDescuento;
        private System.Windows.Forms.DateTimePicker fechaDescuento;
        private System.Windows.Forms.Button guardar;
    }
}
