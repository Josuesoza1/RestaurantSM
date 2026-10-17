using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.DAL;
using SistemaPedidoRestaurante.Entities;

class VerificarEmpleados
{
    static int checks;
    static Usuario administrador;
    static readonly EmpleadoBLL bll = new EmpleadoBLL();
    static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString; } }

    static object Sql(string query, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(ConnectionString))
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddRange(parameters);
            connection.Open();
            return command.ExecuteScalar();
        }
    }
    static void Assert(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
        checks++;
        Console.WriteLine("OK: " + message);
    }
    static void Rechaza(Action action, string message)
    {
        try { action(); }
        catch (ArgumentException) { Assert(true, message); return; }
        catch (UnauthorizedAccessException) { Assert(true, message); return; }
        throw new Exception("Se acepto una operacion invalida: " + message);
    }
    static Empleado Nuevo(out Usuario usuario)
    {
        string suffix = Guid.NewGuid().ToString("N");
        usuario = new Usuario { NombreUsuario = "prueba_" + suffix, Contrasena = Guid.NewGuid().ToString("N"), IdRol = 2 };
        return new Empleado { PrimerNombre = "Prueba", PrimerApellido = "Temporal", Cargo = "Mesero",
            Cedula = "PRUEBA-" + suffix.Substring(0, 16), Telefono = "8888-8888", Genero = "M", Activo = true };
    }
    static Empleado Crear(out Usuario usuario)
    {
        Empleado empleado = Nuevo(out usuario);
        bll.GuardarEmpleado(empleado, usuario, true);
        return bll.ObtenerEmpleados().Single(e => e.Cedula == empleado.Cedula);
    }
    static void Aislar(Action prueba)
    {
        SesionActual.Iniciar(administrador);
        using (var scope = new TransactionScope())
        {
            prueba();
            // No se llama Complete: revierte los registros de prueba.
        }
    }
    static void Login(Usuario usuario, bool permitido)
    {
        if (permitido)
        {
            Usuario resultado = new UsuarioBLL().IniciarSesion(usuario.NombreUsuario, usuario.Contrasena);
            Assert(resultado.IdRol == usuario.IdRol, "Login conserva el rol esperado");
        }
        else Rechaza(() => new UsuarioBLL().IniciarSesion(usuario.NombreUsuario, usuario.Contrasena), "Login rechaza cuenta inactiva o credenciales anteriores");
    }

    static int Main()
    {
        try
        {
            object usersBefore = Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo.usuario;");
            object employeesBefore = Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo.empleado;");
            int countUsers = Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.usuario;"));
            int countEmployees = Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.empleado;"));
            administrador = new Usuario { IdUsuario = Convert.ToInt32(Sql("SELECT TOP (1) u.idUsuario FROM dbo.usuario u WHERE u.idRol=1 AND NOT EXISTS (SELECT 1 FROM dbo.empleado e WHERE e.idUsuario=u.idUsuario AND e.activo=0) ORDER BY u.idUsuario;")),
                NombreUsuario = "AdministradorDePrueba", IdRol = 1, NombreRol = "Administrador" };
            Assert(administrador.IdUsuario > 0, "Existe administrador activo para las comprobaciones");

            Aislar(delegate
            {
                Usuario usuario;
                Empleado empleado = Crear(out usuario);
                Assert(empleado.IdUsuario.HasValue && empleado.IdRol == 2 && empleado.NombreUsuario == usuario.NombreUsuario,
                    "Alta vincula empleado y cuenta de Empleado");
                Assert(empleado.SegundoNombre == "" && empleado.SegundoApellido == "", "Nombres opcionales admiten NULL");
                Login(usuario, true);
                empleado.Cargo = "Cajero";
                empleado.Telefono = "7777-7777";
                usuario.NombreUsuario += "_e";
                string anterior = usuario.Contrasena;
                usuario.Contrasena = "";
                bll.GuardarEmpleado(empleado, usuario, false);
                Empleado editado = bll.ObtenerEmpleados().Single(e => e.IdEmpleado == empleado.IdEmpleado);
                Assert(editado.Cargo == "Cajero" && editado.Telefono == "7777-7777" && editado.NombreUsuario == usuario.NombreUsuario,
                    "Edicion guarda datos personales y de cuenta");
                usuario.Contrasena = anterior;
                Login(usuario, true);
                usuario.Contrasena = Guid.NewGuid().ToString("N");
                bll.GuardarEmpleado(empleado, usuario, false);
                Login(new Usuario { NombreUsuario = usuario.NombreUsuario, Contrasena = anterior, IdRol = 2 }, false);
                Login(usuario, true);
                bll.DesactivarEmpleado(empleado.IdEmpleado);
                Assert(!bll.ObtenerEmpleados().Single(e => e.IdEmpleado == empleado.IdEmpleado).Activo, "Baja conserva el empleado como inactivo");
                Assert(Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.usuario WHERE idUsuario=@id", new SqlParameter("@id", empleado.IdUsuario))) == 1,
                    "Baja conserva la cuenta y las relaciones");
                Login(usuario, false);
                empleado.Activo = true;
                bll.GuardarEmpleado(empleado, usuario, false);
                Login(usuario, true);
                Assert(bll.BuscarEmpleados("Cajero").Any(e => e.IdEmpleado == empleado.IdEmpleado), "Busqueda encuentra el cargo actualizado");
            });

            Aislar(delegate
            {
                Usuario usuario;
                Empleado empleado = Nuevo(out usuario);
                int id = Convert.ToInt32(Sql("INSERT dbo.empleado (pNombreEmp,pApellidoEmp,cargo,genero,cedula,telefono,activo) VALUES ('Prueba','SinCuenta','Mesero','O',@cedula,'8888-8888',1); SELECT CONVERT(int,SCOPE_IDENTITY());",
                    new SqlParameter("@cedula", empleado.Cedula)));
                empleado = bll.ObtenerEmpleados().Single(e => e.IdEmpleado == id);
                Assert(!empleado.IdUsuario.HasValue && !empleado.IdRol.HasValue, "Listado admite empleado sin usuario");
                bll.GuardarEmpleado(empleado, usuario, false);
                Assert(bll.ObtenerEmpleados().Single(e => e.IdEmpleado == id).IdUsuario.HasValue, "Editar empleado sin cuenta crea su usuario");
                Login(usuario, true);
            });

            foreach (bool repetirUsuario in new[] { true, false })
                Aislar(delegate
                {
                    Usuario uno, dos;
                    Empleado existente = Crear(out uno);
                    Empleado otro = Nuevo(out dos);
                    if (repetirUsuario) dos.NombreUsuario = uno.NombreUsuario;
                    else otro.Cedula = existente.Cedula;
                    Rechaza(() => bll.GuardarEmpleado(otro, dos, true), repetirUsuario ? "Rechaza usuario duplicado" : "Rechaza cedula duplicada y revierte cuenta parcial");
                });

            SesionActual.Iniciar(administrador);
            Rechaza(() => bll.GuardarEmpleado(null, null, false), "Valida datos ausentes sin error de referencia");
            Usuario datos;
            Empleado invalido = Nuevo(out datos);
            invalido.Genero = "X";
            Rechaza(() => bll.GuardarEmpleado(invalido, datos, true), "Valida genero");
            invalido.Genero = "M";
            invalido.Cargo = "";
            Rechaza(() => bll.GuardarEmpleado(invalido, datos, true), "Valida cargo obligatorio");
            invalido.Cargo = "Mesero";
            datos.IdRol = 3;
            Rechaza(() => bll.GuardarEmpleado(invalido, datos, true), "No asigna Cliente a un empleado");
            datos.IdRol = 2;
            datos.Contrasena = "";
            Rechaza(() => bll.GuardarEmpleado(invalido, datos, true), "Alta requiere contrasena");
            datos.Contrasena = "temporal";
            invalido.Cedula = new string('A', 31);
            Rechaza(() => bll.GuardarEmpleado(invalido, datos, true), "Valida limites antes de SQL");
            SesionActual.Cerrar();
            Rechaza(() => bll.DesactivarEmpleado(1), "No permite escribir sin sesion administrativa");

            Aislar(delegate
            {
                Empleado propio = bll.ObtenerEmpleados().FirstOrDefault(e => e.IdUsuario == administrador.IdUsuario);
                if (propio != null)
                    Rechaza(() => bll.DesactivarEmpleado(propio.IdEmpleado), "No permite desactivar al administrador conectado");
                else
                    Rechaza(() => bll.DesactivarEmpleado(int.MaxValue), "Rechaza seleccion inexistente");
            });
            Aislar(delegate
            {
                Usuario usuario;
                Empleado empleado = Nuevo(out usuario);
                bool denegado = false;
                try { new EmpleadoDAL().Guardar(empleado, usuario, 0, true); }
                catch (SqlException ex) { denegado = ex.Number == 50010; }
                Assert(denegado, "SQL tambien exige administrador valido");
            });

            Assert(countUsers == Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.usuario;")), "No quedan usuarios de prueba");
            Assert(countEmployees == Convert.ToInt32(Sql("SELECT COUNT(*) FROM dbo.empleado;")), "No quedan empleados de prueba");
            Assert(Equals(usersBefore, Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo.usuario;")), "Cuentas originales sin cambios");
            Assert(Equals(employeesBefore, Sql("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM dbo.empleado;")), "Empleados originales sin cambios");
            Console.WriteLine("TOTAL: " + checks + " comprobaciones correctas.");
            return 0;
        }
        catch (Exception ex)
        {
            // No imprimir mensajes SQL que puedan incluir valores introducidos.
            Console.Error.WriteLine("FALLO: " + ex.GetType().Name);
            if (!(ex is SqlException)) Console.Error.WriteLine(ex.Message);
            return 1;
        }
        finally { SesionActual.Cerrar(); }
    }
}
