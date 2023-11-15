

-- Creacion de procdimiento almacenado --
USE Sistema_De_Ventas
GO

-- CREACION PROCEDURE PARA USUARIO --
-- PROCEDURE PARA REGISTRO DE USUARIO --
CREATE PROC SP_REGISTRARUSUARIO(
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(100),
@Clave VARCHAR(100),
@IDRol INT,
@Estado BIT,
@IDUsuarioResultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @IDUsuarioResultado = 0
	SET @Mensaje =  ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Documento = @Documento) 
	BEGIN 
	
	INSERT INTO usuario(Documento,NombreCompleto,Correo,Clave,IDRol,Estado) VALUES
	(@Documento,@NombreCompleto,@Correo,@Clave,@IDRol,@Estado)

		SET @IDUsuarioResultado = SCOPE_IDENTITY() 
		

	END	
	ELSE
	
		SET @Mensaje = 'No se puede repetir el documento para mas de un usuario'

END
GO


-- CREACION PROCEDURE PARA EDITAR USUARIO --
CREATE PROC SP_EDITARUSUARIO(
@IDUsuario INT,
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(100),
@Correo VARCHAR(100),
@Clave VARCHAR(100),
@IDRol INT,
@Estado BIT,
@Respuesta BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Respuesta = 0
	SET @Mensaje =  ''

	IF NOT EXISTS(SELECT * FROM USUARIO WHERE Documento = @Documento AND idusuario != @IDUsuario) 
	BEGIN 
	
	UPDATE usuario SET
	Documento = @Documento,
	NombreCompleto =@NombreCompleto,
	Correo = @Correo,
	Clave = @Clave,
	IDRol = @IDRol,
	Estado = @Estado
	WHERE IDUsuario = @IDUsuario
	

		SET @Respuesta = 1
		

	END	
	ELSE
	
		SET @Mensaje = 'No se puede repetir el documento para mas de un usuario'

END
GO


-- CREACION PROCEDURE PARA ELIMINAR EL USUARIO --
ALTER PROC SP_ELIMINARUSUARIO(
@IDUsuario INT,
@Respuesta BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN

		SET @Respuesta = 0
		SET @Mensaje =  ''
		DECLARE @pasoregla BIT = 1

	IF EXISTS(SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IDUsuario = C.IDUsuario
	WHERE U.IDUsuario = @IDUsuario
	)
	BEGIN
		SET @pasoregla = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'El usuario no se puede eliminar, porque esta relazionado a una COMPRA\n'

	END

	IF EXISTS(SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IDUsuario = V.IDUsuario
	WHERE U.IDUsuario = @IDUsuario
	)
	BEGIN
	SET @pasoregla = 0
	SET @Respuesta = 0
	SET @Mensaje = @Mensaje + 'El usuario no se puede eliminar, porque esta relazionado a una VENTA\n'

	END

	IF(@pasoregla = 1)
	BEGIN 

		DELETE FROM USUARIO WHERE IDUsuario = @IDUsuario
		SET @Respuesta = 1

END 
END
GO





select * from USUARIO
GO
	
SELECT * FROM CATEGORIA
GO
	



DECLARE @Respuesta BIT 
DECLARE @mensaje VARCHAR(500)
EXEC SP_EDITARUSUARIO 6, '123', 'Prueba 2', 'prueba@gmail.com', '456', 2, 1,@Respuesta OUTPUT,@mensaje OUTPUT


SELECT @Respuesta

SELECT @mensaje

SELECT * FROM USUARIO
GO






-- PROCESIDIMIENTO ALMACENADO PARA CATEGORÍA --
-- PROCEDURE REGISTRAR CATEGORIA --
-- CODIGO BUENO --
CREATE PROCEDURE SP_REGISTRARCATEGORIA (
@Descripcion VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT *  FROM CATEGORIA WHERE Descripcion = @Descripcion)
	BEGIN
		INSERT INTO CATEGORIA(Descripcion,Estado) VALUES (@Descripcion,@Estado)
		SET @Resultado = SCOPE_IDENTITY()
END
	ELSE  
	SET  @Mensaje = 'No es valido repetir la descripcion de una categoria'
END
GO


-- PROCEDIMIENTO ALMACENADO PARA CATEGORIA --
-- PROCEDIMIENTO EDITAR CATEGORIA --
-- BUENO --
CREATE PROCEDURE SP_EDITARCATEGORIA (
@IDCategoria INT,
@Descripcion VARCHAR(50),
@Estado BIT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN 
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion AND IDCategoria != @IDCategoria)

	-- SI NO EXISTE UNA CATEGORIA CON DESCRIPCION Y UNA ID DIFERENTE SERA ACTUALIZADO --
		UPDATE CATEGORIA SET 
		Descripcion = @Descripcion,
		Estado = @Estado
		WHERE IDCategoria = @IDCategoria
	ELSE 
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'No se puede repetir la descripcion de una Categoria'
	END
END 
GO


-- PROCEDIMIENTO ALMACENADO PARA CATEGORIA --
-- PROCEDIMIENTO ELIMINAR CATEGORIA --
CREATE PROCEDURE SP_ELIMINARCATEGORIA (
@IDCategoria INT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN 
	SET @Resultado = 1
	IF NOT EXISTS ( 
	SELECT * FROM CATEGORIA c
	INNER JOIN PRODUCTO p ON p.IDCategoria = c.IDCategoria
	WHERE c.IDCategoria = @IDCategoria
	)
	BEGIN 
		DELETE TOP(1) FROM  CATEGORIA WHERE IDCategoria = @IDCategoria
	END 
	ELSE 
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'Esta categoria se encuentra relazionada a un producto y no puede ser eliminada'
	END
END 
GO

 
 -- Procedimiento almacenado para registrar producto -- 
 CREATE PROCEDURE SP_REGISTRARPRODUCTO(
 @Codigo VARCHAR(20),
 @Nombre VARCHAR(30),
 @Descripcion VARCHAR(30),
 @IDCategoria INT,
 @Estado BIT, 
 @Resultado INT OUTPUT,
 @Mensaje VARCHAR(500) OUTPUT
 )AS
 BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Codigo = @Codigo)
	BEGIN 

		INSERT INTO PRODUCTO(Codigo,Nombre,Descripcion,IDCategoria,Estado) VALUES (@Codigo,@Nombre,@Descripcion,@IDCategoria,@Estado)
		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje ='Ya existe un producto con el mismo codigo'
END
GO


-- PROCEDIMIENTO PARA MODIFICAR PRODUCTO -- 
CREATE PROCEDURE SP_MODIFICARPRODUCTO(
@IDProducto INT,
@Codigo VARCHAR(20),
@Nombre VARCHAR(30),
@Descripcion VARCHAR(30),
@IDCategoria INT,
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN 
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Codigo = @Codigo AND IDProducto != @IDProducto)

	 UPDATE PRODUCTO SET
	 Codigo = @Codigo,
	 Nombre = @Nombre,
	 Descripcion = @Descripcion,
	 IDCategoria = @IDCategoria,
	 Estado = @Estado
	 WHERE IDProducto = @IDProducto
ELSE
BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'Ya existe un producto con el mismo codigo'
	END 
END
GO


-- PROCEDIMIENTO PARA ELIMINAR PRODUCTO -- 
ALTER PROCEDURE SP_ELIMINARPRODUCTO (
@IDProducto INT,
@Respuesta INT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN 
	SET @Respuesta = 0
	SET @Mensaje = ''
	DECLARE  @pasoreglas BIT = 1

	IF EXISTS (SELECT * FROM DETALLE_COMPRA dc
	INNER JOIN PRODUCTO p ON p.IDProducto = dc.IDProducto
	WHERE p.IDProducto = @IDProducto
	)
	BEGIN
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'NO se puede eliminar debido a que se encuentra relaccionado con una Compra\N'
	END

	IF EXISTS (SELECT * FROM  DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IDProducto = dv.IDProducto
	WHERE p.IDProducto = @IDProducto
	)
	BEGIN 
		SET @pasoreglas = 0
		SET @Respuesta = 0
		SET @Mensaje = @Mensaje + 'No se puede eliminar porque se encuentra relaccionado a una Venta\N'
	END

	IF(@pasoreglas = 1)
	BEGIN
		DELETE FROM PRODUCTO  WHERE IDProducto = @IDProducto
		SET @Respuesta = 1
	END
END
GO

SELECT * FROM PRODUCTO 

SELECT IDProducto, Codigo, Nombre, p.Descripcion, c.IDCategoria,c.Descripcion[DescripcionCategoria], Stock, PrecioCompra, PrecioVenta, p.Estado FROM PRODUCTO p
INNER JOIN CATEGORIA c ON c.IDCategoria = p.IDCategoria

UPDATE PRODUCTO SET Descripcion = 'Lechuga'

-- INSERTAR EN PRODUCTO --
INSERT INTO PRODUCTO(Codigo,Nombre,Descripcion,IDCategoria) VALUES ('102048','Verduras','2limones',1020)

SELECT * FROM CATEGORIA

USE Sistema_De_Ventas
GO
--- PROCEDIMIENTO ALMACENADO PARA CLIENTE ---
CREATE PROCEDURE SP_REGISTRARCLIENTE(
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@Resultado INT OUTPUT,
@Mensaje VARCHAR (500) OUTPUT
)AS
BEGIN
	SET @Resultado = 0
	DECLARE @IDPersona INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento)
	BEGIN
		INSERT INTO CLIENTE(Documento,NombreCompleto,Correo,Telefono,Estado) VALUES
		(@Documento,@NombreCompleto,@Correo,@Telefono,@Estado)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El numero de coumento ya existe'
END
GO


-- PARA MODIFICAR USUARIO --
CREATE PROCEDURE SP_MODIFICARCLIENTE(
@IDCliente INT,
@Documento VARCHAR(50),
@NombreCompleto VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN 
	SET @Resultado = 1
	DECLARE @IDPersona INT
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Documento = @Documento AND IDCliente != @IDCliente)
	BEGIN
		UPDATE CLIENTE SET
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IDCliente = @IDCliente
	END
	ELSE
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El número de documento ya existe'
	END
END
GO



SELECT IDCliente,Documento,NombreCompleto,Correo,Telefono,Estado FROM CLIENTE
GO





 SELECT * FROM CLIENTE
 GO
 SELECT * FROM USUARIO
 GO
 USE Sistema_De_Ventas
 GO



 -- CREANDO PROCEDIMIENTO ALMACENADO PROVEEDOR --
 -- REGISTRAR PROVEEDOR --
 CREATE PROCEDURE SP_REGISTRARPROVEEDOR (
 @Documento VARCHAR(50),
 @RazonSocial VARCHAR(50),
 @Correo VARCHAR(50),
 @Telefono VARCHAR(50),
 @Estado BIT,
 @Resultado BIT OUTPUT,
 @Mensaje VARCHAR(500) OUTPUT
 )AS
 BEGIN
	SET @Resultado = 0
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento)
	BEGIN 
		INSERT INTO PROVEEDOR(Documento,RazonSocial,Correo,Telefono,Estado) VALUES (
		@Documento,@RazonSocial,@Correo,@Telefono,@Estado)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'El numero de documento ya existe'
END
GO


-- PROCEDIMIENTO MODIFICAR O EDITAR PROVEEDOR --
CREATE PROCEDURE SP_EDITARPROVEEDOR(
@IDProveedor INT,
@Documento VARCHAR(50),
@RazonSocial VARCHAR(50),
@Correo VARCHAR(50),
@Telefono VARCHAR(50),
@Estado BIT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)AS
BEGIN 
	SET @Resultado = 1
	DECLARE @IDPERSONA INT
	IF NOT EXISTS (SELECT * FROM PROVEEDOR WHERE Documento = @Documento AND IDProveedor != @IDProveedor)
	BEGIN 
		UPDATE PROVEEDOR SET
		Documento = @Documento,
		RazonSocial = @RazonSocial,
		Correo = @Correo,
		Telefono = @Telefono,
		Estado = @Estado
		WHERE IDProveedor = @IDProveedor
	END 
	ELSE 
	BEGIN 
		SET @Resultado = 0
		SET @Mensaje = 'El numero de documento ya existe'
	END
END
GO


-- PROCEDIMIENTO PARA ELIMINAR PROVEEDOR --
CREATE PROCEDURE SP_ELIMINARPROVEEDOR (
@IDProveedor INT,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
	SET @Resultado = 1
	IF NOT EXISTS (
		SELECT * FROM PROVEEDOR p
		INNER JOIN COMPRA c	ON p.IDProveedor = c.IDProveedor
		WHERE p.IDProveedor = @IDProveedor	
	)
	BEGIN
		DELETE TOP(1) FROM PROVEEDOR WHERE IDProveedor = @IDProveedor
	END
	ELSE 
	BEGIN
		SET @Resultado = 0
		SET @Mensaje = 'El proveedor esta relacionado a una compra'
 END
END
GO


SELECT * FROM PROVEEDOR
GO
SELECT IDProveedor, Documento, RazonSocial, Correo, Telefono, Estado FROM PROVEEDOR
GO


/* PROCESO PARA REGISTRAR COMPRA*/
CREATE TYPE EDETALLE_COMPRA AS TABLE(
IDProducto INT NULL,
PrecioCompra DECIMAL(18,2) NULL,
PrecioVenta DECIMAL(18,2) NULL,
Cantidad INT NULL,
MontoTotal DECIMAL(18,2) NULL
)
GO
 
 -- PROCEDIMIENTO ALMACENADO REGISTRAR COMPRA --
CREATE PROCEDURE SP_REGISTRARCOMPRA (
@IDUsuario INT,
@IDProveedor INT,
@TipoDocumento VARCHAR(500),
@NumeroDocumento VARCHAR(500),
@MontoTotal DECIMAL(18,2),
@DetalleCompra [EDETALLE_COMPRA] READONLY,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
) AS
BEGIN

	BEGIN TRY
		DECLARE @IDCompra INT = 0
		SET @Resultado = 1
		SET @Mensaje = ''

		BEGIN TRANSACTION registro

		INSERT INTO COMPRA(IDUsuario, IDProveedor, TipoDocumento,NumeroDocumento,MontoTotal)
		VALUES(@IDUsuario,@IDProveedor,@TipoDocumento,@NumeroDocumento,@MontoTotal)

		SET @IDCompra = SCOPE_IDENTITY()

		INSERT INTO DETALLE_COMPRA(IDCompra,IDProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal)
		SELECT @IDCompra,IDProducto,PrecioCompra,PrecioVenta,Cantidad,MontoTotal FROM @DetalleCompra -- CON ESTE SELECT LLAMAMOS LA TABLA EDETALLE_COMPRA--


		UPDATE p SET p.Stock = p.Stock + dc.Cantidad,
		p.PrecioCompra = dc.PrecioCompra,
		p.PrecioVenta = dc.PrecioVenta
		FROM PRODUCTO p
		INNER JOIN @DetalleCompra dc ON dc.IDProducto = p.IDProducto

		COMMIT TRANSACTION registro

	END TRY

	BEGIN CATCH
		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION registro
	END CATCH
END
GO

SELECT c.IDCompra,
u.NombreCompleto,
pr.Documento, pr.RazonSocial,
c.TipoDocumento, c.NumeroDocumento, c.MontoTotal, CONVERT(Char(10), c.FechaRegistro,103)[FechaRegistro]
FROM COMPRA c
INNER JOIN USUARIO u ON u.IDUsuario = c.IDUsuario
INNER JOIN PROVEEDOR pr ON pr.IDProveedor = c.IDProveedor
WHERE c.NumeroDocumento = '00001'


SELECT p.Nombre, dc.PrecioCompra, DC.Cantidad, DC.MontoTotal 
FROM DETALLE_COMPRA dc
INNER JOIN PRODUCTO p ON p.IDProducto = dc.IDProducto
WHERE dc.IDCompra = 1

select * from DETALLE_COMPRA




/* Proceso para registrar una venta */
CREATE TYPE EDETALLE_VENTA AS TABLE(
IDProducto INT NULL,
PrecioVenta DECIMAL(18,2) NULL,
Cantidad INT NULL,
Subtotal DECIMAL(18,2) NULL
);
GO

/* PROCEDIMIENTO ALMACENADO */
CREATE PROCEDURE SP_REGISTRARVENTA (
@IDUsuario INT,
@TipoDocumento VARCHAR(500),
@NumeroDocumento VARCHAR(500),
@DocumentoCliente VARCHAR(500),
@NombreCliente VARCHAR(500),
@MontoPago DECIMAL(18,2),
@MontoCambio DECIMAL(18,2),
@MontoTotal DECIMAL(18,2),
@DetalleVenta [EDETALLE_VENTA] READONLY,
@Resultado BIT OUTPUT,
@Mensaje VARCHAR(500) OUTPUT
) AS
BEGIN

	BEGIN TRY
		DECLARE @IDVenta INT = 0
		SET @Resultado = 1
		SET @Mensaje = ''

		BEGIN TRANSACTION registro

		INSERT INTO VENTA(IDUsuario, TipoDocumento, NumeroDocumento, DocumentoCliente, NombreCliente,MontoPago,MontoCambio,MontoTotal)
		VALUES(@IDUsuario, @TipoDocumento, @NumeroDocumento, @DocumentoCliente, @NombreCliente, @MontoPago, @MontoCambio, @MontoTotal)

		SET @IDVenta = SCOPE_IDENTITY() -- Devuelve el ultimo ID

		INSERT INTO DETALLE_VENTA(IDVenta, IDProducto, PrecioVenta, Cantidad, SubTotal)
		SELECT @IDVenta, IDProducto, PrecioVenta, Cantidad, Subtotal FROM @DetalleVenta

		COMMIT TRANSACTION registro

	END TRY

	BEGIN CATCH
		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION registro
	END CATCH
END
GO

select * from VENTA where NumeroDocumento = '00001'