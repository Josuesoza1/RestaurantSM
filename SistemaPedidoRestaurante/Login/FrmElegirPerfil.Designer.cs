namespace SistemaPedidoRestaurante.Loggin
{
    partial class FrmElegirPerfil
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
            this.lblCuenta = new System.Windows.Forms.Label();
            this.lblIndicacion = new System.Windows.Forms.Label();
            this.opciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTrabajo = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.grpAltaCliente = new System.Windows.Forms.GroupBox();
            this.contacto = new System.Windows.Forms.TableLayoutPanel();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.btnHabilitar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.opciones.SuspendLayout();
            this.grpAltaCliente.SuspendLayout();
            this.contacto.SuspendLayout();
            this.SuspendLayout();
            this.panel.Name = "panel";
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Padding = new System.Windows.Forms.Padding(20);
            this.panel.ColumnCount = 1;
            this.panel.RowCount = 5;
            this.panel.AutoScroll = true;
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Text = "Elija su perfil";
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCuenta.Padding = new System.Windows.Forms.Padding(0,0,0,10);
            this.lblIndicacion.Name = "lblIndicacion";
            this.lblIndicacion.Text = "Seleccione como desea ingresar.";
            this.lblIndicacion.AutoSize = true;
            this.lblIndicacion.MaximumSize = new System.Drawing.Size(530,0);
            this.lblIndicacion.Padding = new System.Windows.Forms.Padding(0,0,0,12);
            this.opciones.Name = "opciones";
            this.opciones.AutoSize = true;
            this.opciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrabajo.Name = "btnTrabajo";
            this.btnTrabajo.Text = "Ingresar a trabajar";
            this.btnTrabajo.AutoSize = true;
            this.btnTrabajo.Padding = new System.Windows.Forms.Padding(8);
            this.btnTrabajo.Click += new System.EventHandler(this.btnTrabajo_Click);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Text = "Ingresar como Cliente";
            this.btnCliente.AutoSize = true;
            this.btnCliente.Padding = new System.Windows.Forms.Padding(8);
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            this.opciones.Controls.Add(this.btnTrabajo);
            this.opciones.Controls.Add(this.btnCliente);
            this.grpAltaCliente.Name = "grpAltaCliente";
            this.grpAltaCliente.Text = "Habilitar mi perfil cliente";
            this.grpAltaCliente.AutoSize = true;
            this.grpAltaCliente.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAltaCliente.Padding = new System.Windows.Forms.Padding(10);
            this.contacto.Name = "contacto";
            this.contacto.ColumnCount = 1;
            this.contacto.RowCount = 5;
            this.contacto.AutoSize = true;
            this.contacto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCorreo.Text = "Correo";
            this.lblCorreo.AutoSize = true;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.MaxLength = 120;
            this.txtCorreo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTelefono.Text = "Telefono";
            this.lblTelefono.AutoSize = true;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHabilitar.Name = "btnHabilitar";
            this.btnHabilitar.Text = "Habilitar perfil cliente";
            this.btnHabilitar.AutoSize = true;
            this.btnHabilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            this.contacto.Controls.Add(this.lblCorreo,0,0);
            this.contacto.Controls.Add(this.txtCorreo,0,1);
            this.contacto.Controls.Add(this.lblTelefono,0,2);
            this.contacto.Controls.Add(this.txtTelefono,0,3);
            this.contacto.Controls.Add(this.btnHabilitar,0,4);
            this.grpAltaCliente.Controls.Add(this.contacto);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Text = "Volver al inicio de sesion";
            this.btnVolver.AutoSize = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.panel.Controls.Add(this.lblCuenta,0,0);
            this.panel.Controls.Add(this.lblIndicacion,0,1);
            this.panel.Controls.Add(this.opciones,0,2);
            this.panel.Controls.Add(this.grpAltaCliente,0,3);
            this.panel.Controls.Add(this.btnVolver,0,4);
            this.Controls.Add(this.panel);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590,420);
            this.MinimumSize = new System.Drawing.Size(606,459);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Elegir perfil";
            this.Name = "FrmElegirPerfil";
            this.CancelButton = this.btnVolver;
            this.Load += new System.EventHandler(this.FrmElegirPerfil_Load);
            this.contacto.ResumeLayout(false);
            this.contacto.PerformLayout();
            this.grpAltaCliente.ResumeLayout(false);
            this.grpAltaCliente.PerformLayout();
            this.opciones.ResumeLayout(false);
            this.opciones.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel panel, contacto;
        private System.Windows.Forms.Label lblCuenta,lblIndicacion,lblCorreo,lblTelefono;
        private System.Windows.Forms.FlowLayoutPanel opciones;
        private System.Windows.Forms.GroupBox grpAltaCliente;
        private System.Windows.Forms.TextBox txtCorreo,txtTelefono;
        private System.Windows.Forms.Button btnTrabajo,btnCliente,btnHabilitar,btnVolver;
    }
}
