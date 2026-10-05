using SistemaPedidoRestaurante.Loggin;
using SistemaPedidoRestaurante.UIAdministracion;
using System;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
