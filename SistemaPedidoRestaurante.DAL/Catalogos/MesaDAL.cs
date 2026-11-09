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
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Mesa_Listar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        public int Guardar(int id, int numero, int capacidad, string estado, bool activa, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Mesa_Guardar", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                comando.Parameters.Add("@Numero", SqlDbType.Int).Value = numero;
                comando.Parameters.Add("@Capacidad", SqlDbType.Int).Value = capacidad;
                comando.Parameters.Add("@Estado", SqlDbType.VarChar, 20).Value = (object)estado ?? DBNull.Value;
                comando.Parameters.Add("@Activa", SqlDbType.Bit).Value = activa;
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        public int Eliminar(int id, int idAdministrador)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                using (SqlCommand comando = new SqlCommand("sp_Mesa_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = idAdministrador;
                    comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    conexion.Open();
                    object resultado = comando.ExecuteScalar();
                    if (resultado == null || resultado == DBNull.Value)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(resultado);
                }
            }
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

        public DataTable ListarMesas(int actor)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Operacion_Mesas", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }

        public void AsignarMesa(int actor, int pedido, int mesa)
        {
            const string consulta = @"
                EXEC dbo.sp_Operacion_ExigirPedidoAbierto @Actor, @IdPedido;

                DECLARE @Cliente int, @Anterior int, @Existe int;
                SELECT @Existe = idPedido, @Cliente = idCliente, @Anterior = idMesa
                FROM dbo.pedido WITH (UPDLOCK)
                WHERE idPedido = @IdPedido AND idOrigenPedido = 2 AND idTipoEntrega IN (1, 2)
                    AND estado IN ('Pendiente', 'En preparación', 'Listo');
                IF @Existe IS NULL
                    THROW 50531, N'Solo se asigna mesa a una venta local antes del despacho.', 1;

                DECLARE @EstadoMesa varchar(20);
                SELECT @EstadoMesa = estado FROM dbo.mesa WITH (UPDLOCK) WHERE idMesa = @IdMesa AND activa = 1;
                IF @EstadoMesa IS NULL
                    THROW 50532, N'La mesa no está disponible. Seleccione otra mesa.', 1;

                IF @EstadoMesa = 'No disponible' AND NOT EXISTS (
                    SELECT 1 FROM dbo.pedido WHERE idMesa = @IdMesa
                        AND (idPedido = @IdPedido OR idCliente = @Cliente) AND estado NOT IN ('Pagado', 'Cancelado'))
                    THROW 50533, N'La mesa no está disponible. Seleccione otra mesa.', 1;

                IF EXISTS (SELECT 1 FROM dbo.pedido WHERE idMesa = @IdMesa AND idPedido <> @IdPedido
                    AND (@Cliente IS NULL OR ISNULL(idCliente, 0) <> @Cliente) AND estado NOT IN ('Pagado', 'Cancelado'))
                    THROW 50533, N'La mesa tiene otra venta abierta.', 1;

                UPDATE dbo.pedido
                SET idMesa = @IdMesa, idTipoEntrega = 1,
                    idEmpleado = COALESCE(idEmpleado, (SELECT idEmpleado FROM dbo.vw_Empleado WHERE idUsuario = @Actor AND activo = 1))
                WHERE idPedido = @IdPedido;
                UPDATE dbo.mesa SET estado = 'No disponible' WHERE idMesa = @IdMesa;

                IF @Anterior IS NOT NULL AND @Anterior <> @IdMesa AND NOT EXISTS (
                    SELECT 1 FROM dbo.pedido WHERE idMesa = @Anterior AND estado NOT IN ('Pagado', 'Cancelado'))
                    UPDATE dbo.mesa SET estado = 'Disponible' WHERE idMesa = @Anterior AND activa = 1;";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction(IsolationLevel.Serializable))
                using (SqlCommand comando = new SqlCommand(consulta, conexion, transaccion))
                {
                    comando.Parameters.Add("@Actor", SqlDbType.Int).Value = actor;
                    comando.Parameters.Add("@IdPedido", SqlDbType.Int).Value = pedido;
                    comando.Parameters.Add("@IdMesa", SqlDbType.Int).Value = mesa;
                    comando.ExecuteNonQuery();
                    transaccion.Commit();
                }
            }
        }
    }
}
