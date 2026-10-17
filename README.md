# RestaurantSM

Sistema de restaurante en C# Windows Forms y SQL Server, con cinco proyectos
orientados a UI, BLL, DAL, entidades y validaciones.

## Documentación y traslado

- [Informe explicado de cambios](docs/Informe-de-cambios-y-traslado.docx)
- [Versión de texto del informe](docs/Informe-de-cambios-y-traslado.md)
- [Instalación de SQL en otra PC](sql/instalacion/LEEME.md)
- [Script completo para una base nueva](sql/instalacion/INSTALAR_NUEVA.sql)
- [Actualización de una base anterior](sql/instalacion/ACTUALIZAR_EXISTENTE.sql)
- [Estado y próximos bloques](docs/Cola-de-mejoras.md)

Abrir la solución `RestaurantSM.slnx` con Visual Studio compatible y las
herramientas de escritorio .NET Framework 4.8. La conexión de cada PC se configura
en `SistemaPedidoRestaurante/App.config`; hay un ejemplo sin contraseñas junto a él.
Consultar la guía SQL antes de ejecutar los scripts. No subir credenciales locales.

El pedido del cliente se prepara en memoria. El guardado, cobro y factura aún
están pendientes; el catálogo y las gestiones de empleados y productos ya funcionan.
