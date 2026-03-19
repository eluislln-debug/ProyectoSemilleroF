using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoSemilleroF
{
    internal class Conexion
    {
        SqlConnection con;
        public SqlConnection Conectar()
        {
            try
            {
                con = new SqlConnection("data source= DESKTOP-MKKOR6P;Initial Catalog=ProyectoFinal6toTRI; Integrated Security=true");
                con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return con;
        }

        public void Cerrar()
        {
            con.Close();
        }
    }
}
