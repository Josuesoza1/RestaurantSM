using System;

namespace SistemaPedidoRestaurante.Validaciones
{
    public static class ConfiguracionValidacion
    {
        public static string Validar(string nombre, decimal tasa)
        {
            nombre = CamposValidacion.Texto(nombre, 120, "nombre del restaurante", true);
            if (tasa < 0 || tasa > 100 || decimal.Round(tasa, 2) != tasa)
                throw new ArgumentException("La tasa incluida debe estar entre 0 y 100, con hasta dos decimales.");
            return nombre;
        }
    }
}
