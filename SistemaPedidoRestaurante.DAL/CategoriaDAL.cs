using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Entities;

namespace SistemaPedidoRestaurante.DAL
{
    public class CategoriaDAL
    {
        public void Eliminar(int idCategoria, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Categoria_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;
                    comando.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    EjecutarEscritura(comando, "La categoría tiene datos relacionados.");
                }
            }
        }

        public Categorias Listar()
        {
            Categorias lista = new Categorias();
            using (SqlConnection connection = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand command = new SqlCommand("sp_Categoria_Listar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                Nombre = reader["nombre"].ToString(),
                                Descripcion = reader["descripcion"] == DBNull.Value ? string.Empty : reader["descripcion"].ToString(),
                                Activa = Convert.ToBoolean(reader["activa"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public void Guardar(Categoria categoria, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Categoria_Guardar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = categoria.IdCategoria;
                    comando.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 60).Value = categoria.Nombre;
                    object descripcion = categoria.Descripcion;
                    if (descripcion == null)
                    {
                        descripcion = DBNull.Value;
                    }

                    comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 200).Value = descripcion;
                    comando.Parameters.Add("@Activa", SqlDbType.Bit).Value = categoria.Activa;
                    EjecutarEscritura(comando, "Ya existe una categoría con ese nombre.");
                }
            }
        }

        public void Desactivar(int idCategoria, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Categoria_Desactivar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;
                    comando.Parameters.Add("@IdAdministrador", SqlDbType.Int).Value = idAdministrador;
                    EjecutarEscritura(comando, "Ya existe una categoría con ese nombre.");
                }
            }
        }

        // Comparte la ejecución y el manejo de errores sin recibir funciones ni lambdas.
        private void EjecutarEscritura(SqlCommand comando, string mensajeDuplicado)
        {
            try
            {
                comando.Connection.Open();
                comando.ExecuteNonQuery();
            }
            catch (SqlException error)
            {
                switch (error.Number)
                {
                    case 2601:
                    case 2627:
                        throw new ArgumentException(mensajeDuplicado, error);
                    case 1205:
                        throw new ArgumentException("Otra operación modificó estos datos al mismo tiempo. Recargue e inténtelo de nuevo.", error);
                    case 547:
                        throw new ArgumentException("El registro tiene relaciones o valores incompatibles. No se realizó el cambio.", error);
                    case 2812:
                        throw new ArgumentException("Falta un procedimiento en la base. Instale las actualizaciones SQL del proyecto.", error);
                    default:
                        if (error.Number >= 50000)
                        {
                            throw new ArgumentException(error.Message, error);
                        }

                        throw;
                }
            }
        }
    }
}
