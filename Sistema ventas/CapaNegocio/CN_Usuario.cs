using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

// Codigo bueno

namespace CapaNegocio
{
    public class CN_Usuario
    {

        private CD_Usuario objcd_usuario = new CD_Usuario();


        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }



        // Clase Registrar 
        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.Documento == "" ) {

                Mensaje += "Es necesario agregar el documento del usuario\n";

            }


            if (obj.NombreCompleto == "") {

                Mensaje += "Es necesario agregar el nombre completo del usuario\n";

            }


            if (obj.Clave == "")
            {

                Mensaje += "Es necesario agregar la contraseña del usuario\n";

            }

            if (Mensaje != string.Empty){

                return 0;

            }
            else{

                return objcd_usuario.Registrar(obj, out Mensaje); 

            }         
        }




        //Clase editar
        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Documento == "")
            {

                Mensaje += "Es necesario agregar el documento del usuario\n";

            }


            if (obj.NombreCompleto == "")
            {

                Mensaje += "Es necesario agregar el nombre completo del usuario\n";

            }


            if (obj.Clave == "")
            {

                Mensaje += "Es necesario agregar la contraseña del usuario\n";

            }


            if (Mensaje != string.Empty)
            {

                return false;

            }


            else
            {

                return objcd_usuario.Editar(obj, out Mensaje);

            }           
        }



        // Clase eliminar
        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objcd_usuario.Eliminar(obj, out Mensaje);
        }

    }


}








   


