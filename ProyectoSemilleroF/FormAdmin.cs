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
        Conexion cn = new Conexion();// Instancia de la clase Conexion para manejar la conexión a la base de datos
        DataSet ds = new DataSet();// DataSet para almacenar los datos consultados de la base de datos
        public FormAdmin()
        {
            InitializeComponent();
        }

        public void EliminarUsuario(int idUsuario)// Método para eliminar un usuario de la base de datos
        {
            SqlCommand eliminar;
            eliminar = new SqlCommand("delete from Usuario where idUsuario= @idUsuario", cn.Conectar());// Comando SQL para eliminar un usuario basado en su ID
            eliminar.CommandType = CommandType.Text;// Especifica que el comando es de tipo texto
            eliminar.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = idUsuario;// Agrega el parámetro del ID del usuario al comando SQL
            if (MessageBox.Show("¿Estas seguro que desea eliminar el registro de usuario?", "SemilleroProyecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == System.Windows.Forms.DialogResult.Yes)// Muestra un mensaje de confirmación antes de eliminar el usuario
            {
                eliminar.ExecuteNonQuery();// Ejecuta el comando SQL para eliminar el usuario
                MessageBox.Show("El usuario sera eliminado");// Muestra un mensaje indicando que el usuario ha sido eliminado
                ConsultarUser();// Refresca el DataGridView para mostrar los cambios después de eliminar el usuario
            }
        }

        public void ConsultarUser()// Método para consultar los usuarios de la base de datos y mostrar los resultados en un DataGridView
        {
            SqlCommand consultausuarios;// Declara un objeto SqlCommand para ejecutar la consulta SQL
            consultausuarios = new SqlCommand("Select * from Usuario", cn.Conectar());// Comando SQL para seleccionar todos los registros de la tabla Usuario
            consultausuarios.CommandType = CommandType.Text;// Especifica que el comando es de tipo texto
            consultausuarios.ExecuteNonQuery();// Ejecuta el comando SQL para obtener los datos de los usuarios
            ds.Clear();// Limpia el DataSet antes de llenarlo con los nuevos datos
            SqlDataAdapter da = new SqlDataAdapter(consultausuarios);// Crea un SqlDataAdapter para llenar el DataSet con los resultados de la consulta SQL
            da.Fill(ds, "Usuario");// Llena el DataSet con los datos obtenidos de la consulta SQL y los asigna a una tabla llamada "Usuario"
            try// Intenta mostrar los datos en el DataGridView
            {
                dataGridView1.DataMember = ("Usuario");// Especifica que el DataGridView debe mostrar los datos de la tabla "Usuario" del DataSet
                dataGridView1.DataSource = ds;// Asigna el DataSet como fuente de datos del DataGridView para mostrar los resultados de la consulta SQL
            }
            catch (Exception e)// Captura cualquier excepción que ocurra al intentar mostrar los datos en el DataGridView y muestra un mensaje de error
            {
                MessageBox.Show(e.Message);// Muestra el mensaje de error en caso de que ocurra una excepción
            }
        }

        public void ConsultarInvestigadores()// Método para consultar los investigadores de la base de datos y mostrar los resultados en un DataGridView
        {
            SqlCommand consultainvestigador;// Declara un objeto SqlCommand para ejecutar la consulta SQL
            consultainvestigador = new SqlCommand("Select * from Investigadores", cn.Conectar());// Comando SQL para seleccionar todos los registros de la tabla Investigadores
            consultainvestigador.CommandType = CommandType.Text;// Especifica que el comando es de tipo texto
            consultainvestigador.ExecuteNonQuery();// Ejecuta el comando SQL para obtener los datos de los investigadores
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consultainvestigador);// Crea un SqlDataAdapter para llenar el DataSet con los resultados de la consulta SQL
            da.Fill(ds, "Investigadores");// Llena el DataSet con los datos obtenidos de la consulta SQL y los asigna a una tabla llamada "Investigadores"
            try// Intenta mostrar los datos en el DataGridView
            {
                dataGridView1.DataMember = ("Investigadores");// Especifica que el DataGridView debe mostrar los datos de la tabla "Investigadores" del DataSet
                dataGridView1.DataSource = ds;// Asigna el DataSet como fuente de datos del DataGridView para mostrar los resultados de la consulta SQL
            }
            catch (Exception e)// Captura cualquier excepción que ocurra al intentar mostrar los datos en el DataGridView y muestra un mensaje de error
            {
                MessageBox.Show(e.Message);// Muestra el mensaje de error en caso de que ocurra una excepción
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
            SqlCommand insertarusaurio;// Declara un objeto SqlCommand para ejecutar el comando SQL de inserción
            try// Intenta insertar un nuevo usuario en la base de datos
            {
                // Comando SQL para insertar un nuevo registro en la tabla Usuario con los valores proporcionados como parámetros
                insertarusaurio = new SqlCommand("insert into Usuario (idUsuario,nombreUsuario,apellidoUsuario,contraseñaUsuario,emailUsuario,telefonoUsuario,estadoUsuario,rolUsuario,idReporte) values (@idUsuario,@nombreUsuario,@apellidoUsuario,@contraseñaUsuario,@emailUsuario,@telefonoUsuario,@estadoUsuario,@rolUsuario,@idReporte)", cn.Conectar());
                insertarusaurio.CommandType = CommandType.Text;// Especifica que el comando es de tipo texto
                // Agrega los parámetros al comando SQL con sus respectivos tipos de datos y valores
                insertarusaurio.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = idUsuario;
                insertarusaurio.Parameters.AddWithValue("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
                insertarusaurio.Parameters.AddWithValue("@apellidoUsuario", SqlDbType.VarChar).Value = apellidoUsuario;
                insertarusaurio.Parameters.AddWithValue("@contraseñaUsuario", SqlDbType.VarChar).Value = contraseñaUsuario;
                insertarusaurio.Parameters.AddWithValue("@emailUsuario", SqlDbType.VarChar).Value = emailUsuario;
                insertarusaurio.Parameters.AddWithValue("@telefonoUsuario", SqlDbType.Int).Value = telefonoUsuario;
                insertarusaurio.Parameters.AddWithValue("@estadoUsuario", SqlDbType.VarChar).Value = estadoUsuario;
                insertarusaurio.Parameters.AddWithValue("@rolUsuario", SqlDbType.VarChar).Value = rolUsuario;
                insertarusaurio.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = idReporte;
                insertarusaurio.ExecuteNonQuery();// Ejecuta el comando SQL para insertar el nuevo usuario en la base de datos
                // Muestra un mensaje indicando que el usuario ha sido registrado exitosamente
                MessageBox.Show("Usuario registrado exitosamente", "ProyectoSemillero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)// Captura cualquier excepción que ocurra al intentar insertar el nuevo usuario en la base de datos y muestra un mensaje de error
            {
                MessageBox.Show(e.Message);// Muestra el mensaje de error en caso de que ocurra una excepción
            }
        }

        public void insertarInvestigador(int idInvestigador, string tipoInvestigador, string nombreInvestigador, string emailInvestigador, int telefonoInvestigador, string generoInvestigador, int idUsuario, int idSemillero)
        {
            SqlCommand insertarInvest;// Declara un objeto SqlCommand para ejecutar el comando SQL de inserción
            try
            {
                insertarInvest = new SqlCommand("insert into Investigadores (idInvestigador,tipoInvestigador,nombreInvestigador,emailInvestigador,telefonoInvestigador,generoInvestigador,idUsuario,idSemillero) values (@idInvestigador,@tipoInvestigador,@nombreInvestigador,@emailInvestigador,@telefonoInvestigador,@generoInvestigador,@idUsuario,@idSemillero)", cn.Conectar());
                insertarInvest.CommandType = CommandType.Text;
                insertarInvest.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = idInvestigador;
                insertarInvest.Parameters.AddWithValue("@tipoInvestigador", SqlDbType.VarChar).Value = tipoInvestigador;
                insertarInvest.Parameters.AddWithValue("@nombreInvestigador", SqlDbType.VarChar).Value = nombreInvestigador;
                insertarInvest.Parameters.AddWithValue("@emailInvestigador", SqlDbType.VarChar).Value = emailInvestigador;
                insertarInvest.Parameters.AddWithValue("@telefonoInvestigador", SqlDbType.Int).Value = telefonoInvestigador;
                insertarInvest.Parameters.AddWithValue("@generoInvestigador", SqlDbType.VarChar).Value = generoInvestigador;
                insertarInvest.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = idUsuario;
                insertarInvest.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = idSemillero;
                insertarInvest.ExecuteNonQuery();
                MessageBox.Show("Investigador registrado exitosamente", "ProyectoSemillero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void InsertarReporte(int idReporte, string tipoReporte, DateTime fechaReporte, string horaReporte, string formatoReporte)
        {
            SqlCommand insertarReporte;
            try
            {
                insertarReporte = new SqlCommand("INSERT INTO Reporte (idReporte,tipoReporte,fechaReporte,horaReporte,formatoReporte) VALUES (@idReporte,@tipoReporte,@fechaReporte,@horaReporte,@formatoReporte)", cn.Conectar());
                insertarReporte.CommandType = CommandType.Text;
                insertarReporte.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = idReporte;
                insertarReporte.Parameters.AddWithValue("@tipoReporte", SqlDbType.VarChar).Value = tipoReporte;
                insertarReporte.Parameters.AddWithValue("@fechaReporte", SqlDbType.Date).Value = fechaReporte;
                insertarReporte.Parameters.AddWithValue("@horaReporte", SqlDbType.VarChar).Value = horaReporte;
                insertarReporte.Parameters.AddWithValue("@formatoReporte", SqlDbType.VarChar).Value = formatoReporte;
                insertarReporte.ExecuteNonQuery();
                MessageBox.Show("Reporte registrado exitosamente", "ProyectoSemillero", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ActualizarUsuario(int idUsuario, string nombreUsuario, string apellidoUsuario,
                                string contraseñaUsuario, string emailUsuario, int telefonoUsuario,
                                string estadoUsuario, string rolUsuario, int idReporte)
        {
            SqlCommand actualizarUsuario;// Declara un objeto SqlCommand para ejecutar el comando SQL de actualización
            try// Intenta actualizar la información de un usuario en la base de datos
            {
                // Comando SQL para actualizar los campos de un registro en la tabla Usuario basado en el ID del usuario
                actualizarUsuario = new SqlCommand("UPDATE Usuario SET " +
                    "nombreUsuario = @nombreUsuario, " +
                    "apellidoUsuario = @apellidoUsuario, " +
                    "contraseñaUsuario = @contraseñaUsuario, " +
                    "emailUsuario = @emailUsuario, " +
                    "telefonoUsuario = @telefonoUsuario, " +
                    "estadoUsuario = @estadoUsuario, " +
                    "rolUsuario = @rolUsuario, " +
                    "idReporte = @idReporte " +
                    "WHERE idUsuario = @idUsuario", cn.Conectar());

                // Especifica que el comando es de tipo texto
                actualizarUsuario.CommandType = CommandType.Text;
                // Agrega los parámetros al comando SQL con sus respectivos tipos de datos y valores
                actualizarUsuario.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = idUsuario;
                actualizarUsuario.Parameters.AddWithValue("@nombreUsuario", SqlDbType.VarChar).Value = nombreUsuario;
                actualizarUsuario.Parameters.AddWithValue("@apellidoUsuario", SqlDbType.VarChar).Value = apellidoUsuario;
                actualizarUsuario.Parameters.AddWithValue("@contraseñaUsuario", SqlDbType.VarChar).Value = contraseñaUsuario;
                actualizarUsuario.Parameters.AddWithValue("@emailUsuario", SqlDbType.VarChar).Value = emailUsuario;
                actualizarUsuario.Parameters.AddWithValue("@telefonoUsuario", SqlDbType.Int).Value = telefonoUsuario;
                actualizarUsuario.Parameters.AddWithValue("@estadoUsuario", SqlDbType.VarChar).Value = estadoUsuario;
                actualizarUsuario.Parameters.AddWithValue("@rolUsuario", SqlDbType.VarChar).Value = rolUsuario;
                actualizarUsuario.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = idReporte;
                actualizarUsuario.ExecuteNonQuery();// Ejecuta el comando SQL para actualizar la información del usuario en la base de datos

                MessageBox.Show("Usuario actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);// Muestra un mensaje indicando que el usuario ha sido actualizado exitosamente
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);// Muestra el mensaje de error en caso de que ocurra una excepción al intentar actualizar la información del usuario en la base de datos
            }
        }

        public void ValidacionNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ':')// Permite solo dígitos, tecla de retroceso y dos puntos (para campos de hora)
            {
                // Si el carácter ingresado no es un dígito, retroceso o dos puntos, se marca como manejado para evitar que se ingrese en el campo
                e.Handled = true;
                MessageBox.Show("Solo se permiten números",
                    "Caracter invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConsultarUsuarios_Click(object sender, EventArgs e)
        {
            ConsultarUser();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtIdUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContraseñaUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtEmailUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoUsuario.Text) ||
                RolComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtIdReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "ProyectoSemillero",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                insertarusuario(int.Parse(txtIdUsuario.Text), txtNombreUsuario.Text, txtApellidoUsuario.Text, txtContraseñaUsuario.Text, txtEmailUsuario.Text, int.Parse(txtTelefonoUsuario.Text), txtEstadoUsuario.Text, RolComboBox.SelectedItem.ToString(), int.Parse(txtIdReporte.Text));
                ConsultarUser();
            }
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Al cargar el formulario, se ocultan todos los GroupBox para mostrar solo la sección de usuarios por defecto
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón de usuarios, se muestra el GroupBox de usuarios y se ocultan los demás GroupBox para mostrar solo la sección de usuarios
            UsuariosGroupBox.Visible = true;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnSemilleros_Click(object sender, EventArgs e)
        {
            SemilleroGroupBox.Visible = true;
            UsuariosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = true;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnReuniones_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = true;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = true;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnPatrocinadores_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = true;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
        }

        private void btnCerrarsesion_Click(object sender, EventArgs e)
        {
            // Muestra un mensaje de confirmación antes de cerrar la sesión y salir de la aplicación
            if (MessageBox.Show("¿Desea cerrar sesion?", "SemilleroProyecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Application.Exit();// Cierra la aplicación si el usuario confirma que desea cerrar sesión
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

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Porfavor indique el id del usuario que desea eliminar", "SemilleroProyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                EliminarUsuario(int.Parse(txtID.Text));
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)// Verifica que se haya hecho clic en una fila válida del DataGridView
            {
                // Al hacer clic en una celda del DataGridView, se llenan los campos de texto con los valores de la fila seleccionada para facilitar la edición o eliminación del registro
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDinvest.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDrepo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDsem.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDevent.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDpatro.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDreu.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void txtIdUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtTelefonoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtIdUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContraseñaUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtEmailUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoUsuario.Text) ||
                RolComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtIdReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActualizarUsuario(
                int.Parse(txtIdUsuario.Text),
                txtNombreUsuario.Text,
                txtApellidoUsuario.Text,
                txtContraseñaUsuario.Text,
                txtEmailUsuario.Text,
                int.Parse(txtTelefonoUsuario.Text),
                txtEstadoUsuario.Text,
                RolComboBox.SelectedItem.ToString(),
                int.Parse(txtIdReporte.Text)
            );

            ConsultarUser(); // Refresca el DataGridView
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            txtIdUsuario.Clear();
            txtNombreUsuario.Clear();
            txtApellidoUsuario.Clear();
            txtContraseñaUsuario.Clear();
            txtEmailUsuario.Clear();
            txtTelefonoUsuario.Clear();
            txtEstadoUsuario.Clear();
            txtIdReporte.Clear();
            RolComboBox.SelectedIndex = -1;
        }

        private void btnProyectos_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = false;
            ProyectosGroupBox.Visible = true;
        }

        private void btnInvestigadores_Click(object sender, EventArgs e)
        {
            UsuariosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReporteGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            InvestigadoresGroupBox.Visible = true;
            ProyectosGroupBox.Visible = false;
        }

        private void btnConsultarInvest_Click(object sender, EventArgs e)
        {
            ConsultarInvestigadores();
        }

        private void btnEliminarInvest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDinvest.Text))
            {
                MessageBox.Show("Porfavor indique el id del investigador que desea eliminar", "SemilleroProyecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                eliminarInvestigador(int.Parse(txtIDinvest.Text));
            }
        }

        public void eliminarInvestigador(int idInvestigador)
        {
            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Investigadores WHERE idInvestigador = @idInvestigador", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = idInvestigador;
            if (MessageBox.Show("¿Estás seguro que desea eliminar el registro del investigador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Investigador eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarInvestigadores(); // Refresca el DataGridView después de eliminar
            }
        }

        private void btnGuardarInvestigador_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtidInvestigadores.Text) ||
                string.IsNullOrWhiteSpace(txtTipoInvestigadores.Text) ||
                string.IsNullOrWhiteSpace(txtNombreInvest.Text) ||
                string.IsNullOrWhiteSpace(txtEmailinvest.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoInvest.Text) ||
                GeneroInvestComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtidUser.Text) ||
                string.IsNullOrWhiteSpace(txtSemillero.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            insertarInvestigador(
               int.Parse(txtidInvestigadores.Text),
               txtTipoInvestigadores.Text,
               txtNombreInvest.Text,
               txtEmailinvest.Text,
               int.Parse(txtTelefonoInvest.Text),
               GeneroInvestComboBox.SelectedItem.ToString(),
               int.Parse(txtIdUsuario.Text),
               int.Parse(txtSemillero.Text)
            );

            ConsultarInvestigadores();
        }

        private void btnActualizarInvest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtidInvestigadores.Text) ||
                string.IsNullOrWhiteSpace(txtTipoInvestigadores.Text) ||
                string.IsNullOrWhiteSpace(txtNombreInvest.Text) ||
                string.IsNullOrWhiteSpace(txtEmailinvest.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoInvest.Text) ||
                GeneroInvestComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtidUser.Text) ||
                string.IsNullOrWhiteSpace(txtSemillero.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Investigadores SET tipoInvestigador = @tipoInvestigador, nombreInvestigador = @nombreInvestigador, emailInvestigador = @emailInvestigador, telefonoInvestigador = @telefonoInvestigador, generoInvestigador = @generoInvestigador, idUsuario = @idUsuario, idSemillero = @idSemillero WHERE idInvestigador = @idInvestigador", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = int.Parse(txtidInvestigadores.Text);
            actualizar.Parameters.AddWithValue("@tipoInvestigador", SqlDbType.VarChar).Value = txtTipoInvestigadores.Text;
            actualizar.Parameters.AddWithValue("@nombreInvestigador", SqlDbType.VarChar).Value = txtNombreInvest.Text;
            actualizar.Parameters.AddWithValue("@emailInvestigador", SqlDbType.VarChar).Value = txtEmailinvest.Text;
            actualizar.Parameters.AddWithValue("@telefonoInvestigador", SqlDbType.Int).Value = int.Parse(txtTelefonoInvest.Text);
            actualizar.Parameters.AddWithValue("@generoInvestigador", SqlDbType.VarChar).Value = GeneroInvestComboBox.SelectedItem.ToString();
            actualizar.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = int.Parse(txtidUser.Text);
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtSemillero.Text);
            if (MessageBox.Show("¿Estás seguro que desea actualizar el registro del investigador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Investigador actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarInvestigadores();
            }
        }

        private void btnlimpiarInvest_Click(object sender, EventArgs e)
        {
            txtidInvestigadores.Clear();
            txtTipoInvestigadores.Clear();
            txtNombreInvest.Clear();
            txtEmailinvest.Clear();
            txtTelefonoInvest.Clear();
            txtidUser.Clear();
            txtSemillero.Clear();
            GeneroInvestComboBox.SelectedIndex = -1;
        }

        private void btnGuardarreporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReport.Text) ||
                string.IsNullOrWhiteSpace(txtTipoReporte.Text) ||
                string.IsNullOrWhiteSpace(txtHoraReporte.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "ProyectoSemillero", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InsertarReporte(
                int.Parse(txtIdReport.Text),
                txtTipoReporte.Text,
                DateTime.Now,
                txtHoraReporte.Text,
                txtFormatoReporte.Text
            );

            ReporteConsulta();
        }

        private void btnEliminarReporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDrepo.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del reporte a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Reporte WHERE idReporte = @idReporte", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIDrepo.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar el reporte?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Reporte eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReporteConsulta();
            }
        }

        private void btnActualizarreporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReport.Text) ||
                string.IsNullOrWhiteSpace(txtTipoReporte.Text) ||
                string.IsNullOrWhiteSpace(txtHoraReporte.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Reporte SET tipoReporte = @tipoReporte, fechaReporte = @fechaReporte, horaReporte = @horaReporte, formatoReporte = @formatoReporte WHERE idReporte = @idReporte", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIdReport.Text);
            actualizar.Parameters.AddWithValue("@tipoReporte", SqlDbType.VarChar).Value = txtTipoReporte.Text;
            actualizar.Parameters.AddWithValue("@fechaReporte", SqlDbType.Date).Value = DateTime.Now;
            actualizar.Parameters.AddWithValue("@horaReporte", SqlDbType.VarChar).Value = txtHoraReporte.Text;
            actualizar.Parameters.AddWithValue("@formatoReporte", SqlDbType.VarChar).Value = txtFormatoReporte.Text;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el reporte?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Reporte actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReporteConsulta();
            }
        }

        private void btnLimpiarreporte_Click(object sender, EventArgs e)
        {
            txtIdReport.Clear();
            txtTipoReporte.Clear();
            txtHoraReporte.Clear();
            txtFormatoReporte.Clear();
            dateTimePickerReporte.Value = DateTime.Now;
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdSe.Text) ||
            string.IsNullOrWhiteSpace(txtNombreSemillero.Text) ||
            string.IsNullOrWhiteSpace(txtLineaInvestigacion.Text) ||
            string.IsNullOrWhiteSpace(txtEstadoSemillero.Text) ||
            string.IsNullOrWhiteSpace(txtDescripcionSemillero.Text) ||
            string.IsNullOrWhiteSpace(txtIdReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertarSemillero;
            try
            {
                insertarSemillero = new SqlCommand("INSERT INTO Semillero (idSemillero,nombreSemillero,fechacreaSemillero,lineaInvestigacion,estadoSemillero,descripcionSemillero,idReunion) VALUES (@idSemillero,@nombreSemillero,@fechacreaSemillero,@lineaInvestigacion,@estadoSemillero,@descripcionSemillero,@idReunion)", cn.Conectar());
                insertarSemillero.CommandType = CommandType.Text;
                insertarSemillero.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtIdSe.Text);
                insertarSemillero.Parameters.AddWithValue("@nombreSemillero", SqlDbType.VarChar).Value = txtNombreSemillero.Text;
                insertarSemillero.Parameters.AddWithValue("@fechacreaSemillero", SqlDbType.Date).Value = DateTime.Now;
                insertarSemillero.Parameters.AddWithValue("@lineaInvestigacion", SqlDbType.VarChar).Value = txtLineaInvestigacion.Text;
                insertarSemillero.Parameters.AddWithValue("@estadoSemillero", SqlDbType.VarChar).Value = txtEstadoSemillero.Text;
                insertarSemillero.Parameters.AddWithValue("@descripcionSemillero", SqlDbType.VarChar).Value = txtDescripcionSemillero.Text;
                insertarSemillero.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIdReunion.Text);
                insertarSemillero.ExecuteNonQuery();
                MessageBox.Show("Semillero registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                SemilleroConsulta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActSemillero_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdSe.Text) ||
                string.IsNullOrWhiteSpace(txtNombreSemillero.Text) ||
                string.IsNullOrWhiteSpace(txtLineaInvestigacion.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoSemillero.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionSemillero.Text) ||
                string.IsNullOrWhiteSpace(txtIdReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Semillero SET nombreSemillero = @nombreSemillero, fechacreaSemillero = @fechacreaSemillero, lineaInvestigacion = @lineaInvestigacion, estadoSemillero = @estadoSemillero, descripcionSemillero = @descripcionSemillero, idReunion = @idReunion WHERE idSemillero = @idSemillero", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtIdSe.Text);
            actualizar.Parameters.AddWithValue("@nombreSemillero", SqlDbType.VarChar).Value = txtNombreSemillero.Text;
            actualizar.Parameters.AddWithValue("@fechacreaSemillero", SqlDbType.Date).Value = DateTime.Now;
            actualizar.Parameters.AddWithValue("@lineaInvestigacion", SqlDbType.VarChar).Value = txtLineaInvestigacion.Text;
            actualizar.Parameters.AddWithValue("@estadoSemillero", SqlDbType.VarChar).Value = txtEstadoSemillero.Text;
            actualizar.Parameters.AddWithValue("@descripcionSemillero", SqlDbType.VarChar).Value = txtDescripcionSemillero.Text;
            actualizar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIdReunion.Text);
            if (MessageBox.Show("¿Estás seguro que desea actualizar el semillero?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Semillero actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                SemilleroConsulta();
            }
        }

        private void btnElimSemillero_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdSe.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del semillero a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Semillero WHERE idSemillero = @idSemillero", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtIdSe.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar el semillero?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Semillero eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                SemilleroConsulta();
            }
        }

        private void btnLimpiarSemillero_Click(object sender, EventArgs e)
        {
            txtIdSe.Clear();
            txtNombreSemillero.Clear();
            txtLineaInvestigacion.Clear();
            txtEstadoSemillero.Clear();
            txtDescripcionSemillero.Clear();
            txtIdReunion.Clear();
            dateTimePickerSemillero.Value = DateTime.Now;
        }

        private void btnGuardarevento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEvento.Text) ||
                string.IsNullOrWhiteSpace(txtNombreEvento.Text) ||
                string.IsNullOrWhiteSpace(txtTipoEvento.Text) ||
                string.IsNullOrWhiteSpace(txtLugarEvento.Text) ||
                string.IsNullOrWhiteSpace(txtHoraEvento.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionEvento.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertarEvento;
            try
            {
                insertarEvento = new SqlCommand("INSERT INTO Eventos (idEvento,nombreEvento,tipoEvento,fechaEvento,lugarEvento,horaEvento,descripcionEvento) VALUES (@idEvento,@nombreEvento,@tipoEvento,@fechaEvento,@lugarEvento,@horaEvento,@descripcionEvento)", cn.Conectar());
                insertarEvento.CommandType = CommandType.Text;
                insertarEvento.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento.Text);
                insertarEvento.Parameters.AddWithValue("@nombreEvento", SqlDbType.VarChar).Value = txtNombreEvento.Text;
                insertarEvento.Parameters.AddWithValue("@tipoEvento", SqlDbType.VarChar).Value = txtTipoEvento.Text;
                insertarEvento.Parameters.AddWithValue("@fechaEvento", SqlDbType.Date).Value = DateTime.Now;
                insertarEvento.Parameters.AddWithValue("@lugarEvento", SqlDbType.VarChar).Value = txtLugarEvento.Text;
                insertarEvento.Parameters.AddWithValue("@horaEvento", SqlDbType.VarChar).Value = txtHoraEvento.Text;
                insertarEvento.Parameters.AddWithValue("@descripcionEvento", SqlDbType.VarChar).Value = txtDescripcionEvento.Text;
                insertarEvento.ExecuteNonQuery();
                MessageBox.Show("Evento registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarEvento();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarevento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdEvento.Text) ||
                string.IsNullOrWhiteSpace(txtNombreEvento.Text) ||
                string.IsNullOrWhiteSpace(txtTipoEvento.Text) ||
                string.IsNullOrWhiteSpace(txtLugarEvento.Text) ||
                string.IsNullOrWhiteSpace(txtHoraEvento.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionEvento.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Eventos SET nombreEvento = @nombreEvento, tipoEvento = @tipoEvento, fechaEvento = @fechaEvento, lugarEvento = @lugarEvento, horaEvento = @horaEvento, descripcionEvento = @descripcionEvento WHERE idEvento = @idEvento", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento.Text);
            actualizar.Parameters.AddWithValue("@nombreEvento", SqlDbType.VarChar).Value = txtNombreEvento.Text;
            actualizar.Parameters.AddWithValue("@tipoEvento", SqlDbType.VarChar).Value = txtTipoEvento.Text;
            actualizar.Parameters.AddWithValue("@fechaEvento", SqlDbType.Date).Value = DateTime.Now;
            actualizar.Parameters.AddWithValue("@lugarEvento", SqlDbType.VarChar).Value = txtLugarEvento.Text;
            actualizar.Parameters.AddWithValue("@horaEvento", SqlDbType.VarChar).Value = txtHoraEvento.Text;
            actualizar.Parameters.AddWithValue("@descripcionEvento", SqlDbType.VarChar).Value = txtDescripcionEvento.Text;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el evento?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Evento actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarEvento();
            }
        }

        private void btnEliminarEvento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDevent.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del evento a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Eventos WHERE idEvento = @idEvento", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIDevent.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar el evento?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Evento eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarEvento();
            }
        }

        private void btnLimpiarEvento_Click(object sender, EventArgs e)
        {
            txtIdEvento.Clear();
            txtNombreEvento.Clear();
            txtTipoEvento.Clear();
            txtLugarEvento.Clear();
            txtHoraEvento.Clear();
            txtDescripcionEvento.Clear();
            dateTimePickerEvento.Value = DateTime.Now;
        }

        private void btnguardarPatro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtEventoID.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertarPatrocinador;
            try
            {
                insertarPatrocinador = new SqlCommand("INSERT INTO Patrocinadores (idPatrocinador,nombrePatrocinador,tipoPatrocinador,correoPatrocinador,telefonoPatrocinador,direccionPatrocinador,idEvento) VALUES (@idPatrocinador,@nombrePatrocinador,@tipoPatrocinador,@correoPatrocinador,@telefonoPatrocinador,@direccionPatrocinador,@idEvento)", cn.Conectar());
                insertarPatrocinador.CommandType = CommandType.Text;
                insertarPatrocinador.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIdPatrocinador.Text);
                insertarPatrocinador.Parameters.AddWithValue("@nombrePatrocinador", SqlDbType.VarChar).Value = txtNombrePatrocinador.Text;
                insertarPatrocinador.Parameters.AddWithValue("@tipoPatrocinador", SqlDbType.VarChar).Value = txtTipoPatrocinador.Text;
                insertarPatrocinador.Parameters.AddWithValue("@correoPatrocinador", SqlDbType.VarChar).Value = txtCorreoPatrocinador.Text;
                insertarPatrocinador.Parameters.AddWithValue("@telefonoPatrocinador", SqlDbType.Int).Value = int.Parse(txtTelefonoPatrocinador.Text);
                insertarPatrocinador.Parameters.AddWithValue("@direccionPatrocinador", SqlDbType.VarChar).Value = txtDireccionPatrocinador.Text;
                insertarPatrocinador.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtEventoID.Text);
                insertarPatrocinador.ExecuteNonQuery();
                MessageBox.Show("Patrocinador registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarPatro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarPatro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtEventoID.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Patrocinadores SET nombrePatrocinador = @nombrePatrocinador, tipoPatrocinador = @tipoPatrocinador, correoPatrocinador = @correoPatrocinador, telefonoPatrocinador = @telefonoPatrocinador, direccionPatrocinador = @direccionPatrocinador, idEvento = @idEvento WHERE idPatrocinador = @idPatrocinador", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIdPatrocinador.Text);
            actualizar.Parameters.AddWithValue("@nombrePatrocinador", SqlDbType.VarChar).Value = txtNombrePatrocinador.Text;
            actualizar.Parameters.AddWithValue("@tipoPatrocinador", SqlDbType.VarChar).Value = txtTipoPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@correoPatrocinador", SqlDbType.VarChar).Value = txtCorreoPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@telefonoPatrocinador", SqlDbType.Int).Value = int.Parse(txtTelefonoPatrocinador.Text);
            actualizar.Parameters.AddWithValue("@direccionPatrocinador", SqlDbType.VarChar).Value = txtDireccionPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtEventoID.Text);
            if (MessageBox.Show("¿Estás seguro que desea actualizar el patrocinador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Patrocinador actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarPatro();
            }
        }

        private void btnEliminarPatro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDpatro.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del patrocinador a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Patrocinadores WHERE idPatrocinador = @idPatrocinador", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIDpatro.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar el patrocinador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Patrocinador eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarPatro();
            }
        }

        private void btnLimpiarPatro_Click(object sender, EventArgs e)
        {
            txtIdPatrocinador.Clear();
            txtNombrePatrocinador.Clear();
            txtTipoPatrocinador.Clear();
            txtCorreoPatrocinador.Clear();
            txtTelefonoPatrocinador.Clear();
            txtDireccionPatrocinador.Clear();
            txtEventoID.Clear();
        }

        private void btnGuardarReunion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReunionId.Text) ||
                string.IsNullOrWhiteSpace(txtMotivoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtLugarReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertarReunion;
            try
            {
                insertarReunion = new SqlCommand("INSERT INTO Reunion (idReunion,fechaReunion,motivoReunion,estadoReunion,lugarReunion) VALUES (@idReunion,@fechaReunion,@motivoReunion,@estadoReunion,@lugarReunion)", cn.Conectar());
                insertarReunion.CommandType = CommandType.Text;
                insertarReunion.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtReunionId.Text);
                insertarReunion.Parameters.AddWithValue("@fechaReunion", SqlDbType.Date).Value = DateTime.Now;
                insertarReunion.Parameters.AddWithValue("@motivoReunion", SqlDbType.VarChar).Value = txtMotivoReunion.Text;
                insertarReunion.Parameters.AddWithValue("@estadoReunion", SqlDbType.VarChar).Value = txtEstadoReunion.Text;
                insertarReunion.Parameters.AddWithValue("@lugarReunion", SqlDbType.VarChar).Value = txtLugarReunion.Text;
                insertarReunion.ExecuteNonQuery();
                MessageBox.Show("Reunion registrada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarReunion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarReu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReunionId.Text) ||
                string.IsNullOrWhiteSpace(txtMotivoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtLugarReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Reunion SET fechaReunion = @fechaReunion, motivoReunion = @motivoReunion, estadoReunion = @estadoReunion, lugarReunion = @lugarReunion WHERE idReunion = @idReunion", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtReunionId.Text);
            actualizar.Parameters.AddWithValue("@fechaReunion", SqlDbType.Date).Value = DateTime.Now;
            actualizar.Parameters.AddWithValue("@motivoReunion", SqlDbType.VarChar).Value = txtMotivoReunion.Text;
            actualizar.Parameters.AddWithValue("@estadoReunion", SqlDbType.VarChar).Value = txtEstadoReunion.Text;
            actualizar.Parameters.AddWithValue("@lugarReunion", SqlDbType.VarChar).Value = txtLugarReunion.Text;
            if (MessageBox.Show("¿Estás seguro que desea actualizar la reunion?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Reunion actualizada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarReunion();
            }
        }

        private void btnEliminarReu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDreu.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID de la reunion a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Reunion WHERE idReunion = @idReunion", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIDreu.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar la reunion?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Reunion eliminada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarReunion();
            }
        }

        private void btnLimpiarReunion_Click(object sender, EventArgs e)
        {
            txtReunionId.Clear();
            txtMotivoReunion.Clear();
            txtEstadoReunion.Clear();
            txtLugarReunion.Clear();
            dateTimePickerReunion.Value = DateTime.Now;
        }

        private void btnConsultarProyectos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdSemilleroConsulta.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del semillero.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validacion que el ID exista en la tabla Semillero
            SqlCommand validar;
            validar = new SqlCommand("SELECT COUNT(*) FROM Semillero WHERE idSemillero = @idSemillero", cn.Conectar());
            validar.CommandType = CommandType.Text;
            validar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtIdSemilleroConsulta.Text);
            int existe = (int)validar.ExecuteScalar();

            if (existe == 0)
            {
                MessageBox.Show("El ID ingresado no corresponde a ningun semillero registrado.",
                                "ID invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Si el ID es valido continua con la consulta
            SqlCommand consulta;
            consulta = new SqlCommand(
                "SELECT " +
                "p.idProyecto, p.tituloProyecto, p.estadoProyecto, p.fechainiProyecto, p.duracionProyecto, " +
                "f.idFase, f.descripcionFase, f.estadoFase, f.fechaFase, " +
                "a.idActividad, a.nombreActividad, a.estadoActividad, a.fechaActividad, a.lugarActividad " +
                "FROM Proyecto p, Fase f, Actividad a " +
                "WHERE p.idSemillero = @idSemillero " +
                "AND f.idProyecto = p.idProyecto " +
                "AND a.idFase = f.idFase", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = int.Parse(txtIdSemilleroConsulta.Text);
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "ConsultaSemillero");
            try
            {
                dataGridView1.DataMember = "ConsultaSemillero";
                dataGridView1.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConsultarallProyect_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand(
                "SELECT " +
                "p.idProyecto, p.tituloProyecto, p.estadoProyecto, p.fechainiProyecto, p.duracionProyecto, " +
                "f.idFase, f.descripcionFase, f.estadoFase, f.fechaFase, " +
                "a.idActividad, a.nombreActividad, a.estadoActividad, a.fechaActividad, a.lugarActividad " +
                "FROM Proyecto p, Fase f, Actividad a " +
                "WHERE f.idProyecto = p.idProyecto " +
                "AND a.idFase = f.idFase", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "ConsultaTodo");
            try
            {
                dataGridView1.DataMember = "ConsultaTodo";
                dataGridView1.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReporteP.Text) ||
                string.IsNullOrWhiteSpace(txtTipoReporteP.Text) ||
                string.IsNullOrWhiteSpace(txtHoraReporteP.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoReporteP.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertarReporte;
            try
            {
                insertarReporte = new SqlCommand("INSERT INTO Reporte (idReporte,tipoReporte,fechaReporte,horaReporte,formatoReporte) VALUES (@idReporte,@tipoReporte,@fechaReporte,@horaReporte,@formatoReporte)", cn.Conectar());
                insertarReporte.CommandType = CommandType.Text;
                insertarReporte.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIdReporteP.Text);
                insertarReporte.Parameters.AddWithValue("@tipoReporte", SqlDbType.VarChar).Value = txtTipoReporteP.Text;
                insertarReporte.Parameters.AddWithValue("@fechaReporte", SqlDbType.Date).Value = DateTime.Now;
                insertarReporte.Parameters.AddWithValue("@horaReporte", SqlDbType.VarChar).Value = txtHoraReporteP.Text;
                insertarReporte.Parameters.AddWithValue("@formatoReporte", SqlDbType.VarChar).Value = txtFormatoReporteP.Text;
                insertarReporte.ExecuteNonQuery();
                MessageBox.Show("Reporte agregado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiarProyectos_Click(object sender, EventArgs e)
        {
            txtIdSemilleroConsulta.Clear();
            txtIdReporteP.Clear();
            txtTipoReporteP.Clear();
            txtHoraReporteP.Clear();
            txtFormatoReporteP.Clear();
            dateTimePickerReporteP.Value = DateTime.Now;
            dataGridView1.DataSource = null;
        }

        private void txtIdSemilleroConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReporteP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtHoraReporteP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtTelefonoInvest_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtidUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtSemillero_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtidInvestigadores_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReport_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtHoraReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdSe_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReunion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdEvento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtHoraEvento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdPatrocinador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtEventoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtTelefonoPatrocinador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtReunionId_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void cboTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTabla.SelectedItem != null)// Verificar que se haya seleccionado una tabla antes de cargar los parámetros
            {
                // Cargar los parámetros correspondientes a la tabla seleccionada en el combo box de parámetros
                ConsultarConParametro.CargarParametros(cboTabla.SelectedItem.ToString(), cboParametro);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado una tabla, un parámetro y que se haya ingresado un valor de búsqueda antes de realizar la consulta
            if (cboTabla.SelectedItem == null ||
                cboParametro.SelectedItem == null ||
                string.IsNullOrWhiteSpace(cboParametro.SelectedItem.ToString()) ||
                string.IsNullOrWhiteSpace(txtValorBusqueda.Text))
            {
                // Mostrar un mensaje de advertencia al usuario indicando que se deben completar todos los campos necesarios para realizar la búsqueda
                MessageBox.Show("Por favor seleccione tabla, parametro e ingrese un valor.",
                                "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Realizar la consulta utilizando el método ConsultarParametro de la clase ConsultarConParametro, pasando los valores seleccionados y el valor de búsqueda como parámetros
            DataTable resultado = ConsultarConParametro.ConsultarParametro(
                cboTabla.SelectedItem.ToString(),
                cboParametro.SelectedItem.ToString(),
                txtValorBusqueda.Text,
                cn);

            if (resultado.Rows.Count == 0)// Verificar si la consulta no devolvió resultados y mostrar un mensaje informativo al usuario en caso de que no se hayan encontrado coincidencias para la búsqueda realizada
            {
                // Mostrar un mensaje informativo al usuario indicando que no se encontraron resultados para la búsqueda realizada
                MessageBox.Show("No se encontraron resultados para la búsqueda realizada.",
                                "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;// Limpiar el DataGridView para mostrar que no hay resultados
                return;
            }

            dataGridView1.DataSource = resultado;// Si la consulta devolvió resultados, asignar el resultado al DataGridView para mostrar los datos encontrados
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de búsqueda y el DataGridView para permitir al usuario realizar una nueva búsqueda desde cero
            cboTabla.SelectedIndex = -1;
            cboParametro.Items.Clear();
            txtValorBusqueda.Clear();
            dataGridView1.DataSource = null;
        }
    }
}