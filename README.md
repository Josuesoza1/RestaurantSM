# RestaurantSM

Sistema de restaurante en C# Windows Forms y SQL Server, con cinco proyectos
orientados a UI, BLL, DAL, entidades y validaciones.

## Documentación y traslado

- [Actualizar la base existente en otra PC: instrucciones](sql/LEEME_ACTUALIZACION.md)
- [Archivo SQL único de actualización](sql/ACTUALIZAR_TODO_RESTAURANTSM.sql)

Abrir la solución `RestaurantSM.slnx` con Visual Studio compatible y las
herramientas de escritorio .NET Framework 4.8. La conexión de cada PC se configura
en `SistemaPedidoRestaurante/App.config`; hay un ejemplo sin contraseñas junto a él.
Consultar la guía SQL antes de ejecutar los scripts. No subir credenciales locales.

El cliente puede guardar pedidos, consultar sus compras, actualizar sus datos y
gestionar reservas. El inventario permite controlar existencias por producto terminado.
También funcionan las gestiones de empleados, clientes, roles, mesas, categorías y
productos. Los cobros y la facturación de las pantallas internas continúan pendientes.
