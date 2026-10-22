using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.DAL
{

    public class GestionDAL
    {
        private SqlCommand Crear(SqlConnection cn, string procedimiento, object[] parametros)
        {
            var cmd = new SqlCommand(procedimiento, cn) { CommandType = CommandType.StoredProcedure };
            for (int i = 0; i < parametros.Length; i += 2)
            {
                string nombre = (string)parametros[i];
                object valor = parametros[i + 1] ?? DBNull.Value;
                var p = new SqlParameter(nombre, valor);
                if (nombre == "@Detalle") p.SqlDbType = SqlDbType.Xml;
                if (nombre == "@Version") { p.SqlDbType = SqlDbType.Binary; p.Size = 8; }
                cmd.Parameters.Add(p);
            }
            return cmd;
        }

        public DataTable Consultar(string procedimiento, params object[] parametros)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = Crear(cn, procedimiento, parametros))
            using (var da = new SqlDataAdapter(cmd))
            {
                var datos = new DataTable();
                da.Fill(datos);
                return datos;
            }
        }

        public int Ejecutar(string procedimiento, params object[] parametros)
        {
            using (var cn = ConexionBD.ObtenerConexion())
            using (var cmd = Crear(cn, procedimiento, parametros))
            {
                cn.Open();
                object id = cmd.ExecuteScalar();
                return id == null || id == DBNull.Value ? 0 : Convert.ToInt32(id);
            }
        }
    }
}
