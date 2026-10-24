using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal static class VistaGestion
    {
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
        }

        internal static DataRow Fila(DataGridView grid)
        {
            return grid.SelectedRows.Count == 0 ? null : (grid.CurrentRow?.DataBoundItem as DataRowView)?.Row;
        }

        internal static void Columna(DataGridView grid, string propiedad, string titulo)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "col" + propiedad, DataPropertyName = propiedad, HeaderText = titulo, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }
    }
}
