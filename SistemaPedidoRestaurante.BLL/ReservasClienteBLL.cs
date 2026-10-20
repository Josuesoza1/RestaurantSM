using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ReservasClienteBLL
    {
        public DataTable Listar() { GestionReglas.Rol(3); return GestionReglas.Datos.Consultar("sp_Cliente_Reservas", "@Actor", SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable t, string filtro) { GestionReglas.Rol(3); return GestionReglas.Filtrar(t, filtro); }
        public int Guardar(int id, int mesa, DateTime fecha, int personas)
        {
            GestionReglas.Rol(3);
            GestionValidacion.Reserva(mesa,fecha,personas);
            return GestionReglas.Escribir("sp_Cliente_Reserva_Guardar", "@Actor", SesionActual.IdUsuario,
                "@Id", id, "@IdMesa", mesa, "@Fecha", fecha, "@Personas", personas);
        }
        public void Cancelar(int id) { GestionReglas.Rol(3); GestionReglas.Escribir("sp_Cliente_Reserva_Cancelar", "@Actor", SesionActual.IdUsuario, "@Id", id); }
    }
}
