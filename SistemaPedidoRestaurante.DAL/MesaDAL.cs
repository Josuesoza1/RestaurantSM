using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class MesaDAL
    {
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