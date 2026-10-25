using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UIEmpleado;
using SistemaPedidoRestaurante.UICliente;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.BLL;
using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.UIPrincipal;
class VerificarOperacion
{
 static int pruebas;
 static string cs=ConfigurationManager.ConnectionStrings["RestauranteDB"].ConnectionString;
 static void Ok(bool valido,string mensaje){if(!valido)throw new Exception(mensaje);pruebas++;Console.WriteLine("OK: "+mensaje);}
 static void Rechazar(Action accion,string mensaje){try{accion();}catch(ArgumentException){Ok(true,mensaje);return;}catch(UnauthorizedAccessException){Ok(true,mensaje);return;}throw new Exception("No rechazo: "+mensaje);}
 static object Sql(string texto){using(var c=new SqlConnection(cs))using(var cmd=new SqlCommand(texto,c)){c.Open();return cmd.ExecuteScalar();}}
 static int Entero(string texto){return Convert.ToInt32(Sql(texto));}
 static void Sesion(int id,int rol){SesionActual.Iniciar(new Usuario{IdUsuario=id,NombreUsuario="prueba",IdRol=rol});}
 static PedidoBorrador Pedido(int producto,int cantidad,decimal precio){var p=new PedidoBorrador();p.Agregar(new Producto{IdProducto=producto,Nombre="Producto prueba",Disponible=true,Precio=precio},cantidad);return p;}
 static T Campo<T>(object objeto,string nombre){return (T)objeto.GetType().GetField(nombre,BindingFlags.NonPublic|BindingFlags.Instance).GetValue(objeto);}
 static void Capturar(Form formulario,string nombre,string carpeta)
 {
  formulario.ShowInTaskbar=false;formulario.StartPosition=FormStartPosition.Manual;formulario.Location=new Point(-20000,-20000);formulario.Show();Application.DoEvents();
  using(var imagen=new Bitmap(formulario.Width,formulario.Height)){formulario.DrawToBitmap(imagen,new Rectangle(0,0,formulario.Width,formulario.Height));imagen.Save(Path.Combine(carpeta,nombre+".png"));}
  formulario.WindowState=FormWindowState.Maximized;Application.DoEvents();
  using(var imagen=new Bitmap(formulario.Width,formulario.Height)){formulario.DrawToBitmap(imagen,new Rectangle(0,0,formulario.Width,formulario.Height));imagen.Save(Path.Combine(carpeta,nombre+"-max.png"));}
 }
 [STAThread]static int Main(string[] args)
 {
  try {
   Sesion(1,1);var op=new OperacionRestauranteBLL();Ok(Convert.ToDecimal(op.VerificarInstalacion().Rows[0]["TasaImpuesto"])==0,"Configuracion inicial no altera los precios finales");
   var categorias=new CategoriaBLL();categorias.GuardarCategoria(new Categoria{Nombre="Carta prueba"});int categoria=categorias.BuscarCategorias("Carta prueba").Single().IdCategoria;
   var productos=new ProductoBLL();productos.GuardarProducto(new Producto{IdCategoria=categoria,Nombre="Almuerzo prueba",Codigo="OP-01",Precio=115m,Disponible=true});int producto=productos.BuscarProductos("OP-01").Single().IdProducto;
   productos.GuardarProducto(new Producto{IdCategoria=categoria,Nombre="Sin inventario",Codigo="OP-02",Precio=75m,Disponible=true});int sinStock=productos.BuscarProductos("OP-02").Single().IdProducto;
   Ok(productos.ObtenerDisponiblesParaPedido().Count==0,"Catalogo oculta productos sin inventario");
   var inv=new InventarioBLL();inv.Guardar(producto,10,2,true,0);
   Ok(op.ListarMovimientos().Rows.Count==1 && op.ListarMovimientos().Rows[0]["Usuario"].ToString()=="qa.admin","Alta de inventario auditada con usuario");
   var cliente=new Cliente{PrimerNombre="Lucia",PrimerApellido="Prueba",Cedula="001-010100-9001A",Telefono="88889001",Genero="F",Email="lucia@example.invalid"};
   new ClienteBLL().InsertarCliente(cliente,new Usuario{NombreUsuario="qa.lucia",Contrasena="ClaveQA-123"},"ClaveQA-123");
   int usuario=Entero("SELECT idUsuario FROM dbo.usuario WHERE nombreUsuario='qa.lucia'"),idCliente=Entero("SELECT idCliente FROM dbo.cliente WHERE idUsuario="+usuario);
   var empleado=new Empleado{PrimerNombre="Alba",PrimerApellido="Prueba",Genero="F",Cedula="001-010100-9002A",Telefono="88889002",Cargo="Mesera",Activo=true};
   new EmpleadoBLL().GuardarEmpleado(empleado,new Usuario{NombreUsuario="qa.alba",Contrasena="ClaveQA-123",IdRol=2},true);
   int personal=Entero("SELECT idUsuario FROM dbo.usuario WHERE nombreUsuario='qa.alba'");
   int mesaPedido=new MesasGestionBLL().Guardar(0,201,4,"Disponible",true);
   int mesaAjena=new MesasGestionBLL().Guardar(0,202,4,"Ocupada",true);
   Sesion(usuario,3);var compras=new ComprasClienteBLL();Rechazar(()=>compras.Guardar(Pedido(sinStock,1,75m),Guid.NewGuid()),"Servidor rechaza producto sin inventario incluso manipulando el cliente");
   Ok(productos.ObtenerDisponiblesParaPedido().Single().IdProducto==producto,"Cliente ve solo catalogo con unidades disponibles");
   var incompleto=Pedido(producto,1,115m);incompleto.Agregar(new Producto{IdProducto=sinStock,Nombre="Sin inventario",Precio=75m,Disponible=true},1);
   Rechazar(()=>compras.Guardar(incompleto,Guid.NewGuid()),"Error en segunda linea revierte un pedido completo");
   Ok(Entero("SELECT COUNT(*) FROM dbo.pedido")==0 && Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==10 && Entero("SELECT COUNT(*) FROM dbo.movimientoInventario")==1,"Rollback no deja cabecera, descuento ni movimiento parcial");
   Guid solicitud=Guid.NewGuid();int pedido=compras.Guardar(Pedido(producto,3,115m),solicitud);
   Ok(compras.Guardar(Pedido(producto,3,115m),solicitud)==pedido,"Solicitud repetida de pedido no duplica");
   Ok(Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==7,"Pedido descuenta unidades una sola vez");
   Rechazar(()=>compras.Guardar(Pedido(producto,4,115m),solicitud),"Una misma solicitud con otras cantidades se rechaza");
   Rechazar(()=>compras.Guardar(incompleto,Guid.NewGuid(),pedido),"Editar con una linea invalida revierte devolucion y reemplazo");
   Ok(Entero("SELECT cantidad FROM dbo.detallePedido WHERE idPedido="+pedido)==3 && Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==7,"La edicion fallida conserva detalle y existencias anteriores");
   Guid edicion=Guid.NewGuid();compras.Guardar(Pedido(producto,2,115m),edicion,pedido);
   Ok(Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==8,"Edicion devuelve unidades y aplica el nuevo detalle");
   int movimientosEdicion=Entero("SELECT COUNT(*) FROM dbo.movimientoInventario");compras.Guardar(Pedido(producto,2,115m),edicion,pedido);
   Ok(Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==8 && Entero("SELECT COUNT(*) FROM dbo.movimientoInventario")==movimientosEdicion,"Reintento de edicion no modifica nuevamente el inventario");
   compras.Guardar(Pedido(producto,3,115m),Guid.NewGuid(),pedido);
   var comprobante=op.ObtenerDocumento(pedido,false);Ok(comprobante.Total==345m && comprobante.Detalle.Count==1 && !comprobante.EsFactura,"Comprobante de pedido con detalle real");
   DocumentoPdf.Guardar(comprobante,Path.Combine(args[0],"pedido.pdf"));Ok(new FileInfo(Path.Combine(args[0],"pedido.pdf")).Length>1000,"QuestPDF genera comprobante en .NET Framework 4.8");
   Rechazar(()=>op.ObtenerDocumento(pedido,true),"No se genera factura antes del cobro");
   Rechazar(()=>op.AvanzarPedido(pedido,"Pendiente"),"Cliente no cambia estados operativos");
   Sesion(personal,2);
   using(var bandeja=new FrmBandejaOperaciones("Pedidos")){Capturar(bandeja,"bandeja",args[0]);Ok(Campo<DataGridView>(bandeja,"listado").Rows.Count==1,"Bandeja carga el pedido real");bandeja.Close();}
   using(var local=new FrmPedidoCliente(0,idCliente)){Capturar(local,"pedido-local",args[0]);Ok(Campo<TextBox>(local,"txtCliente").Text.Length>0 && Campo<Button>(local,"btnAceptar").Enabled,"Personal usa el carrito completo para un cliente del local");local.DialogResult=DialogResult.OK;local.Close();}
   using(var elegirMesa=new FrmElegirMesa(pedido)){Capturar(elegirMesa,"asignar-mesa",args[0]);Ok(Campo<ComboBox>(elegirMesa,"mesas").Items.Count==2,"Selector carga las mesas activas");elegirMesa.Close();}
   Rechazar(()=>op.AsignarMesa(pedido,mesaAjena),"No asigna una mesa ocupada por otra atencion");
   op.AsignarMesa(pedido,mesaPedido);Ok(Entero("SELECT idMesa FROM dbo.pedido WHERE idPedido="+pedido)==mesaPedido && Sql("SELECT estado FROM dbo.mesa WHERE idMesa="+mesaPedido).ToString()=="Ocupada","Asignar mesa vincula pedido y ocupa la mesa");
   Rechazar(()=>op.CambiarMesa(mesaPedido,"Ocupada"),"No libera una mesa con pedidos sin pagar");
   Sesion(personal,2);Ok(op.ListarPedidos().Rows.Count==1,"Personal consulta pedidos recibidos");op.AvanzarPedido(pedido,"Pendiente");
   Ok(Sql("SELECT estado FROM dbo.pedido WHERE idPedido="+pedido).ToString()=="En preparación","Pedido pasa a preparacion");
   Rechazar(()=>op.AvanzarPedido(pedido,"Pendiente"),"Estado anterior desactualizado no sobrescribe cambios");
   Rechazar(()=>op.Cotizar(pedido),"No cobra antes de entregar");
   Sesion(usuario,3);Rechazar(()=>compras.Cancelar(pedido),"Cliente no cancela pedido en preparacion");Rechazar(()=>compras.Guardar(Pedido(producto,1,115m),Guid.NewGuid(),pedido),"Cliente no edita pedido en preparacion");
   Sesion(personal,2);op.AvanzarPedido(pedido,"En preparación");op.AvanzarPedido(pedido,"Listo");Ok(Convert.ToDecimal(op.Cotizar(pedido)["Total"])==345m,"Importe final listo para cobro");
   Rechazar(()=>op.Cobrar(pedido,"Efectivo",300,345,Guid.NewGuid(),null),"No cobra efectivo insuficiente");Rechazar(()=>op.Cobrar(pedido,"Tarjeta",400,345,Guid.NewGuid(),null),"Tarjeta no admite cambio");
   using(var cobro=new FrmPago(pedido)){Capturar(cobro,"cobro",args[0]);Ok(Campo<TextBox>(cobro,"txtMontoTotal").ReadOnly && Campo<TextBox>(cobro,"txtMontoTotal").Text.Contains("345"),"Cobro muestra importe del servidor sin permitir editarlo");cobro.Close();}
   using(var confirmar=new FrmConfirmarCobro(345,"Efectivo")){Capturar(confirmar,"confirmar-cobro",args[0]);Campo<NumericUpDown>(confirmar,"monto").Value=400;Ok(confirmar.Recibido==400 && Campo<Label>(confirmar,"cambio").Text.Contains("55"),"Confirmacion calcula cambio al escribir efectivo");confirmar.Close();}
   Sql("CREATE OR ALTER TRIGGER dbo.tr_Pago_FalloPrueba ON dbo.pago AFTER INSERT AS BEGIN THROW 50999,'Fallo de prueba despues de emitir factura',1; END");
   Rechazar(()=>op.Cobrar(pedido,"Efectivo",400,345,Guid.NewGuid(),null),"Fallo al guardar pago revierte la operacion completa");
   Ok(Entero("SELECT COUNT(*) FROM dbo.factura WHERE idPedido="+pedido)==0 && Sql("SELECT estado FROM dbo.pedido WHERE idPedido="+pedido).ToString()=="Entregado","Rollback no deja factura huerfana ni pedido marcado como pagado");
   Sql("DROP TRIGGER dbo.tr_Pago_FalloPrueba");
   Sql("UPDATE dbo.pedido SET subtotal=346,total=346 WHERE idPedido="+pedido);
   Rechazar(()=>op.Cobrar(pedido,"Efectivo",400,346,Guid.NewGuid(),null),"No cobra importes de pedido inconsistentes con sus detalles");
   Sql("UPDATE dbo.pedido SET subtotal=345,total=345 WHERE idPedido="+pedido);
   Guid pago=Guid.NewGuid();var resultado=op.Cobrar(pedido,"Efectivo",400,345,pago,"Pago de prueba");int factura=Convert.ToInt32(resultado["IdFactura"]);
   Ok(Convert.ToDecimal(resultado["Cambio"])==55m,"Pago calcula cambio correcto");
   Ok(Sql("SELECT estado FROM dbo.pedido WHERE idPedido="+pedido).ToString()=="Pagado","Pedido cierra como pagado");
   Ok(Sql("SELECT estado FROM dbo.mesa WHERE idMesa="+mesaPedido).ToString()=="En limpieza","Cobro deja la mesa cerrada en limpieza");
   op.CambiarMesa(mesaPedido,"En limpieza");Ok(Sql("SELECT estado FROM dbo.mesa WHERE idMesa="+mesaPedido).ToString()=="Disponible","Personal confirma limpieza y libera mesa");
   Rechazar(()=>op.CambiarMesa(mesaPedido,"En limpieza"),"Estado viejo de mesa no sobrescribe cambios");
   Ok(Entero("SELECT COUNT(*) FROM dbo.factura WHERE idPedido="+pedido)==1 && Entero("SELECT COUNT(*) FROM dbo.pago WHERE idFactura="+factura)==1,"Factura y pago unicos se guardan juntos");
   Ok(Convert.ToInt32(op.Cobrar(pedido,"Efectivo",400,345,pago,"Pago de prueba")["IdFactura"])==factura,"Reintento de cobro devuelve la misma factura");
   Rechazar(()=>op.Cobrar(pedido,"Efectivo",400,345,Guid.NewGuid(),null),"No cobra el pedido dos veces");
   Rechazar(()=>op.Cobrar(pedido,"Tarjeta",345,345,pago,null),"Reutilizar solicitud con otros datos de cobro es rechazado");
   using(var vista=new FrmFacturas(pedido)){Capturar(vista,"factura-vista",args[0]);Ok(Campo<TextBox>(vista,"txtTotalFactura").Text.Contains("345") && Campo<DataGridView>(vista,"dataGridView1").Rows.Count==1,"Formulario de factura carga instantanea pagada y detalle");vista.Close();}
   var original=op.ObtenerDocumento(pedido,true);Ok(original.Total==345m && original.Impuestos==0 && original.Recibido==400,"Factura conserva importes del cobro");
   Ok(original.Mesa=="201" && original.TipoPedido=="Local","Factura conserva mesa y tipo del pedido");
   Sql("UPDATE dbo.mesa SET numero=299 WHERE idMesa="+mesaPedido);Ok(op.ObtenerDocumento(pedido,true).Mesa=="201","Cambiar el numero de mesa no altera una factura anterior");
   Sql("UPDATE dbo.producto SET nombre='Nombre cambiado',precio=999 WHERE idProducto="+producto+";UPDATE dbo.cliente SET pNombreCl='Nombre nuevo' WHERE idCliente="+idCliente+";UPDATE dbo.configuracionRestaurante SET nombre='Otro nombre',tasaImpuesto=15 WHERE id=1;");
   var copia=op.ObtenerDocumento(pedido,true);Ok(copia.Restaurante==original.Restaurante && copia.Cliente==original.Cliente && copia.Detalle[0].Producto==original.Detalle[0].Producto && copia.Detalle[0].PrecioUnitario==115m && copia.Total==345m,"Factura inmutable aunque cambien cliente producto e impuesto");
   DocumentoPdf.Guardar(copia,Path.Combine(args[0],"factura.pdf"));Ok(new FileInfo(Path.Combine(args[0],"factura.pdf")).Length>1000,"QuestPDF genera factura pagada");
   Sesion(usuario,3);Ok(op.ObtenerDocumento(pedido,true).Numero==original.Numero,"Cliente obtiene su propia factura");
   Sesion(1,1);var c2=new Cliente{PrimerNombre="Diego",PrimerApellido="Prueba",Genero="M",Cedula="001-010100-9003A",Telefono="88889003",Email="diego@example.invalid"};new ClienteBLL().InsertarCliente(c2,new Usuario{NombreUsuario="qa.diego",Contrasena="ClaveQA-123"},"ClaveQA-123");int otro=Entero("SELECT idUsuario FROM dbo.usuario WHERE nombreUsuario='qa.diego'");Sesion(otro,3);Rechazar(()=>op.ObtenerDocumento(pedido,true),"Otro cliente no consulta factura ajena");
   Sesion(personal,2);Rechazar(()=>op.ListarMovimientos(),"Empleado no consulta movimientos administrativos");
   int segundo=op.GuardarPedido(idCliente,Pedido(producto,1,999m),Guid.NewGuid());op.AvanzarPedido(segundo,"Pendiente");op.AvanzarPedido(segundo,"En preparación");op.AvanzarPedido(segundo,"Listo");op.Cobrar(segundo,"Tarjeta",999,999,Guid.NewGuid(),null);
   var conImpuesto=op.ObtenerDocumento(segundo,true);Ok(conImpuesto.Subtotal+conImpuesto.Impuestos==999 && conImpuesto.Total==999 && conImpuesto.Impuestos==130.30m,"15 por ciento incluido se desglosa sin aumentar total");
   Sesion(1,1);var stock=inv.Listar().Single();op.AjustarInventario(stock,4,"Entrada","Reposicion de prueba");Ok(inv.Listar().Single().StockActual==10,"Entrada suma unidades");Rechazar(()=>op.AjustarInventario(stock,1,"Salida","Version vieja"),"Ajuste desactualizado no sobrescribe inventario");stock=inv.Listar().Single();Rechazar(()=>op.AjustarInventario(stock,11,"Salida","Perdida"),"Salida no permite stock negativo");op.AjustarInventario(stock,2,"Salida","Merma de prueba");Ok(inv.Listar().Single().StockActual==8,"Salida resta unidades");
   Ok(op.ListarMovimientos().AsEnumerable().Any(r=>r["Motivo"].ToString()=="Merma de prueba" && Convert.ToInt32(r["Diferencia"])==-2),"Historial conserva motivo y diferencia");
   int mesa=new MesasGestionBLL().Guardar(0,101,4,"Disponible",true);Sesion(usuario,3);var reservas=new ReservasClienteBLL();int reserva=reservas.Guardar(0,mesa,DateTime.Now.AddDays(1),2);Sesion(personal,2);Ok(op.ListarReservas().Rows.Count==1,"Personal consulta reservas");Rechazar(()=>op.CambiarReserva(reserva,"Confirmada","Atendida"),"No recibe reserva fuera de horario");
   Sql("UPDATE dbo.reservacion SET fechaHoraReserva=GETDATE(),estado='Confirmada' WHERE idReservacion="+reserva);op.CambiarReserva(reserva,"Confirmada","Atendida");Ok(Sql("SELECT estado FROM dbo.mesa WHERE idMesa="+mesa).ToString()=="Ocupada","Atender reserva ocupa la mesa");Rechazar(()=>op.CambiarReserva(reserva,"Confirmada","Cancelada"),"No cambia reserva ya atendida");
   Sesion(1,1);new MesasGestionBLL().Guardar(mesa,101,4,"Disponible",true);Sesion(usuario,3);int ausente=reservas.Guardar(0,mesa,DateTime.Now.AddDays(2),2);Sql("UPDATE dbo.reservacion SET fechaHoraReserva=DATEADD(hour,-1,GETDATE()),estado='Confirmada' WHERE idReservacion="+ausente);Sesion(personal,2);op.CambiarReserva(ausente,"Confirmada","No asistio");Ok(Sql("SELECT estado FROM dbo.reservacion WHERE idReservacion="+ausente).ToString()=="No asistio","Registrar ausencia conserva historial");
   Sql("UPDATE dbo.empleado SET activo=0 WHERE idUsuario="+personal);Rechazar(()=>op.ListarPedidos(),"Sesion antigua de empleado inactivo es rechazada por SQL");Sql("UPDATE dbo.empleado SET activo=1 WHERE idUsuario="+personal);
   Sesion(1,1);stock=inv.Listar().Single();op.AjustarInventario(stock,stock.StockActual-2,"Salida","Preparar prueba de concurrencia");Sesion(usuario,3);int ganador=0;Func<bool> enviar=()=>{try{int n=compras.Guardar(Pedido(producto,2,999),Guid.NewGuid());System.Threading.Interlocked.Exchange(ref ganador,n);return true;}catch(ArgumentException){return false;}};var t1=Task.Run(enviar);var t2=Task.Run(enviar);Task.WaitAll(t1,t2);Ok(t1.Result!=t2.Result && Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==0,"Pedidos simultaneos no sobrevendan unidades");compras.Cancelar(ganador);Ok(Entero("SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto="+producto)==2,"Cancelar devuelve existencias una vez");Rechazar(()=>compras.Cancelar(ganador),"Cancelacion repetida no devuelve mas unidades");
   Sesion(personal,2);int concurrente=op.GuardarPedido(idCliente,Pedido(producto,1,999),Guid.NewGuid());op.AvanzarPedido(concurrente,"Pendiente");op.AvanzarPedido(concurrente,"En preparación");op.AvanzarPedido(concurrente,"Listo");Func<bool> cobrar=()=>{try{op.Cobrar(concurrente,"Transferencia",999,999,Guid.NewGuid(),null);return true;}catch(ArgumentException){return false;}};var p1=Task.Run(cobrar);var p2=Task.Run(cobrar);Task.WaitAll(p1,p2);Ok(p1.Result!=p2.Result && Entero("SELECT COUNT(*) FROM dbo.factura WHERE idPedido="+concurrente)==1,"Dos cajeros no cobran el mismo pedido simultaneamente");
   Sesion(1,1);
   using(var movimientos=new FrmBandejaOperaciones("Movimientos")){Capturar(movimientos,"movimientos",args[0]);Ok(Campo<DataGridView>(movimientos,"listado").Rows.Count>0,"Pantalla de movimientos muestra auditoria");movimientos.Close();}
   using(var configuracion=new FrmConfiguracionRestaurante()){Capturar(configuracion,"configuracion",args[0]);Ok(Campo<NumericUpDown>(configuracion,"tasa").Value==15,"Configuracion muestra tasa incluida guardada");configuracion.Close();}
   using(var ajuste=new FrmAjusteInventario()){Capturar(ajuste,"ajuste",args[0]);Ok(Campo<ComboBox>(ajuste,"productos").Items.Count==1,"Pantalla de ajustes ofrece solo inventario activo");ajuste.Close();}
   using(var reservaVista=new FrmBandejaOperaciones("Reservas")){Capturar(reservaVista,"reservas",args[0]);Ok(Campo<DataGridView>(reservaVista,"listado").Rows.Count==2,"Bandeja de reservas consulta estados finales");reservaVista.Close();}
   using(var mesasVista=new FrmBandejaOperaciones("Mesas")){Capturar(mesasVista,"mesas",args[0]);Ok(Campo<DataGridView>(mesasVista,"listado").Rows.Count==3,"Bandeja de mesas permite consultar la limpieza");mesasVista.Close();}
   op.CambiarMesa(mesaAjena,"Ocupada");Ok(Sql("SELECT estado FROM dbo.mesa WHERE idMesa="+mesaAjena).ToString()=="En limpieza","Mesa sin pedidos abiertos pasa a limpieza");op.CambiarMesa(mesaAjena,"En limpieza");
   op.GuardarConfiguracion("Restaurante de prueba",0);Ok(Convert.ToDecimal(op.VerificarInstalacion().Rows[0]["TasaImpuesto"])==0,"Administrador puede configurar tasa sin SQL manual");
   Sesion(personal,2);Rechazar(()=>op.GuardarConfiguracion("Intruso",20),"Empleado no cambia configuracion comercial");
   var extenso=new DocumentoVenta{EsFactura=true,Restaurante="Restaurante de prueba",Moneda="C$",Numero="F-PRUEBA-EXTENSA",Fecha=DateTime.Now,Cliente=new string('A',300),Cedula="001-010100-0001A",Estado="Pagada",Metodo="Tarjeta",Empleado="Cajero de prueba"};
   for(int i=1;i<=150;i++)extenso.Detalle.Add(new DetalleDocumento{Producto="Producto de prueba numero "+i+" con descripcion extensa para verificar saltos de pagina",Cantidad=1,PrecioUnitario=10,Subtotal=10});
   extenso.Subtotal=extenso.Total=extenso.Recibido=1500;DocumentoPdf.Guardar(extenso,Path.Combine(args[0],"factura-extensa.pdf"));Ok(new FileInfo(Path.Combine(args[0],"factura-extensa.pdf")).Length>1000,"QuestPDF genera factura extensa sin error de distribucion");
   Console.WriteLine("TOTAL: "+pruebas+" comprobaciones correctas.");return 0;
  }catch(Exception error){Console.Error.WriteLine(error);return 1;}
 }
}
