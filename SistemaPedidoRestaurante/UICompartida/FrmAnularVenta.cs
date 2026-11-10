using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;

namespace SistemaPedidoRestaurante.UICompartida
{
    internal sealed partial class FrmAnularVenta : Form
    {
        private readonly Action<string, bool> registrarAnulacion;

        public FrmAnularVenta()
        {
            InitializeComponent();
        }

        public FrmAnularVenta(DocumentoVenta factura, Action<string, bool> registrarAnulacion) : this()
        {
            this.registrarAnulacion = registrarAnulacion;
            Text = "Anular venta " + factura.Numero;
            resumen.Text = "Importe a devolver: " + factura.Moneda + " " + factura.Total.ToString("N2") + " — " + factura.Metodo;
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            string motivoIngresado = motivo.Text.Trim();
            if (motivoIngresado.Length == 0 || !devolucionConfirmada.Checked)
            {
                MessageBox.Show(this, "Escriba el motivo y confirme que ya devolvió el importe.", Text);
                return;
            }

            try
            {
                registrarAnulacion(motivoIngresado, reponer.Checked);
                DialogResult = DialogResult.OK;
            }
            catch (Exception error)
            {
                MessageBox.Show(this, VistaGestion.MensajeParaUsuario(error,
                    "No se pudo confirmar el registro.") + Environment.NewLine + Environment.NewLine +
                    "Si el cliente ya recibió el dinero, no vuelva a entregárselo. " +
                    "Conserve esta ventana y pulse «Verificar y registrar» cuando pueda volver a conectarse. " +
                    "El sistema comprobará primero el estado de esta factura.",
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                registrar.Text = "Verificar y registrar";
            }
        }

        private void Volver_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
