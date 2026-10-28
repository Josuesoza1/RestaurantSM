namespace SistemaPedidoRestaurante.Entities
{
    public static class CargosPersonal
    {
        public const string Mesero = "Mesero";
        public const string Cajero = "Cajero";

        public static string Normalizar(string cargo)
        {
            cargo = (cargo ?? "").Trim();
            switch (cargo.ToUpperInvariant())
            {
                case "MESERO": return Mesero;
                case "CAJERO": return Cajero;
                default: return cargo;
            }
        }

        public static string[] ParaRol(int rol)
        {
            if (rol == RolesSistema.Empleado)
                return new[] { Mesero, Cajero };
            if (rol == RolesSistema.Administrador)
                return new[] { "Administrador", "Gerente", "Encargado" };
            return new string[0];
        }
    }
}
