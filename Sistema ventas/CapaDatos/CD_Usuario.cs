using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using CapaEntidad;
using System.Xml.Linq;
using System.Security.Claims;
using System.Collections;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public List<Usuario> Listar(){
            List<Usuario> lista = new List<Usuario> ();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IDUsuario,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.IDRol,r.Descripcion from usuario u");
                    query.AppendLine("inner join rol r on r.IDRol = u.IDRol ");
                   

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                      
                        while (dr.Read()) {
                           
                            // Para que me muestre los usuarios en las tablas del formulario
                            lista.Add(new Usuario()
                            {
                                IDUsuario = Convert.ToInt32(dr["IDUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oRol = new Rol() { IDRol = Convert.ToInt32(dr["IDRol"]), Descripcion = dr["Descripcion"].ToString() } 
                            });
                        }

                    }
                }

                catch (Exception ex)
                {
                    lista = new List<Usuario>();
                }
            }

            return lista;
        } 


        // Procedure Registrar usuario 
        // Codigo bueno

        public int Registrar(Usuario obj, out string Mensaje) {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try{

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);
                     cmd.Parameters.AddWithValue("Documento", obj.Documento);
                     cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                     cmd.Parameters.AddWithValue("Correo", obj.Correo);
                     cmd.Parameters.AddWithValue("Clave", obj.Clave);
                     cmd.Parameters.AddWithValue("IDRol", obj.oRol.IDRol);
                     cmd.Parameters.AddWithValue("Estado", obj.Estado);
                     cmd.Parameters.Add("IDUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                     cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IDUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex){              
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }
            return idusuariogenerado;            
        }


        // Procedure de editar usuario
        // Codigo bueno
        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", obj.IDUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("IDRol", obj.oRol.IDRol);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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




        // Procedure eliminar Usuario
        // Codigo Bueno
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool Respuesta = false;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", obj.IDUsuario);                   
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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

    }
}
