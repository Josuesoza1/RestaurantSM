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
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMenuAdministracion : Form
    {
        public FrmMenuAdministracion()
        {
            InitializeComponent();
            MenuSesion.Configurar(this, menuStrip1, 1, expandirOpciones: false);
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
        private void FrmMenuAdministracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.Cerrar();
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmUsuarios())
            {
                formulario.ShowDialog(this);
            }
        }

        private void mesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmMesas())
            {
                formulario.ShowDialog(this);
            }
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmInventario())
            {
                formulario.ShowDialog(this);
            }
        }

        private void configuracionRestauranteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmConfiguracionRestaurante())
            {
                formulario.ShowDialog(this);
            }
        }

        private void reservasPersonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmReservasAdministracion())
            {
                formulario.ShowDialog(this);
            }
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmPedidosAdministracion())
            {
                formulario.ShowDialog(this);
            }
        }

        private void historialVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmPedidosAdministracion(false, historial: true))
                formulario.ShowDialog(this);
        }

        private void estadoMesasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmMesasAdministracion())
                formulario.ShowDialog(this);
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form formulario = new FrmConsultaFacturasAdministracion())
            {
                formulario.ShowDialog(this);
            }
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grpAdmin_Enter(object sender, EventArgs e)
        {

        }
    }
}
