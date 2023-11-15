using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;
using CapaPresentacion.Modales;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {

        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;

        public Inicio(Usuario objUsuario)
        {
            usuarioActual = objUsuario;

            InitializeComponent();
        }

        private void iconMenuItem2_Click(object sender, EventArgs e)
        {
            mdAcercade md = new mdAcercade();
            md.ShowDialog();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IDUsuario);

            foreach(IconMenuItem iconmenu in Menu.Items){

                Boolean encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if(encontrado == false){ 
                    iconmenu.Visible = false;

                }
            }

            lblusuario.Text = usuarioActual.NombreCompleto;
        }
        private void AbrirFormulario(IconMenuItem menu,Form formulario)
        {
            if (MenuActivo != null){

                MenuActivo.BackColor = Color.White;  
            }
            menu.BackColor = Color.Gray;
            MenuActivo = menu;

            if (FormularioActivo != null) {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.DimGray;

            Contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        private void menuusuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmUsuarios());
        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new FrmCategoria());
        }

        private void submenuproducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new FrmProducto());
        }

        private void submenuregistrarventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmVentas(usuarioActual));
        }

        private void submenuverdetalleventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmDetalleVenta());
        }

        private void submenuregistrarcompr_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmCompras(usuarioActual));
        }

        private void submenuverdetalle_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmDetalleCompra());
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new Clientes());
        }

        private void menuproveedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmProveedores());
        }    

        private void lblusuario_Click(object sender, EventArgs e)
        {

        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new FrmNegocio());
        }

        private void submenureportecompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteCompras());
        }

        private void submenureporteventas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteVentas());
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Quiere cerrar sesion?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
