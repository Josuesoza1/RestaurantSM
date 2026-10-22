using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class MesaDAL
    {
        public DataTable ListarGestion()
        {
            return new GestionDAL().Consultar("sp_Mesa_Listar");
        }

        public int Guardar(int id, int numero, int capacidad, string estado, bool activa, int idAdministrador)
        {
            return new GestionDAL().Ejecutar("sp_Mesa_Guardar", "@Actor", idAdministrador, "@Id", id,
                "@Numero", numero, "@Capacidad", capacidad, "@Estado", estado, "@Activa", activa);
        }

        public int Eliminar(int id, int idAdministrador)
        {
            return new GestionDAL().Ejecutar("sp_Mesa_Eliminar", "@Actor", idAdministrador, "@Id", id);
        }
        public List<Mesa> Listar()
        {
            List<Mesa> lista = new List<Mesa>();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Mesa_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Mesa
                            {
                                IdMesa = Convert.ToInt32(reader["idMesa"]),
                                Numero = Convert.ToInt32(reader["numero"]),
                                Capacidad = Convert.ToInt32(reader["capacidad"]),
                                Estado = reader["estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
