param([string]$EsquemaOriginal='C:\Users\Josue\Documents\RestaurantSM.sql')
$ErrorActionPreference='Stop'
$repo=Split-Path $PSScriptRoot -Parent
$cfg=[xml](Get-Content -LiteralPath (Join-Path $repo 'SistemaPedidoRestaurante\App.config'))
$cs=($cfg.configuration.connectionStrings.add|Where-Object name -eq 'RestauranteDB').connectionString
$builder=New-Object System.Data.SqlClient.SqlConnectionStringBuilder($cs)
$builder['Initial Catalog']='master'
$cn=New-Object System.Data.SqlClient.SqlConnection($builder.ConnectionString)
$db='RestaurantSM_Actualizacion_'+[guid]::NewGuid().ToString('N')
$checks=0
function Batches([string]$sql) {
 foreach($batch in [regex]::Split($sql,'(?im)^\s*GO\s*\r?$')) {
  if([string]::IsNullOrWhiteSpace($batch)){continue}
  $cmd=$cn.CreateCommand();$cmd.CommandText=$batch;$cmd.CommandTimeout=90
  try{[void]$cmd.ExecuteNonQuery()}finally{$cmd.Dispose()}
 }
}
function Scalar([string]$sql) {
 $cmd=$cn.CreateCommand();$cmd.CommandText=$sql
 try{return $cmd.ExecuteScalar()}finally{$cmd.Dispose()}
}
function Check([bool]$ok,[string]$mensaje) {
 if(!$ok){throw $mensaje};$script:checks++
}
try {
 $cn.Open(); Batches "CREATE DATABASE [$db];"; $cn.ChangeDatabase($db)
 $s=[IO.File]::ReadAllText($EsquemaOriginal)
 $a=$s.IndexOf('/****** Objeto: Table [dbo].[categoria]');$b=$s.IndexOf('/****** Objeto: StoredProcedure')
 if($a -lt 0 -or $b -le $a){throw 'No se reconoce el esquema original.'}
 Batches $s.Substring($a,$b-$a)
 Batches @'
SET IDENTITY_INSERT dbo.rol ON;
INSERT dbo.rol(idRol,nombre) VALUES(1,'Administrador'),(2,'Empleado'),(3,'Cliente');
SET IDENTITY_INSERT dbo.rol OFF;
INSERT dbo.usuario(nombreUsuario,contrasena,idRol) VALUES('qa_admin','SoloPruebas',1),('qa_cliente','SoloPruebas',3);
INSERT dbo.cliente(idUsuario,PNombreCl,PApellidoCl,genero,cedula) VALUES(2,'Cliente','Conservado','M','QA-CLIENTE');
INSERT dbo.empleado(idUsuario,pNombreEmp,pApellidoEmp,cargo,genero,cedula,telefono,activo) VALUES(1,'Admin','Conservado','Administrador','M','QA-EMP','00000000',1);
INSERT dbo.categoria(nombre,descripcion) VALUES('Categoria personal','No sobrescribir');
INSERT dbo.producto(idCategoria,nombre,precio,codigo,disponible) VALUES(1,'Producto personal',17.25,'QA-PERSONAL',1);
INSERT dbo.mesa(numero,capacidad,estado) VALUES(90,4,'Disponible');
INSERT dbo.pedido(idCliente,idEmpleado,idMesa,fechaHora,estado,costoEnvio,subtotal,total,esDelivery) VALUES(1,1,1,GETDATE(),'Pendiente',0,17.25,17.25,0);
INSERT dbo.detallePedido(idPedido,idProducto,cantidad,precioUnitario,subtotal,total) VALUES(1,1,1,17.25,17.25,17.25);
INSERT dbo.categoria(nombre) VALUES('Entradas'),('Platos Fuertes'),('Bebidas'),('Postres'),('Sopas y Ensaladas'),('Comida Rapida');
INSERT dbo.producto(idCategoria,nombre,precio,codigo,disponible) VALUES
 (2,'Dedos de Queso',123.45,'PROD-001',1),(3,'Lomito a la Plancha',320.50,'PROD-002',1),
 (4,'Jugo Natural de Naranja',45,'PROD-003',1),(5,'Tres Leches',65,'PROD-004',1),
 (6,'Sopa de Res',150,'PROD-005',1),(7,'Hamburguesa con Tocino',210,'PROD-006',1);
'@
 $snap=@{}
 $columns=@{
 usuario='idUsuario,nombreUsuario,contrasena,idRol';cliente='idCliente,idUsuario,PNombreCl,PApellidoCl,genero,cedula';
 empleado='idEmpleado,idUsuario,pNombreEmp,pApellidoEmp,cargo,genero,cedula,telefono,activo';
 mesa='idMesa,numero,capacidad,estado';pedido='idPedido,idCliente,idEmpleado,idMesa,fechaHora,estado,costoEnvio,subtotal,total,esDelivery';
 detallePedido='idDetalle,idPedido,idProducto,cantidad,precioUnitario,subtotal,total'
 }
 foreach($t in $columns.Keys){$snap[$t]=Scalar "SELECT CHECKSUM_AGG(BINARY_CHECKSUM($($columns[$t]))) FROM dbo.[$t]"}
 $upgrade=[IO.File]::ReadAllText((Join-Path $repo 'sql\ACTUALIZAR_TODO_RESTAURANTSM.sql'))
 $upgrade=[regex]::Replace($upgrade,'(?im)^:On Error exit\s*\r?\n','')
 $upgrade=$upgrade.Replace('[RestaurantSM]',"[$db]").Replace("N'RestaurantSM'","N'$db'")
 for($run=1;$run -le 2;$run++) {
  Batches $upgrade
  Check ((Scalar "SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped=0") -eq 35) 'Deben instalarse los 35 procedimientos.'
  Check ((Scalar 'SELECT COUNT(*) FROM dbo.categoria') -eq 8) 'Catálogo de categorías incompleto o duplicado.'
  Check ((Scalar 'SELECT COUNT(*) FROM dbo.subcategoria') -eq 12) 'Subcategorías incompletas o duplicadas.'
  Check ((Scalar 'SELECT COUNT(*) FROM dbo.producto') -eq 25) 'Productos incompletos o duplicados.'
  Check ((Scalar "SELECT precio FROM dbo.producto WHERE codigo='QA-PERSONAL'") -eq 17.25) 'El producto personal debe conservarse.'
  Check ((Scalar "SELECT precio FROM dbo.producto WHERE codigo='PROD-001'") -eq 123.45) 'El catálogo no debe sobrescribir un precio anterior.'
  foreach($t in $columns.Keys){Check ((Scalar "SELECT CHECKSUM_AGG(BINARY_CHECKSUM($($columns[$t]))) FROM dbo.[$t]") -eq $snap[$t]) "Se alteraron datos previos de $t."}
  Check ((Scalar "SELECT COUNT(*) FROM sys.columns WHERE object_id=OBJECT_ID('dbo.pedido') AND name='idEmpleado' AND is_nullable=1") -eq 1) 'Falta permitir pedidos de cliente sin empleado.'
  Check ((Scalar "SELECT COUNT(*) FROM dbo.cliente WHERE activo=1") -eq 1) 'El cliente previo debe permanecer activo.'
  Check ((Scalar "SELECT COUNT(*) FROM dbo.mesa WHERE activa=1") -eq 1) 'La mesa previa debe permanecer activa.'
  Check ((Scalar "SELECT COUNT(*) FROM dbo.detallePedido WHERE controlInventario=0") -eq 1) 'El pedido previo no debe descontar inventario retroactivamente.'
  Write-Output "Actualización ${run}: correcta, datos previos conservados."
 }
 Check ((Scalar "EXEC dbo.sp_Usuario_ValidarLogin @NombreUsuario='qa_cliente',@Contrasena='SoloPruebas'") -eq 2) 'El login debe funcionar con la cuenta anterior.'
 # Invocar las consultas de las pantallas contra la base actualizada.
 Batches 'EXEC dbo.sp_Categoria_Listar; EXEC dbo.sp_Subcategoria_Listar; EXEC dbo.sp_Producto_Listar; EXEC dbo.sp_Producto_Disponibles; EXEC dbo.sp_Empleado_Listar; EXEC dbo.sp_Rol_Listar; EXEC dbo.sp_Gestion_Clientes @Actor=1; EXEC dbo.sp_Mesa_Listar; EXEC dbo.sp_Inventario_Listar @Actor=1; EXEC dbo.sp_Cliente_ObtenerMiPerfil @IdUsuario=2; EXEC dbo.sp_Cliente_Pedidos @Actor=2; EXEC dbo.sp_Cliente_Detalle @Actor=2,@IdPedido=1; EXEC dbo.sp_Cliente_Reservas @Actor=2; EXEC dbo.sp_Pedido_ListarAbiertos;'
 $checks+=14
 Batches "EXEC dbo.sp_InsertarCliente @PrimerNombre='Nuevo',@SegundoNombre=NULL,@PrimerApellido='Prueba',@SegundoApellido=NULL,@Telefono='88888888',@Email='qa@example.invalid',@Cedula='QA-NUEVO',@Genero='O',@Usuario='qa_registro',@Contrasena='SoloPruebas';"
 Check ((Scalar "SELECT idRol FROM dbo.usuario WHERE nombreUsuario='qa_registro'") -eq 3) 'El registro público debe asignar rol Cliente.'
 Batches 'EXEC dbo.sp_Inventario_Guardar @Actor=1,@IdProducto=1,@Stock=10,@Minimo=2,@Activo=1;'
 $pedido=Scalar @'
DECLARE @s uniqueidentifier=NEWID(); EXEC dbo.sp_Cliente_Pedido_Guardar @Actor=2,@Solicitud=@s,@Detalle=N'<pedido><item producto="1" cantidad="2" precio="17.25" /></pedido>';
'@
 Check ((Scalar 'SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1') -eq 8) 'Enviar un pedido debe descontar inventario.'
 Check ((Scalar "SELECT total FROM dbo.pedido WHERE idPedido=$pedido") -eq 34.50) 'El importe del pedido debe coincidir.'
 Batches "EXEC dbo.sp_Cliente_Pedido_Cancelar @Actor=2,@IdPedido=$pedido;"
 Check ((Scalar 'SELECT stockActual FROM dbo.inventarioProducto WHERE idProducto=1') -eq 10) 'Cancelar debe devolver existencias.'
 Batches "EXEC dbo.sp_Mesa_Guardar @Actor=1,@Id=0,@Numero=91,@Capacidad=6,@Estado='Disponible',@Activa=1;"
 Check ((Scalar 'SELECT capacidad FROM dbo.mesa WHERE numero=91') -eq 6) 'El alta de mesa debe funcionar.'
 Write-Output "$checks comprobaciones correctas. Base original reconstruida y archivo completo ejecutado dos veces."
}
finally {
 if($cn.State -eq 'Open') {
  $cn.ChangeDatabase('master')
  if($db -notmatch '^RestaurantSM_Actualizacion_[a-f0-9]{32}$'){throw 'Nombre temporal no permitido.'}
  Batches "IF DB_ID(N'$db') IS NOT NULL BEGIN ALTER DATABASE [$db] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$db]; END;"
 }
 $cn.Dispose()
}
