namespace ModeloSistRestaurante.UI
{
    partial class FrmEmpleado
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblMesasTitulo = new System.Windows.Forms.Label();
            this.dgvMesas = new System.Windows.Forms.DataGridView();
            this.lblPedidosTitulo = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.btnNuevoPedido = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
            //
            // lblBienvenida
            //
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(20, 15);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(150, 25);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido";
            //
            // lblMesasTitulo
            //
            this.lblMesasTitulo.AutoSize = true;
            this.lblMesasTitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMesasTitulo.Location = new System.Drawing.Point(20, 55);
            this.lblMesasTitulo.Name = "lblMesasTitulo";
            this.lblMesasTitulo.Size = new System.Drawing.Size(48, 17);
            this.lblMesasTitulo.TabIndex = 1;
            this.lblMesasTitulo.Text = "Mesas";
            //
            // dgvMesas
            //
            this.dgvMesas.AllowUserToAddRows = false;
            this.dgvMesas.AllowUserToDeleteRows = false;
            this.dgvMesas.ReadOnly = true;
            this.dgvMesas.RowHeadersVisible = false;
            this.dgvMesas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMesas.MultiSelect = false;
            this.dgvMesas.Location = new System.Drawing.Point(20, 75);
            this.dgvMesas.Name = "dgvMesas";
            this.dgvMesas.Size = new System.Drawing.Size(300, 180);
            this.dgvMesas.TabIndex = 2;
            //
            // lblPedidosTitulo
            //
            this.lblPedidosTitulo.AutoSize = true;
            this.lblPedidosTitulo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPedidosTitulo.Location = new System.Drawing.Point(340, 55);
            this.lblPedidosTitulo.Name = "lblPedidosTitulo";
            this.lblPedidosTitulo.Size = new System.Drawing.Size(108, 17);
            this.lblPedidosTitulo.TabIndex = 3;
            this.lblPedidosTitulo.Text = "Pedidos abiertos";
            //
            // dgvPedidos
            //
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.ReadOnly = true;
            this.dgvPedidos.RowHeadersVisible = false;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Location = new System.Drawing.Point(340, 75);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(340, 180);
            this.dgvPedidos.TabIndex = 4;
            //
            // btnNuevoPedido
            //
            this.btnNuevoPedido.Location = new System.Drawing.Point(20, 270);
            this.btnNuevoPedido.Name = "btnNuevoPedido";
            this.btnNuevoPedido.Size = new System.Drawing.Size(150, 32);
            this.btnNuevoPedido.TabIndex = 5;
            this.btnNuevoPedido.Text = "Nuevo pedido";
            this.btnNuevoPedido.UseVisualStyleBackColor = true;
            this.btnNuevoPedido.Click += new System.EventHandler(this.btnNuevoPedido_Click);
            //
            // btnActualizar
            //
            this.btnActualizar.Location = new System.Drawing.Point(180, 270);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 32);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.Location = new System.Drawing.Point(20, 320);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(150, 32);
            this.btnCerrarSesion.TabIndex = 7;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            //
            // lblMensaje
            //
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(20, 370);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 15);
            this.lblMensaje.TabIndex = 8;
            //
            // FrmEmpleado
            //
            this.ClientSize = new System.Drawing.Size(700, 410);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnNuevoPedido);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.lblPedidosTitulo);
            this.Controls.Add(this.dgvMesas);
            this.Controls.Add(this.lblMesasTitulo);
            this.Controls.Add(this.lblBienvenida);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo Empleado - Sistema Restaurante";
            this.Load += new System.EventHandler(this.FrmEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMesas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblMesasTitulo;
        private System.Windows.Forms.DataGridView dgvMesas;
        private System.Windows.Forms.Label lblPedidosTitulo;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Button btnNuevoPedido;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblMensaje;
    }
}
