using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CapaDatos
{
    public class CD_Compra
    {       
        public int obtenercorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) + 1 FROM COMPRA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idcorrelativo = 0;
                }
            }
            return idcorrelativo;
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            { 
                try
                {                 
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCOMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", obj.oUsuario.IDUsuario);
                    cmd.Parameters.AddWithValue("IDProveedor", obj.oProvedor.IDProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal) ;
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                  
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }

        public Compra ObtenerCompra(string numero) {
            Compra obj = new Compra();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.IDCompra,");
                    query.AppendLine("u.NombreCompleto,");
                    query.AppendLine("pr.Documento, pr.RazonSocial,");
                    query.AppendLine("c.TipoDocumento, c.NumeroDocumento, c.MontoTotal, CONVERT(Char(10), c.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("FROM COMPRA c");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IDUsuario = c.IDUsuario");
                    query.AppendLine("INNER JOIN PROVEEDOR pr ON pr.IDProveedor = c.IDProveedor");
                    query.AppendLine("WHERE c.NumeroDocumento = @numero");
                    


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            obj = new Compra() {  
                                IDCompra = Convert.ToInt32(dr["IDCompra"]),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString()},
                                oProvedor = new Proveedor() { Documento = dr["Documento"].ToString(), RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal (dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };                        
                        }

                    }
                }

                catch (Exception ex)
                {
                    obj = new Compra();
                }
            }

            return obj;
        }

        public List<Detalle_Compra> ObtenerDetalleCompra(int IDCompra)
        {
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT p.Nombre, dc.PrecioCompra, DC.Cantidad, DC.MontoTotal FROM DETALLE_COMPRA dc");
                    query.AppendLine("INNER JOIN PRODUCTO p ON p.IDProducto = dc.IDProducto");
                    query.AppendLine("WHERE dc.IDCompra = @IDCompra");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IDCompra", IDCompra);
                    cmd.CommandType = CommandType.Text;

                   
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {

                            // Para que me muestre los Proveedors en las tablas del formulario
                            oLista.Add(new Detalle_Compra()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),                              
                            });

                        }

                    }
                }

                catch (Exception ex)
                {
                    oLista = new List<Detalle_Compra>();
                }
            }

            return oLista;
        }
    }
}