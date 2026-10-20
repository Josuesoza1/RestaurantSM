using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMesas : Form
    {
        private readonly MesasGestionBLL servicio=new MesasGestionBLL();
        private DataTable mesas;
        private int id;
        public FrmMesas()
        {
            InitializeComponent();
            VistaGestion.Listado(dgvMesas);
            dgvMesas.Columns[0].DataPropertyName="Numero";dgvMesas.Columns[1].DataPropertyName="Capacidad";dgvMesas.Columns[2].DataPropertyName="Estado";
            VistaGestion.Columna(dgvMesas,"Activa","Activa");
            cmbEstadoMesas.DropDownStyle=ComboBoxStyle.DropDownList;
            cmbEstadoMesas.Items.Clear();cmbEstadoMesas.Items.AddRange(new object[]{"Disponible","Ocupada","Reservada","En limpieza"});
            agregarToolStripMenuItem.Click+=(s,e)=>Nuevo();
            editarToolStripMenuItem.Click+=(s,e)=>Seleccionar();
            eliminarToolStripMenuItem.Text="Desactivar";
            eliminarToolStripMenuItem.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{
                Seleccionar(); if(id==0)throw new ArgumentException("Seleccione una mesa.");
                if(VistaGestion.Confirmar(this,"¿Desactivar esta mesa?")){ Guardar(false);Recargar();Nuevo(); }
            });
            volverToolStripMenuItem1.Click+=(s,e)=>Close();
            btnGuardarMesas.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{Guardar(chkActiva.Checked);Recargar();Nuevo();});
            btnLimpiar.Click+=(s,e)=>Nuevo();btnCancelarMesas.Click+=(s,e)=>Nuevo();
            btnBuscarMesas.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>dgvMesas.DataSource=servicio.Buscar(mesas,txtBuscarMesas.Text));
            ayudaToolStripMenuItem.Click+=(s,e)=>MessageBox.Show(this,"Agregue o seleccione una mesa. Para reactivarla, marque Activa y guarde. Capacidad: 1 a 20 personas.","Mesas");
            dgvMesas.CellClick+=(s,e)=>{if(e.RowIndex>=0)Seleccionar();};
            VistaGestion.Iniciar(this,1,()=>{Recargar();Nuevo();});
        }
        private void Recargar(){mesas=servicio.Listar();dgvMesas.DataSource=mesas;}
        private void Guardar(bool activa){int numero,capacidad;if(!int.TryParse(txtNumeroMesas.Text,out numero)||!int.TryParse(txtCapacidadMesas.Text,out capacidad))throw new ArgumentException("Número y capacidad deben ser enteros.");servicio.Guardar(id,numero,capacidad,cmbEstadoMesas.SelectedItem as string,activa);}
        private void Seleccionar(){var f=VistaGestion.Fila(dgvMesas);if(f==null)return;id=(int)f["IdMesa"];txtNumeroMesas.Text=f["Numero"].ToString();txtCapacidadMesas.Text=f["Capacidad"].ToString();cmbEstadoMesas.SelectedItem=f["Estado"].ToString();chkActiva.Checked=(bool)f["Activa"];}
        private void Nuevo(){id=0;txtNumeroMesas.Clear();txtCapacidadMesas.Clear();cmbEstadoMesas.SelectedItem="Disponible";chkActiva.Checked=true;dgvMesas.ClearSelection();}
        private void groupBox1_Enter(object sender,EventArgs e){}
        private void volverToolStripMenuItem_Click(object sender,EventArgs e){Close();}
    }
}
