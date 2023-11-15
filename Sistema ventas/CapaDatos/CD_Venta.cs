using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Venta
    {
        public int obtenercorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*) + 1 FROM VENTA");
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

        // Resta los productos de stock a la hora de una venta
        public bool RestarStock(int IDProducto, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update producto set stock = stock - @cantidad where idproducto = @idproducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", IDProducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;

                }
            }
            return respuesta;
        }
    
        // Suma el stock
        public bool SumarStock(int IDProducto, int Cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update producto set stock = stock + @cantidad where idproducto = @idproducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", Cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", IDProducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception ex)
                {
                    respuesta = false;

                }
            }
            return respuesta;
        }

        // Registrar 
    
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARVENTA", oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", obj.oUsuario.IDUsuario);// bien
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);// Bien
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento); // Bien
                    cmd.Parameters.AddWithValue("DocumentoCliente", obj.DocumentoCliente);
                    cmd.Parameters.AddWithValue("NombreCliente", obj.NombreCliente);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }
            return Respuesta;
        }

        public Venta obtenerVenta(string numero) {
            
            Venta obj = new Venta();
           
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) {

                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("SELECT v.IDVenta, u.NombreCompleto,");
                    query.AppendLine("v.DocumentoCliente, v.NombreCliente,");
                    query.AppendLine("v.TipoDocumento, v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago, v.MontoCambio, v.MontoTotal,");
                    query.AppendLine("CONVERT(CHAR(10),v.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IDUsuario = v.IDUsuario");
                    query.AppendLine("WHERE v.NumeroDocumento = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader()) { 
                    
                        while (dr.Read()) {
                            obj = new Venta()
                            {
                                IDVenta = int.Parse(dr["IDVenta"].ToString()),
                                oUsuario = new Usuario() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                            };
                        }
                    }
                }
                catch{
                   obj = new Venta();
                }
            }
            return obj;
        }

        public List<Detalle_Venta> ObtenerDetalleVenta(int IDVenta) {
            List<Detalle_Venta> oLista = new List<Detalle_Venta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.Nombre, dv.PrecioVenta,dv.Cantidad, dv.SubTotal FROM DETALLE_VENTA dv");
                    query.AppendLine("INNER JOIN PRODUCTO p ON p.IDProducto = dv.IDProducto");
                    query.AppendLine("WHERE dv.IDVenta = @idventa");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idventa", IDVenta);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Detalle_Venta()
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()), 
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString()),
                            });
                        }
                    }
                }
                catch
                {
                    oLista = new List<Detalle_Venta>();
                }
            }

                return oLista;
        }
    }
}

