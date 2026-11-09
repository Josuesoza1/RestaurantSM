using System;
using System.Security.Cryptography;

namespace SistemaPedidoRestaurante.DAL
{
    internal static class ContrasenaProtegida
    {
        private const string Prefijo = "PBKDF2-SHA256-600000";
        private const int Iteraciones = 600000;
        private const int LongitudSal = 16;
        private const int LongitudHash = 32;

        internal static bool EsProtegida(string valor)
        {
            return valor.StartsWith(Prefijo + "$", StringComparison.Ordinal);
        }

        internal static string Generar(string contrasena)
        {
            byte[] sal = new byte[LongitudSal];
            using (var aleatorio = RandomNumberGenerator.Create())
            {
                aleatorio.GetBytes(sal);
            }

            byte[] hash = Calcular(contrasena, sal);
            return Prefijo + "$" + Convert.ToBase64String(sal) + "$" + Convert.ToBase64String(hash);
        }

        internal static bool Coincide(string contrasena, string guardada)
        {
            string[] partes = guardada.Split('$');
            if (partes.Length != 3)
                return false;

            try
            {
                byte[] sal = Convert.FromBase64String(partes[1]);
                byte[] esperado = Convert.FromBase64String(partes[2]);
                if (sal.Length != LongitudSal || esperado.Length != LongitudHash)
                    return false;

                byte[] actual = Calcular(contrasena, sal);
                int diferencia = 0;
                for (int i = 0; i < actual.Length; i++)
                    diferencia |= actual[i] ^ esperado[i];

                return diferencia == 0;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static byte[] Calcular(string contrasena, byte[] sal)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(LongitudHash);
            }
        }
    }
}
