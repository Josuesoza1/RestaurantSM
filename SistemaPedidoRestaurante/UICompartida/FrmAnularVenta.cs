using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.UICompartida
{
    internal sealed partial class FrmAnularVenta : Form
    {
        public string Motivo => motivo.Text.Trim();
        public bool ReponerInventario => reponer.Checked;

        public FrmAnularVenta()
        {
            InitializeComponent();
        }

        public FrmAnularVenta(DocumentoVenta factura) : this()
        {
            Text = "Anular venta " + factura.Numero;
            resumen.Text = "Importe a devolver: " + factura.Moneda + " " + factura.Total.ToString("N2") + " — " + factura.Metodo;
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

        private void Volver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
