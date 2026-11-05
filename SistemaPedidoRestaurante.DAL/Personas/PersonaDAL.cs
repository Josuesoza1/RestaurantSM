using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class PersonaDAL
    {
        public List<Persona> Listar(int administrador)
        {
            var personas = new List<Persona>();
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Persona_Listar", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = administrador;
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        personas.Add(new Persona
                        {
                            IdPersona = Convert.ToInt32(lector["idPersona"]),
                            PrimerNombre = Convert.ToString(lector["primerNombre"]),
                            SegundoNombre = Convert.ToString(lector["segundoNombre"]),
                            PrimerApellido = Convert.ToString(lector["primerApellido"]),
                            SegundoApellido = Convert.ToString(lector["segundoApellido"]),
                            Genero = Convert.ToString(lector["genero"]), Cedula = Convert.ToString(lector["cedula"]),
                            Telefono = Convert.ToString(lector["telefono"]), Email = Convert.ToString(lector["email"])
                        });
                    }
                }
            }
            return personas;
        }
    }
}
