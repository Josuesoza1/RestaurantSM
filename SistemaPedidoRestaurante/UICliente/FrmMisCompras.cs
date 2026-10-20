using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UICliente
{
    public partial class FrmMisCompras:Form
    {
        private readonly ComprasClienteBLL servicio=new ComprasClienteBLL();
        private DataTable pedidos;
        public FrmMisCompras()
        {
            InitializeComponent();
            btnVolver.Click+=(s,e)=>Close();
            btnActualizar.Click+=(s,e)=>VistaGestion.Ejecutar(this,Recargar);
            txtBuscar.TextChanged+=(s,e)=>VistaGestion.Ejecutar(this,()=>{dgvPedidos.DataSource=servicio.Buscar(pedidos,txtBuscar.Text);dgvDetalle.DataSource=null;});
            dgvPedidos.CellClick+=(s,e)=>{if(e.RowIndex>=0)VistaGestion.Ejecutar(this,Detalle);};
            btnEditar.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{var fila=Seleccionar();using(var f=new FrmPedidoCliente((int)fila["IdPedido"]))f.ShowDialog(this);Recargar();});
            btnCancelarPedido.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{var f=Seleccionar();if(VistaGestion.Confirmar(this,"¿Cancelar el pedido y devolver las existencias?")){servicio.Cancelar((int)f["IdPedido"]);Recargar();}});
            VistaGestion.Iniciar(this,3,Recargar);
        }
        private void Recargar(){pedidos=servicio.Listar();dgvPedidos.DataSource=pedidos;dgvDetalle.DataSource=null;dgvPedidos.ClearSelection();if(dgvPedidos.Columns.Contains("Total"))dgvPedidos.Columns["Total"].DefaultCellStyle.Format="N2";}
        private DataRow Seleccionar(){var f=VistaGestion.Fila(dgvPedidos);if(f==null)throw new ArgumentException("Seleccione un pedido.");if(f["Estado"].ToString()!="Pendiente")throw new ArgumentException("Solo puede cambiar pedidos pendientes.");return f;}
        private void Detalle(){var f=VistaGestion.Fila(dgvPedidos);if(f==null)return;dgvDetalle.DataSource=servicio.Detalle((int)f["IdPedido"]);foreach(string c in new[]{"IdProducto","IdCategoria"})if(dgvDetalle.Columns.Contains(c))dgvDetalle.Columns[c].Visible=false;foreach(string c in new[]{"PrecioUnitario","Subtotal"})if(dgvDetalle.Columns.Contains(c))dgvDetalle.Columns[c].DefaultCellStyle.Format="N2";}
    }
}
