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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblImpuesto = new System.Windows.Forms.Label();
            this.tasa = new System.Windows.Forms.NumericUpDown();
            this.explicacion = new System.Windows.Forms.Label();
            this.guardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tasa)).BeginInit();
            this.panel.Name = "panel";
            this.lblNombre.Name = "lblNombre";
            this.nombre.Name = "nombre";
            this.lblImpuesto.Name = "lblImpuesto";
            this.tasa.Name = "tasa";
            this.explicacion.Name = "explicacion";
            this.guardar.Name = "guardar";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 6;
            this.lblNombre.Text = "Nombre del restaurante";
            this.lblNombre.AutoSize = true;
            this.nombre.MaxLength = 120;
            this.nombre.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblImpuesto.Text = "Impuesto incluido (%)";
            this.lblImpuesto.AutoSize = true;
            this.tasa.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.tasa.DecimalPlaces = 2;
            this.explicacion.Text = "Los precios del menu son finales. La tasa se desglosa dentro de ese precio y se aplica solo a nuevas facturas.";
            this.explicacion.AutoSize = true;
            this.explicacion.MaximumSize = new System.Drawing.Size(460, 0);
            this.guardar.Text = "Guardar configuracion";
            this.guardar.AutoSize = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.panel.Controls.Add(this.lblNombre, 0, 0);
            this.panel.Controls.Add(this.nombre, 0, 1);
            this.panel.Controls.Add(this.lblImpuesto, 0, 2);
            this.panel.Controls.Add(this.tasa, 0, 3);
            this.panel.Controls.Add(this.explicacion, 0, 4);
            this.panel.Controls.Add(this.guardar, 0, 5);
            this.Controls.Add(this.panel);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 280);
            this.MinimumSize = new System.Drawing.Size(536, 319);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmConfiguracionRestaurante";
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
