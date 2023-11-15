
USE  Sistema_De_Ventas


select * from COMPRA

-- Creacion de los roles --

-- Administrador --

INSERT INTO ROL(Descripcion)
VALUES ('Administrador')
GO


-- Empleado --
INSERT INTO ROL(Descripcion)
VALUES ('Empleado')
GO

UPDATE USUARIO SET IDRol = 1 WHERE IDUsuario = 2
UPDATE USUARIO SET IDUsuario = 3 WHERE IDUsuario = 2;


DELETE FROM USUARIO WHERE IDUsuario = 3
-- Creacion de usuario administrador --
INSERT INTO USUARIO(Documento, NombreCompleto,Correo,Clave,IDRol,Estado) 
VALUES ('1020487321', 'ADMIN','@gmail.com', '123',1,1)
GO

-- consultas sql --
select u.IDUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IDRol,r.Descripcion from usuario u
inner join rol r on r.IDRol = u.IDRol 

SELECT * FROM ROL


-- Creacion del usuario empleado --


INSERT INTO USUARIO (IDUsuario, Documento,NombreCompleto, Correo,Clave,IDRol, Estado) 
VALUES (2, '103542', 'Empleado nuevo', 'empleado@gmail.com','123',2,2)

DELETE FROM USUARIO WHERE IDUSUARIO = 2

UPDATE USUARIO SET Estado = 0 WHERE IDUsuario = 2

SELECT * FROM ROL

SELECT * FROM PERMISO

SET IDENTITY_INSERT USUARIO ON;

SELECT * FROM USUARIO
GO



-- Mostrar los permisos de los usuarios --
SELECT p.IDRol, p.NombreMenu FROM PERMISO p
INNER JOIN ROL r ON r.IDRol = p.IDRol
INNER JOIN USUARIO u ON u.IDRol = r.IDRol
WHERE u.IDUsuario = 2


-- Permisos para el rol tipo administrador --
--INSERT INTO PERMISO(IDRol, NombreMenu)Values
--(1,'menuusuario'),
--(1,'menumantenedor'),
--(1,'menuventas'),
--(1,'menucompras'),
--(1,'menuclientes'),
--(1,'menuproveedor'),
--(1,'menureportes'),
--(1,'menuacercade')

--INSERT INTO PERMISO(IDRol, NombreMenu)Values
--(2,'menuventas'),
--(2,'menucompras'),
--(2,'menuclientes'),
--(2,'menuproveedor'),
--(2,'menuacercade')






-- INSERTANDO A LA TABLA CATEGORIA--
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES 
('Verduras',1)
GO

INSERT INTO CATEGORIA(Descripcion,Estado) VALUES 
('Frutas',1)
GO

INSERT INTO CATEGORIA(Descripcion,Estado) VALUES 
('Lacteos',1)
GO


SELECT IDCategoria,Descripcion,Estado FROM CATEGORIA

SELECT * FROM CATEGORIA

DELETE CATEGORIA WHERE IDCategoria = 1023

-- INSERTANDO EN LA TABLA NEGOCIOS --
INSERT INTO NEGOCIO (IDNegocio, Nombre, RUC,Direccion) VALUES (1,'ESTUDIANTES DEL TECNOLOGICO SARA FLOREZ Y KEVYN CORTES','12345','AV. CODIGO 123')
GO

/* SELECT TABLA COMPRAS CON ALIAS */
SELECT c.IDCompra,
u.NombreCompleto,
pr.Documento, pr.RazonSocial,
c.TipoDocumento, c.NumeroDocumento, c.MontoTotal, CONVERT(CHAR(10), c.FechaRegistro, 103)[FechaRegistro]
FROM COMPRA c 
INNER JOIN USUARIO u ON u.IDUsuario = c.IDUsuario
INNER JOIN PROVEEDOR pr ON pr.IDProveedor = c.IDProveedor
WHERE c.NumeroDocumento = '00001'



/* CONSULTA PARA VER QUE PRODUCTOS ESTAN RELACCIONADOS A UNA COMPRA */
SELECT p.Nombre,dc.PrecioCompra,dc.Cantidad,dc.MontoTotal
FROM DETALLE_COMPRA dc
INNER JOIN  PRODUCTO p ON p.IDProducto = dc.IDProducto
WHERE dc.IDCompra = 1

use Sistema_De_Ventas



SELECT v.IDVenta, u.NombreCompleto, 
v.DocumentoCliente, v.NombreCliente,
v.TipoDocumento, v.NumeroDocumento,
v.MontoPago, v.MontoCambio, v.MontoTotal,
CONVERT(CHAR(10),v.FechaRegistro, 103)[FechaRegistro]
FROM VENTA v
INNER JOIN USUARIO u ON u.IDUsuario = v.IDUsuario
WHERE v.NumeroDocumento = '00001'


SELECT p.Nombre, dv.PrecioVenta,dv.Cantidad, dv.SubTotal
FROM DETALLE_VENTA dv
INNER JOIN PRODUCTO p ON p.IDProducto = dv.IDProducto
WHERE dv.IDVenta = 1