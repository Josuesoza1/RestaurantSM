using System;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmInventario : Form
    {
        private readonly InventarioBLL servicio=new InventarioBLL();
        private DataTable inventario;
        private byte[] version;
        private int seleccionado;
        private bool cargando;
        private readonly ToolStripMenuItem bajos=new ToolStripMenuItem("Solo existencias bajas"){CheckOnClick=true};
        public FrmInventario()
        {
            InitializeComponent();VistaGestion.Listado(dgvProductoInventario);
            string[] campos={"Producto","Categoria","Unidad","PrecioVenta","StockActual","StockMinimo"};
            for(int i=0;i<campos.Length;i++)dgvProductoInventario.Columns[i].DataPropertyName=campos[i];
            dgvProductoInventario.Columns[3].DefaultCellStyle.Format="N2";
            VistaGestion.Columna(dgvProductoInventario,"Activo","Control activo");
            txtPrecioVenta.ReadOnly=txtUnidad.ReadOnly=true;cmbCategoriaInventario.Enabled=false;
            cmbProductoInventario.DisplayMember="Nombre";cmbProductoInventario.ValueMember="IdProducto";
            cmbProductoInventario.SelectedValueChanged+=(s,e)=>{if(!cargando)ElegirProducto();};
            agregarToolStripMenuItem.Click+=(s,e)=>Nuevo();
            editarToolStripMenuItem.Click+=(s,e)=>Seleccionar();
            eliminarToolStripMenuItem.Text="Retirar control";
            eliminarToolStripMenuItem.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{
                Seleccionar();if(seleccionado==0)throw new ArgumentException("Seleccione un registro de inventario.");
                if(VistaGestion.Confirmar(this,"¿Retirar el control de existencias? Requiere stock cero y ningún pedido pendiente.")){Guardar(false);Recargar();Nuevo();}
            });
            volverToolStripMenuItem.Click+=(s,e)=>Close();
            btnGuardarProducto.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{Guardar(chkControlActivo.Checked);Recargar();Nuevo();});
            btnLimpiar.Click+=(s,e)=>Nuevo();btnCancelar.Click+=(s,e)=>Nuevo();
            btnBuscar.Click+=(s,e)=>Filtrar();bajos.CheckedChanged+=(s,e)=>Filtrar();
            menuStrip1.Items.Insert(menuStrip1.Items.Count-1,bajos);
            var actualizar=new ToolStripMenuItem("Recargar");actualizar.Click+=(s,e)=>VistaGestion.Ejecutar(this,()=>{Recargar();Nuevo();});menuStrip1.Items.Add(actualizar);
            ayudaToolStripMenuItem.Click+=(s,e)=>MessageBox.Show(this,"Seleccione un producto existente. Unidad y precio vienen de Productos. Guardar establece las unidades disponibles; los pedidos las descuentan. Productos sin control activo no limitan existencias. Use Recargar si otra operación cambió el stock.","Inventario");
            dgvProductoInventario.CellClick+=(s,e)=>{if(e.RowIndex>=0)Seleccionar();};
            VistaGestion.Iniciar(this,1,()=>{Recargar();Nuevo();});
        }
        private void Recargar(){cargando=true;try{cmbProductoInventario.DataSource=new ProductoBLL().ObtenerProductos();inventario=servicio.Listar();dgvProductoInventario.DataSource=inventario;}finally{cargando=false;}}
        private void Filtrar(){VistaGestion.Ejecutar(this,()=>dgvProductoInventario.DataSource=servicio.Buscar(inventario,textBox2.Text,bajos.Checked));}
        private void ElegirProducto(){var p=cmbProductoInventario.SelectedItem as Producto;seleccionado=0;version=null;txtStockActual.Text="0";textBox1.Text="0";chkControlActivo.Checked=true;txtUnidad.Text="Unidad";txtPrecioVenta.Text=p==null?"":p.Precio.ToString("N2");cmbCategoriaInventario.Items.Clear();if(p!=null){cmbCategoriaInventario.Items.Add(p.CategoriaNombre);cmbCategoriaInventario.SelectedIndex=0;
            if(inventario!=null){foreach(DataRow f in inventario.Rows)if((int)f["IdProducto"]==p.IdProducto){CargarFila(f);break;}}}}
        private void CargarFila(DataRow f){seleccionado=(int)f["IdProducto"];version=(byte[])f["Version"];txtStockActual.Text=f["StockActual"].ToString();textBox1.Text=f["StockMinimo"].ToString();chkControlActivo.Checked=(bool)f["Activo"];}
        private void Seleccionar(){var f=VistaGestion.Fila(dgvProductoInventario);if(f==null)return;cmbProductoInventario.SelectedValue=f["IdProducto"];CargarFila(f);}
        private void Guardar(bool activo){var p=cmbProductoInventario.SelectedItem as Producto;int stock,minimo;if(!int.TryParse(txtStockActual.Text,out stock)||!int.TryParse(textBox1.Text,out minimo))throw new ArgumentException("Stock actual y mínimo deben ser cantidades enteras.");servicio.Guardar(p==null?0:p.IdProducto,stock,minimo,activo,version);}
        private void Nuevo(){dgvProductoInventario.ClearSelection();ElegirProducto();}
        private void FrmInventario_Load(object sender,EventArgs e){}
    }
}
