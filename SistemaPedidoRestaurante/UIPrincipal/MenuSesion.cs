using System;
using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal static class MenuSesion
    {
        public static void Configurar(Form formulario, MenuStrip menu, int idRol)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            var cerrarSesion = new ToolStripMenuItem("Cerrar sesión");
            cerrarSesion.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesion.Click += (sender, e) => formulario.Close();
            menu.Items.Insert(Math.Max(0, menu.Items.Count - 1), cerrarSesion);
            MenuAdaptable.Configurar(formulario, menu);

            formulario.Load += (sender, e) =>
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                    formulario.Site?.DesignMode == true)
                    return;

                if (!SesionActual.TieneRol(idRol))
                {
                    formulario.Close();
                    return;
                }

                formulario.Text += " - " + SesionActual.NombreUsuario + " (" + SesionActual.NombreRol + ")";
            };
        }
    }
}
