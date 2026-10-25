-- Procedimientos de Reservas.
USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
-- Procedimiento sp_Cliente_Reserva_Cancelar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Cancelar
    @Actor INT,
    @Id INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    UPDATE dbo.reservacion
    SET estado = 'Cancelada'
    WHERE idReservacion = @Id
          AND idCliente = @Cliente
          AND estado IN ('Pendiente', 'Confirmada')
          AND fechaHoraReserva > GETDATE();
    IF @@ROWCOUNT = 0
        THROW 50228, 'Solo puede cancelar sus reservas futuras activas.', 1;
END
GO

-- Procedimiento sp_Cliente_Reserva_Guardar: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reserva_Guardar
    @Actor INT,
    @Id INT=0,
    @IdMesa INT,
    @Fecha DATETIME,
    @Personas INT
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    DECLARE @Cliente AS INT;
    IF @Fecha <= GETDATE()
       OR @Personas NOT BETWEEN 1 AND 20
        THROW 50226, 'Seleccione una fecha futura y una cantidad válida.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
        IF NOT EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @IdMesa
                             AND activa = 1
                             AND capacidad >= @Personas
                             AND estado IN ('Disponible', 'Reservada'))
            THROW 50227, 'La mesa no está disponible o no tiene la capacidad necesaria.', 1;
        IF @Id <> 0
           AND NOT EXISTS (SELECT 1
                           FROM dbo.reservacion
                           WHERE idReservacion = @Id
                                 AND idCliente = @Cliente
                                 AND estado IN ('Pendiente', 'Confirmada')
                                 AND fechaHoraReserva > GETDATE())
            THROW 50228, 'Solo puede modificar sus reservas futuras activas.', 1;
        IF EXISTS (SELECT 1
                   FROM dbo.reservacion
                   WHERE idMesa = @IdMesa
                         AND idReservacion <> @Id
                         AND estado IN ('Pendiente', 'Confirmada')
                         AND fechaHoraReserva > DATEADD(hour, -2, @Fecha)
                         AND fechaHoraReserva < DATEADD(hour, 2, @Fecha))
            THROW 50229, 'La mesa tiene otra reserva dentro del intervalo de dos horas.', 1;
        IF @Id = 0
            BEGIN
                INSERT dbo.reservacion (idCliente, idMesa, fechaHoraReserva, cantidadPersonas, estado)
                VALUES                (@Cliente, @IdMesa, @Fecha, @Personas, 'Pendiente');
                SET @Id = CONVERT (INT, SCOPE_IDENTITY());
            END
        ELSE
            UPDATE dbo.reservacion
            SET idMesa = @IdMesa,
                fechaHoraReserva = @Fecha,
                cantidadPersonas = @Personas
            WHERE idReservacion = @Id
                  AND idCliente = @Cliente;
        COMMIT TRANSACTION;
        SELECT @Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Cliente_Reservas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Cliente_Reservas
    @Actor INT
AS
BEGIN
    DECLARE @Cliente AS INT;
    EXECUTE dbo.sp_Cliente_ExigirActivo @Actor, @Cliente OUTPUT;
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           r.fechaHoraReserva AS Fecha,
           r.cantidadPersonas AS Personas,
           r.estado AS Estado
    FROM dbo.reservacion AS r
         INNER JOIN
         dbo.mesa AS m
         ON m.idMesa = r.idMesa
    WHERE r.idCliente = @Cliente
    ORDER BY r.fechaHoraReserva DESC;
END
GO

-- Procedimiento sp_Operacion_EstadoReserva: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_EstadoReserva
    @Actor INT,
    @IdReserva INT,
    @Anterior VARCHAR (20),
    @Nuevo VARCHAR (20)
AS
BEGIN
    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    IF @Nuevo NOT IN ('Atendida', 'No asistio', 'Cancelada')
        THROW 50523, 'Seleccione una accion de reserva valida.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @Mesa AS INT, @Fecha AS DATETIME, @Estado AS VARCHAR (20);
        SELECT @Mesa = idMesa,
               @Fecha = fechaHoraReserva,
               @Estado = estado
        FROM dbo.reservacion
        WHERE idReservacion = @IdReserva;
        IF @Estado IS NULL
           OR @Estado <> @Anterior
           OR @Estado NOT IN ('Pendiente', 'Confirmada')
            THROW 50524, 'La reserva cambio o ya fue cerrada.', 1;
        IF @Nuevo = 'No asistio'
           AND GETDATE() < DATEADD(minute, 30, @Fecha)
            THROW 50525, 'Espere 30 minutos despues de la reserva para marcar ausencia.', 1;
        IF @Nuevo = 'Atendida'
           AND GETDATE() < DATEADD(minute, -30, @Fecha)
            THROW 50525, 'La reserva aun no esta dentro del horario de llegada.', 1;
        IF @Nuevo = 'Atendida'
           AND EXISTS (SELECT 1
                       FROM dbo.mesa
                       WHERE idMesa = @Mesa
                             AND (activa = 0
                                  OR estado NOT IN ('Disponible', 'Reservada')))
            THROW 50526, 'La mesa no esta disponible para recibir al cliente.', 1;
        UPDATE dbo.reservacion
        SET estado = @Nuevo
        WHERE idReservacion = @IdReserva;
        IF @Nuevo = 'Atendida'
            UPDATE dbo.mesa
            SET estado = 'Ocupada'
            WHERE idMesa = @Mesa;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;
        THROW;
    END CATCH
END
GO

-- Procedimiento sp_Operacion_Reservas: consulta o modifica los datos de su gestion con parametros definidos.
CREATE OR ALTER PROCEDURE dbo.sp_Operacion_Reservas
    @Actor INT
AS
BEGIN
    SET NOCOUNT ON;
    EXECUTE dbo.sp_Operacion_ExigirPersonal @Actor;
    SELECT r.idReservacion AS IdReserva,
           r.idMesa AS IdMesa,
           m.numero AS Mesa,
           CONCAT(c.pNombreCl, ' ', c.pApellidoCl) AS Cliente,
           r.fechaHoraReserva AS Fecha,
           r.cantidadPersonas AS Personas,
           r.estado AS Estado
    FROM dbo.reservacion AS r
         INNER JOIN
         dbo.cliente AS c
         ON c.idCliente = r.idCliente
         INNER JOIN
         dbo.mesa AS m
         ON m.idMesa = r.idMesa
    ORDER BY r.fechaHoraReserva DESC;
END
GO
