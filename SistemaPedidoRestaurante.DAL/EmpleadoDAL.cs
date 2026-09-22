using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class EmpleadoDAL
    {
        public Empleados Listar()
        {
            Empleados lista = new Empleados();

            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Empleado_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                PrimerNombre = reader["pNombreEmp"].ToString(),
                                SegundoNombre = reader["sNombreEmp"] == DBNull.Value ? string.Empty : reader["sNombreEmp"].ToString(),
                                PrimerApellido = reader["pApellidoEmp"].ToString(),
                                SegundoApellido = reader["sApellidoEmp"] == DBNull.Value ? string.Empty : reader["sApellidoEmp"].ToString(),
                                Cargo = reader["cargo"].ToString(),
                                Genero = reader["genero"].ToString(),
                                Cedula = reader["cedula"].ToString(),
                                Telefono = reader["telefono"].ToString(),
                                Activo = Convert.ToBoolean(reader["activo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}