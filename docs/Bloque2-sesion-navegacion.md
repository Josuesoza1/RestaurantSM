# Bloque 2: sesión y navegación

## Cómo funciona

1. `UsuarioBLL.IniciarSesion` comprueba las credenciales como antes.
2. `FrmLogin.AbrirSesion` copia el ID, nombre y rol a `SesionActual` y abre el
   menú del rol. La contraseña no forma parte de esa clase.
3. La identidad permanece en memoria mientras se navega. Por ejemplo, una
   operación futura podrá consultar `SesionActual.IdUsuario` para identificar
   al usuario que la realiza; relacionar ese usuario con el empleado o cliente
   correspondiente se hará al implementar cada operación.
4. `MenuSesion` incorpora Cerrar sesión en los tres menús y muestra el usuario
   y rol en el título. También cierra un menú si se intenta abrir sin una
   sesión de su rol.
5. Cerrar sesión o la X del menú vuelve al mismo login, borra la identidad y
   limpia el campo de contraseña. Salir cierra la aplicación.

No se modificaron el almacenamiento de contraseñas ni la base de datos.

## Por qué cambiamos la navegación

Antes, los formularios ocultaban una ventana y creaban otra cada vez que se
volvía al menú. Ahora el login abre el menú con `ShowDialog(this)`, y el menú
administrativo hace lo mismo con sus pantallas de gestión. Al volver se cierra
la pantalla actual y se reutiliza la anterior, sin generar menús ni logins
adicionales.

Cancelar, Iniciar sesión y la X del registro regresan al login que lo abrió.
Guardar correctamente una cuenta también cierra ese diálogo y regresa al mismo
login. No se altera el diseño de los formularios nuevos.

La distribución de carpetas pendiente está en [Cola-de-mejoras.md](Cola-de-mejoras.md).

## Cómo probarlo

1. Ejecutar con F5 e ingresar con una cuenta existente. El título del menú
   debe incluir su nombre de usuario y rol.
2. Como administrador, abrir Roles, Categorías, Productos o Empleados. Volver
   o cerrar esa pantalla debe conservar la sesión y regresar al menú anterior.
3. Elegir Cerrar sesión y entrar con otra cuenta: debe aparecer la identidad
   nueva y su menú correspondiente.
4. Cerrar el menú con la X debe volver al login. Elegir Salir debe cerrar
   toda la aplicación.
5. Abrir Crear cuenta y cancelar o cerrar con X: debe quedar un único login.

## Verificación realizada

- Compilación Debug satisfactoria de los cinco proyectos.
- 60 comprobaciones de identidad, navegación modal, cambio de roles, cierre
  de sesión, salida y ausencia de ventanas duplicadas.
- Las pruebas de navegación partieron de identidades simuladas después del
  punto de autenticación; no necesitaron contraseñas de cuentas reales.
- Se abrieron las pantallas administrativas y se consultaron sus listados,
  sin operaciones de escritura en SQL.
- Revisión visual del menú con nombre, rol y Cerrar sesión.

Los recursos `.resx` agregados estaban marcados como descargados de Internet.
Se revisaron como XML: no contienen entradas de datos ni objetos serializados.
Se retiró esa marca para que MSBuild pudiera compilarlos, sin cambiar su contenido.

Próximo bloque: completar empleados, incluyendo los campos que exige la base
y las operaciones sobre empleado y usuario.
