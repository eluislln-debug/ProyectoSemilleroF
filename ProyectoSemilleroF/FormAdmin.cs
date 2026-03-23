using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSemilleroF
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Visible = true;

            // Hace visible el GroupBox de gestión
            GestionarUsuariosGroupBox.Visible = true;

            // --- NUEVO: Hace visible el GroupBox de Datos del Usuario ---
            guna2GroupBox1.Visible = true;

            // Los trae al frente para asegurar que nada los tape
            guna2DataGridView1.BringToFront();
            GestionarUsuariosGroupBox.BringToFront();
            guna2GroupBox1.BringToFront();
        }
    }
}




