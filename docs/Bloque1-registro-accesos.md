# Bloque 1: registro y accesos

El registro público crea cuentas de Cliente. El login abre una pantalla distinta
para cada rol y rechaza cualquier identificador que no esté contemplado.

| idRol | Nombre en SQL | Formulario |
|---|---|---|
| 1 | Administrador | FrmMenuAdministracion |
| 2 | Empleado | FrmMenuEmpleado |
| 3 | Cliente | FrmMenuCliente |

## Cambios y motivo

1. `Login/FrmCrearCuenta.Designer.cs`: incorpora el desplegable de género y
   amplía el formulario para conservar la distribución de los controles.
2. `Login/FrmCrearCuenta.cs`: traduce Masculino, Femenino y Otro a M, F y O,
   respectivamente. Limpiar también reinicia esa selección.
3. `ClienteValidacion.cs`: exige un género válido. `ClienteBLL.cs` reutiliza
   la validación de usuario y contraseña vacíos antes de llegar al DAL.
4. `sql/001_corregir_rol_registro_cliente.sql`: cambia el rol asignado por
   `sp_InsertarCliente` de 2 (Empleado) a 3 (Cliente). Mantiene la transacción
   para guardar juntos el usuario y el cliente.
5. `Login/FrmLogin.cs`: `CrearMenuSegunRol` elige la pantalla mediante un
   `switch`. Un rol desconocido produce un mensaje de acceso denegado y
   mantiene visible el login.
6. `UIEmpleado/FrmMenuEmpleado`: incorpora el destino del empleado. Pedidos y
   Cobrar venta están deshabilitados mientras se desarrollan sus módulos;
   Salir cierra la aplicación.

El formato de almacenamiento de contraseñas no cambió. La sesión compartida
se documenta en [Bloque2-sesion-navegacion.md](Bloque2-sesion-navegacion.md).

## Base de datos

La migración 001 se aplicó a la base configurada `RestaurantSM` el 17/10/2026.
No se cambiaron los roles de cuentas existentes.

Se conserva la definición anterior del procedimiento en
`sql/respaldos/001_sp_InsertarCliente_antes.sql`. Ese archivo sirve para revertir
el procedimiento; no es necesario ejecutarlo para usar la aplicación.

El archivo original `C:\Users\Josue\Documents\RestaurantSM.sql` se conserva
sin cambios como referencia. Si se vuelve a crear la base a partir de él,
también debe aplicarse la migración 001. Antes de ejecutar la migración en otra
máquina, comprobar que los tres identificadores de rol coincidan con la tabla
de arriba. Detener la ejecución si aparece un error.

## Cómo comprobarlo desde Visual Studio

1. Abrir `RestaurantSM.slnx`, seleccionar el proyecto de interfaz como inicio
   y ejecutar con F5.
2. En Crear cuenta, seleccionar género y completar los datos. La cuenta debe
   guardarse y permitir volver al login. Esta comprobación manual sí crea una
   cuenta real.
3. Iniciar sesión con esa cuenta: debe abrir el menú del cliente.
4. Usar cuentas existentes de Administrador y Empleado: cada una debe abrir
   su propio menú. No modificar los roles para realizar esta comprobación.
5. Sin seleccionar género, el registro debe mostrar una validación. Limpiar
   debe vaciar los datos y restablecer el desplegable.

## Verificación realizada

- Reconstrucción Debug de los cinco proyectos con MSBuild y .NET Framework 4.8.
- 23 comprobaciones, incluyendo registro y login con los tres géneros, los
  destinos de los tres roles, credenciales incorrectas y roles desconocidos.
- Datos de prueba dentro de transacciones revertidas: los conteos finales de
  usuarios y clientes permanecieron iguales a los iniciales.
- Revisión visual del registro y del menú de empleado en dos tamaños.

Las comprobaciones de base se hicieron a través de BLL/DAL. Se verificó la
selección del menú y los controles de WinForms por separado; no se automatizó
la escritura y el clic del registro en una sesión de escritorio.
