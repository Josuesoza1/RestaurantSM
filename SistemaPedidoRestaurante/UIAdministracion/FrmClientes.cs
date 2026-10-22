using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmClientes : Form
    {
        private readonly ClientesGestionBLL servicio = new ClientesGestionBLL();
        private DataTable clientes;
        private int id;
        public FrmClientes()
        {
            InitializeComponent();
            ValidacionEntradaUI.Nombres(txtPnombre,txtSnombre,txtPapellido,txtSapellido); ValidacionEntradaUI.Telefonos(txtTelefono); ValidacionEntradaUI.Cedulas(txtCedula); txtEmail.MaxLength=120;
            VistaGestion.Listado(dgvClientes);
            string[] campos = { "Id", "PrimerNombre", "SegundoNombre", "PrimerApellido", "SegundoApellido", "Cedula", "Telefono", "Email" };
            for (int i=0; i<campos.Length; i++) dgvClientes.Columns[i].DataPropertyName=campos[i];
            VistaGestion.Columna(dgvClientes,"Genero","Género");
            VistaGestion.Columna(dgvClientes,"Activo","Activo");
            btnEliminar.Text="Eliminar";
            btnEliminar.Font=new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Bold);

            dgvClientes.CellClick += (s,e) => { if(e.RowIndex>=0) Mostrar(); };
            VistaGestion.Iniciar(this,1,()=>{ Recargar(); Nuevo(); });
        }
        private void Recargar() { clientes=servicio.Listar(); dgvClientes.DataSource=clientes; }
        private Cliente Leer() { return new Cliente { Id=id,PrimerNombre=txtPnombre.Text,SegundoNombre=txtSnombre.Text,
            PrimerApellido=txtPapellido.Text,SegundoApellido=txtSapellido.Text,Cedula=txtCedula.Text,
            Telefono=txtTelefono.Text,Email=txtEmail.Text,Genero=cmbGenero.SelectedItem as string }; }
        private void Mostrar()
        {
            var f=VistaGestion.Fila(dgvClientes); if(f==null)return;
            id=(int)f["Id"]; txtPnombre.Text=Convert.ToString(f["PrimerNombre"]); txtSnombre.Text=Convert.ToString(f["SegundoNombre"]);
            txtPapellido.Text=Convert.ToString(f["PrimerApellido"]); txtSapellido.Text=Convert.ToString(f["SegundoApellido"]);
            txtCedula.Text=Convert.ToString(f["Cedula"]); txtTelefono.Text=Convert.ToString(f["Telefono"]); txtEmail.Text=Convert.ToString(f["Email"]);
            cmbGenero.SelectedItem=Convert.ToString(f["Genero"]); chkActivo.Checked=(bool)f["Activo"]; btnGuardar.Text="Actualizar";
        }
        private void Nuevo() { id=0; txtPnombre.Clear();txtSnombre.Clear();txtPapellido.Clear();txtSapellido.Clear();
            txtCedula.Clear();txtTelefono.Clear();txtEmail.Clear();cmbGenero.SelectedIndex=-1;chkActivo.Checked=true;dgvClientes.ClearSelection();btnGuardar.Text="Guardar"; }
        private void FrmClientes_Load(object sender,EventArgs e) { }
        private void btnVolver_Click(object sender,EventArgs e) { Close(); }
        private void btnLimpiar_Click(object sender,EventArgs e) { Nuevo(); }

        private void btnNuevo_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnGuardar_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this, () => { servicio.Guardar(Leer(),chkActivo.Checked); Recargar(); Nuevo(); });
        }

        private void btnEliminar_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this, () => {
                            if(id==0) throw new ArgumentException("Seleccione un cliente del listado.");
                            if(VistaGestion.Confirmar(this,"¿Eliminar permanentemente este cliente y su cuenta de acceso asociada? Solo se permite si no tiene pedidos ni reservas.")) { servicio.Eliminar(id); Recargar(); Nuevo(); }
                        });
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this, () => dgvClientes.DataSource=servicio.Buscar(clientes,txtTextoBuscar.Text));
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this, () => {
                if(id==0) throw new ArgumentException("Seleccione un cliente del listado.");
                if(VistaGestion.Confirmar(this,"¿Desactivar al cliente conservando su historial?")) {
                    servicio.Desactivar(id); Recargar(); Nuevo();
                }
            });
        }
    }
}
