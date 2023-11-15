using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utlidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace CapaPresentacion
{
    public partial class FrmVentas : Form
    {
        private Usuario _Usuario;

        public FrmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // load frm ventas
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            // Codigo bueno
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Recibo", Texto = "Recibo" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidproducto.Text = "0";

            txtpagacon.Text = "";
            txtcambio.Text = "";
            txttotalpagar.Text = "0";
        }

        // Boton buscar cliente
        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdClientes())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtdocumentocliente.Text = modal._Cliente.Documento.ToString();
                    txtnombrecliente.Text = modal._Cliente.NombreCompleto.ToString();
                    txtcodproducto.Select();
                }
                else
                {
                    txtdocumentocliente.Select();
                }
            }
        }

        // Boton buscar producto
        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            // Codigo bueno
            using (var modal = new mdProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IDProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txtprecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtstock.Text = modal._Producto.Stock.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodproducto.Select();
                }
            }
        }

        
        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo ==
                txtcodproducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtidproducto.BackColor = Color.Honeydew;
                    txtcodproducto.Text = oProducto.IDProducto.ToString();
                    txtproducto.Text = oProducto.Nombre;
                    txtprecio.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtstock.Text = oProducto.Stock.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodproducto.BackColor = Color.MistyRose;
                    txtidproducto.Text = "0";
                    txtproducto.Text = "";
                    txtprecio.Text = "";
                    txtstock.Text = "";
                    txtcantidad.Value = 1;
                }
            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Debes seleccionar algun producto", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtprecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecio.Select();
                return;
            }

            if (Convert.ToInt32(txtstock.Text) < Convert.ToInt32(txtcantidad.Value.ToString()))
            {

                MessageBox.Show("La cantidad no debe ser mayor al stock", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IDProducto"].Value.ToString() == txtidproducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }
            if (!producto_existe)
            {
                string mensaje = string.Empty;
                bool respuesta = new CN_Venta().RestarStock(
                      Convert.ToInt32(txtidproducto.Text),
                      Convert.ToInt32 (txtcantidad.Value.ToString())
                    ); 
                if (respuesta) {
                    dgvdata.Rows.Add(new object[] {
                    txtidproducto.Text,
                    txtproducto.Text,
                    precio.ToString("0,00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * precio).ToString("0.00")
                 });
                    calcularTotal();
                    limpiarProducto();
                    txtcodproducto.Select();
                }           
            }
        }
        // Metodo para calcular total.
        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            txttotalpagar.Text = total.ToString("0.00");
        }

        // Limpiar la tabla al agregar producto
        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtproducto.Text = "";
            txtprecio.Text = "";
            txtstock.Text = "";
            txtcantidad.Value = 1;
        }

        // pintar el boton eliminar
        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Eliminar.Width;
                var h = Properties.Resources.Eliminar.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.Eliminar, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                       Convert.ToInt32 (dgvdata.Rows[index].Cells["IDProducto"].Value.ToString()),
                       Convert.ToInt32 (dgvdata.Rows[index].Cells["Cantidad"].Value.ToString()));

                    if (respuesta) {
                        dgvdata.Rows.RemoveAt(index);
                        calcularTotal();
                    }                 
                }
            }
            
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (txtprecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
        }

        private void txtpagacon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (txtpagacon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
        }
        private void calcularCambio()
        {
            if (txttotalpagar.Text.Trim() ==  "") {
                MessageBox.Show("No existe productos en la venta", "Mensaje", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagacon;
            decimal total = Convert.ToDecimal(txttotalpagar.Text);
            
            if (txtpagacon.Text.Trim() == "")
            {
                txtpagacon.Text = "0";
            }
            if (decimal.TryParse(txtpagacon.Text.Trim(), out pagacon))
            {
                if (pagacon < total){
                    txtcambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtcambio.Text = cambio.ToString("0.00");
                }
            }
        }

        private void txtpagacon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }

        private void btncrearventa_Click(object sender, EventArgs e)
        {
        
           if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show("Tienes que ingresar el documento del cliente", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

           if (txtnombrecliente.Text == "")
            {
                MessageBox.Show("Tienes que agregar el nombre del cliente", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

           if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debes ingresar un producto a la venta", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

           // Hace referencia a la tabla EDETALEE_VENTA
            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IDProducto", typeof(int));
            detalle_venta.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("Subtotal", typeof(decimal));

            // LLENA LOS CAMPOS DEL DGVDATA
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_venta.Rows.Add(new object[]
                {
                    row.Cells["IDProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }

            int idcorrelactivo = new CN_Venta().obtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idcorrelactivo);
            calcularCambio();

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IDUsuario = _Usuario.IDUsuario },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtdocumentocliente.Text,
                NombreCliente = txtnombrecliente.Text,
                MontoPago = Convert.ToDecimal(txtpagacon.Text),
                MontoCambio = Convert.ToDecimal(txtcambio.Text),
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Se genero el codigo de venta:\n" + numeroDocumento +
                "\n\n¿Quieres copiar el codigo al portapapeles?", "Mensaje", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numeroDocumento);

                txtdocumentocliente.Text = "";
                txtnombrecliente.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
                txtpagacon.Text = "";
                txtcambio.Text = "";
            }
            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}