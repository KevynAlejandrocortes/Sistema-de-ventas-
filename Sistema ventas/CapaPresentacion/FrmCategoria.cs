using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utlidades;
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
    public partial class FrmCategoria : Form
    {
        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {

                if (columna.Visible == true && columna.Name != "BtnSeleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;




            // Mostrar todos los usuarios
            List<Categoria> lista = new CN_Categoria().Listar();

            foreach (Categoria item in lista)
            {
                dgvdata.Rows.Add(new object[] {"",item.IDCategoria,
                     item.Descripcion,
                     item.Estado == true ? 1 : 0 ,
                     item.Estado == true ? "Activo" : "No activo"
            });

            }
        }

        // Boton guardar 
        private void btnguardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Categoria objCategoria = new Categoria()
            {
                IDCategoria = Convert.ToInt32(txtid.Text),
                Descripcion = txtdescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };


            if (objCategoria.IDCategoria == 0)
            {

                int idCategoriagenerado = new CN_Categoria().Registrar(objCategoria, out Mensaje);


                if (idCategoriagenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idCategoriagenerado,txtdescripcion.Text,
                ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
                });
                    limpiar();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }
            }

            else
            {
                bool Resultado = new CN_Categoria().Editar(objCategoria, out Mensaje);

                if (Resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["IDU"].Value = txtid.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                    limpiar();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }
            }
        }
        private void limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtdescripcion.Text = "";
            cboestado.SelectedIndex = 0;

            txtdescripcion.Select();

        }


        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));
                e.Handled = true;

            }
        }


        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "BtnSeleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {


                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["IDU"].Value.ToString();
                    txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value.ToString();


                    // ComboBox Estado
                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;

                        }
                    }
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Está seguro de eliminar la categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string Mensaje = string.Empty;

                    Categoria objCategoria = new Categoria()
                    {
                        IDCategoria = Convert.ToInt32(txtid.Text),

                    };


                    bool Respuesta = new CN_Categoria().Eliminar(objCategoria, out Mensaje);



                    if (Respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }

            }


        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {

                row.Visible = true;
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
    

