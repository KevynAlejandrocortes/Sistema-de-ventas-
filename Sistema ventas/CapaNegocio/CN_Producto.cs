using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {


        private CD_Producto objcd_Producto = new CD_Producto();


        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }



        // Clase Registrar 
        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.Codigo == "")
            {

                Mensaje += "Es necesario agregar el codigo del Producto\n";

            }


            if (obj.Nombre == "")
            {

                Mensaje += "Es necesario agregar el nombre del Producto\n";

            }


            if (obj.Descripcion == "")
            {

                Mensaje += "Es necesario agregar la descripcion del Producto\n";

            }

            if (Mensaje != string.Empty)
            {

                return 0;

            }
            else
            {

                return objcd_Producto.Registrar(obj, out Mensaje);

            }
        }







        //Clase editar
        public bool Editar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {

                Mensaje += "Es necesario agregar el codigo del Producto\n";

            }


            if (obj.Nombre == "")
            {

                Mensaje += "Es necesario agregar el nombre del Producto\n";

            }


            if (obj.Descripcion == "")
            {

                Mensaje += "Es necesario agregar la descripcion del Producto\n";

            }

            if (Mensaje != string.Empty)
            {

                return false;

            }


            else
            {

                return objcd_Producto.Editar(obj, out Mensaje);

            }
        }




        // Clase eliminar
        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return objcd_Producto.Eliminar(obj, out Mensaje);
        }
    }
}











