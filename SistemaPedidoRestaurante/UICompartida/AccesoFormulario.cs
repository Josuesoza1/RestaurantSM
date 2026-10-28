using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UICompartida
{
    internal static class AccesoFormulario
    {
        public static bool Permitir(Form formulario, int rol)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || formulario.Site?.DesignMode == true)
                return true;
            if (rol > 0 && SesionActual.TieneRol(rol))
                return true;
            formulario.DialogResult = DialogResult.Cancel;
            formulario.Close();
            return false;
        }

        public static bool PermitirPersonal(Form formulario)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || formulario.Site?.DesignMode == true)
                return true;
            if (SesionActual.TieneRol(1) || SesionActual.TieneRol(2))
                return true;
            formulario.DialogResult = DialogResult.Cancel;
            formulario.Close();
            return false;
        }
    }
}
