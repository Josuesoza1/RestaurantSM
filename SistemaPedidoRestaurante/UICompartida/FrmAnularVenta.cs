using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UICompartida
{
    internal sealed class FrmAnularVenta : Form
    {
        private readonly TextBox motivo = new TextBox();
        private readonly CheckBox devolucionConfirmada = new CheckBox();
        private readonly CheckBox reponer = new CheckBox();

        public string Motivo => motivo.Text.Trim();
        public bool ReponerInventario => reponer.Checked;

        public FrmAnularVenta(DocumentoVenta factura)
        {
            Text = "Anular venta " + factura.Numero;
            ClientSize = new Size(460, 234);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = MaximizeBox = false;

            var resumen = new Label
            {
                Text = "Importe a devolver: " + factura.Moneda + " " + factura.Total.ToString("N2") + " — " + factura.Metodo,
                Location = new Point(15, 15), Size = new Size(430, 24)
            };
            var etiqueta = new Label { Text = "Motivo", Location = new Point(15, 45), AutoSize = true };
            motivo.SetBounds(15, 67, 430, 53);
            motivo.Multiline = true;
            motivo.MaxLength = 200;

            devolucionConfirmada.Text = "Ya devolví el importe por el medio correspondiente";
            devolucionConfirmada.Location = new Point(15, 128);
            devolucionConfirmada.AutoSize = true;
            reponer.Text = "Todos los productos regresaron al inventario";
            reponer.Location = new Point(15, 154);
            reponer.AutoSize = true;

            var registrar = new Button { Text = "Registrar anulación", Location = new Point(202, 190), Size = new Size(140, 30) };
            registrar.Click += Registrar_Click;
            var volver = new Button { Text = "Volver", Location = new Point(350, 190), Size = new Size(95, 30), DialogResult = DialogResult.Cancel };
            CancelButton = volver;
            Controls.AddRange(new Control[] { resumen, etiqueta, motivo, devolucionConfirmada, reponer, registrar, volver });
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            if (Motivo.Length == 0 || !devolucionConfirmada.Checked)
            {
                MessageBox.Show(this, "Escriba el motivo y confirme que ya devolvió el importe.", Text);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
