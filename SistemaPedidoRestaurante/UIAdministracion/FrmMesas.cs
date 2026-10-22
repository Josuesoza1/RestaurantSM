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
            ValidacionEntradaUI.Enteros(txtNumeroMesas,txtCapacidadMesas);
            VistaGestion.Listado(dgvMesas);
            dgvMesas.Columns[0].DataPropertyName="Numero";dgvMesas.Columns[1].DataPropertyName="Capacidad";dgvMesas.Columns[2].DataPropertyName="Estado";
            VistaGestion.Columna(dgvMesas,"Activa","Activa");
            cmbEstadoMesas.DropDownStyle=ComboBoxStyle.DropDownList;
            cmbEstadoMesas.Items.Clear();cmbEstadoMesas.Items.AddRange(new object[]{"Disponible","Ocupada","Reservada","En limpieza"});
            agregarToolStripMenuItem.Click+=(s,e)=>Nuevo();
            editarToolStripMenuItem.Click+=(s,e)=>Seleccionar();
            volverToolStripMenuItem1.Click+=(s,e)=>Close();

            ayudaToolStripMenuItem.Click+=(s,e)=>MessageBox.Show(this,"Agregue o seleccione una mesa. Para reactivarla, marque Activa y guarde. Capacidad: 1 a 20 personas.","Mesas");
            dgvMesas.CellClick+=(s,e)=>{if(e.RowIndex>=0)Seleccionar();};
            VistaGestion.Iniciar(this,1,()=>{Recargar();Nuevo();});
        }
        private void Recargar(){mesas=servicio.Listar();dgvMesas.DataSource=mesas;}
        private void Guardar(bool activa){int numero=SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtNumeroMesas.Text,"número de mesa",1);int capacidad=SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtCapacidadMesas.Text,"capacidad",1,20);servicio.Guardar(id,numero,capacidad,cmbEstadoMesas.SelectedItem as string,activa);}
        private void Seleccionar(){var f=VistaGestion.Fila(dgvMesas);if(f==null)return;id=(int)f["IdMesa"];txtNumeroMesas.Text=f["Numero"].ToString();txtCapacidadMesas.Text=f["Capacidad"].ToString();cmbEstadoMesas.SelectedItem=f["Estado"].ToString();chkActiva.Checked=(bool)f["Activa"];}
        private void Nuevo(){id=0;txtNumeroMesas.Clear();txtCapacidadMesas.Clear();cmbEstadoMesas.SelectedItem="Disponible";chkActiva.Checked=true;dgvMesas.ClearSelection();}
        private void groupBox1_Enter(object sender,EventArgs e){}
        private void volverToolStripMenuItem_Click(object sender,EventArgs e){Close();}

        private void btnGuardarMesas_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{Guardar(chkActiva.Checked);Recargar();Nuevo();});
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnCancelarMesas_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscarMesas_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>dgvMesas.DataSource=servicio.Buscar(mesas,txtBuscarMesas.Text));
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{
                var fila=VistaGestion.Fila(dgvMesas);
                if(fila==null)throw new ArgumentException("Seleccione una mesa del listado.");
                if(VistaGestion.Confirmar(this,"¿Eliminar permanentemente esta mesa? No debe tener pedidos ni reservas en su historial.")) {
                    servicio.Eliminar((int)fila["IdMesa"]); Recargar(); Nuevo();
                }
            });
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{
                if(VistaGestion.Fila(dgvMesas)==null)throw new ArgumentException("Seleccione una mesa del listado.");
                Seleccionar();
                if(VistaGestion.Confirmar(this,"¿Desactivar esta mesa conservando su historial?")) {
                    Guardar(false); Recargar(); Nuevo();
                }
            });
        }
    }
}
