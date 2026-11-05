using System;
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
