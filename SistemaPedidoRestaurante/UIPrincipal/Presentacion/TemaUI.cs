using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIPrincipal
{

    public static class TemaUI
    {
        private static readonly Font FuenteBoton = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 9F, FontStyle.Bold);

        public static void AjustarMenu(Form formulario, MenuStrip menu)
        {
            foreach (Control encabezado in formulario.Controls.Find("pnlEncabezado", true))
            {
                if (encabezado.Dock == DockStyle.None)
                    encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                foreach (Control titulo in encabezado.Controls.Find("lblTitulo", true))
                {
                    titulo.AutoSize = false;
                    titulo.Dock = DockStyle.Fill;
                    if (titulo is Label etiqueta) etiqueta.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
            foreach (Control contenido in formulario.Controls.Find("grpAdmin", true))
                contenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            menu.Dock = DockStyle.Top;
            menu.AutoSize = true;
            menu.CanOverflow = false;
            menu.LayoutStyle = ToolStripLayoutStyle.Flow;
            var distribucion = (FlowLayoutSettings)menu.LayoutSettings;
            distribucion.FlowDirection = FlowDirection.LeftToRight;
            distribucion.WrapContents = true;
            formulario.MinimumSize = new Size(System.Math.Max(480, formulario.MinimumSize.Width),
                                              System.Math.Max(300, formulario.MinimumSize.Height));
        }

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
                if (contenedor.Dock == DockStyle.None)
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
