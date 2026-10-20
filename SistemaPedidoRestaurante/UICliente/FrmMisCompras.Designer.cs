namespace SistemaPedidoRestaurante.UICliente
{
    partial class FrmMisCompras
    {
        private System.ComponentModel.IContainer components=null;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCancelarPedido;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvPedidos,dgvDetalle;
        private System.Windows.Forms.TextBox txtBuscar;

        private void InitializeComponent()
        {
            this.raiz = new System.Windows.Forms.TableLayoutPanel();
            this.titulo = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.acciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnCancelarPedido = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.raiz.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // raiz
            // 
            this.raiz.ColumnCount = 1;
            this.raiz.Controls.Add(this.titulo, 0, 0);
            this.raiz.Controls.Add(this.pnlBusqueda, 0, 1);
            this.raiz.Controls.Add(this.dgvPedidos, 0, 2);
            this.raiz.Controls.Add(this.dgvDetalle, 0, 3);
            this.raiz.Controls.Add(this.acciones, 0, 4);
            this.raiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.raiz.Location = new System.Drawing.Point(0, 0);
            this.raiz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.raiz.Name = "raiz";
            this.raiz.Padding = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this.raiz.RowCount = 5;
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.raiz.Size = new System.Drawing.Size(1093, 738);
            this.raiz.TabIndex = 0;
            // 
            // titulo
            // 
            this.titulo.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.titulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.White;
            this.titulo.Location = new System.Drawing.Point(20, 15);
            this.titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(1053, 80);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "MIS COMPRAS Y PEDIDOS";
            this.titulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.txtBuscar);
            this.pnlBusqueda.Controls.Add(this.lblBuscar);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBusqueda.Location = new System.Drawing.Point(20, 99);
            this.pnlBusqueda.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(1053, 35);
            this.pnlBusqueda.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBuscar.Location = new System.Drawing.Point(93, 0);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(960, 22);
            this.txtBuscar.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBuscar.Location = new System.Drawing.Point(0, 0);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(93, 35);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar:";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.AllowUserToDeleteRows = false;
            this.dgvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPedidos.ColumnHeadersHeight = 29;
            this.dgvPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPedidos.Location = new System.Drawing.Point(20, 142);
            this.dgvPedidos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPedidos.MultiSelect = false;
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.ReadOnly = true;
            this.dgvPedidos.RowHeadersWidth = 51;
            this.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedidos.Size = new System.Drawing.Size(1053, 255);
            this.dgvPedidos.TabIndex = 2;
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.ColumnHeadersHeight = 29;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Location = new System.Drawing.Point(20, 405);
            this.dgvDetalle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersWidth = 51;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(1053, 255);
            this.dgvDetalle.TabIndex = 3;
            // 
            // acciones
            // 
            this.acciones.AutoScroll = true;
            this.acciones.Controls.Add(this.btnEditar);
            this.acciones.Controls.Add(this.btnCancelarPedido);
            this.acciones.Controls.Add(this.btnActualizar);
            this.acciones.Controls.Add(this.btnVolver);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acciones.Location = new System.Drawing.Point(20, 668);
            this.acciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.acciones.Name = "acciones";
            this.acciones.Size = new System.Drawing.Size(1053, 51);
            this.acciones.TabIndex = 4;
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(4, 4);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(167, 39);
            this.btnEditar.TabIndex = 0;
            this.btnEditar.Text = "Editar pendiente";
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.Location = new System.Drawing.Point(179, 4);
            this.btnCancelarPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.Size = new System.Drawing.Size(167, 39);
            this.btnCancelarPedido.TabIndex = 1;
            this.btnCancelarPedido.Text = "Cancelar pedido";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(354, 4);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(167, 39);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "Recargar";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(529, 4);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(167, 39);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver";
            // 
            // FrmMisCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1093, 738);
            this.Controls.Add(this.raiz);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(994, 678);
            this.Name = "FrmMisCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MIS COMPRAS Y PEDIDOS";
            this.raiz.ResumeLayout(false);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TableLayoutPanel raiz;
        private System.Windows.Forms.Label titulo;
    }
}
