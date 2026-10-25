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
