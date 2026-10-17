# Bloque 3: gestión de empleados

## Qué permite el formulario

- Agregar empleado y crear su cuenta en la misma operación.
- Seleccionar un empleado y editar sus datos y su cuenta.
- Desactivar sin borrar al empleado, su usuario ni las relaciones con ventas.
- Reactivar desde Editar, marcando Activo.
- Buscar por nombre, cargo, género, cédula o teléfono.

Se agregaron cargo, género y cédula. El listado muestra Activo y solo permite
seleccionar filas; las modificaciones se hacen desde los campos del formulario.

Los roles disponibles son Administrador y Empleado. Los nuevos registros
proponen Empleado y Activo. El registro de clientes sigue siendo independiente.

## Cómo probarlo paso a paso

1. Guardar los archivos y recompilar la solución. Ejecutar con F5 e iniciar
   sesión con una cuenta de administrador.
2. Abrir Empleados y pulsar Agregar. Completar primer nombre, primer apellido,
   teléfono, cargo, género, cédula, usuario y contraseña. Segundo nombre y
   segundo apellido son opcionales.
3. Guardar: debe aparecer una sola fila nueva y su cuenta debe existir para
   iniciar sesión. Esta prueba manual sí conserva el registro que se crea.
4. Seleccionar la fila y pulsar Editar. Cambiar el cargo y guardar. Dejar la
   contraseña vacía conserva la actual; escribir otra la reemplaza. Nunca se
   carga la contraseña guardada en el formulario.
5. Seleccionar la fila y pulsar Desactivar. Confirmar la operación: el empleado
   permanece en el listado con Activo desmarcado y ya no puede iniciar sesión.
6. Para reactivarlo, editarlo y marcar Activo. Sus datos y contraseña permanecen.

Limpiar vacía los campos, pero al editar conserva el ID del empleado
seleccionado. Cancelar abandona la operación y vuelve al modo neutral.
El administrador no puede desactivarse ni quitarse su propio rol.

## Cómo se reparte la lógica

1. **FrmEmpleados:** recoge los campos y muestra el listado y los mensajes.
2. **EmpleadoValidacion:** comprueba obligatorios, género, rol y tamaños que
   admite SQL antes de guardar.
3. **EmpleadoBLL:** exige sesión administrativa y coordina la operación.
4. **EmpleadoDAL:** envía parámetros SQL con tipos y tamaños explícitos.
5. **Procedimientos:** guardan cuenta y empleado dentro de una transacción;
   también comprueban que quien solicita la escritura sea un administrador activo.

No se incorporó un registro de auditoría. El ID del administrador se utiliza
para autorizar la operación, no para guardar un historial de cambios.

## Cambios en SQL Server

Se aplicó `sql/002_gestion_empleados.sql` a la base configurada RestaurantSM:

- Actualiza `sp_Empleado_Listar` para incluir usuario y rol mediante LEFT JOIN.
- Crea `sp_Empleado_Insertar`, `sp_Empleado_Actualizar` y `sp_Empleado_Desactivar`.
- Actualiza `sp_Usuario_ValidarLogin` para rechazar cuentas vinculadas a
  empleados inactivos. Los clientes sin relación con empleado mantienen su login.

No se cambiaron las tablas ni los datos existentes. En particular, los cinco
empleados que estaban vinculados a administradores conservan esa asignación.

`empleado.idUsuario` sigue admitiendo NULL en SQL. La entidad ahora usa `int?`
y el listado lo maneja sin fallar. Los empleados nuevos siempre se crean con
cuenta; al editar un registro antiguo sin cuenta se exige contraseña y se crea
su usuario, sin apropiarse de una cuenta existente.

Se guardaron los procedimientos anteriores en
`sql/respaldos/002_procedimientos_antes.sql`. Es un respaldo para una reversión
coordinada con el código; la aplicación actual requiere las columnas del
procedimiento de listado actualizado.

El archivo original de Documentos se conserva sin modificaciones. Para recrear
la base en otra máquina se deben aplicar después las migraciones 001 y 002.
El almacenamiento de contraseñas mantiene el formato existente, según lo acordado.

## Comprobaciones realizadas

- Compilación Debug de los cinco proyectos con MSBuild de Visual Studio.
- 31 comprobaciones de BLL/DAL/SQL: alta, edición, contraseñas, baja lógica,
  reactivación, duplicados, validaciones, permisos y empleados sin cuenta.
- 11 comprobaciones del formulario, sus campos, la selección de empleado y
  carga de su diseñador, además de revisión visual en dos tamaños.
- Las operaciones de prueba en SQL se hicieron dentro de transacciones que
  se revirtieron. Se verificaron los conteos y una suma de comprobación de las
  filas originales: no quedaron cuentas ni empleados de prueba.

El verificador de lógica puede repetirse, tras compilar, con:

```powershell
.\tests\VerificarEmpleados.ps1
```

Ejecutarlo mientras nadie edita esos datos. No imprime contraseñas ni abre
formularios o mensajes. Las pruebas revertidas pueden dejar saltos en los
identificadores IDENTITY; eso no representa filas faltantes ni afecta las relaciones.

Próximo bloque: completar categorías y productos. La reorganización de los
formularios nuevos continúa en la cola de mejoras.
