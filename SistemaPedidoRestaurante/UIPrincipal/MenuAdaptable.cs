using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    // Reubica las mismas opciones: conserva sus eventos y permisos al cambiar el ancho.
    internal sealed class MenuAdaptable
    {
        private readonly Form formulario;
        private readonly MenuStrip menu;
        private readonly ToolStripItem[] originales;
        private readonly List<Grupo> grupos = new List<Grupo>();
        private readonly List<ToolStripItem> acciones = new List<ToolStripItem>();
        private bool ampliado;
        private bool actualizando;

        private sealed class Grupo
        {
            internal ToolStripMenuItem Opcion;
            internal ToolStripItem[] Hijos;
        }

        internal static void Configurar(Form formulario, MenuStrip menu)
        {
            TemaUI.AjustarMenu(formulario, menu);
            new MenuAdaptable(formulario, menu);
        }

        private MenuAdaptable(Form formulario, MenuStrip menu)
        {
            this.formulario = formulario;
            this.menu = menu;
            originales = menu.Items.Cast<ToolStripItem>().ToArray();
            Registrar(originales);
            formulario.Resize += Actualizar;
            menu.SizeChanged += Actualizar;
            formulario.Load += Actualizar;
            formulario.Disposed += Liberar;
            Actualizar(formulario, EventArgs.Empty);
        }

        private void Registrar(IEnumerable<ToolStripItem> opciones)
        {
            foreach (var opcion in opciones)
            {
                var grupo = opcion as ToolStripMenuItem;
                if (grupo != null && grupo.HasDropDownItems)
                {
                    var hijos = grupo.DropDownItems.Cast<ToolStripItem>().ToArray();
                    grupos.Add(new Grupo { Opcion = grupo, Hijos = hijos });
                    Registrar(hijos);
                }
                else if (!(opcion is ToolStripSeparator)) acciones.Add(opcion);
            }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            if (actualizando || menu.IsDisposed) return;
            int anchoNecesario = acciones.Sum(opcion =>
                TextRenderer.MeasureText(opcion.Text, menu.Font).Width + 24) + menu.Padding.Horizontal;
            bool mostrarTodas = formulario.WindowState == FormWindowState.Maximized ||
                                menu.ClientSize.Width >= anchoNecesario;
            if (mostrarTodas == ampliado) return;

            actualizando = true;
            menu.SuspendLayout();
            try
            {
                foreach (var grupo in grupos) grupo.Opcion.HideDropDown();
                menu.Items.Clear();
                foreach (var grupo in grupos) grupo.Opcion.DropDownItems.Clear();
                if (mostrarTodas)
                    menu.Items.AddRange(acciones.ToArray());
                else
                {
                    foreach (var grupo in grupos) grupo.Opcion.DropDownItems.AddRange(grupo.Hijos);
                    menu.Items.AddRange(originales);
                }
                ampliado = mostrarTodas;
            }
            finally
            {
                menu.ResumeLayout(true);
                actualizando = false;
            }
        }

        private void Liberar(object sender, EventArgs e)
        {

            foreach (var grupo in grupos)
                if (!grupo.Opcion.IsDisposed) grupo.Opcion.Dispose();
        }
    }
}
