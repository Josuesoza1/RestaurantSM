namespace SistemaPedidoRestaurante.UIEmpleado
{
    partial class FrmPedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblCiente = new System.Windows.Forms.Label();
            this.lblCedulaCliente = new System.Windows.Forms.Label();
            this.grpProducto = new System.Windows.Forms.GroupBox();
            this.lblCantidadPlato = new System.Windows.Forms.Label();
            this.lblPrecioPlato = new System.Windows.Forms.Label();
            this.lblPlato = new System.Windows.Forms.Label();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblBebida = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblPrecioBebida = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbPlato = new System.Windows.Forms.ComboBox();
            this.guardarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbCantidadPlato = new System.Windows.Forms.ComboBox();
            this.cmbPrecioPlato = new System.Windows.Forms.ComboBox();
            this.cmbBebida = new System.Windows.Forms.ComboBox();
            this.cmbCantidadBebida = new System.Windows.Forms.ComboBox();
            this.cmbPrecioBebida = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.grpProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guardarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.limpiarToolStripMenuItem});
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.agregarToolStripMenuItem.Text = "Acciones";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 24);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(784, 70);
            this.pnlEncabezado.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(48, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(291, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "REGISTRO DE PEDIDO\r\n";
            // 
            // grpDatosCliente
            // 
            this.grpDatosCliente.Controls.Add(this.btnBuscar);
            this.grpDatosCliente.Controls.Add(this.lblCedulaCliente);
            this.grpDatosCliente.Controls.Add(this.txtEmail);
            this.grpDatosCliente.Controls.Add(this.txtCedula);
            this.grpDatosCliente.Controls.Add(this.txtTelefono);
            this.grpDatosCliente.Controls.Add(this.txtCliente);
            this.grpDatosCliente.Controls.Add(this.lblEmail);
            this.grpDatosCliente.Controls.Add(this.lblTelefono);
            this.grpDatosCliente.Controls.Add(this.lblCiente);
            this.grpDatosCliente.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosCliente.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDatosCliente.Location = new System.Drawing.Point(12, 109);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Size = new System.Drawing.Size(310, 175);
            this.grpDatosCliente.TabIndex = 2;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Datos del cliente";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(76, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(201, 20);
            this.txtEmail.TabIndex = 13;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(76, 51);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(201, 20);
            this.txtCedula.TabIndex = 12;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(76, 81);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(201, 20);
            this.txtTelefono.TabIndex = 8;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(76, 19);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(201, 20);
            this.txtCliente.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail.Location = new System.Drawing.Point(30, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 14);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTelefono.Location = new System.Drawing.Point(12, 84);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(58, 14);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "Telefono:";
            // 
            // lblCiente
            // 
            this.lblCiente.AutoSize = true;
            this.lblCiente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCiente.Location = new System.Drawing.Point(21, 22);
            this.lblCiente.Name = "lblCiente";
            this.lblCiente.Size = new System.Drawing.Size(49, 14);
            this.lblCiente.TabIndex = 0;
            this.lblCiente.Text = "Cliente:";
            this.lblCiente.Click += new System.EventHandler(this.lblPnombre_Click);
            // 
            // lblCedulaCliente
            // 
            this.lblCedulaCliente.AutoSize = true;
            this.lblCedulaCliente.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCedulaCliente.Location = new System.Drawing.Point(21, 54);
            this.lblCedulaCliente.Name = "lblCedulaCliente";
            this.lblCedulaCliente.Size = new System.Drawing.Size(48, 14);
            this.lblCedulaCliente.TabIndex = 14;
            this.lblCedulaCliente.Text = "Cédula:";
            // 
            // grpProducto
            // 
            this.grpProducto.Controls.Add(this.cmbPrecioBebida);
            this.grpProducto.Controls.Add(this.cmbCantidadBebida);
            this.grpProducto.Controls.Add(this.cmbBebida);
            this.grpProducto.Controls.Add(this.cmbPrecioPlato);
            this.grpProducto.Controls.Add(this.cmbCantidadPlato);
            this.grpProducto.Controls.Add(this.cmbPlato);
            this.grpProducto.Controls.Add(this.btnCancelar);
            this.grpProducto.Controls.Add(this.btnAceptar);
            this.grpProducto.Controls.Add(this.lblPrecioBebida);
            this.grpProducto.Controls.Add(this.lblCantidad);
            this.grpProducto.Controls.Add(this.lblBebida);
            this.grpProducto.Controls.Add(this.lblCantidadPlato);
            this.grpProducto.Controls.Add(this.lblPrecioPlato);
            this.grpProducto.Controls.Add(this.lblPlato);
            this.grpProducto.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProducto.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpProducto.Location = new System.Drawing.Point(342, 109);
            this.grpProducto.Name = "grpProducto";
            this.grpProducto.Size = new System.Drawing.Size(435, 175);
            this.grpProducto.TabIndex = 3;
            this.grpProducto.TabStop = false;
            this.grpProducto.Text = "Producto";
            // 
            // lblCantidadPlato
            // 
            this.lblCantidadPlato.AutoSize = true;
            this.lblCantidadPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidadPlato.Location = new System.Drawing.Point(6, 51);
            this.lblCantidadPlato.Name = "lblCantidadPlato";
            this.lblCantidadPlato.Size = new System.Drawing.Size(58, 14);
            this.lblCantidadPlato.TabIndex = 14;
            this.lblCantidadPlato.Text = "Cantidad:";
            // 
            // lblPrecioPlato
            // 
            this.lblPrecioPlato.AutoSize = true;
            this.lblPrecioPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrecioPlato.Location = new System.Drawing.Point(11, 81);
            this.lblPrecioPlato.Name = "lblPrecioPlato";
            this.lblPrecioPlato.Size = new System.Drawing.Size(45, 14);
            this.lblPrecioPlato.TabIndex = 5;
            this.lblPrecioPlato.Text = "Precio:";
            // 
            // lblPlato
            // 
            this.lblPlato.AutoSize = true;
            this.lblPlato.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPlato.Location = new System.Drawing.Point(19, 22);
            this.lblPlato.Name = "lblPlato";
            this.lblPlato.Size = new System.Drawing.Size(37, 14);
            this.lblPlato.TabIndex = 0;
            this.lblPlato.Text = "Plato:";
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.guardarToolStripMenuItem1});
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // limpiarToolStripMenuItem
            // 
            this.limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            this.limpiarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.limpiarToolStripMenuItem.Text = "Limpiar";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(115, 119);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(88, 31);
            this.btnAceptar.TabIndex = 18;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Red;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(209, 119);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(81, 31);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lblBebida
            // 
            this.lblBebida.AutoSize = true;
            this.lblBebida.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBebida.Location = new System.Drawing.Point(186, 22);
            this.lblBebida.Name = "lblBebida";
            this.lblBebida.Size = new System.Drawing.Size(47, 14);
            this.lblBebida.TabIndex = 15;
            this.lblBebida.Text = "Bebida:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCantidad.Location = new System.Drawing.Point(175, 51);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(58, 14);
            this.lblCantidad.TabIndex = 17;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // lblPrecioBebida
            // 
            this.lblPrecioBebida.AutoSize = true;
            this.lblPrecioBebida.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrecioBebida.Location = new System.Drawing.Point(188, 81);
            this.lblPrecioBebida.Name = "lblPrecioBebida";
            this.lblPrecioBebida.Size = new System.Drawing.Size(45, 14);
            this.lblPrecioBebida.TabIndex = 18;
            this.lblPrecioBebida.Text = "Precio:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btnBuscar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Blue;
            this.btnBuscar.Location = new System.Drawing.Point(115, 138);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(89, 31);
            this.btnBuscar.TabIndex = 15;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRegistrarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarPago.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarPago.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrarPago.Location = new System.Drawing.Point(303, 298);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(133, 31);
            this.btnRegistrarPago.TabIndex = 18;
            this.btnRegistrarPago.Text = "Registrar Pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = false;
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // cmbPlato
            // 
            this.cmbPlato.FormattingEnabled = true;
            this.cmbPlato.Location = new System.Drawing.Point(62, 19);
            this.cmbPlato.Name = "cmbPlato";
            this.cmbPlato.Size = new System.Drawing.Size(85, 22);
            this.cmbPlato.TabIndex = 20;
            // 
            // guardarToolStripMenuItem1
            // 
            this.guardarToolStripMenuItem1.Name = "guardarToolStripMenuItem1";
            this.guardarToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.guardarToolStripMenuItem1.Text = "Guardar";
            // 
            // cmbCantidadPlato
            // 
            this.cmbCantidadPlato.FormattingEnabled = true;
            this.cmbCantidadPlato.Location = new System.Drawing.Point(62, 46);
            this.cmbCantidadPlato.Name = "cmbCantidadPlato";
            this.cmbCantidadPlato.Size = new System.Drawing.Size(85, 22);
            this.cmbCantidadPlato.TabIndex = 21;
            // 
            // cmbPrecioPlato
            // 
            this.cmbPrecioPlato.FormattingEnabled = true;
            this.cmbPrecioPlato.Location = new System.Drawing.Point(62, 78);
            this.cmbPrecioPlato.Name = "cmbPrecioPlato";
            this.cmbPrecioPlato.Size = new System.Drawing.Size(85, 22);
            this.cmbPrecioPlato.TabIndex = 22;
            // 
            // cmbBebida
            // 
            this.cmbBebida.FormattingEnabled = true;
            this.cmbBebida.Location = new System.Drawing.Point(239, 18);
            this.cmbBebida.Name = "cmbBebida";
            this.cmbBebida.Size = new System.Drawing.Size(85, 22);
            this.cmbBebida.TabIndex = 23;
            // 
            // cmbCantidadBebida
            // 
            this.cmbCantidadBebida.FormattingEnabled = true;
            this.cmbCantidadBebida.Location = new System.Drawing.Point(239, 46);
            this.cmbCantidadBebida.Name = "cmbCantidadBebida";
            this.cmbCantidadBebida.Size = new System.Drawing.Size(85, 22);
            this.cmbCantidadBebida.TabIndex = 24;
            // 
            // cmbPrecioBebida
            // 
            this.cmbPrecioBebida.FormattingEnabled = true;
            this.cmbPrecioBebida.Location = new System.Drawing.Point(239, 76);
            this.cmbPrecioBebida.Name = "cmbPrecioBebida";
            this.cmbPrecioBebida.Size = new System.Drawing.Size(85, 22);
            this.cmbPrecioBebida.TabIndex = 25;
            // 
            // FmrCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(784, 341);
            this.Controls.Add(this.btnRegistrarPago);
            this.Controls.Add(this.grpProducto);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.pnlEncabezado);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FmrCompra";
            this.Text = "REGISTRO DE PEDIDO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpProducto.ResumeLayout(false);
            this.grpProducto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblCiente;
        private System.Windows.Forms.Label lblCedulaCliente;
        private System.Windows.Forms.GroupBox grpProducto;
        private System.Windows.Forms.Label lblCantidadPlato;
        private System.Windows.Forms.Label lblPrecioPlato;
        private System.Windows.Forms.Label lblPlato;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblBebida;
        private System.Windows.Forms.Label lblPrecioBebida;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem1;
        private System.Windows.Forms.ComboBox cmbPrecioPlato;
        private System.Windows.Forms.ComboBox cmbCantidadPlato;
        private System.Windows.Forms.ComboBox cmbPlato;
        private System.Windows.Forms.ComboBox cmbPrecioBebida;
        private System.Windows.Forms.ComboBox cmbCantidadBebida;
        private System.Windows.Forms.ComboBox cmbBebida;
    }
}