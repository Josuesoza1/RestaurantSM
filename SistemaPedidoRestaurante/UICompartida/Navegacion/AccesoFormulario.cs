using System.ComponentModel;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UICompartida
{
    internal static class AccesoFormulario
    {
        public static bool PermitirOperacion(Form formulario, int rol, bool permiso)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || formulario.Site?.DesignMode == true)
                return true;
            if (permiso) return Permitir(formulario, rol);
            formulario.DialogResult = DialogResult.Cancel;
            formulario.Close();
            return false;
        }

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

        public static bool PermitirAtencion(Form formulario)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || formulario.Site?.DesignMode == true)
                return true;
            if (Permisos.PuedeAtender)
                return true;
            formulario.DialogResult = DialogResult.Cancel;
            formulario.Close();
            return false;
        }
    }
}
