using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    // Permite revisar el diseño sin ofrecer acciones todavía no implementadas.
    internal static class PantallaPendiente
    {
        public static void Configurar(Form formulario, params int[] roles)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            formulario.Text += " - En preparación";
            PrepararControles(formulario, formulario);
            formulario.Load += (sender, e) =>
            {
                if (formulario.Site?.DesignMode == true) return;
                if (!SesionActual.EstaAutenticado || !roles.Contains(SesionActual.IdRol))
                    formulario.Close();
            };
        }

        private static void PrepararControles(Control raiz, Form formulario)
        {
            foreach (Control control in raiz.Controls)
            {
                if (control is Button boton)
                {
                    bool volver = boton.Text.Trim().StartsWith("Volver", StringComparison.OrdinalIgnoreCase);
                    boton.Enabled = volver;
                    if (volver) boton.Click += (sender, e) => formulario.Close();
                }
                else if (control is DataGridView listado)
                {
                    listado.ReadOnly = true;
                    listado.AllowUserToAddRows = false;
                    listado.AllowUserToDeleteRows = false;
                }
                else if (control is MenuStrip menu)
                {
                    foreach (ToolStripItem item in menu.Items) PrepararOpcion(item, formulario);
                }
                PrepararControles(control, formulario);
            }
        }

        private static void PrepararOpcion(ToolStripItem item, Form formulario)
        {
            bool volver = item.Text.Trim().StartsWith("Volver", StringComparison.OrdinalIgnoreCase);
            if (volver) item.Click += (sender, e) => formulario.Close();
            else if (item is ToolStripMenuItem opcion && opcion.DropDownItems.Count > 0)
                foreach (ToolStripItem hijo in opcion.DropDownItems) PrepararOpcion(hijo, formulario);
            else item.Enabled = false;
        }

        public static ToolStripMenuItem Opcion(Form propietario, string nombre, string texto, Func<Form> crear)
        {
            var item = new ToolStripMenuItem(texto + " (en preparación)") { Name = nombre };
            item.Click += (sender, e) =>
            {
                using (Form formulario = crear()) formulario.ShowDialog(propietario);
            };
            return item;
        }
    }
}
