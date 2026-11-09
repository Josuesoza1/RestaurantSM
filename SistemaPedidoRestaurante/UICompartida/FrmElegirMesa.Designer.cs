namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmElegirMesa
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
            this.lblMesa = new System.Windows.Forms.Label();
            this.mesas = new System.Windows.Forms.ComboBox();
            this.asignar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.LightCoral;
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 480F));
            this.panel.Controls.Add(this.lblMesa, 0, 0);
            this.panel.Controls.Add(this.mesas, 0, 1);
            this.panel.Controls.Add(this.asignar, 0, 2);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.RowCount = 3;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel.Size = new System.Drawing.Size(520, 180);
            this.panel.TabIndex = 0;
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.Location = new System.Drawing.Point(23, 20);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(65, 13);
            this.lblMesa.TabIndex = 0;
            this.lblMesa.Text = "Mesa activa";
            // 
            // mesas
            // 
            this.mesas.DisplayMember = "Nombre";
            this.mesas.Dock = System.Windows.Forms.DockStyle.Top;
            this.mesas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mesas.Location = new System.Drawing.Point(23, 43);
            this.mesas.Name = "mesas";
            this.mesas.Size = new System.Drawing.Size(474, 21);
            this.mesas.TabIndex = 1;
            this.mesas.ValueMember = "IdMesa";
            // 
            // asignar
            // 
            this.asignar.AutoSize = true;
            this.asignar.BackColor = System.Drawing.Color.Pink;
            this.asignar.Location = new System.Drawing.Point(23, 63);
            this.asignar.Name = "asignar";
            this.asignar.Size = new System.Drawing.Size(80, 23);
            this.asignar.TabIndex = 2;
            this.asignar.Text = "Asignar mesa";
            this.asignar.UseVisualStyleBackColor = false;
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            // 
            // FrmElegirMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 180);
            this.Controls.Add(this.panel);
            this.MinimumSize = new System.Drawing.Size(536, 219);
            this.Name = "FrmElegirMesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asignar mesa";
            this.Load += new System.EventHandler(this.cargar);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.ComboBox mesas;
        private System.Windows.Forms.Button asignar;
    }
}
