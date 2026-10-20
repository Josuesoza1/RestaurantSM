# RestaurantSM

Sistema de restaurante en C# Windows Forms y SQL Server, con cinco proyectos
orientados a UI, BLL, DAL, entidades y validaciones.

## Documentación y traslado

- [Informe explicado de cambios](docs/Informe-de-cambios-y-traslado.docx)
- [Versión de texto del informe](docs/Informe-de-cambios-y-traslado.md)
- [Instalación de SQL en otra PC](sql/instalacion/LEEME.md)
- [Script completo para una base nueva](sql/instalacion/INSTALAR_NUEVA.sql)
- [Actualizar la base anterior en otra PC: guía y archivo único](sql/LEEME_ACTUALIZACION.md)
- [Estado y próximos bloques](docs/Cola-de-mejoras.md)

Abrir la solución `RestaurantSM.slnx` con Visual Studio compatible y las
herramientas de escritorio .NET Framework 4.8. La conexión de cada PC se configura
en `SistemaPedidoRestaurante/App.config`; hay un ejemplo sin contraseñas junto a él.
Consultar la guía SQL antes de ejecutar los scripts. No subir credenciales locales.

El cliente puede guardar pedidos y consultar sus compras, actualizar sus datos y
gestionar reservas. El inventario puede controlar existencias por producto terminado.
Los cobros y la facturación de las pantallas internas continúan pendientes.
