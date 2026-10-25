using System.Windows.Forms;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.UIPrincipal
{

    internal static class ValidacionEntradaUI
    {
        internal static void Nombres(params TextBox[] campos)
        { foreach(var campo in campos) { campo.MaxLength=80; campo.KeyPress+=Nombre_KeyPress; } }
        internal static void Enteros(params TextBox[] campos)
        { foreach(var campo in campos) { campo.MaxLength=10; campo.KeyPress+=Entero_KeyPress; } }
        internal static void Precios(params TextBox[] campos)
        { foreach(var campo in campos) { campo.MaxLength=11; campo.KeyPress+=Precio_KeyPress; } }
        internal static void Telefonos(params TextBox[] campos)
        { foreach(var campo in campos) { campo.MaxLength=20; campo.KeyPress+=Telefono_KeyPress; } }
        internal static void Cedulas(params TextBox[] campos)
        { foreach(var campo in campos) { campo.MaxLength=16; campo.CharacterCasing=CharacterCasing.Upper; } }
        private static void Nombre_KeyPress(object sender, KeyPressEventArgs e) { e.Handled=!CamposValidacion.CaracterNombre(e.KeyChar); }
        private static void Entero_KeyPress(object sender, KeyPressEventArgs e) { e.Handled=!CamposValidacion.CaracterEntero(e.KeyChar); }
        private static void Precio_KeyPress(object sender, KeyPressEventArgs e) { e.Handled=!CamposValidacion.CaracterPrecio(e.KeyChar); }
        private static void Telefono_KeyPress(object sender, KeyPressEventArgs e) { e.Handled=!CamposValidacion.CaracterTelefono(e.KeyChar); }
    }
}
