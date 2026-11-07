namespace SistemaPedidoRestaurante.Entities
{
    public static class CargosPersonal
    {
        public const string Despachador = "Despachador";

        public static string Normalizar(string cargo)
        {
            cargo = (cargo ?? "").Trim();
            switch (cargo.ToUpperInvariant())
            {
                case "MESERO":
                case "CAJERO":
                case "DESPACHADOR": return Despachador;
                default: return cargo;
            }
        }

        public static string[] ParaRol(int rol)
        {
            if (rol == RolesSistema.Empleado)
                return new[] { Despachador };
            if (rol == RolesSistema.Administrador)
                return new[] { "Administrador", "Gerente", "Encargado" };
            return new string[0];
        }
    }
}
