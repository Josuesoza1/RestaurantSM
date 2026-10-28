namespace SistemaPedidoRestaurante.Entities
{
    public static class RolesSistema
    {
        public const int Administrador = 1;
        public const int Empleado = 2;
        public const int Cliente = 3;

        public static string Nombre(int rol)
        {
            switch (rol)
            {
                case Administrador: return "Administrador";
                case Empleado: return "Empleado";
                case Cliente: return "Cliente";
                default: return null;
            }
        }
    }
}
