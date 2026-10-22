using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMisReservas:Form
    {
        private readonly ReservasClienteBLL servicio=new ReservasClienteBLL();
        private int id;
        public FrmMisReservas()
        {
            InitializeComponent();cmbMesa.DisplayMember="Numero";cmbMesa.ValueMember="IdMesa";

            dgvReservas.CellClick+=(s,e)=>{if(e.RowIndex<0)return;var f=VistaGestion.Fila(dgvReservas);if(f==null)return;id=(int)f["IdReserva"];cmbMesa.SelectedValue=f["IdMesa"];fecha.Value=(DateTime)f["Fecha"];personas.Value=(int)f["Personas"];};
            VistaGestion.Iniciar(this,3,Recargar);
        }
        private void Recargar(){cmbMesa.DataSource=new MesasGestionBLL().Listar(true);dgvReservas.DataSource=servicio.Listar();if(dgvReservas.Columns.Contains("IdMesa"))dgvReservas.Columns["IdMesa"].Visible=false;Nuevo();}
        private void Nuevo(){id=0;fecha.Value=DateTime.Now.AddDays(1);personas.Value=1;dgvReservas.ClearSelection();}

        private void btnVolver_Click(object s, EventArgs e)
        {
            Close();
        }

        private void btnNuevo_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnRecargar_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,Recargar);
        }

        private void btnGuardar_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{servicio.Guardar(id,cmbMesa.SelectedValue is int?(int)cmbMesa.SelectedValue:0,fecha.Value,(int)personas.Value);Recargar();});
        }

        private void btnCancelarReserva_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{var f=VistaGestion.Fila(dgvReservas);if(f==null)throw new ArgumentException("Seleccione una reserva.");if(VistaGestion.Confirmar(this,"¿Cancelar la reserva seleccionada?")){servicio.Cancelar((int)f["IdReserva"]);Recargar();}});
        }
    }
}
