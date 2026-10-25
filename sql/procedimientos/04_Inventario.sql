-- Procedimientos de Inventario.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Registra una entrada o salida con motivo; no permite stock negativo.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Ajustar
    @Actor int,
    @IdProducto int,
    @Cantidad int,
    @Tipo varchar(20),
    @Motivo nvarchar(200),
    @Revision int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Cantidad <= 0 OR @Tipo NOT IN ('Entrada', 'Salida') OR NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL
        THROW 50527, 'Indique tipo, cantidad positiva y motivo.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Anterior int;
        DECLARE @Nuevo bigint;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND activo = 1 AND revision = @Revision;
        IF @Anterior IS NULL THROW 50528, 'El inventario cambio o esta inactivo. Recargue.', 1;
        IF @Tipo = 'Entrada' SET @Nuevo = CONVERT(bigint, @Anterior) + @Cantidad;
        ELSE SET @Nuevo = CONVERT(bigint, @Anterior) - @Cantidad;
        IF @Nuevo < 0 OR @Nuevo > 2147483647 THROW 50529, 'Las existencias quedan fuera del rango permitido.', 1;
        UPDATE dbo.inventarioProducto SET stockActual = @Nuevo, revision = revision + 1
        WHERE idProducto = @IdProducto AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, 'El inventario cambio. Recargue.', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior, @Cantidad, @Tipo, @Motivo;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Elimina el control solo si no quedan unidades ni pedidos abiertos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Eliminar @Actor int,
    @IdProducto int,
    @Revision int
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM dbo.detallePedido d INNER JOIN dbo.pedido p ON p.idPedido = d.idPedido
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN ('Pagado', 'Cancelado'))
            THROW 50529, 'El producto tiene pedidos abiertos.', 1;
        DELETE FROM dbo.inventarioProducto
        WHERE idProducto = @IdProducto AND stockActual = 0 AND revision = @Revision;
        IF @@ROWCOUNT = 0 THROW 50528, 'El inventario cambio o aun tiene existencias. Recargue.', 1;
        EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, 0, 'Salida', 'Eliminacion de inventario sin existencias';
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Crea o modifica inventario. Revision es un contador entero que aumenta en cada cambio.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Guardar
    @Actor int,
    @IdProducto int,
    @Stock int,
    @Minimo int,
    @Activo bit,
    @Revision int = 0
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    EXEC dbo.sp_Catalogo_ExigirAdministrador @Actor;
    IF @Stock < 0 OR @Minimo < 0 THROW 50213, 'Las existencias no pueden ser negativas.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.producto WHERE idProducto = @IdProducto)
            THROW 50214, 'Seleccione un producto existente.', 1;
        IF @Activo = 0 AND (@Stock <> 0 OR EXISTS (
            SELECT 1 FROM dbo.detallePedido d INNER JOIN dbo.pedido p ON p.idPedido = d.idPedido
            WHERE d.idProducto = @IdProducto AND d.controlInventario = 1 AND p.estado NOT IN ('Pagado', 'Cancelado')))
            THROW 50215, 'Retire el control solo con stock cero y sin pedidos abiertos.', 1;

        DECLARE @Anterior int;
        SELECT @Anterior = stockActual FROM dbo.inventarioProducto WHERE idProducto = @IdProducto;
        IF @Anterior IS NULL
        BEGIN
            IF @Revision <> 0 THROW 50216, 'El inventario cambio. Recargue el listado.', 1;
            INSERT INTO dbo.inventarioProducto(idProducto, stockActual, stockMinimo, activo, revision)
            VALUES (@IdProducto, @Stock, @Minimo, @Activo, 1);
            EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, 0, @Stock, 'Entrada', 'Alta de inventario';
        END
        ELSE
        BEGIN
            -- Revision es un contador: 1, 2, 3... Cada cambio suma uno.
            UPDATE dbo.inventarioProducto
            SET stockActual = @Stock, stockMinimo = @Minimo, activo = @Activo, revision = revision + 1
            WHERE idProducto = @IdProducto AND revision = @Revision;
            IF @@ROWCOUNT = 0 THROW 50216, 'El inventario cambio. Recargue antes de guardar.', 1;
            IF @Stock <> @Anterior
            BEGIN
                DECLARE @Cantidad int;
                DECLARE @Tipo varchar(20);
                SET @Cantidad = ABS(@Stock - @Anterior);
                IF @Stock > @Anterior SET @Tipo = 'Entrada'; ELSE SET @Tipo = 'Salida';
                EXEC dbo.sp_Inventario_RegistrarMovimiento @Actor, @IdProducto, @Anterior,
                    @Cantidad, @Tipo, 'Ajuste desde gestion de inventario';
            END;
        END;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- Procedimiento sp_Inventario_Listar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Listar
    @Actor INT
AS
BEGIN
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT p.idProducto AS IdProducto,
           p.nombre AS Producto,
           c.nombre AS Categoria,
           p.precio AS PrecioVenta,
           'Unidad' AS Unidad,
           i.stockActual AS StockActual,
           i.stockMinimo AS StockMinimo,
           i.activo AS Activo,
           i.revision AS Revision
    FROM dbo.inventarioProducto AS i
         INNER JOIN
         dbo.producto AS p
         ON p.idProducto = i.idProducto
         INNER JOIN
         dbo.categoria AS c
         ON c.idCategoria = p.idCategoria
    ORDER BY p.nombre;
END
GO

-- Procedimiento sp_Inventario_Movimientos: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_Movimientos
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Catalogo_ExigirAdministrador @Actor;
    SELECT id AS Id,
           fecha AS Fecha,
           producto AS Producto,
           anterior AS Anterior,
           nuevo AS Nuevo,
           diferencia AS Diferencia,
           usuario AS Usuario,
           motivo AS Motivo
    FROM dbo.movimientoInventario
    ORDER BY id DESC;
END
GO

-- Registra el usuario, el producto, las unidades y el motivo recibidos por parametros.
-- Cada procedimiento recibe el usuario y el motivo de forma explicita.
CREATE OR ALTER PROCEDURE dbo.sp_Inventario_RegistrarMovimiento
    @Actor int,
    @IdProducto int,
    @Anterior int,
    @Cantidad int,
    @Tipo varchar(20),
    @Motivo nvarchar(200)
AS
BEGIN
    SET NOCOUNT ON;
    IF @@TRANCOUNT = 0 THROW 50720, 'El movimiento debe formar parte de una transaccion.', 1;
    DECLARE @Nuevo int;
    IF @Tipo = 'Entrada' SET @Nuevo = @Anterior + @Cantidad;
    ELSE SET @Nuevo = @Anterior - @Cantidad;
    INSERT INTO dbo.movimientoInventario
        (idProducto, producto, anterior, nuevo, diferencia, idUsuario, usuario, motivo)
    SELECT p.idProducto, p.nombre, @Anterior, @Nuevo, @Nuevo - @Anterior,
        @Actor, u.nombreUsuario, @Motivo
    FROM dbo.producto p
    INNER JOIN dbo.usuario u ON u.idUsuario = @Actor
    WHERE p.idProducto = @IdProducto;
END;
GO
