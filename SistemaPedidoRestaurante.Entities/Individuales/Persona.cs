namespace SistemaPedidoRestaurante.Entities
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Genero { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string NombreCompleto
        {
            get { return (PrimerNombre + " " + SegundoNombre + " " + PrimerApellido + " " + SegundoApellido).Replace("  ", " ").Trim(); }
        }
    }
}
