namespace SistemaPedidoRestaurante.UIEmpleado
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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.lblMesa = new System.Windows.Forms.Label();
            this.mesas = new System.Windows.Forms.ComboBox();
            this.asignar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.panel.SuspendLayout();
            this.panel.Name = "panel";
            this.lblMesa.Name = "lblMesa";
            this.mesas.Name = "mesas";
            this.asignar.Name = "asignar";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 3;
            this.lblMesa.Text = "Mesa activa";
            this.lblMesa.AutoSize = true;
            this.mesas.Dock = System.Windows.Forms.DockStyle.Top;
            this.mesas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mesas.DisplayMember = "Nombre";
            this.mesas.ValueMember = "IdMesa";
            this.asignar.Text = "Asignar mesa";
            this.asignar.AutoSize = true;
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            this.panel.Controls.Add(this.lblMesa, 0, 0);
            this.panel.Controls.Add(this.mesas, 0, 1);
            this.panel.Controls.Add(this.asignar, 0, 2);
            this.Controls.Add(this.panel);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 180);
            this.MinimumSize = new System.Drawing.Size(536, 219);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmElegirMesa";
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
