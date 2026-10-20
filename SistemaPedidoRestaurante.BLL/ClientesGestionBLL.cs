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
    public class ClientesGestionBLL
    {
        public DataTable Listar() { GestionReglas.Rol(1); return GestionReglas.Datos.Consultar("sp_Gestion_Clientes", "@Actor", SesionActual.IdUsuario); }
        public DataTable Buscar(DataTable tabla, string filtro) { GestionReglas.Rol(1); return GestionReglas.Filtrar(tabla, filtro); }
        public int Guardar(Cliente c, bool activo, bool propio = false)
        {
            GestionReglas.Rol(propio ? 3 : 1);
            GestionValidacion.Cliente(c);
            if (propio)
            {
                var perfil = new ClienteBLL().ObtenerMiPerfil();
                if (perfil == null) throw new ArgumentException("No existe un perfil asociado.");
                c.Id = perfil.Id; activo = true;
            }
            string email = GestionReglas.Texto(c.Email, 120, "Correo");
            return GestionReglas.Escribir("sp_Cliente_Guardar", "@Actor", SesionActual.IdUsuario, "@Id", c.Id,
                "@PrimerNombre", GestionReglas.Texto(c.PrimerNombre, 80, "Primer nombre", true),
                "@SegundoNombre", GestionReglas.Texto(c.SegundoNombre, 80, "Segundo nombre"),
                "@PrimerApellido", GestionReglas.Texto(c.PrimerApellido, 80, "Primer apellido", true),
                "@SegundoApellido", GestionReglas.Texto(c.SegundoApellido, 80, "Segundo apellido"),
                "@Genero", c.Genero, "@Cedula", GestionReglas.Texto(c.Cedula, 30, "Cédula"),
                "@Telefono", GestionReglas.Texto(c.Telefono, 20, "Teléfono"), "@Email", email, "@Activo", activo);
        }
    }
}
