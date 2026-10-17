USE [RestaurantSM];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE sp_Empleado_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT idEmpleado, idUsuario, pNombreEmp, sNombreEmp, pApellidoEmp, 
           sApellidoEmp, cargo, genero, cedula, telefono, activo
    FROM empleado;
END

GO
DROP PROCEDURE IF EXISTS dbo.sp_Empleado_Insertar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Empleado_Actualizar;
GO
DROP PROCEDURE IF EXISTS dbo.sp_Empleado_Desactivar;
GO

CREATE OR ALTER PROCEDURE sp_Usuario_ValidarLogin
    @NombreUsuario VARCHAR(50),
    @Contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT u.idUsuario, u.nombreUsuario, u.idRol, r.nombre AS NombreRol
    FROM usuario u
    INNER JOIN rol r ON u.idRol = r.idRol
    WHERE u.nombreUsuario = @NombreUsuario AND u.contrasena = @Contrasena;
END

GO
