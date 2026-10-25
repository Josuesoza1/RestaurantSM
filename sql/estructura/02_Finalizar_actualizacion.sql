UPDATE f SET clienteNombre=CONCAT(c.pNombreCl,' ',c.pApellidoCl),clienteCedula=c.cedula,empleadoNombre=COALESCE(u.nombreUsuario,'Registro anterior'),tasaImpuesto=NULL FROM dbo.factura f JOIN dbo.pedido p ON p.idPedido=f.idPedido JOIN dbo.cliente c ON c.idCliente=p.idCliente LEFT JOIN dbo.empleado e ON e.idEmpleado=p.idEmpleado LEFT JOIN dbo.usuario u ON u.idUsuario=e.idUsuario WHERE f.clienteNombre IS NULL;
INSERT dbo.facturaDetalle(idFactura,idProducto,producto,cantidad,precio,subtotal) SELECT f.idFactura,d.idProducto,p.nombre,d.cantidad,d.precioUnitario,d.total FROM dbo.factura f JOIN dbo.detallePedido d ON d.idPedido=f.idPedido JOIN dbo.producto p ON p.idProducto=d.idProducto WHERE NOT EXISTS(SELECT 1 FROM dbo.facturaDetalle fd WHERE fd.idFactura=f.idFactura);
IF NOT EXISTS(SELECT 1 FROM dbo.versionEsquema WHERE version=9) INSERT dbo.versionEsquema(version,descripcion) VALUES(9,N'Procedimientos legibles, pedidos por lineas e inventario con contador entero');
GO

EXEC dbo.sp_Sistema_Verificar;
PRINT N'Base preparada. Version 009: procedimientos simplificados.';
GO
