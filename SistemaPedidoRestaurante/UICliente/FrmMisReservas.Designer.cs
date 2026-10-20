namespace SistemaPedidoRestaurante.UICliente
{
    partial class FrmMisReservas
    {
        private System.ComponentModel.IContainer components=null;
        protected override void Dispose(bool disposing){if(disposing&&components!=null)components.Dispose();base.Dispose(disposing);}
        private System.Windows.Forms.FlowLayoutPanel acciones;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelarReserva;
        private System.Windows.Forms.Button btnRecargar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvReservas;
        private System.Windows.Forms.TableLayoutPanel campos;
        private System.Windows.Forms.ComboBox cmbMesa;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.NumericUpDown personas;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name="FrmMisReservas";
            this.Text="MIS RESERVAS";
            this.AutoScaleDimensions=new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize=new System.Drawing.Size(820,600);
            this.MinimumSize=new System.Drawing.Size(750,560);
            this.StartPosition=System.Windows.Forms.FormStartPosition.CenterParent;
            this.BackColor=System.Drawing.Color.LightGray;
            this.raiz=new System.Windows.Forms.TableLayoutPanel();
            this.raiz.Dock=System.Windows.Forms.DockStyle.Fill;
            this.raiz.ColumnCount=1;
            this.raiz.Padding=new System.Windows.Forms.Padding(12);
            this.titulo=new System.Windows.Forms.Label();
            this.titulo.Text="MIS RESERVAS";
            this.titulo.BackColor=System.Drawing.Color.DarkSlateBlue;
            this.titulo.ForeColor=System.Drawing.Color.White;
            this.titulo.Font=new System.Drawing.Font("Arial",18F,System.Drawing.FontStyle.Bold);
            this.titulo.Dock=System.Windows.Forms.DockStyle.Fill;
            this.titulo.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.acciones=new System.Windows.Forms.FlowLayoutPanel();
            this.acciones.Dock=System.Windows.Forms.DockStyle.Fill;
            this.acciones.AutoScroll=true;
            this.btnNuevo=new System.Windows.Forms.Button();
            this.btnNuevo.Name="btnNuevo";
            this.btnNuevo.Text="Nueva reserva";
            this.btnNuevo.Size=new System.Drawing.Size(125,32);
            this.acciones.Controls.Add(this.btnNuevo);
            this.btnGuardar=new System.Windows.Forms.Button();
            this.btnGuardar.Name="btnGuardar";
            this.btnGuardar.Text="Guardar reserva";
            this.btnGuardar.Size=new System.Drawing.Size(125,32);
            this.acciones.Controls.Add(this.btnGuardar);
            this.btnCancelarReserva=new System.Windows.Forms.Button();
            this.btnCancelarReserva.Name="btnCancelarReserva";
            this.btnCancelarReserva.Text="Cancelar reserva";
            this.btnCancelarReserva.Size=new System.Drawing.Size(125,32);
            this.acciones.Controls.Add(this.btnCancelarReserva);
            this.btnRecargar=new System.Windows.Forms.Button();
            this.btnRecargar.Name="btnRecargar";
            this.btnRecargar.Text="Recargar";
            this.btnRecargar.Size=new System.Drawing.Size(125,32);
            this.acciones.Controls.Add(this.btnRecargar);
            this.btnVolver=new System.Windows.Forms.Button();
            this.btnVolver.Name="btnVolver";
            this.btnVolver.Text="Volver";
            this.btnVolver.Size=new System.Drawing.Size(125,32);
            this.acciones.Controls.Add(this.btnVolver);
            this.dgvReservas=new System.Windows.Forms.DataGridView();
            this.dgvReservas.Name="dgvReservas";
            this.dgvReservas.Dock=System.Windows.Forms.DockStyle.Fill;
            this.dgvReservas.ReadOnly=true;
            this.dgvReservas.AllowUserToAddRows=false;
            this.dgvReservas.AllowUserToDeleteRows=false;
            this.dgvReservas.MultiSelect=false;
            this.dgvReservas.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReservas.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.campos=new System.Windows.Forms.TableLayoutPanel();
            this.campos.Dock=System.Windows.Forms.DockStyle.Fill;this.campos.ColumnCount=2;
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,140F));
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent,100F));
            this.cmbMesa=new System.Windows.Forms.ComboBox();this.cmbMesa.Name="cmbMesa";
            this.cmbMesa.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fecha=new System.Windows.Forms.DateTimePicker();this.fecha.Name="fecha";
            this.fecha.Format=System.Windows.Forms.DateTimePickerFormat.Custom;this.fecha.CustomFormat="dd/MM/yyyy HH:mm";
            this.fecha.Width=220;
            this.personas=new System.Windows.Forms.NumericUpDown();this.personas.Name="personas";this.personas.Minimum=1;this.personas.Maximum=20;
            this.lblCampo0=new System.Windows.Forms.Label();
            this.lblCampo0.Name="lblCampo0";
            this.lblCampo0.Text="Mesa";
            this.lblCampo0.AutoSize=true;
            this.lblCampo0.Anchor=System.Windows.Forms.AnchorStyles.Left;
            this.campos.Controls.Add(this.lblCampo0,0,0);this.campos.Controls.Add(this.cmbMesa,1,0);
            this.lblCampo1=new System.Windows.Forms.Label();
            this.lblCampo1.Name="lblCampo1";
            this.lblCampo1.Text="Fecha y hora";
            this.lblCampo1.AutoSize=true;
            this.lblCampo1.Anchor=System.Windows.Forms.AnchorStyles.Left;
            this.campos.Controls.Add(this.lblCampo1,0,1);this.campos.Controls.Add(this.fecha,1,1);
            this.lblCampo2=new System.Windows.Forms.Label();
            this.lblCampo2.Name="lblCampo2";
            this.lblCampo2.Text="Personas";
            this.lblCampo2.AutoSize=true;
            this.lblCampo2.Anchor=System.Windows.Forms.AnchorStyles.Left;
            this.campos.Controls.Add(this.lblCampo2,0,2);this.campos.Controls.Add(this.personas,1,2);

            this.lblDuracion=new System.Windows.Forms.Label();
            this.lblDuracion.Name="lblDuracion";
            this.lblDuracion.Text="Cada reserva ocupa un intervalo de 2 horas.";
            this.lblDuracion.AutoSize=true;
            this.campos.Controls.Add(this.lblDuracion,0,3);
            this.campos.SetColumnSpan(this.lblDuracion,2);
            this.raiz.RowCount=4;
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,65F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,115F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent,100F));
            this.raiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,48F));
            this.raiz.Controls.Add(this.titulo,0,0);this.raiz.Controls.Add(this.campos,0,1);this.raiz.Controls.Add(this.dgvReservas,0,2);this.raiz.Controls.Add(this.acciones,0,3);

            this.Controls.Add(this.raiz);
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label lblCampo0;
        private System.Windows.Forms.Label lblCampo1;
        private System.Windows.Forms.Label lblCampo2;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.TableLayoutPanel raiz;
        private System.Windows.Forms.Label titulo;
    }
}
