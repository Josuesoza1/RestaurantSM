using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class CamposValidacion
    {
        public static bool CaracterEntero(char c)
        {
            return char.IsControl(c) || c >= '0' && c <= '9';
        }

        public static bool CaracterPrecio(char c)
        {
            return CaracterEntero(c) || c == '.' || c == ',';
        }

        public static bool CaracterNombre(char c)
        {
            return char.IsControl(c) || char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '\u2019';
        }

        public static bool CaracterTelefono(char c)
        {
            return CaracterEntero(c) || c == '+' || c == '-' || c == ' ';
        }

        public static void MascaraCompleta(bool completa, bool tieneDatos, string campo, bool requerido)
        {
            if (!completa && (tieneDatos || requerido))
            {
                throw new ArgumentException("Complete correctamente el campo " + campo + ".");
            }
        }

        public static string Texto(string valor, int maximo, string campo, bool requerido = false)
        {
            valor = (valor ?? string.Empty).Trim();
            if (requerido && valor.Length == 0)
            {
                throw new ArgumentException("El campo " + campo + " es obligatorio.");
            }

            if (valor.Length > maximo)
            {
                throw new ArgumentException("El campo " + campo + " admite hasta " + maximo + " caracteres.");
            }

            return valor.Length == 0 ? null : valor;
        }

        public static string NombrePersona(string valor, string campo, bool requerido)
        {
            valor = Texto(valor, 80, campo, requerido);
            if (valor == null)
            {
                return null;
            }

            if (Regex.IsMatch(valor, @"[\r\n\t]"))
            {
                throw new ArgumentException("Revise " + campo + ": no admite saltos de línea.");
            }

            valor = Regex.Replace(valor, " +", " ");
            if (!Regex.IsMatch(valor, @"^\p{L}[\p{L}\p{M}]*(?:[ '\u2019-]\p{L}[\p{L}\p{M}]*)*$"))
            {
                throw new ArgumentException("El campo " + campo + " solo admite letras, espacios, guiones y apóstrofes entre nombres.");
            }

            return valor;
        }

        public static string Cedula(string valor, bool requerido)
        {
            valor = Regex.Replace((valor ?? string.Empty).Trim(), "[ -]", "").ToUpperInvariant();
            valor = Texto(valor, 14, "cédula", requerido);
            if (valor == null)
            {
                return null;
            }

            if (!Regex.IsMatch(valor, @"^[0-9]{13}[A-Z]$"))
            {
                throw new ArgumentException("La cédula debe estar completa, por ejemplo 001-010100-0001A.");
            }

            return valor.Substring(0, 3) + "-" + valor.Substring(3, 6) + "-" + valor.Substring(9);
        }

        public static string Telefono(string valor, bool requerido)
        {
            valor = Regex.Replace((valor ?? string.Empty).Trim(), "[ -]", "");
            valor = Texto(valor, 20, "teléfono", requerido);
            if (valor == null)
            {
                return null;
            }

            if (!Regex.IsMatch(valor, @"^(?:\+[1-9][0-9]{0,2})?[0-9]{8}$"))
            {
                throw new ArgumentException("El teléfono debe tener 8 dígitos, con prefijo internacional opcional; por ejemplo 88888888 o +505 8888-8888.");
            }

            return valor;
        }

        public static string Correo(string valor, bool requerido)
        {
            valor = Texto(valor, 120, "correo", requerido);
            if (valor != null && !Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }

            return valor;
        }

        public static string Usuario(string valor)
        {
            valor = Texto(valor, 50, "usuario", true);
            if (!Regex.IsMatch(valor, @"^[\p{L}\p{N}_.-]+$"))
            {
                throw new ArgumentException("El usuario admite letras, números, punto, guion y guion bajo, sin espacios.");
            }

            return valor;
        }

        public static int Entero(string valor, string campo, int minimo, int maximo = int.MaxValue)
        {
            int numero;
            if (!int.TryParse((valor ?? string.Empty).Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out numero) || numero < minimo || numero > maximo)
            {
                throw new ArgumentException("El campo " + campo + " debe ser un número entero entre " + minimo + " y " + maximo + ".");
            }

            return numero;
        }

        public static decimal Precio(string valor)
        {
            decimal numero;
            valor = (valor ?? string.Empty).Trim().Replace(',', '.');
            if (!decimal.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out numero) || numero <= 0 || numero > 99999999.99m || decimal.Round(numero, 2) != numero)
            {
                throw new ArgumentException("Ingrese un precio positivo con hasta dos decimales y sin separadores de miles, por ejemplo 150.50.");
            }

            return numero;
        }
    }
}
