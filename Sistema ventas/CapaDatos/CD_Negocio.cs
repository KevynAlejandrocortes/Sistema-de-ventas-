﻿using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class CD_Negocio
    {
        // Mostrar los datos
        public  Negocio ObtenerDatos() {
            
            Negocio obj = new Negocio();

            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena)) {
                    conexion.Open();

                    string query = "select IDNegocio, Nombre, RUC, Direccion from NEGOCIO where IDNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion); 
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read()) {
                            obj = new Negocio() {
                                IDNegocio = int.Parse(dr["IDNegocio"].ToString()),
                                Nombre = dr["Nombre"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new Negocio();
            }
            return obj;
        }


        // Guardar datos 
        public bool GuardarDatos(Negocio objeto, out string Mensaje) {
           
            Mensaje = string.Empty;
            bool Respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                                     
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Nombre = @nombre,");
                    query.AppendLine("RUC = @ruc,");
                    query.AppendLine("Direccion = @direccion");
                    query.AppendLine("where IDNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@ruc", objeto.RUC);
                    cmd.Parameters.AddWithValue("@direccion", objeto.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1) {
                        Mensaje = "No se guardaron los datos";
                        Respuesta = false;
                    }

                    }                 
            }
            catch(Exception ex)
            {
               Mensaje = ex.Message;
               Respuesta = false;
            }
            return Respuesta;
        }



        // Obtener logo
            public byte[] ObtenerLogo(out bool obtenido) {
                obtenido = true;
                byte[] LogoBytes = new byte[0];

            try
            {

                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select Logo from NEGOCIO where IDNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) 
                        {
                            LogoBytes = (byte[])dr["Logo"];                       
                        }                    
                       }
                    }              
            }
            catch (Exception ex)
            {
                obtenido = false;
                LogoBytes = new byte[0];
            }
            return LogoBytes;
        }



        // Actualizar logo
        public bool ActualizarLogo(byte[] image, out string mensaje) {
           
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO set Logo = @imagen");                    
                    query.AppendLine("where IDNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);                  
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se actualizo el logo";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }
  }  
}

