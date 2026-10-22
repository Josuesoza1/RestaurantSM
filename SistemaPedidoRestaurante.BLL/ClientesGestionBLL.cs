using System;
using System.Data;
using System.Linq;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Validaciones;

namespace SistemaPedidoRestaurante.BLL
{
    public class ClientesGestionBLL
    {
        private readonly ClienteDAL clienteDAL = new ClienteDAL();
        public DataTable Listar() { Permisos.ExigirRol(1); return clienteDAL.ListarGestion(SesionActual.IdUsuario); }
        public void Eliminar(int id)
        {
            Permisos.ExigirRol(1);
            if (id <= 0) throw new ArgumentException("Seleccione un cliente para eliminar.");
            OperacionDatos.Ejecutar(() => clienteDAL.Eliminar(id, SesionActual.IdUsuario));
        }
        public void Desactivar(int id)
        {
            Permisos.ExigirRol(1);
            var fila=Listar().AsEnumerable().FirstOrDefault(r=>r.Field<int>("Id")==id);
            if(fila==null) throw new ArgumentException("Seleccione un cliente existente.");
            // Desactivar no debe guardar cambios que todavía estén sin confirmar en el formulario.
            Guardar(new Cliente { Id=id, PrimerNombre=Convert.ToString(fila["PrimerNombre"]),
                SegundoNombre=Convert.ToString(fila["SegundoNombre"]), PrimerApellido=Convert.ToString(fila["PrimerApellido"]),
                SegundoApellido=Convert.ToString(fila["SegundoApellido"]), Genero=Convert.ToString(fila["Genero"]),
                Cedula=Convert.ToString(fila["Cedula"]), Telefono=Convert.ToString(fila["Telefono"]),
                Email=Convert.ToString(fila["Email"]) },false);
        }
        public DataTable Buscar(DataTable tabla, string filtro) { Permisos.ExigirRol(1); return BusquedaLINQ.Filtrar(tabla, filtro); }
        public int Guardar(Cliente c, bool activo, bool propio = false)
        {
            Permisos.ExigirRol(propio ? 3 : 1);
            GestionValidacion.Cliente(c);
            if (propio)
            {
                var perfil = new ClienteBLL().ObtenerMiPerfil();
                if (perfil == null) throw new ArgumentException("No existe un perfil asociado.");
                c.Id = perfil.Id; activo = true;
            }
            var validado = new Cliente { Id = c.Id, Genero = c.Genero,
                PrimerNombre = GestionValidacion.Texto(c.PrimerNombre, 80, "Primer nombre", true),
                SegundoNombre = GestionValidacion.Texto(c.SegundoNombre, 80, "Segundo nombre"),
                PrimerApellido = GestionValidacion.Texto(c.PrimerApellido, 80, "Primer apellido", true),
                SegundoApellido = GestionValidacion.Texto(c.SegundoApellido, 80, "Segundo apellido"),
                Cedula = GestionValidacion.Texto(c.Cedula, 30, "Cédula"),
                Telefono = GestionValidacion.Texto(c.Telefono, 20, "Teléfono"),
                Email = GestionValidacion.Texto(c.Email, 120, "Correo") };
            return OperacionDatos.Ejecutar(() => clienteDAL.GuardarGestion(validado, activo, SesionActual.IdUsuario));
        }
    }
}
