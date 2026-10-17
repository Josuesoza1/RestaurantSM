# Diagnóstico del diseñador de WinForms

La captura del 17/10/2026 muestra un error al cargar la clase base
`System.Windows.Forms.Form` en el diseñador de Visual Studio.

## Comprobaciones

- La referencia a System.Windows.Forms existe en el proyecto y se resuelve
  contra los ensamblados de referencia de .NET Framework 4.8.
- La reconstrucción con el MSBuild de Visual Studio Community 2026 termina
  correctamente.
- Se reprodujo ejecución de lógica de formularios durante una prueba con
  DesignSurface; podía abrir mensajes de error fuera del flujo normal.
- Se añadieron comprobaciones de modo de diseño a los eventos Load que
  preparan listados y a la configuración compartida de menús y sesión.
- Después, los 15 formularios cargaron con FormDocumentDesigner mediante
  DesignSurface, sin configurar una conexión SQL en el proceso de prueba.
- Las 60 comprobaciones anteriores de sesión y navegación siguen pasando.

No se confirmó una causa única para el mensaje mostrado por Visual Studio.
La prueba con DesignSurface valida los formularios compilados, pero no sustituye
la apertura de los archivos fuente en el diseñador del IDE.

## Archivos temporales regenerados

Al no detectar un proceso devenv.exe en ejecución, se movieron las carpetas
obj de los cinco proyectos y la carpeta .vs a un respaldo antes de recompilar:

`C:\Users\Josue\AppData\Local\Temp\RestaurantSM-cache-designer-0987f3a7bad64311ac7175a5cca24e13`

No se borraron diseños, archivos fuente ni configuración de conexión. La
disposición anterior de pestañas está en el respaldo de .vs y puede recuperarse
si hace falta. La base de datos no se modificó.

## Comprobación pendiente en Visual Studio

1. Volver a abrir RestaurantSM.slnx.
2. Ejecutar Compilar > Recompilar solución.
3. Abrir FrmRoles.cs y usar Ver diseñador (Shift+F7).
4. Si el error continúa, abrir Mostrar pila de llamadas en la página de error.
   Esa información permite distinguir un fallo del servicio del diseñador,
   una dependencia o una excepción durante la carga.
