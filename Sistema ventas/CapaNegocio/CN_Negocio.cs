﻿using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Negocio
    {

        private CD_Negocio objcd_negocio = new CD_Negocio();


        public Negocio ObtenerDatos()
        {
            return objcd_negocio.ObtenerDatos();
        }

        // Clase Registrar 
        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.Nombre == "")
            {

                Mensaje += "Es necesario agregar el nombre del negocio\n";

            }


            if (obj.RUC == "")
            {

                Mensaje += "Es necesario agregar el RUC del negocio\n";

            }


            if (obj.Direccion == "")
            {

                Mensaje += "Es necesario agregar la direccion del negocio\n";

            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_negocio.GuardarDatos(obj, out Mensaje);
            }
        }

        // Obtener logo
        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objcd_negocio.ObtenerLogo(out obtenido);
        }

        // Actualizar logo
        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return objcd_negocio.ActualizarLogo(imagen, out mensaje);
        }

    }
}
