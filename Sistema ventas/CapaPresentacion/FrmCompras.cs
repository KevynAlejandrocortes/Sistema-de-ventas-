using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utlidades;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmCompras : Form
    {
        // Codigo bueno
        private Usuario _Usuario;
        public FrmCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }
        private void FrmCompras_Load(object sender, EventArgs e)
        {
            // Codigo bueno
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Recibo", Texto = "Recibo" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidproveedor.Text = "0";
            txtidproducto.Text = "0";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnbuscarproveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproveedor.Text = modal._Proveedor.IDProveedor.ToString();
                    txtdocproveedor.Text = modal._Proveedor.Documento.ToString();
                    txtnombreproveedor.Text = modal._Proveedor.RazonSocial.ToString();
                }
                else
                {
                    txtdocproveedor.Select();

                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IDProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo.ToString();
                    txtproducto.Text = modal._Producto.Nombre.ToString();
                    txtpreciocompra.Select();
                }
                else
                {
                    txtcodproducto.Select();
                }
            }
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            // Codigo bueno
            if (e.KeyData == Keys.Enter){

                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == 
                txtcodproducto.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtcodproducto.BackColor = System.Drawing.Color.DimGray;
                    txtidproducto.Text = oProducto.IDProducto.ToString();
                    txtproducto.Text = oProducto.Nombre;
                    txtpreciocompra.Select();
                }
                else{
                    txtcodproducto.BackColor = System.Drawing.Color.Crimson;
                    txtidproducto.Text = "0";
                    txtproducto.Text = "";                   
                }
            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Tienes que seleccionar un proucto", "Mensaje", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Precio compra
            if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio compra o formato de nomeda incorrecto", 
                "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpreciocompra.Select();
                return;
            }

            // Precio venta
            if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
            {
                MessageBox.Show("Precio venta o formato de nomeda incorrecto", 
                "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioventa.Select();
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
                dgvdata.Rows.Add(new object[] {
                    txtidproducto.Text,
                    txtproducto.Text,
                    preciocompra.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * preciocompra).ToString("0.00")
                });
                calcularTotal();
                limpiarProducto();
                txtcodproducto.Select();
            }
        }
        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtcodproducto.BackColor = System.Drawing.Color.White;
            txtproducto.Text = "";
            txtpreciocompra.Text = "";
            txtprecioventa.Text = "";
            txtcantidad.Value = 1;
        }

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

        // pintar logo eliminar
        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
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

        // evento al dar doble click
        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvdata.Rows.RemoveAt(indice);
                    calcularTotal();
                }
            }
        }

        private void txtpreciocompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) {
                e.Handled = false;
            }
            else {
               
            if(txtpreciocompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == "."){
                    e.Handled = true;
                }
                else{
                    if(Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
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

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {

                if (txtprecioventa.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidproveedor.Text) == 0)
            {
                MessageBox.Show("Debes de elegir un proveedor", 
                "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar el producto a la compra",
                "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Tabla Detalle Compra
            DataTable detalle_Compra = new DataTable();

            detalle_Compra.Columns.Add("IDProducto", typeof(int));
            detalle_Compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_Compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_Compra.Columns.Add("Cantidad", typeof(int));
            detalle_Compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvdata.Rows) {
                detalle_Compra.Rows.Add(
                    new object[] {
                        Convert.ToInt32(row.Cells["IDProducto"].Value.ToString()),
                        row.Cells["PrecioCompra"].Value.ToString(),
                        row.Cells["PrecioVenta"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString(),
                    });
            }

            int idcorrelativo = new CN_Compra().obtenerCorrelativo();
            string numerodocumento = string.Format("{0:00000}",idcorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { IDUsuario = _Usuario.IDUsuario },
                oProvedor = new Proveedor() { IDProveedor = Convert.ToInt32(txtidproveedor.Text) },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numerodocumento,
                MontoTotal = Convert.ToDecimal (txttotalpagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Registrar(oCompra,detalle_Compra, out mensaje);

            if (respuesta){
                var result = MessageBox.Show("Numero de compra generado:\n "
                + numerodocumento + "\n\n¿Enviar al portapapeles?", 
                "Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
               
                if (result == DialogResult.Yes)
                Clipboard.SetText(numerodocumento);

                txtidproveedor.Text = "0";
                txtdocproveedor.Text = "";
                txtnombreproveedor.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();               
            }
            else {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, 
                MessageBoxIcon.Exclamation);            
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}