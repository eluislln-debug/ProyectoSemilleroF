using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoSemilleroF
{
    internal class Consultas
    {
        Conexion cn = new Conexion();
        DataSet ds = new DataSet();
        Boolean EstadoConexion = false;

        public Boolean IniciarSesion(int idUsuario, string contraseñaUsuario)
        {
            SqlCommand Consulta;
            Consulta = new SqlCommand("select idUsuario,contraseñaUsuario,rolUsuario from Usuario where idUsuario=@idUsuario and contraseñaUsuario=@contraseñaUsuario", cn.Conectar());
            Consulta.CommandType = CommandType.Text;
            Consulta.Parameters.AddWithValue("@idUsuario", idUsuario);
            Consulta.Parameters.AddWithValue("@contraseñaUsuario", contraseñaUsuario);
            Consulta.ExecuteNonQuery();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta);
                da.Fill(ds, "Usuario");
                DataRow dr;
                dr = ds.Tables["Usuario"].Rows[0];
                if (Convert.ToString(idUsuario) == dr["idUsuario"].ToString() & contraseñaUsuario == dr["contraseñaUsuario"].ToString() & "Admin" == dr["rolUsuario"].ToString())
                {
                    MessageBox.Show("¡Bienvenido! Panel de administración listo para su uso.");
                    FormAdmin frm = new FormAdmin();
                    frm.Show();
                    EstadoConexion = true;
                }
                //else
                //{
                    //if (Convert.ToString(idUsuario) == dr["idUsuario"].ToString() & contraseñaUsuario == dr["contraseñaUsuario"].ToString() & "Investigador" == dr["rolUsuario"].ToString())
                    //{
                       //MessageBox.Show("¡Bienvenido! Continúa impulsando tu semillero de investigación desde este panel.");
                        //FormInvestigador frm = new FormInvestigador();
                        //frm.Show();
                        //EstadoConexion = true;
                    //}
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Usuario/Contraseña incorrectos", "GestionSemillero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Cerrar();
            }
            return EstadoConexion;
        }
    }
}
