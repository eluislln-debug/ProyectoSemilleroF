using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSemilleroF
{
    public partial class FormAdmin : Form
    {
        Conexion cn = new Conexion();
        DataSet ds = new DataSet();
        public FormAdmin()
        {
            InitializeComponent();
        }

        public void ConsultarUser()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("Select * from Usuario", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Usuario");
            try
            {
                dataGridView1.DataMember = ("Usuario");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ConsultarPatro()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("select * from Patrocinadores", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Patrocinadores");
            try
            {
                dataGridView1.DataMember = ("Patrocinadores");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ConsultarReunion()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("select * from Reunion", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Reunion");
            try
            {
                dataGridView1.DataMember = ("Reunion");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SemilleroConsulta()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("select * from Semillero", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Semillero");
            try
            {
                dataGridView1.DataMember = ("Semillero");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ReporteConsulta()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("select * from Reporte", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Reporte");
            try
            {
                dataGridView1.DataMember = ("Reporte");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ConsultarEvento()
        {
            SqlCommand consultausuarios;
            consultausuarios = new SqlCommand("select * from Eventos", cn.Conectar());
            consultausuarios.CommandType = CommandType.Text;
            consultausuarios.ExecuteNonQuery();
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);
            da.Fill(ds, "Eventos");
            try
            {
                dataGridView1.DataMember = ("Eventos");
                dataGridView1.DataSource = ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void insertarusuario(int idUsuario, string nombreUsuario, string apellidoUsuario, string contraseñaUsuario, string emailUsuario, int telefonoUsuario, string estadoUsuario, string rolUsuario, int idReporte)
        {
            SqlCommand insertarusaurio;
            try
            {
                insertarusaurio = new SqlCommand("insert into Usuario (idUsuario,nombreUsuario,apellidoUsuario,contraseñaUsuario,emailUsuario,telefonoUsuario,estadoUsuario,rolUsuario,idReporte) values (@idUsuario,@nombreUsuario,@apellidoUsuario,@contraseñaUsuario,@emailUsuario,@telefonoUsuario,@estadoUsuario,@rolUsuario,@idReporte)", cn.Conectar());
                insertarusaurio.CommandType = CommandType.Text;
                insertarusaurio.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = idUsuario;
                insertarusaurio.Parameters.AddWithValue("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
                insertarusaurio.Parameters.AddWithValue("@apellidoUsuario", SqlDbType.VarChar).Value = apellidoUsuario;
                insertarusaurio.Parameters.AddWithValue("@contraseñaUsuario", SqlDbType.VarChar).Value = contraseñaUsuario;
                insertarusaurio.Parameters.AddWithValue("@emailUsuario", SqlDbType.VarChar).Value = emailUsuario;
                insertarusaurio.Parameters.AddWithValue("@telefonoUsuario", SqlDbType.Int).Value = telefonoUsuario;
                insertarusaurio.Parameters.AddWithValue("@estadoUsuario", SqlDbType.VarChar).Value = estadoUsuario;
                insertarusaurio.Parameters.AddWithValue("@rolUsuario", SqlDbType.VarChar).Value = rolUsuario;
                insertarusaurio.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = idReporte;
                insertarusaurio.ExecuteNonQuery();
                MessageBox.Show("Usuario registrado exitosamente", "ProyectoSemillero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConsultarUsuarios_Click(object sender, EventArgs e)
        {
            ConsultarUser();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            insertarusuario(int.Parse(txtIdUsuario.Text), txtNombreUsuario.Text, txtApellidoUsuario.Text, txtContraseñaUsuario.Text, txtEmailUsuario.Text, int.Parse(txtTelefonoUsuario.Text), txtEstadoUsuario.Text, RolComboBox.SelectedItem.ToString(), int.Parse(txtIdReporte.Text));
            ConsultarUser();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = true;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnSemilleros_Click(object sender, EventArgs e)
        {
            SemilleroGroupBox.Visible = true;
            UsuariosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = true;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnReuniones_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = true;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = true;
            PatrocinadoresGroupBox.Visible = false;
        }

        private void btnPatrocinadores_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = true;
        }

        private void btnCerrarsesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesion?", "SemilleroProyecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnConsultarpatrocinadores_Click(object sender, EventArgs e)
        {
            ConsultarPatro();
        }

        private void btnReunionconsulta_Click(object sender, EventArgs e)
        {
            ConsultarReunion();
        }

        private void btnConsultarSemillero_Click(object sender, EventArgs e)
        {
            SemilleroConsulta();
        }

        private void ReporteGroupBox_Click(object sender, EventArgs e)
        {

        }

        private void btnConsultarReporte_Click(object sender, EventArgs e)
        {
            ReporteConsulta();
        }

        private void btnEventosConsulta_Click(object sender, EventArgs e)
        {
            ConsultarEvento();
        }
    }
}




