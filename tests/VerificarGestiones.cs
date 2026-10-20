using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;

class VerificarGestiones
{
    static int cantidad;
    static string cs=ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString;
    static void Ok(bool valor,string texto){if(!valor)throw new Exception(texto);cantidad++;Console.WriteLine("OK: "+texto);}
    static void Rechaza(Action accion,string texto){bool rechazo=false;try{accion();}catch(ArgumentException){rechazo=true;}catch(SqlException){rechazo=true;}catch(UnauthorizedAccessException){rechazo=true;}Ok(rechazo,texto);}
    static object Sql(string sql){using(var c=new SqlConnection(cs))using(var cmd=new SqlCommand(sql,c)){c.Open();return cmd.ExecuteScalar();}}
    static int Int(string sql){return Convert.ToInt32(Sql(sql));}
    static void Sesion(int id,int rol){SesionActual.Iniciar(new Usuario{IdUsuario=id,IdRol=rol,NombreUsuario="prueba",NombreRol=rol==1?"Administrador":"Cliente"});}
    static Cliente Cliente(string nombre){return new Cliente{PrimerNombre=nombre,PrimerApellido="Prueba",Genero="O",Email=nombre+"@example.invalid",Cedula="ID-"+nombre,Telefono="88888888"};}
    static int CrearCuenta(string nombre)
    {
        var c=Cliente(nombre);new ClienteBLL().InsertarCliente(c,new Usuario{NombreUsuario=nombre,Contrasena="PruebaLocal-123"},"PruebaLocal-123");
        return Int("SELECT idUsuario FROM dbo.usuario WHERE nombreUsuario='"+nombre+"'");
    }
    static PedidoBorrador Carrito(int producto,int cantidad,decimal precio=10m){var p=new PedidoBorrador();p.Agregar(new Producto{IdProducto=producto,Nombre="Producto prueba",Precio=precio,Disponible=true},cantidad);return p;}
    static T Campo<T>(Form f,string n) where T:class{return f.GetType().GetField(n,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(f) as T;}
    static void Llamar(Form f,string metodo){f.GetType().GetMethod(metodo,BindingFlags.NonPublic|BindingFlags.Instance).Invoke(f,null);}
    [STAThread]static int Main()
    {
        try
        {
            Sesion(1,1);
            var clientes=new ClientesGestionBLL();var mesas=new MesasGestionBLL();var inventario=new InventarioBLL();var roles=new RolBLL();
            int persona=clientes.Guardar(Cliente("Mostrador"),true);
            Ok(clientes.Listar().Rows.Count==1,"Alta de cliente administrativo sin inventar una cuenta");
            var datos=Cliente("Mostrador");datos.Id=persona;datos.Telefono="77777777";clientes.Guardar(datos,true);
            Ok(clientes.Buscar(clientes.Listar(),"77777777").Rows.Count==1,"Edición y búsqueda LINQ de cliente");
            clientes.Guardar(datos,false);Ok(!Convert.ToBoolean(Sql("SELECT activo FROM dbo.cliente WHERE idCliente="+persona)),"Baja lógica de cliente");
            clientes.Guardar(datos,true);
            Rechaza(()=>clientes.Guardar(Cliente("Mostrador"),true),"Rechaza cédula o correo duplicado");
            var malo=Cliente("X");malo.Email="correo incorrecto";Rechaza(()=>clientes.Guardar(malo,true),"Valida correo antes de SQL");
            int mesa=mesas.Guardar(0,901,4,"Disponible",true);
            Ok(mesas.Buscar(mesas.Listar(),"901").Rows.Count==1,"Alta y filtro LINQ de mesa");
            mesas.Guardar(mesa,901,6,"Disponible",true);Ok(Int("SELECT capacidad FROM dbo.mesa WHERE idMesa="+mesa)==6,"Editar mesa");
            Rechaza(()=>mesas.Guardar(0,902,21,"Disponible",true),"Rechaza capacidad fuera de rango");
            mesas.Guardar(mesa,901,6,"Disponible",false);Ok(!Convert.ToBoolean(Sql("SELECT activa FROM dbo.mesa WHERE idMesa="+mesa)),"Baja lógica de mesa");
            mesas.Guardar(mesa,901,6,"Disponible",true);
            int rol=roles.Guardar(0,"Consulta adicional","Rol de catálogo sin nuevo acceso");
            roles.Guardar(rol,"Consulta adicional","Descripción editada");Ok(roles.BuscarRoles("editada").Count==1,"Alta edición y LINQ de roles");
            Rechaza(()=>roles.Eliminar(1),"Protege el rol Administrador");Rechaza(()=>roles.Guardar(2,"Otro",""),"Protege el nombre de los roles del sistema");
            roles.Eliminar(rol);Ok(Int("SELECT COUNT(*) FROM dbo.rol WHERE idRol="+rol)==0,"Elimina rol adicional sin usuarios");
            inventario.Guardar(1,10,3,true,null);var version=(byte[])inventario.Listar().Rows[0]["Version"];
            inventario.Guardar(1,12,3,true,version);Rechaza(()=>inventario.Guardar(1,100,3,true,version),"Evita sobrescribir una versión de stock desactualizada");
            version=(byte[])inventario.Listar().Rows[0]["Version"];inventario.Guardar(1,10,3,true,version);
            Rechaza(()=>inventario.Guardar(1,-1,0,true,null),"Rechaza inventario negativo");
            Rechaza(()=>inventario.Guardar(1,10,3,false,(byte[])inventario.Listar().Rows[0]["Version"]),"No retira control con existencias positivas");
            int usuario1=CrearCuenta("clienteUno"),usuario2=CrearCuenta("clienteDos");
            Sesion(usuario1,3);
            Ok(new ProductoBLL().ObtenerDisponiblesParaPedido().Count==2,"Catálogo disponible obtenido mediante procedimiento almacenado");
            var compras=new ComprasClienteBLL();var reservas=new ReservasClienteBLL();
            int pedido=compras.Guardar(Carrito(1,3),Guid.NewGuid());
            Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==7,"Guardar pedido descuenta stock");
            Ok(compras.Listar().Rows.Count==1 && compras.Detalle(pedido).Rows.Count==1,"Historial y detalle propios");
            Ok(Convert.ToDecimal(Sql("SELECT total FROM dbo.pedido WHERE idPedido="+pedido))==30m,"Importe guardado coincide con el detalle");
            Ok(Sql("SELECT idEmpleado FROM dbo.pedido WHERE idPedido="+pedido)==DBNull.Value,"Pedido de cliente no inventa un empleado");
            compras.Guardar(Carrito(1,5),Guid.NewGuid(),pedido);Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==5,"Editar pedido ajusta diferencias de stock");
            Rechaza(()=>compras.Guardar(Carrito(1,20),Guid.NewGuid(),pedido),"Rechaza edición sin existencias suficientes");
            Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==5 && Int("SELECT cantidad FROM dbo.detallePedido WHERE idPedido="+pedido)==5,"Rollback conserva stock y detalle anterior");
            Rechaza(()=>compras.Guardar(Carrito(1,1,11m),Guid.NewGuid()),"Rechaza un precio manipulado o desactualizado");
            Rechaza(()=>compras.Guardar(Carrito(3,1),Guid.NewGuid()),"Rechaza producto no disponible");
            int sinControl=compras.Guardar(Carrito(2,2),Guid.NewGuid());Ok(sinControl>0,"Permite productos sin control de existencias");
            var token=Guid.NewGuid();int unico=compras.Guardar(Carrito(1,1),token);int repetido=compras.Guardar(Carrito(1,1),token);
            Ok(unico==repetido && Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==4,"Solicitud repetida no duplica pedido ni descuento");
            Sesion(usuario2,3);Ok(compras.Listar().Rows.Count==0,"Otro cliente no ve compras ajenas");
            Rechaza(()=>compras.Detalle(pedido),"No consulta detalle de otro cliente");
            Rechaza(()=>compras.Cancelar(pedido),"No cancela el pedido de otro cliente");
            Rechaza(()=>compras.Guardar(Carrito(1,1),Guid.NewGuid(),pedido),"No edita el pedido de otro cliente");
            var propio=Cliente("NombreActualizado");propio.Id=persona;clientes.Guardar(propio,true,true);
            Ok(new ClienteBLL().ObtenerMiPerfil().PrimerNombre=="NombreActualizado" && Sql("SELECT PNombreCl FROM dbo.cliente WHERE idCliente="+persona).ToString()=="Mostrador","Mis datos ignora IDs ajenos y actualiza solo el perfil propio");
            Rechaza(()=>clientes.Listar(),"El cliente no accede a la gestión administrativa");
            Rechaza(()=>roles.Guardar(0,"Intruso",""),"El cliente no crea roles");
            Rechaza(()=>inventario.Guardar(1,200,0,true,null),"El cliente no modifica inventario");
            Sesion(usuario1,3);compras.Cancelar(pedido);
            Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==9,"Cancelar devuelve las existencias");
            Rechaza(()=>compras.Cancelar(pedido),"No devuelve existencias dos veces");
            Ok(compras.Buscar(compras.Listar(),"Cancelado").Rows.Count==1,"LINQ filtra el historial por estado");
            int reserva=reservas.Guardar(0,mesa,DateTime.Now.AddDays(2),4);Ok(reservas.Listar().Rows.Count==1,"Crear reserva propia");
            reservas.Guardar(reserva,mesa,DateTime.Now.AddDays(2).AddHours(3),5);Ok(Int("SELECT cantidadPersonas FROM dbo.reservacion WHERE idReservacion="+reserva)==5,"Editar reserva futura");
            Sesion(usuario2,3);Rechaza(()=>reservas.Guardar(0,mesa,DateTime.Now.AddDays(2).AddHours(3),2),"Bloquea reservas superpuestas en la misma mesa");
            Rechaza(()=>reservas.Cancelar(reserva),"No cancela reservas ajenas");
            Rechaza(()=>reservas.Guardar(0,mesa,DateTime.Now.AddDays(3),7),"Verifica la capacidad de la mesa");
            Sesion(1,1);Rechaza(()=>mesas.Guardar(mesa,901,6,"Disponible",false),"No desactiva una mesa con reservas futuras");
            Sesion(usuario1,3);reservas.Cancelar(reserva);Ok(reservas.Listar().Rows[0]["Estado"].ToString()=="Cancelada","Cancelar reserva conserva historial");
            Sesion(1,1);int idcliente=Int("SELECT idCliente FROM dbo.cliente WHERE idUsuario="+usuario1);
            var perfil=Cliente("clienteUno");perfil.Id=idcliente;
            Rechaza(()=>clientes.Guardar(perfil,false),"No desactiva un cliente con pedido pendiente");
            Sesion(usuario1,3);compras.Cancelar(unico);compras.Cancelar(sinControl);
            Sesion(1,1);clientes.Guardar(perfil,false);
            Sesion(usuario1,3);Rechaza(()=>compras.Guardar(Carrito(1,1),Guid.NewGuid()),"Sesión antigua de cliente inactivo no envía pedidos");
            SesionActual.Cerrar();Rechaza(()=>compras.Listar(),"Sin sesión no consulta compras");
            Sesion(usuario2,3);
            int ganador=0;
            Func<bool> intentar=()=>{try{int id=compras.Guardar(Carrito(1,7),Guid.NewGuid());System.Threading.Interlocked.Exchange(ref ganador,id);return true;}catch(ArgumentException){return false;}};
            var t1=System.Threading.Tasks.Task.Run(intentar);var t2=System.Threading.Tasks.Task.Run(intentar);System.Threading.Tasks.Task.WaitAll(t1,t2);
            Ok(t1.Result!=t2.Result && Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==3,"Dos pedidos concurrentes no venden más unidades que las existentes");
            compras.Cancelar(ganador);
            Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1")==10,"Cancelación tras concurrencia restituye el stock exacto");
            // Diseñadores y vistas fuera de pantalla, sin consultas SQL ni mensajes.
            Application.EnableVisualStyles();LicenseManager.CurrentContext=new DesigntimeLicenseContext();
            Sesion(1,1);
            using(var f=new FrmClientes())
            {
                f.ShowInTaskbar=false;f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-30000,-30000);f.Show();
                Llamar(f,"Recargar");Campo<Button>(f,"btnNuevo").PerformClick();
                Campo<TextBox>(f,"txtPnombre").Text="Boton";Campo<TextBox>(f,"txtPapellido").Text="Formulario";
                Campo<ComboBox>(f,"cmbGenero").SelectedItem="O";Campo<TextBox>(f,"txtEmail").Text="boton@example.invalid";
                Campo<Button>(f,"btnGuardar").PerformClick();
                Ok(Int("SELECT COUNT(*) FROM dbo.cliente WHERE PNombreCl='Boton'")==1,"Botón Guardar de Clientes ejecuta el alta");
                Campo<TextBox>(f,"txtTextoBuscar").Text="Boton";Campo<Button>(f,"btnBuscar").PerformClick();
                Ok(Campo<DataGridView>(f,"dgvClientes").Rows.Count==1,"Botón Buscar de Clientes aplica LINQ al listado");
            }
            using(var f=new FrmMesas())
            {
                f.ShowInTaskbar=false;f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-30000,-30000);f.Show();Llamar(f,"Recargar");
                Campo<TextBox>(f,"txtNumeroMesas").Text="999";Campo<TextBox>(f,"txtCapacidadMesas").Text="4";Campo<ComboBox>(f,"cmbEstadoMesas").SelectedItem="Disponible";
                Campo<Button>(f,"btnGuardarMesas").PerformClick();Ok(Int("SELECT COUNT(*) FROM dbo.mesa WHERE numero=999")==1,"Botón Guardar de Mesas ejecuta el alta");
            }
            using(var f=new FrmInventario())
            {
                f.ShowInTaskbar=false;f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-30000,-30000);f.Show();Llamar(f,"Recargar");
                Campo<ComboBox>(f,"cmbProductoInventario").SelectedValue=2;Llamar(f,"ElegirProducto");
                Campo<TextBox>(f,"txtStockActual").Text="8";Campo<TextBox>(f,"textBox1").Text="2";
                Campo<Button>(f,"btnGuardarProducto").PerformClick();Ok(Int("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=2")==8,"Botón Guardar de Inventario registra unidades del producto elegido");
            }
            foreach(var tipo in new[]{typeof(FrmClientes),typeof(FrmMesas),typeof(FrmInventario),typeof(FrmRoles),typeof(FrmPedidoCliente),typeof(FrmMisCompras),typeof(FrmMisDatos),typeof(FrmMisReservas)})
            {
                using(var ds=new DesignSurface(tipo)){Ok(ds.LoadErrors.Count==0,"Diseñador "+tipo.Name);}
                using(var f=(Form)Activator.CreateInstance(tipo))
                {
                    f.StartPosition=FormStartPosition.Manual;f.Location=new Point(-30000,-30000);f.ShowInTaskbar=false;f.Show();Application.DoEvents();
                    using(var b=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(b,new Rectangle(0,0,f.Width,f.Height));b.Save(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,tipo.Name+".png"));}
                }
            }
            Console.WriteLine("TOTAL: "+cantidad+" comprobaciones correctas en base temporal.");return 0;
        }
        catch(Exception e){Console.WriteLine(e);return 1;}
    }
}
