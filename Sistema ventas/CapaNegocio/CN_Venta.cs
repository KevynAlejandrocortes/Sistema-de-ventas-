using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_venta = new CD_Venta();

        public bool RestarStock(int IDProducto, int Cantidad)
        {
            return objcd_venta.RestarStock(IDProducto, Cantidad);
        }

        public bool SumarStock(int IDProducto, int Cantidad)
        {
            return objcd_venta.SumarStock(IDProducto, Cantidad);
        }
        public int obtenerCorrelativo()
        {
            return objcd_venta.obtenercorrelativo();
        }

        // Clase Registrar 
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_venta.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Venta obtenerVenta(string numero)
        {
            Venta oVenta = objcd_venta.obtenerVenta(numero);

            if (oVenta.IDVenta != 0) {
                List<Detalle_Venta> oDetalleVenta = objcd_venta.ObtenerDetalleVenta(oVenta.IDVenta);
                oVenta.oDetalleVenta = oDetalleVenta;
            }
            return oVenta;
        }
    }
}
