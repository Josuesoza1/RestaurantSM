using System;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioBLL servicio=new InventarioBLL();
        private InventarioProductos inventario;
        private byte[] version;
        private int seleccionado;
        private bool cargando;
        private readonly ToolStripMenuItem bajos=new ToolStripMenuItem("Solo existencias bajas"){CheckOnClick=true};
        public FrmInventario()
        {
            InitializeComponent();
            ValidacionEntradaUI.Enteros(txtStockActual,textBox1);VistaGestion.Listado(dgvProductoInventario);
            string[] campos={"Producto","Categoria","Unidad","PrecioVenta","StockActual","StockMinimo"};
            for(int i=0;i<campos.Length;i++)dgvProductoInventario.Columns[i].DataPropertyName=campos[i];
            dgvProductoInventario.Columns[3].DefaultCellStyle.Format="N2";
            VistaGestion.Columna(dgvProductoInventario,"Activo","Control activo");
            txtPrecioVenta.ReadOnly=txtUnidad.ReadOnly=true;cmbCategoriaInventario.Enabled=false;
            cmbProductoInventario.DisplayMember="Nombre";cmbProductoInventario.ValueMember="IdProducto";
            cmbProductoInventario.SelectedValueChanged+=(s,e)=>{if(!cargando)ElegirProducto();};
            agregarToolStripMenuItem.Click+=(s,e)=>Nuevo();
            editarToolStripMenuItem.Click+=(s,e)=>Seleccionar();
            volverToolStripMenuItem.Click+=(s,e)=>Close();

            bajos.CheckedChanged+=(s,e)=>Filtrar();
            menuStrip1.Items.Insert(menuStrip1.Items.Count-1,bajos);
            var actualizar=new ToolStripMenuItem("Recargar");actualizar.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{Recargar();Nuevo();});menuStrip1.Items.Add(actualizar);
            ayudaToolStripMenuItem.Click+=(s,e)=>MessageBox.Show(this,"Seleccione un producto existente. Unidad y precio vienen de Productos. Guardar establece las unidades disponibles; los pedidos las descuentan. Productos sin control activo no limitan existencias. Use Recargar si otra operación cambió el stock.","Inventario");
            dgvProductoInventario.CellClick+=(s,e)=>{if(e.RowIndex>=0)Seleccionar();};
            VistaGestion.Iniciar(this,1,()=>{Recargar();Nuevo();});
        }
        private void Recargar(){cargando=true;try{cmbProductoInventario.DataSource=new ProductoBLL().ObtenerProductos();inventario=servicio.Listar();dgvProductoInventario.DataSource=inventario;}finally{cargando=false;}}
        private void Filtrar(){VistaGestion.Ejecutar(this,()=>dgvProductoInventario.DataSource=servicio.Buscar(inventario,textBox2.Text,bajos.Checked));}
        private void ElegirProducto(){var p=cmbProductoInventario.SelectedItem as Producto;seleccionado=0;version=null;txtStockActual.Text="0";textBox1.Text="0";chkControlActivo.Checked=true;txtUnidad.Text="Unidad";txtPrecioVenta.Text=p==null?"":p.Precio.ToString("N2");cmbCategoriaInventario.Items.Clear();if(p!=null){cmbCategoriaInventario.Items.Add(p.CategoriaNombre);cmbCategoriaInventario.SelectedIndex=0;
            var registro=inventario?.FirstOrDefault(item=>item.IdProducto==p.IdProducto);if(registro!=null)CargarFila(registro);}}
        private void CargarFila(InventarioProducto item){seleccionado=item.IdProducto;version=item.Version;txtStockActual.Text=item.StockActual.ToString();textBox1.Text=item.StockMinimo.ToString();chkControlActivo.Checked=item.Activo;}
        private void Seleccionar(){var item=dgvProductoInventario.CurrentRow?.DataBoundItem as InventarioProducto;if(item==null)return;cmbProductoInventario.SelectedValue=item.IdProducto;CargarFila(item);}
        private void Guardar(bool activo){var p=cmbProductoInventario.SelectedItem as Producto;int stock=SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(txtStockActual.Text,"stock actual",0);int minimo=SistemaPedidoRestaurante.Validaciones.CamposValidacion.Entero(textBox1.Text,"stock mínimo",0);servicio.Guardar(new InventarioProducto{IdProducto=p==null?0:p.IdProducto,StockActual=stock,StockMinimo=minimo,Activo=activo,Version=version});}
        private void Nuevo(){dgvProductoInventario.ClearSelection();ElegirProducto();}
        private void FrmInventario_Load(object sender,EventArgs e){}

        private void btnGuardarProducto_Click(object s, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{Guardar(chkControlActivo.Checked);Recargar();Nuevo();});
        }

        private void btnLimpiar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnCancelar_Click(object s, EventArgs e)
        {
            Nuevo();
        }

        private void btnBuscar_Click(object s, EventArgs e)
        {
            Filtrar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{
                var registro=dgvProductoInventario.CurrentRow?.DataBoundItem as InventarioProducto;
                if(registro==null)throw new ArgumentException("Seleccione un registro de inventario.");
                if(VistaGestion.Confirmar(this,"¿Eliminar permanentemente el registro de inventario? Requiere stock cero y no haber sido usado en pedidos. El producto se conserva.")) {
                    servicio.Eliminar(registro.IdProducto,registro.Version); Recargar(); Nuevo();
                }
            });
        }

        private void desactivarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaGestion.Ejecutar(this,()=>{
                if(!(dgvProductoInventario.CurrentRow?.DataBoundItem is InventarioProducto))
                    throw new ArgumentException("Seleccione un registro de inventario.");
                Seleccionar();
                if(VistaGestion.Confirmar(this,"¿Retirar el control de existencias conservando el registro? Requiere stock cero y ningún pedido pendiente.")) {
                    Guardar(false); Recargar(); Nuevo();
                }
            });
        }
    }
}
