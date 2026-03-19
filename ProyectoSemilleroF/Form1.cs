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
        Consultas cta = new Consultas();
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" || guna2TextBox4.Text == "")
            {
                MessageBox.Show("Es obligatorio ingresar toda la informacion del login", "GestionSemillero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cta.IniciarSesion(int.Parse(guna2TextBox3.Text), guna2TextBox4.Text);
                this.Hide();
            }
        }
    }
}

