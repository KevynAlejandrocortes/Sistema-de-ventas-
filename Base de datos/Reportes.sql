use Sistema_De_Ventas
GO

/* Precedimiento almacenado */
CREATE PROC SP_REPORTECOMPRAS(
@fechainicio VARCHAR(10),
@fechafin VARCHAR(10),
@idproveedor INT 
)
AS 
 BEGIN
	SET DATEFORMAT mdy;
SELECT 
CONVERT(char(10), c.FechaRegistro,101)[FechaRegistro],c.TipoDocumento, c.NumeroDocumento, c.MontoTotal,
u.NombreCompleto[UsuarioRegistro],
pr.Documento[DocumentoProveedor], pr.RazonSocial, 
p.Codigo[CodigoProducto],p.Nombre[NombreProducto], ca.Descripcion[Categoria],dc.PrecioCompra, dc.PrecioVenta,dc.Cantidad, dc.MontoTotal[SubTotal]
FROM COMPRA c
INNER JOIN USUARIO u ON u.IDUsuario = c.IDUsuario
INNER JOIN PROVEEDOR pr ON pr.IDProveedor = c.IDProveedor
INNER JOIN DETALLE_COMPRA dc ON dc.IDCompra = c.IDCompra
INNER JOIN PRODUCTO p ON p.IDProducto = dc.IDProducto
INNER JOIN CATEGORIA ca ON ca.IDCategoria = p.IDCategoria
WHERE CONVERT(date, c.FechaRegistro) BETWEEN @fechainicio AND @fechafin
AND pr.IDProveedor = IIF(@idproveedor = 0, pr.IDProveedor, @idproveedor)
END
GO

/* PROCEDIMIENTO REPORTE VENTA*/
CREATE PROCEDURE SP_REPORTEVENTAS (
@fechainicio VARCHAR(10),
@fechafin VARCHAR(10)
)
AS 
 BEGIN
	SET DATEFORMAT mdy;
SELECT 
CONVERT(char(10), v.FechaRegistro,101)[FechaRegistro],v.TipoDocumento, v.NumeroDocumento, v.MontoTotal,
u.NombreCompleto[UsuarioRegistro],
v.DocumentoCliente, v.NombreCliente,
p.Codigo[CoidgoProducto], p.Nombre[NombreProducto], ca.Descripcion[Categoria], dv.PrecioVenta, dv.Cantidad, dv.SubTotal
from VENTA v
INNER JOIN USUARIO u ON u.IDUsuario = v.IDUsuario
INNER JOIN DETALLE_VENTA dv ON dv.IDVenta = v.IDVenta
INNER JOIN PRODUCTO p ON p.IDProducto = dv.IDProducto 
INNER JOIN CATEGORIA ca ON ca.IDCategoria = p.IDCategoria
WHERE CONVERT(date, v.FechaRegistro) BETWEEN @fechainicio AND @fechafin
END

EXEC SP_REPORTEVENTAS '06/02/2023', '06/02/2023'
