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
        Conexion cn = new Conexion();// Instancia de la clase Conexion para manejar la conexión a la base de datos
        DataSet ds = new DataSet();// Instancia de DataSet para almacenar los datos obtenidos de la base de datos
        Boolean EstadoConexion = false;// Variable para indicar el estado de la conexión (inicialmente falsa)

        public Boolean IniciarSesion(int idUsuario, string contraseñaUsuario)// Método para iniciar sesión, recibe el idUsuario y la contraseñaUsuario como parámetros
        {
            SqlCommand Consulta;// Variable para almacenar la consulta SQL que se ejecutará
            // Crear la consulta SQL para verificar el idUsuario, contraseñaUsuario y rolUsuario en la tabla Usuario
            Consulta = new SqlCommand("select idUsuario,contraseñaUsuario,rolUsuario from Usuario where idUsuario=@idUsuario and contraseñaUsuario=@contraseñaUsuario", cn.Conectar());
            Consulta.CommandType = CommandType.Text;// Especificar que la consulta es de tipo texto
            Consulta.Parameters.AddWithValue("@idUsuario", idUsuario);// Agregar el parámetro @idUsuario con su valor correspondiente
            Consulta.Parameters.AddWithValue("@contraseñaUsuario", contraseñaUsuario);// Agregar el parámetro @contraseñaUsuario con su valor correspondiente
            Consulta.ExecuteNonQuery();// Ejecutar la consulta SQL (aunque no se espera un resultado directo, se ejecuta para llenar el DataSet posteriormente)
            try// Intentar llenar el DataSet con los resultados de la consulta y verificar las credenciales del usuario
            {
                SqlDataAdapter da = new SqlDataAdapter(Consulta);// Crear un SqlDataAdapter para llenar el DataSet con los resultados de la consulta
                da.Fill(ds, "Usuario");// Llenar el DataSet con los resultados de la consulta y nombrar la tabla como "Usuario"
                DataRow dr;// Variable para almacenar la fila de datos obtenida del DataSet
                dr = ds.Tables["Usuario"].Rows[0];// Obtener la primera fila de la tabla "Usuario" del DataSet (se asume que solo habrá una fila si las credenciales son correctas)
                // Verificar si el idUsuario, contraseñaUsuario y rolUsuario coinciden con los datos obtenidos de la base de datos
                if (Convert.ToString(idUsuario) == dr["idUsuario"].ToString() & contraseñaUsuario == dr["contraseñaUsuario"].ToString() & "Admin" == dr["rolUsuario"].ToString())
                {
                    MessageBox.Show("¡Bienvenido! Panel de administración listo para su uso.");// Mostrar un mensaje de bienvenida al usuario con rol de administrador
                    FormAdmin frm = new FormAdmin();// Crear una instancia del formulario de administración (FormAdmin)
                    frm.Show();// Mostrar el formulario de administración
                    EstadoConexion = true;// Establecer el estado de la conexión como verdadero, indicando que el inicio de sesión fue exitoso
                }
                else
                {
                    // Verificar si el idUsuario, contraseñaUsuario y rolUsuario coinciden con los datos obtenidos de la base de datos para un usuario con rol de investigador
                    if (Convert.ToString(idUsuario) == dr["idUsuario"].ToString() & contraseñaUsuario == dr["contraseñaUsuario"].ToString() & "Investigador" == dr["rolUsuario"].ToString())
                    {
                        // Obtener el idSemillero del investigador
                        SqlCommand consultaSemillero;
                        // Crear una consulta SQL para obtener el idSemillero asociado al idUsuario del investigador
                        consultaSemillero = new SqlCommand("SELECT idSemillero FROM Investigadores WHERE idUsuario = @idUsuario", cn.Conectar());
                        consultaSemillero.CommandType = CommandType.Text;// Especificar que la consulta es de tipo texto
                        consultaSemillero.Parameters.AddWithValue("@idUsuario", idUsuario);// Agregar el parámetro @idUsuario con su valor correspondiente
                        int idSemillero = Convert.ToInt32(consultaSemillero.ExecuteScalar());// Ejecutar la consulta SQL y convertir el resultado a un entero para obtener el idSemillero

                        MessageBox.Show("¡Bienvenido! Continúa impulsando tu semillero de investigación desde este panel.");// Mostrar un mensaje de bienvenida al usuario con rol de investigador
                        FormInvestigador frm = new FormInvestigador(idSemillero);// Crear una instancia del formulario de investigador (FormInvestigador) pasando el idSemillero como parámetro para personalizar la experiencia del usuario
                        frm.Show();// Mostrar el formulario de investigador
                        EstadoConexion = true;// Establecer el estado de la conexión como verdadero, indicando que el inicio de sesión fue exitoso para un usuario con rol de investigador
                    }
                }
            }
            catch (Exception e)// Capturar cualquier error que ocurra durante el proceso de inicio de sesión y mostrar un mensaje de error
            {
                MessageBox.Show("Usuario/Contraseña incorrectos", "GestionSemillero", MessageBoxButtons.OK, MessageBoxIcon.Warning);// Mostrar un mensaje de advertencia al usuario indicando que las credenciales son incorrectas
            }
            finally// Finalmente, cerrar la conexión a la base de datos para liberar recursos
            {
                cn.Cerrar();// Cerrar la conexión a la base de datos
            }
            return EstadoConexion;// Devolver el estado de la conexión, indicando si el inicio de sesión fue exitoso o no
        }
    }
}
