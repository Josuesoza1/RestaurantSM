using System;
using System.Data;
namespace SistemaPedidoRestaurante.DAL
{
    public class ReservasClienteDAL
    {
        private readonly GestionDAL datos = new GestionDAL();
        public DataTable Listar(int actor) { return datos.Consultar("sp_Cliente_Reservas", "@Actor", actor); }
        public int Guardar(int actor, int id, int mesa, DateTime fecha, int personas)
        { return datos.Ejecutar("sp_Cliente_Reserva_Guardar", "@Actor", actor, "@Id", id,
            "@IdMesa", mesa, "@Fecha", fecha, "@Personas", personas); }
        public int Cancelar(int actor, int id)
        { return datos.Ejecutar("sp_Cliente_Reserva_Cancelar", "@Actor", actor, "@Id", id); }
    }
}
