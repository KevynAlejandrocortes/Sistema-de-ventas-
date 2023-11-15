using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

           
            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.Documento == txtdocumento.Text && u.Clave
            == txtclave.Text).FirstOrDefault(); 


            if(oUsuario != null){

                Inicio form = new Inicio(oUsuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else{

                MessageBox.Show("No se encontro el usuario", "Mensaje", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);     
            }
            
                 
            


        }
        // Esto sirve para que no se oculte el login al cerrar el ingreso
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            // Para limpiar las cajas de texto al ingresar
            txtdocumento.Text = "";
            txtclave.Text = "";

            this.Show();
        }
    }
}
