using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Loggin;

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
