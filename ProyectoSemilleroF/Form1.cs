using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoSemilleroF
{
    public partial class Form1 : Form
    {
        Consultas cta = new Consultas();//Instancia de la clase Consultas para acceder a sus métodos
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox4.Text == "")//Validación para asegurarse de que los campos de texto no estén vacíos
            {
                // Si alguno de los campos está vacío, se muestra un mensaje de advertencia al usuario
                MessageBox.Show("Es obligatorio ingresar toda la informacion del login", "GestionSemillero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Si ambos campos están llenos, se llama al método IniciarSesion de la clase Consultas, pasando el número de documento y la contraseña ingresados por el usuario
                cta.IniciarSesion(int.Parse(guna2TextBox3.Text), guna2TextBox4.Text);
                this.Hide();
            }
        }
    }
}
