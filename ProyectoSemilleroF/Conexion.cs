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
        SqlConnection con;// Variable para la conexión a la base de datos
        public SqlConnection Conectar()
        {
            try// Intentar establecer la conexión a la base de datos
            {
                // Cadena de conexión con los detalles del servidor, base de datos y autenticación
                con = new SqlConnection("data source= LAPTOP-PG0TD0OR\\MKKOR6P;Initial Catalog=ProyectoFinal6toTRI; Integrated Security=true");
                con.Open();// Abrir la conexión
            }
            catch (Exception e)// Capturar cualquier error que ocurra durante la conexión y mostrar un mensaje de error
            {
                MessageBox.Show(e.Message);// Mostrar el mensaje de error al usuario
            }
            return con;// Devolver la conexión establecida (o nula si hubo un error)
        }

        public void Cerrar()// Método para cerrar la conexión a la base de datos
        {
            con.Close();// Cerrar la conexión a la base de datos
        }
    }
}
