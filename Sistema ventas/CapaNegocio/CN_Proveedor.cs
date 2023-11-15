using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Proveedor
    {

        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();


        public List<Proveedor> Listar()
        {
            return objcd_Proveedor.Listar();
        }



        // Clase Registrar 
        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.Documento == "")
            {

                Mensaje += "Es necesario agregar el documento del Proveedor\n";

            }


            if (obj.RazonSocial == "")
            {

                Mensaje += "Es necesario agregar la razonsocial del Proveedor\n";

            }


            if (obj.Correo == "")
            {

                Mensaje += "Es necesario agregar el correo del Proveedor\n";

            }

            if (Mensaje != string.Empty)
            {

                return 0;

            }
            else
            {

                return objcd_Proveedor.Registrar(obj, out Mensaje);

            }
        }




        //Clase editar
        public bool Editar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Documento == "")
            {

                Mensaje += "Es necesario agregar el documento del Proveedor\n";

            }


            if (obj.RazonSocial == "")
            {

                Mensaje += "Es necesario agregar la razonsocial del Proveedor\n";

            }


            if (obj.Correo == "")
            {

                Mensaje += "Es necesario agregar el correo del Proveedor\n";

            }


            if (Mensaje != string.Empty)
            {

                return false;

            }


            else
            {

                return objcd_Proveedor.Editar(obj, out Mensaje);

            }
        }



        // Clase eliminar
        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objcd_Proveedor.Eliminar(obj, out Mensaje);
        }

    }
}

