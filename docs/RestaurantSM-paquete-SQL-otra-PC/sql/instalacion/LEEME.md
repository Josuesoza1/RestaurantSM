# Instalar RestaurantSM en otra PC

Este paquete acompaña al código actual del repositorio. GitHub guarda los archivos;
no crea ni sincroniza automáticamente la base de datos.

## Requisitos

- Windows con SQL Server o LocalDB y SQL Server Management Studio para ejecutar los scripts.
- Visual Studio con desarrollo de escritorio .NET y el targeting pack de .NET Framework 4.8.
- Una versión de Visual Studio capaz de abrir `RestaurantSM.slnx`.
- Permisos para crear la base en una instalación nueva o para modificar tablas y procedimientos al actualizar.

Se verificó en SQL Server 2025 LocalDB y MSBuild de Visual Studio 2026. El esquema
portable elimina rutas MDF/LDF, compatibilidad 170 y opciones de base exclusivas del
exportador original. No se ha ejecutado en versiones anteriores de SQL Server.

## Opción A PC sin base de datos

1. Clonar o descargar el repositorio y abrir SSMS. Conectarse al motor que usará la
   aplicación; para LocalDB local: `(localdb)\MSSQLLocalDB`, autenticación de Windows.
2. Abrir `INSTALAR_NUEVA.sql`. Activar **Consulta > Modo SQLCMD** y ejecutar todo.
   El script está completo en un archivo; no necesita rutas `:r` ni el SQL de Documents.
   Si `RestaurantSM` ya existe, se detiene sin reemplazarla.
3. Abrir `020_primer_administrador.sql`. Cambiar `CAMBIAR_USUARIO` y
   `CAMBIAR_CLAVE_LOCAL` por valores elegidos en esa PC y ejecutar. Se crea solo la
   primera cuenta administrativa de la aplicación. Guardar estos valores localmente,
   sin subirlos a GitHub. El script se niega a actuar si ya existe un administrador.
4. Abrir `030_verificar.sql` y ejecutar. En una instalación nueva debe mostrar 6
   categorías, 12 subcategorías y 24 productos, con roles 1, 2 y 3.
5. Copiar el contenido de `SistemaPedidoRestaurante/App.config.ejemplo` a
   `SistemaPedidoRestaurante/App.config` si se usa LocalDB con autenticación de Windows.
   Cambiar `Data Source` si se usa otro motor. Mantener el nombre `RestauranteDB`
   de la entrada y la base `RestaurantSM`.
6. Abrir la solución, establecer el proyecto UI como proyecto de inicio, compilar
   y ejecutar. Entrar como administrador; crear empleados desde Empleados y crear
   clientes mediante Crear cuenta. No se copian cuentas personales de la PC original.
7. Entrar como cliente y abrir Mi pedido para comprobar el catálogo.

El administrador inicial no necesita una fila de empleado para abrir el menú y
gestionar cuentas. La relación con el empleado que atiende una venta se completará
cuando se implemente el guardado del pedido.

## Opción B PC con una base anterior del mismo proyecto

1. Hacer un respaldo completo de esa base y guardar los cambios propios del código.
2. Comprobar que es el esquema original de RestaurantSM y que los roles coinciden:
   1 Administrador, 2 Empleado y 3 Cliente.
3. Abrir `ACTUALIZAR_EXISTENTE.sql`, activar **Modo SQLCMD** y ejecutar todo.
4. El archivo aplica las migraciones 001 a 004 y la consulta de verificación.
   No crea tablas base ni elimina usuarios, ventas o registros existentes.
5. Si la base ya tiene otros códigos para los mismos productos, o una de las seis
   categorías falta o está inactiva, el catálogo 004 se detiene para revisar el
   conflicto. No cambiar IDs ni borrar datos para forzar su ejecución.
6. Compilar el código actualizado y repetir la prueba de acceso de los tres roles.

Las migraciones se confirman por bloques, no como una transacción global de toda
la actualización. Si un bloque falla, detenerse, revisar el mensaje y corregirlo
antes de continuar. Los bloques ya aplicados pueden permanecer aplicados. Los
scripts de `sql/respaldos` son copias anteriores, no migraciones de instalación.

Para una aplicación manual por separado, usar este orden desde la raíz:

1. `sql/001_corregir_rol_registro_cliente.sql`
2. `sql/002_gestion_empleados.sql`
3. `sql/003_categorias_productos.sql`
4. `sql/004_catalogo_demostracion.sql`
5. `sql/instalacion/030_verificar.sql`

## Conexión y cuentas

La cuenta de SQL Server autoriza la conexión al motor. La cuenta de RestaurantSM
autoriza el acceso como administrador, empleado o cliente dentro de la aplicación.
Son dos cosas distintas. El usuario creado por `020_primer_administrador.sql` no
crea un login de SQL Server.

El ejemplo de App.config utiliza Windows y no contiene contraseñas. Si el equipo
decide usar autenticación SQL, cada PC debe tener un login con acceso a RestaurantSM
y configurar sus credenciales localmente. Este paquete no crea logins del servidor.
No hace falta copiar archivos MDF o LDF desde la PC original.

Al compilar, Visual Studio copia App.config al archivo
`SistemaPedidoRestaurante.exe.config` junto al ejecutable. Si se distribuye el
ejecutable, llevar también sus DLL y configurar ese archivo de conexión en destino.

## Antes del commit

- Incluir código, proyectos `.csproj`, recursos `.resx`, documentación, pruebas y
  `sql`, especialmente archivos nuevos que aún aparezcan como no seguidos.
- Revisar `App.config`: actualmente es un archivo seguido por Git y su configuración
  local puede contener credenciales. Subir una configuración sin secretos, como el
  ejemplo de Windows, y conservar aparte la configuración privada necesaria.
- No subir bin, obj, .vs, MDF, LDF ni copias locales con datos personales.
- No subir `020_primer_administrador.sql` después de escribir una clave real;
  conservar la versión de plantilla en el repositorio.
- No se hizo commit ni push como parte de esta entrega.

## Verificación opcional del paquete

Después de compilar, desde PowerShell en la raíz:

```powershell
.\tests\VerificarInstalacionSql.ps1
.\tests\VerificarCatalogoCliente.ps1
```

El primer verificador crea y elimina exclusivamente una base temporal con nombre
único; necesita permisos CREATE DATABASE. Usa la conexión local de App.config y no
escribe en RestaurantSM. Comprueba instalación, actualización repetida, catálogo,
login y registro. El segundo consulta RestaurantSM y prueba la selección del cliente.

## Si aparece un error

- Directivas `:On Error` no reconocidas: activar Modo SQLCMD antes de ejecutar.
- Base existente: usar la opción B, sin borrar la base para repetir la opción A.
- Error de conexión: verificar instancia, nombre RestaurantSM y autenticación local.
- App.config actualizado pero ejecutable antiguo: recompilar y revisar su `.exe.config`.
- `.slnx` no reconocido: abrir con una versión compatible de Visual Studio.
- SQL Server inaccesible o sin permisos: corregir la instalación o el acceso al motor;
  cambiar el rol del usuario de la aplicación no corrige permisos de SQL Server.
