using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal static class VistaGestion
    {
        internal static void Modo(Label estado, Panel panel, Button accion,
            UIAdministracion.ModoFormulario modo, string entidad)
        {
            switch (modo)
            {
                case UIAdministracion.ModoFormulario.Nuevo:
                    estado.Text = "Operación actual: Agregar " + entidad;
                    panel.BackColor = System.Drawing.Color.LightGreen;
                    accion.Text = "Guardar";
                    break;
                case UIAdministracion.ModoFormulario.Editar:
                    estado.Text = "Operación actual: Editar " + entidad;
                    panel.BackColor = System.Drawing.Color.LightSkyBlue;
                    accion.Text = "Guardar cambios";
                    break;
                case UIAdministracion.ModoFormulario.Eliminar:
                    estado.Text = "Operación actual: Eliminar " + entidad;
                    panel.BackColor = System.Drawing.Color.LightCoral;
                    accion.Text = "Eliminar";
                    break;
                default:
                    estado.Text = "Operación actual: Seleccione una operación";
                    panel.BackColor = System.Drawing.Color.Gainsboro;
                    accion.Text = "Guardar";
                    break;
            }
        }

        internal static bool Confirmar(Form form, string texto)
        {
            return MessageBox.Show(form, texto, form.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        internal static bool ConfirmarDescarte(Form form, bool hayCambios)
        {
            return !hayCambios || Confirmar(form, "Hay cambios sin guardar. ¿Desea descartarlos?");
        }

        internal static string MensajeParaUsuario(Exception error, string aviso)
        {
            if (error is ArgumentException || error is UnauthorizedAccessException)
                return error.Message;

            Trace.TraceError(error.ToString());
            return aviso;
        }

        internal static void Listado(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoGenerateColumns = false;
            PermitirOrden(grid);
        }

        internal static void PermitirOrden(DataGridView grid)
        {
            grid.DataBindingComplete += HabilitarOrdenDeColumnas;
            grid.ColumnHeaderMouseClick += OrdenarPorColumna;
        }

        private static void HabilitarOrdenDeColumnas(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = (DataGridView)sender;
            foreach (DataGridViewColumn columna in grid.Columns)
            {
                if (columna.IsDataBound)
                    columna.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private static void OrdenarPorColumna(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grid = (DataGridView)sender;
            DataGridViewColumn columna = grid.Columns[e.ColumnIndex];
            if (e.Button != MouseButtons.Left || columna.SortMode != DataGridViewColumnSortMode.Programmatic || grid.Rows.Count == 0)
                return;

            bool descendente = grid.SortedColumn == columna && grid.SortOrder == SortOrder.Ascending;
            Ordenar(grid, columna, descendente);
        }

        internal static void Ordenar(DataGridView grid, DataGridViewColumn columna, bool descendente)
        {
            object seleccionado = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem;
            grid.Sort(columna, descendente ? ListSortDirection.Descending : ListSortDirection.Ascending);
            columna.HeaderCell.SortGlyphDirection = descendente ? SortOrder.Descending : SortOrder.Ascending;

            grid.ClearSelection();
            if (seleccionado == null)
                return;

            foreach (DataGridViewRow fila in grid.Rows)
            {
                if (Equals(fila.DataBoundItem, seleccionado))
                {
                    grid.CurrentCell = fila.Cells[columna.Index];
                    fila.Selected = true;
                    break;
                }
            }
        }

        internal static void Vincular<T>(DataGridView grid, IEnumerable<T> datos)
        {
            grid.DataSource = new ListaOrdenable<T>(datos);
        }

        internal static void VincularGestion<T>(DataGridView grid, IEnumerable<T> datos)
        {
            MostrarListadoGestion(grid, new ListaOrdenable<T>(datos));
        }

        internal static void MostrarListadoGestion(DataGridView grid, object datos)
        {
            int primeraFila = grid.FirstDisplayedScrollingRowIndex;
            grid.DataSource = datos;
            grid.CurrentCell = null;
            grid.ClearSelection();

            if (primeraFila >= 0 && grid.Rows.Count > 0)
                grid.FirstDisplayedScrollingRowIndex = Math.Min(primeraFila, grid.Rows.Count - 1);
        }

        internal static T RegistroSeleccionado<T>(DataGridView grid) where T : class
        {
            return grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem as T;
        }

        internal static T SeleccionarRegistro<T>(DataGridView grid, Predicate<T> coincide) where T : class
        {
            foreach (DataGridViewRow fila in grid.Rows)
            {
                if (fila.DataBoundItem is T registro && coincide(registro))
                {
                    grid.CurrentCell = fila.Cells[0];
                    fila.Selected = true;
                    return registro;
                }
            }

            return null;
        }

        internal static string TituloListado(string registros, int cantidad, string filtro)
        {
            if (cantidad == 0)
                return filtro.Length == 0
                    ? "Aún no hay " + registros + "."
                    : "Sin coincidencias. Use Quitar filtro para ver el listado completo.";

            return "Listado de " + registros + " (" + cantidad + ")" +
                (filtro.Length == 0 ? "" : " — Filtro aplicado");
        }

        internal static DataRow Fila(DataGridView grid)
        {
            return RegistroSeleccionado<DataRowView>(grid)?.Row;
        }

        internal static void Columna(DataGridView grid, string propiedad, string titulo)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "col" + propiedad, DataPropertyName = propiedad, HeaderText = titulo, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }
    }
}
