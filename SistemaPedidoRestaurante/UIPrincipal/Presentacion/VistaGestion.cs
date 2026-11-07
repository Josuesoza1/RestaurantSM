using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    accion.Text = "Acción";
                    break;
            }
        }

        internal static bool Confirmar(Form form, string texto)
        {
            return MessageBox.Show(form, texto, form.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
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

            object seleccionado = grid.SelectedRows.Count == 0 ? null : grid.SelectedRows[0].DataBoundItem;
            bool descendente = grid.SortedColumn == columna && grid.SortOrder == SortOrder.Ascending;
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

        internal static DataRow Fila(DataGridView grid)
        {
            return grid.SelectedRows.Count == 0 ? null : (grid.SelectedRows[0].DataBoundItem as DataRowView)?.Row;
        }

        internal static void Columna(DataGridView grid, string propiedad, string titulo)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "col" + propiedad, DataPropertyName = propiedad, HeaderText = titulo, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }
    }
}
