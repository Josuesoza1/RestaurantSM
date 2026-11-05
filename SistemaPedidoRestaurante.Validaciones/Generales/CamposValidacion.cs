using System;
using System.Globalization;

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
            return char.IsControl(c) || char.IsLetter(c) || c == ' ' || c == '-' || c == '\'' || c == '’';
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
            if (valor == null)
            {
                valor = "";
            }
            valor = valor.Trim();
            if (requerido && valor.Length == 0)
            {
                throw new ArgumentException("El campo " + campo + " es obligatorio.");
            }
            if (valor.Length > maximo)
            {
                throw new ArgumentException("El campo " + campo + " admite hasta " + maximo + " caracteres.");
            }
            if (valor.Length == 0)
            {
                return null;
            }
            return valor;
        }

        public static string NombrePersona(string valor, string campo, bool requerido)
        {
            valor = Texto(valor, 80, campo, requerido);
            if (valor == null)
            {
                return null;
            }
            valor = valor.Normalize();
            while (valor.Contains("  "))
            {
                valor = valor.Replace("  ", " ");
            }
            if (!char.IsLetter(valor[0]) || !char.IsLetter(valor[valor.Length - 1]))
            {
                throw new ArgumentException("El campo " + campo + " debe comenzar y terminar con una letra.");
            }
            for (int i = 0; i < valor.Length; i++)
            {
                char c = valor[i];
                if (char.IsLetter(c))
                {
                    continue;
                }
                if (c != ' ' && c != '-' && c != '\'' && c != '’')
                {
                    throw new ArgumentException("El campo " + campo + " solo admite letras, espacios, guiones y apóstrofes.");
                }
                if (i == 0 || i == valor.Length - 1 || !char.IsLetter(valor[i - 1]) || !char.IsLetter(valor[i + 1]))
                {
                    throw new ArgumentException("Los espacios, guiones y apóstrofes deben estar entre nombres.");
                }
            }
            return valor;
        }

        public static string Cedula(string valor, bool requerido)
        {
            if (valor != null)
            {
                valor = valor.Replace(" ", "").Replace("-", "").ToUpperInvariant();
            }
            valor = Texto(valor, 14, "cédula", requerido);
            if (valor == null)
            {
                return null;
            }
            if (valor.Length != 14)
            {
                throw new ArgumentException("La cédula debe tener 13 números y una letra, por ejemplo 001-010100-0001A.");
            }
            for (int i = 0; i < 13; i++)
            {
                if (valor[i] < '0' || valor[i] > '9')
                {
                    throw new ArgumentException("Los primeros 13 caracteres de la cédula deben ser números.");
                }
            }
            if (valor[13] < 'A' || valor[13] > 'Z')
            {
                throw new ArgumentException("El último carácter de la cédula debe ser una letra.");
            }
            return valor.Substring(0, 3) + "-" + valor.Substring(3, 6) + "-" + valor.Substring(9);
        }

        public static string Telefono(string valor, bool requerido)
        {
            if (valor != null)
            {
                valor = valor.Replace(" ", "").Replace("-", "");
            }
            valor = Texto(valor, 20, "teléfono", requerido);
            if (valor == null)
            {
                return null;
            }
            int inicio = 0;
            if (valor[0] == '+')
            {
                if (valor.Length < 10 || valor.Length > 12 || valor[1] == '0')
                {
                    throw new ArgumentException("Revise el prefijo internacional del teléfono.");
                }
                inicio = 1;
            }
            else if (valor.Length != 8)
            {
                throw new ArgumentException("El teléfono debe tener 8 dígitos.");
            }
            for (int i = inicio; i < valor.Length; i++)
            {
                if (valor[i] < '0' || valor[i] > '9')
                {
                    throw new ArgumentException("El teléfono solo admite números y un prefijo opcional como +505.");
                }
            }
            return valor;
        }

        public static string Correo(string valor, bool requerido)
        {
            valor = Texto(valor, 120, "correo", requerido);
            if (valor == null)
            {
                return null;
            }
            int arroba = valor.IndexOf('@');
            int punto = valor.LastIndexOf('.');
            if (arroba <= 0 || arroba != valor.LastIndexOf('@') || punto <= arroba + 1 || punto == valor.Length - 1)
            {
                throw new ArgumentException("Ingrese un correo válido, por ejemplo nombre@correo.com.");
            }
            foreach (char c in valor)
            {
                if (char.IsWhiteSpace(c))
                {
                    throw new ArgumentException("El correo no debe contener espacios.");
                }
            }
            return valor;
        }

        public static string Usuario(string valor)
        {
            valor = Texto(valor, 50, "usuario", true);
            foreach (char c in valor)
            {
                if (!char.IsLetterOrDigit(c) && c != '_' && c != '.' && c != '-')
                {
                    throw new ArgumentException("El usuario admite letras, números, punto, guion y guion bajo, sin espacios.");
                }
            }
            return valor;
        }

        public static int Entero(string valor, string campo, int minimo, int maximo = int.MaxValue)
        {
            if (valor == null)
            {
                valor = "";
            }
            valor = valor.Trim();
            foreach (char c in valor)
            {
                if (c < '0' || c > '9')
                {
                    throw new ArgumentException("El campo " + campo + " solo admite números enteros.");
                }
            }
            int numero;
            if (!int.TryParse(valor, out numero))
            {
                throw new ArgumentException("Ingrese un número entero válido en " + campo + ".");
            }
            if (numero < minimo || numero > maximo)
            {
                throw new ArgumentException("El campo " + campo + " debe estar entre " + minimo + " y " + maximo + ".");
            }
            return numero;
        }

        public static decimal Precio(string valor)
        {
            if (valor == null)
            {
                valor = "";
            }
            valor = valor.Trim().Replace(',', '.');
            decimal numero;
            if (!decimal.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out numero))
            {
                throw new ArgumentException("Ingrese un precio válido, por ejemplo 150.50.");
            }
            Importe(numero, "precio", false);
            return numero;
        }

        public static void Importe(decimal numero, string campo, bool permiteCero)
        {
            if (numero < 0)
            {
                throw new ArgumentException("El campo " + campo + " no puede ser negativo.");
            }
            if (!permiteCero && numero == 0)
            {
                throw new ArgumentException("El campo " + campo + " debe ser mayor que cero.");
            }
            if (numero > 99999999.99m)
            {
                throw new ArgumentException("El campo " + campo + " no puede superar 99999999.99.");
            }
            if (decimal.Round(numero, 2) != numero)
            {
                throw new ArgumentException("El campo " + campo + " admite hasta dos decimales.");
            }
        }
    }
}
