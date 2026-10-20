using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;
using SistemaPedidoRestaurante.UIEmpleado;
namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMenuAdministracion : Form
    {
        public FrmMenuAdministracion()
        {
            InitializeComponent();
            var gestiones = new ToolStripMenuItem("Más opciones") { Name = "masGestionesToolStripMenuItem" };
            gestiones.DropDownItems.Add(CrearGestion("clientesToolStripMenuItem", "Clientes", () => new FrmClientes()));
            gestiones.DropDownItems.Add(CrearGestion("mesasToolStripMenuItem", "Mesas", () => new FrmMesas()));
            gestiones.DropDownItems.Add(CrearGestion("inventarioToolStripMenuItem", "Inventario", () => new FrmInventario()));
            menuStrip1.Items.Insert(menuStrip1.Items.Count - 1, gestiones);
            var operaciones = new ToolStripMenuItem("Operaciones") { Name = "operacionesToolStripMenuItem" };
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "pedidosToolStripMenuItem", "Pedidos en el local", () => new FrmPedido()));
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "pagosToolStripMenuItem", "Cobros", () => new FrmPago()));
            operaciones.DropDownItems.Add(PantallaPendiente.Opcion(this, "facturasToolStripMenuItem", "Facturas", () => new FrmFacturas()));
            gestiones.DropDownItems.Add(new ToolStripSeparator());
            gestiones.DropDownItems.Add(operaciones);
            MenuSesion.Configurar(this, menuStrip1, 1);
        }
        private ToolStripMenuItem CrearGestion(string nombre,string texto,Func<Form> crear)
        {
            var opcion=new ToolStripMenuItem(texto){Name=nombre};
            opcion.Click+=(s,e)=>{using(var f=crear())f.ShowDialog(this);};return opcion;
        }
        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var categoria = new FrmCategoria())
                categoria.ShowDialog(this);
        }
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var producto = new FrmProducto())
                producto.ShowDialog(this);
        }
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var empleados = new FrmEmpleados())
                empleados.ShowDialog(this);
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                using (var roles = new FrmRoles())
                    roles.ShowDialog(this);
            }
        }
        private void FrmMenuAdministracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
    }
}
