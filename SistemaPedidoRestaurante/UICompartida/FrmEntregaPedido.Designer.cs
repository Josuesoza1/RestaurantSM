namespace SistemaPedidoRestaurante.UICompartida
{
    partial class FrmEntregaPedido
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.resumenOrigen = new System.Windows.Forms.Label();
            this.lblModalidad = new System.Windows.Forms.Label();
            this.modalidad = new System.Windows.Forms.ComboBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.direccion = new System.Windows.Forms.TextBox();
            this.lblReferencia = new System.Windows.Forms.Label();
            this.referencia = new System.Windows.Forms.TextBox();
            this.lblContacto = new System.Windows.Forms.Label();
            this.contacto = new System.Windows.Forms.TextBox();
            this.importes = new System.Windows.Forms.Label();
            this.botones = new System.Windows.Forms.FlowLayoutPanel();
            this.confirmar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.botones.SuspendLayout();
            this.SuspendLayout();
            this.panel.Name = "panel";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.AutoScroll = true;
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 11;
            this.resumenOrigen.Name = "resumenOrigen";
            this.resumenOrigen.AutoSize = true;
            this.resumenOrigen.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblModalidad.Text = "Como recibira el pedido";
            this.lblModalidad.AutoSize = true;
            this.modalidad.Name = "modalidad";
            this.modalidad.Dock = System.Windows.Forms.DockStyle.Top;
            this.modalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modalidad.SelectedValueChanged += new System.EventHandler(this.modalidad_SelectedValueChanged);
            this.lblDireccion.Text = "Direccion de entrega (obligatoria para domicilio)";
            this.lblDireccion.AutoSize = true;
            this.direccion.Name = "direccion";
            this.direccion.MaxLength = 250;
            this.direccion.Multiline = true;
            this.direccion.Height = 55;
            this.direccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReferencia.Text = "Referencia (opcional)";
            this.lblReferencia.AutoSize = true;
            this.referencia.Name = "referencia";
            this.referencia.MaxLength = 150;
            this.referencia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContacto.Text = "Telefono de contacto";
            this.lblContacto.AutoSize = true;
            this.contacto.Name = "contacto";
            this.contacto.MaxLength = 20;
            this.contacto.Dock = System.Windows.Forms.DockStyle.Top;
            this.importes.Name = "importes";
            this.importes.AutoSize = true;
            this.importes.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.botones.Name = "botones";
            this.botones.Dock = System.Windows.Forms.DockStyle.Top;
            this.botones.AutoSize = true;
            this.confirmar.Name = "confirmar";
            this.confirmar.Text = "Confirmar pedido";
            this.confirmar.AutoSize = true;
            this.confirmar.Click += new System.EventHandler(this.confirmar_Click);
            this.cancelar.Name = "cancelar";
            this.cancelar.Text = "Volver al detalle";
            this.cancelar.AutoSize = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            this.botones.Controls.Add(this.confirmar);
            this.botones.Controls.Add(this.cancelar);
            this.panel.Controls.Add(this.resumenOrigen, 0, 0);
            this.panel.Controls.Add(this.lblModalidad, 0, 1);
            this.panel.Controls.Add(this.modalidad, 0, 2);
            this.panel.Controls.Add(this.lblDireccion, 0, 3);
            this.panel.Controls.Add(this.direccion, 0, 4);
            this.panel.Controls.Add(this.lblReferencia, 0, 5);
            this.panel.Controls.Add(this.referencia, 0, 6);
            this.panel.Controls.Add(this.lblContacto, 0, 7);
            this.panel.Controls.Add(this.contacto, 0, 8);
            this.panel.Controls.Add(this.importes, 0, 9);
            this.panel.Controls.Add(this.botones, 0, 10);
            this.Controls.Add(this.panel);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 460);
            this.MinimumSize = new System.Drawing.Size(586, 499);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Confirmar entrega y total";
            this.Name = "FrmEntregaPedido";
            this.Load += new System.EventHandler(this.FrmEntregaPedido_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.botones.ResumeLayout(false);
            this.botones.PerformLayout();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Label resumenOrigen, lblModalidad, lblDireccion, lblReferencia, lblContacto, importes;
        private System.Windows.Forms.ComboBox modalidad;
        private System.Windows.Forms.TextBox direccion, referencia, contacto;
        private System.Windows.Forms.FlowLayoutPanel botones;
        private System.Windows.Forms.Button confirmar, cancelar;
    }
}
