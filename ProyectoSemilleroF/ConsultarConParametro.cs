using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSemilleroF
{
    internal class ConsultarConParametro
    {
        //Metodo para cargar los parametros de busqueda en el comboBox dependiendo del tipo de consulta que se quiera realizar
        public static void CargarParametros(string tipo, ComboBox cbo)
        {
            cbo.Items.Clear();//Limpiar el comboBox antes de cargar los nuevos parametros

            if (tipo == "Usuario")//Si el tipo de consulta es "Usuario", se cargan los parametros correspondientes a la tabla Usuario
            {
                //Agregar un item vacio para que el usuario pueda seleccionar la opcion de no filtrar por ese parametro
                cbo.Items.AddRange(new string[] { "", "idUsuario", "nombreUsuario", "apellidoUsuario", "emailUsuario", "telefonoUsuario", "estadoUsuario", "rolUsuario" });
            }
            else if (tipo == "Investigadores")
            {
                cbo.Items.AddRange(new string[] { "", "idInvestigador", "tipoInvestigador", "nombreInvestigador", "emailInvestigador", "telefonoInvestigador", "generoInvestigador" });
            }
            else if (tipo == "Semillero")
            {
                cbo.Items.AddRange(new string[] { "", "idSemillero", "nombreSemillero", "lineaInvestigacion", "estadoSemillero", "descripcionSemillero" });
            }
            else if (tipo == "Proyecto")
            {
                cbo.Items.AddRange(new string[] { "", "idProyecto", "tituloProyecto", "estadoProyecto", "duracionProyecto", "descripcionProyecto" });
            }
            else if (tipo == "Fase")
            {
                cbo.Items.AddRange(new string[] { "", "idFase", "descripcionFase", "estadoFase", "idProyecto" });
            }
            else if (tipo == "Actividad")
            {
                cbo.Items.AddRange(new string[] { "", "idActividad", "nombreActividad", "descripActividad", "estadoActividad", "lugarActividad" });
            }
            else if (tipo == "Eventos")
            {
                cbo.Items.AddRange(new string[] { "", "idEvento", "nombreEvento", "tipoEvento", "lugarEvento", "horaEvento", "descripcionEvento" });
            }
            else if (tipo == "Patrocinadores")
            {
                cbo.Items.AddRange(new string[] { "", "idPatrocinador", "nombrePatrocinador", "tipoPatrocinador", "correoPatrocinador", "telefonoPatrocinador", "direccionPatrocinador" });
            }
            else if (tipo == "Reporte")
            {
                cbo.Items.AddRange(new string[] { "", "idReporte", "tipoReporte", "fechaReporte", "horaReporte", "formatoReporte" });
            }
            else if (tipo == "Reunion")
            {
                cbo.Items.AddRange(new string[] { "", "idReunion", "motivoReunion", "estadoReunion", "lugarReunion", "fechaReunion" });
            }
            else if (tipo == "SemilleroEventos")
            {
                cbo.Items.AddRange(new string[] { "", "idSemillero", "idEvento" });
            }
        }

        //Metodo para consultar los datos de una tabla segun un parametro de busqueda, se utiliza el operador LIKE para permitir busquedas parciales
        public static DataTable ConsultarParametro(string tabla, string columna, string valor, Conexion conexion)
        {
            DataTable datatable = new DataTable();//Crear un DataTable para almacenar los resultados de la consulta
            try//Utilizar un bloque try-catch para manejar cualquier error que pueda ocurrir durante la consulta
            {
                SqlCommand consulta = new SqlCommand(
                    $"SELECT * FROM {tabla} WHERE {columna} LIKE '%' + @valor + '%'",
                    conexion.Conectar());//Crear un SqlCommand con la consulta SQL, utilizando parámetros para evitar inyecciones SQL
                consulta.Parameters.AddWithValue("@valor", valor);//Agregar el valor del parámetro a la consulta
                SqlDataAdapter datadapter = new SqlDataAdapter(consulta);//Crear un SqlDataAdapter para ejecutar la consulta y llenar el DataTable con los resultados
                datadapter.Fill(datatable);//Llenar el DataTable con los resultados de la consulta
                conexion.Cerrar();//Cerrar la conexión a la base de datos
            }
            catch (Exception ex)//Capturar cualquier error que ocurra durante la consulta y mostrar un mensaje de error
            {
                MessageBox.Show("Error en la consulta: " + ex.Message);//Mostrar el mensaje de error al usuario
            }
            return datatable;//Devolver el DataTable con los resultados de la consulta
        }

        //Metodo para consultar los datos de una tabla segun un parametro de busqueda y el id del semillero, se utiliza el operador LIKE para permitir busquedas parciales
        public static DataTable ConsultarParametroSegunSemillero(string tabla, string columna, string valor, int idSemillero, Conexion conexion)
        {
            DataTable datatable = new DataTable();//Crear un DataTable para almacenar los resultados de la consulta
            SqlCommand consulta = new SqlCommand();//Crear un SqlCommand para ejecutar la consulta SQL, utilizando parámetros para evitar inyecciones SQL

            if (tabla == "Investigadores")//Si la tabla a consultar es "Investigadores", se agrega una condicion para filtrar por el id del semillero
            {
                //La consulta SQL utiliza el operador LIKE para permitir busquedas parciales, y se agrega una condicion para filtrar por el id del semillero
                consulta = new SqlCommand(
                    $"SELECT * FROM Investigadores WHERE {columna} LIKE '%' + @valor + '%' AND idSemillero = @idSemillero",
                    conexion.Conectar());
            }
            else if (tabla == "Semillero")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Semillero WHERE {columna} LIKE '%' + @valor + '%' AND idSemillero = @idSemillero",
                    conexion.Conectar());
            }
            else if (tabla == "Proyecto")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Proyecto WHERE {columna} LIKE '%' + @valor + '%' AND idSemillero = @idSemillero",
                    conexion.Conectar());
            }
            else if (tabla == "Fase")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Fase WHERE {columna} LIKE '%' + @valor + '%' AND idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero)",
                    conexion.Conectar());
            }
            else if (tabla == "Actividad")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Actividad WHERE {columna} LIKE '%' + @valor + '%' AND idFase IN (SELECT idFase FROM Fase WHERE idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero))",
                    conexion.Conectar());
            }
            else if (tabla == "Eventos")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Eventos WHERE {columna} LIKE '%' + @valor + '%' AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)",
                    conexion.Conectar());
            }
            else if (tabla == "Patrocinadores")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Patrocinadores WHERE {columna} LIKE '%' + @valor + '%' AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)",
                    conexion.Conectar());
            }
            else if (tabla == "Reporte")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Reporte WHERE {columna} LIKE '%' + @valor + '%' AND idReporte IN (SELECT idReporte FROM Usuario WHERE idUsuario IN (SELECT idUsuario FROM Investigadores WHERE idSemillero = @idSemillero))",
                    conexion.Conectar());
            }
            else if (tabla == "Reunion")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM Reunion WHERE {columna} LIKE '%' + @valor + '%' AND idReunion IN (SELECT idReunion FROM Semillero WHERE idSemillero = @idSemillero)",
                    conexion.Conectar());
            }
            else if (tabla == "SemilleroEventos")
            {
                consulta = new SqlCommand(
                    $"SELECT * FROM SemilleroEventos WHERE {columna} LIKE '%' + @valor + '%' AND idSemillero = @idSemillero",
                    conexion.Conectar());
            }

            try//Utilizar un bloque try-catch para manejar cualquier error que pueda ocurrir durante la consulta
            {
                consulta.Parameters.AddWithValue("@valor", valor);//Agregar el valor del parámetro a la consulta
                consulta.Parameters.AddWithValue("@idSemillero", idSemillero);//Agregar el id del semillero como parámetro a la consulta
                SqlDataAdapter datadapter = new SqlDataAdapter(consulta);//Crear un SqlDataAdapter para ejecutar la consulta y llenar el DataTable con los resultados
                datadapter.Fill(datatable);//Llenar el DataTable con los resultados de la consulta
                conexion.Cerrar();//Cerrar la conexión a la base de datos
            }
            catch (Exception ex)//Capturar cualquier error que ocurra durante la consulta y mostrar un mensaje de error
            {
                MessageBox.Show("Error en la consulta: " + ex.Message);//Mostrar el mensaje de error al usuario
            }

            return datatable;//Devolver el DataTable con los resultados de la consulta
        }
    }
}
