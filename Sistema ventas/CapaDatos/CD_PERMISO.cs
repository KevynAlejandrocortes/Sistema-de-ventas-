using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_PERMISO
    {
        
        public List<Permiso> Listar(int IDUsuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IDRol, p.NombreMenu FROM PERMISO p");
                    query.AppendLine("INNER JOIN ROL r ON r.IDRol = p.IDRol");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IDRol = r.IDRol");
                    query.AppendLine("WHERE u.IDUsuario = @IDUsuario");

                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("IDUsuario", IDUsuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() { IDRol = Convert.ToInt32(dr["IDRol"]) }, 
                                NombreMenu = dr["NombreMenu"].ToString(),
                               
                            });





                        }

                    }
                }

                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                }
            }

            return lista;
        }

    }
}

    

