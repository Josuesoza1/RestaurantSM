using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIAdministracion
{

    public static class TemaUI
    {
        private static readonly Font FuenteBoton = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 9F, FontStyle.Bold);

        public static void Aplicar(Form formulario)
        {
            if (formulario == null)
                return;

            AplicarEstilos(formulario);


            DataGridView listado = ObtenerControles<DataGridView>(formulario)
                .OrderByDescending(dgv => (long)dgv.Width * dgv.Height)
                .FirstOrDefault();

            if (listado == null)
                return;

            Control contenedor = listado.Parent;
            while (contenedor != null && contenedor != formulario)
            {
                contenedor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                                    AnchorStyles.Left | AnchorStyles.Right;
                contenedor = contenedor.Parent;
            }

         
            if (listado.Parent != formulario)
            {
                listado.Dock = DockStyle.Fill;
            }
            else
            {
                listado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                                 AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private static void AplicarEstilos(Control raiz)
        {
            foreach (Control control in raiz.Controls)
            {
                if (control is Button boton)
                {
                    boton.FlatStyle = FlatStyle.Flat;
                    boton.FlatAppearance.BorderSize = 1;
                    boton.FlatAppearance.BorderColor = Color.DarkGray;
                    boton.Font = FuenteBoton;
                    boton.Cursor = Cursors.Hand;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BorderStyle = BorderStyle.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgv.ColumnHeadersDefaultCellStyle.Font = FuenteTitulo;
                    dgv.BackgroundColor = Color.White;
                }
                else if (control is GroupBox grupo)
                {
                    grupo.Font = FuenteTitulo;
                }

                
                if (control.HasChildren)
                    AplicarEstilos(control);
            }
        }

        private static System.Collections.Generic.IEnumerable<T> ObtenerControles<T>(Control raiz)
            where T : Control
        {
            foreach (Control control in raiz.Controls)
            {
                if (control is T encontrado)
                    yield return encontrado;

                foreach (T descendiente in ObtenerControles<T>(control))
                    yield return descendiente;
            }
        }
    }
}
