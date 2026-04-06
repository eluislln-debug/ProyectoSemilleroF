using Guna.UI2.Material.Animation;
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
    public partial class FormInvestigador : Form
    {
        int idSemillero;// Variable para almacenar el ID del semillero
        Conexion cn = new Conexion();// Instancia de la clase Conexion para manejar la conexión a la base de datos
        DataSet ds = new DataSet();// DataSet para almacenar los datos consultados de la base de datos
        public FormInvestigador(int idSemillero)
        {
            InitializeComponent();
            this.idSemillero = idSemillero;// Asignar el ID del semillero al campo de la clase para su uso en las consultas y operaciones relacionadas con ese semillero
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho clic en una fila válida (no en el encabezado)
            if (e.RowIndex >= 0)
            {
                // Asignar el valor de la primera celda de la fila seleccionada a los campos de texto correspondientes
                txtIDinvestigadorD.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDproyectoP.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDeventoE.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDreunionr1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDreporteR.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDpatrocinadoresP.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDfasesF.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtIDactividadA.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void btnAgregarMiembro_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén completos antes de intentar guardar el nuevo miembro
            if (string.IsNullOrWhiteSpace(txtIdInvestigador.Text) ||
               string.IsNullOrWhiteSpace(txtTipoInvestigador.Text) ||
               string.IsNullOrWhiteSpace(txtNombreInvestigador.Text) ||
               string.IsNullOrWhiteSpace(txtEmailInvestigador.Text) ||
               string.IsNullOrWhiteSpace(txtTelefonoInvestigador.Text) ||
               GeneroComboBox.SelectedItem == null ||
               string.IsNullOrWhiteSpace(txtIdUsuario.Text))
            {
                // Mostrar un mensaje de advertencia si algún campo está vacío o no se ha seleccionado un género
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;// Declarar un objeto SqlCommand para ejecutar la consulta de inserción en la base de datos
            try// Intentar ejecutar la consulta de inserción y manejar cualquier excepción que pueda ocurrir
            {
                // Configurar el comando SQL para insertar un nuevo investigador en la tabla Investigadores, utilizando parámetros para evitar inyecciones SQL
                insertar = new SqlCommand("INSERT INTO Investigadores (idInvestigador,tipoInvestigador,nombreInvestigador,emailInvestigador,telefonoInvestigador,generoInvestigador,idUsuario,idSemillero) VALUES (@idInvestigador,@tipoInvestigador,@nombreInvestigador,@emailInvestigador,@telefonoInvestigador,@generoInvestigador,@idUsuario,@idSemillero)", cn.Conectar());
                insertar.CommandType = CommandType.Text;// Especificar que el comando es de tipo texto (consulta SQL)
                // Agregar los parámetros necesarios para la consulta de inserción, asignando los valores correspondientes desde los campos de texto y el combo box
                insertar.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = int.Parse(txtIdInvestigador.Text);
                insertar.Parameters.AddWithValue("@tipoInvestigador", SqlDbType.VarChar).Value = txtTipoInvestigador.Text;
                insertar.Parameters.AddWithValue("@nombreInvestigador", SqlDbType.VarChar).Value = txtNombreInvestigador.Text;
                insertar.Parameters.AddWithValue("@emailInvestigador", SqlDbType.VarChar).Value = txtEmailInvestigador.Text;
                insertar.Parameters.AddWithValue("@telefonoInvestigador", SqlDbType.Int).Value = int.Parse(txtTelefonoInvestigador.Text);
                insertar.Parameters.AddWithValue("@generoInvestigador", SqlDbType.VarChar).Value = GeneroComboBox.SelectedItem.ToString();
                insertar.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = int.Parse(txtIdUsuario.Text);
                insertar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
                insertar.ExecuteNonQuery();// Ejecutar la consulta de inserción en la base de datos
                // Mostrar un mensaje de éxito si el miembro se ha agregado correctamente y actualizar la lista de miembros en el DataGridView
                MessageBox.Show("Miembro agregado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarMiembros_Click(sender, e);// Llamar al método para consultar y mostrar los miembros actualizados en el DataGridView
            }
            catch (Exception ex)// Capturar cualquier excepción que ocurra durante la inserción del nuevo miembro y mostrar un mensaje de error con la información de la excepción
            {
                MessageBox.Show(ex.Message);// Mostrar un mensaje de error si ocurre una excepción durante la inserción del nuevo miembro
            }
        }

        // Método para consultar y mostrar los miembros del semillero en el DataGridView, filtrando por el ID del semillero
        private void btnConsultarMiembros_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;// Declarar un objeto SqlCommand para ejecutar la consulta de selección en la base de datos
            consulta = new SqlCommand("SELECT * FROM Investigadores WHERE idSemillero = @idSemillero", cn.Conectar());// Configurar el comando SQL para seleccionar todos los investigadores que pertenecen al semillero actual, utilizando un parámetro para filtrar por el ID del semillero
            consulta.CommandType = CommandType.Text;// Especificar que el comando es de tipo texto (consulta SQL)
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;// Agregar el parámetro necesario para la consulta de selección, asignando el valor del ID del semillero actual
            ds.Clear();// Limpiar el DataSet antes de llenar con los nuevos datos consultados
            SqlDataAdapter da = new SqlDataAdapter(consulta);// Crear un SqlDataAdapter para ejecutar la consulta y llenar el DataSet con los resultados
            da.Fill(ds, "Investigadores");// Llenar el DataSet con los resultados de la consulta, asignando un nombre a la tabla resultante para su referencia en el DataGridView
            dataGridView1.DataMember = "Investigadores";// Establecer el nombre de la tabla en el DataSet que se mostrará en el DataGridView
            dataGridView1.DataSource = ds;// Asignar el DataSet como fuente de datos del DataGridView para mostrar los investigadores consultados
        }

        private void btnEliminarInvest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDinvestigadorD.Text))// Validar que el campo de texto para el ID del investigador a eliminar no esté vacío antes de intentar eliminar
            {
                MessageBox.Show("Por favor, ingrese el ID del investigador a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);// Mostrar un mensaje de advertencia si el campo de texto para el ID del investigador a eliminar está vacío
                return;
            }

            SqlCommand eliminar;// Declarar un objeto SqlCommand para ejecutar la consulta de eliminación en la base de datos
            // Configurar el comando SQL para eliminar un investigador de la tabla Investigadores, utilizando parámetros para evitar inyecciones SQL y asegurando que se elimine solo el investigador que pertenece al semillero actual
            eliminar = new SqlCommand("DELETE FROM Investigadores WHERE idInvestigador = @idInvestigador AND idSemillero = @idSemillero", cn.Conectar());
            eliminar.CommandType = CommandType.Text;// Especificar que el comando es de tipo texto (consulta SQL)
            // Agregar los parámetros necesarios para la consulta de eliminación, asignando el valor del ID del investigador a eliminar desde el campo de texto y el ID del semillero actual
            eliminar.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = int.Parse(txtIDinvestigadorD.Text);
            // Agregar el parámetro del ID del semillero para asegurar que solo se elimine el investigador que pertenece al semillero actual
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar este miembro?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)// Mostrar un mensaje de confirmación antes de eliminar el investigador, preguntando al usuario si está seguro de la acción
            {
                eliminar.ExecuteNonQuery();// Ejecutar la consulta de eliminación en la base de datos si el usuario confirma la acción
                MessageBox.Show("Miembro eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);// Mostrar un mensaje de éxito si el miembro se ha eliminado correctamente y actualizar la lista de miembros en el DataGridView
                btnConsultarMiembros_Click(sender, e);// Llamar al método para consultar y mostrar los miembros actualizados en el DataGridView después de eliminar el investigador
            }
        }

        private void btnlimpiarInvest_Click(object sender, EventArgs e)
        {
            // Limpiar todos los campos de texto y el combo box relacionados con la información del investigador, así como limpiar el DataGridView para mostrar una vista vacía
            txtIdInvestigador.Clear();
            txtTipoInvestigador.Clear();
            txtNombreInvestigador.Clear();
            txtEmailInvestigador.Clear();
            txtTelefonoInvestigador.Clear();
            txtIdUsuario.Clear();
            GeneroComboBox.SelectedIndex = -1;
            dataGridView1.DataSource = null;
            txtIDinvestigadorD.Clear();
        }

        private void btnActualizarInvest_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén completos antes de intentar actualizar la información del miembro
            if (string.IsNullOrWhiteSpace(txtIdInvestigador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoInvestigador.Text) ||
                string.IsNullOrWhiteSpace(txtNombreInvestigador.Text) ||
                string.IsNullOrWhiteSpace(txtEmailInvestigador.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoInvestigador.Text) ||
                GeneroComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtIdUsuario.Text))
            {
                // Mostrar un mensaje de advertencia si algún campo está vacío o no se ha seleccionado un género, indicando que es necesario completar todos los campos antes de continuar con la actualización
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;// Declarar un objeto SqlCommand para ejecutar la consulta de actualización en la base de datos
            // Configurar el comando SQL para actualizar la información de un investigador en la tabla Investigadores, utilizando parámetros para evitar inyecciones SQL y asegurando que se actualice solo el investigador que pertenece al semillero actual
            actualizar = new SqlCommand("UPDATE Investigadores SET tipoInvestigador = @tipoInvestigador, nombreInvestigador = @nombreInvestigador, emailInvestigador = @emailInvestigador, telefonoInvestigador = @telefonoInvestigador, generoInvestigador = @generoInvestigador, idUsuario = @idUsuario WHERE idInvestigador = @idInvestigador AND idSemillero = @idSemillero", cn.Conectar());
            actualizar.CommandType = CommandType.Text;// Especificar que el comando es de tipo texto (consulta SQL)
            // Agregar los parámetros necesarios para la consulta de actualización, asignando los valores correspondientes desde los campos de texto y el combo box, y asegurando que se actualice solo el investigador que pertenece al semillero actual
            actualizar.Parameters.AddWithValue("@idInvestigador", SqlDbType.Int).Value = int.Parse(txtIdInvestigador.Text);
            actualizar.Parameters.AddWithValue("@tipoInvestigador", SqlDbType.VarChar).Value = txtTipoInvestigador.Text;
            actualizar.Parameters.AddWithValue("@nombreInvestigador", SqlDbType.VarChar).Value = txtNombreInvestigador.Text;
            actualizar.Parameters.AddWithValue("@emailInvestigador", SqlDbType.VarChar).Value = txtEmailInvestigador.Text;
            actualizar.Parameters.AddWithValue("@telefonoInvestigador", SqlDbType.Int).Value = int.Parse(txtTelefonoInvestigador.Text);
            actualizar.Parameters.AddWithValue("@generoInvestigador", SqlDbType.VarChar).Value = GeneroComboBox.SelectedItem.ToString();
            actualizar.Parameters.AddWithValue("@idUsuario", SqlDbType.Int).Value = int.Parse(txtIdUsuario.Text);
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            // Mostrar un mensaje de confirmación antes de actualizar la información del miembro, preguntando al usuario si está seguro de la acción
            if (MessageBox.Show("¿Estás seguro que desea actualizar este miembro?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();// Ejecutar la consulta de actualización en la base de datos si el usuario confirma la acción
                MessageBox.Show("Miembro actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);// Mostrar un mensaje de éxito si el miembro se ha actualizado correctamente y actualizar la lista de miembros en el DataGridView
                btnConsultarMiembros_Click(sender, e);// Llamar al método para consultar y mostrar los miembros actualizados en el DataGridView después de actualizar la información del investigador
            }
        }

        private void FormInvestigador_Load(object sender, EventArgs e)
        {
            // Al cargar el formulario, ocultar todos los GroupBox relacionados con las diferentes secciones (Miembros, Semillero, Proyectos, Eventos, Reuniones, Reportes, Patrocinadores, Fases de Actividad) para mostrar una vista inicial limpia y permitir que el usuario seleccione qué sección desea gestionar
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón para gestionar miembros, mostrar el GroupBox de Miembros y ocultar los demás GroupBox relacionados con las otras secciones para que el usuario pueda centrarse en la gestión de miembros
            MiembrosGroupBox.Visible = true;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void btnActualizarSemillero_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreSemillero.Text) ||
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
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            actualizar.Parameters.AddWithValue("@nombreSemillero", SqlDbType.VarChar).Value = txtNombreSemillero.Text;
            actualizar.Parameters.AddWithValue("@fechacreaSemillero", SqlDbType.Date).Value = DateTime.Now;
            actualizar.Parameters.AddWithValue("@lineaInvestigacion", SqlDbType.VarChar).Value = txtLineaInvestigacion.Text;
            actualizar.Parameters.AddWithValue("@estadoSemillero", SqlDbType.VarChar).Value = txtEstadoSemillero.Text;
            actualizar.Parameters.AddWithValue("@descripcionSemillero", SqlDbType.VarChar).Value = txtDescripcionSemillero.Text;
            actualizar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIdReunion.Text);
            if (MessageBox.Show("¿Estás seguro que desea actualizar su semillero?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Semillero actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarSemillero_Click(sender, e);
            }
        }

        private void btnConsultarSemillero_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT * FROM Semillero WHERE idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Semillero");
            dataGridView1.DataMember = "Semillero";
            dataGridView1.DataSource = ds;
        }

        private void btnLimpiarSemillero_Click(object sender, EventArgs e)
        {
            txtNombreSemillero.Clear();
            txtLineaInvestigacion.Clear();
            txtEstadoSemillero.Clear();
            txtDescripcionSemillero.Clear();
            txtIdReunion.Clear();
            dateTimePickerSemillero.Value = DateTime.Now;
            dataGridView1.DataSource = null;
        }

        private void btnConsultarProyecto_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT * FROM Proyecto WHERE idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Proyecto");
            dataGridView1.DataMember = "Proyecto";
            dataGridView1.DataSource = ds;
        }

        private void btnActualizarProyecto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtDuracionProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtTituloProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionProyecto.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Proyecto SET fechainiProyecto = @fechainiProyecto, duracionProyecto = @duracionProyecto, estadoProyecto = @estadoProyecto, tituloProyecto = @tituloProyecto, descripcionProyecto = @descripcionProyecto WHERE idProyecto = @idProyecto AND idSemillero = @idSemillero", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idProyecto", SqlDbType.Int).Value = int.Parse(txtIdProyecto.Text);
            actualizar.Parameters.AddWithValue("@duracionProyecto", SqlDbType.VarChar).Value = txtDuracionProyecto.Text;
            actualizar.Parameters.AddWithValue("@estadoProyecto", SqlDbType.VarChar).Value = txtEstadoProyecto.Text;
            actualizar.Parameters.AddWithValue("@tituloProyecto", SqlDbType.VarChar).Value = txtTituloProyecto.Text;
            actualizar.Parameters.AddWithValue("@fechainiProyecto", SqlDbType.Date).Value = DateTime.Today;
            actualizar.Parameters.AddWithValue("@descripcionProyecto", SqlDbType.VarChar).Value = txtDescripcionProyecto.Text;
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el proyecto?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Proyecto actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarProyecto_Click(sender, e);
            }
        }

        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtDuracionProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtTituloProyecto.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionProyecto.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Proyecto (idProyecto,fechainiProyecto,duracionProyecto,estadoProyecto,tituloProyecto,descripcionProyecto,idSemillero) VALUES (@idProyecto,@fechainiProyecto,@duracionProyecto,@estadoProyecto,@tituloProyecto,@descripcionProyecto,@idSemillero)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idProyecto", SqlDbType.Int).Value = int.Parse(txtIdProyecto.Text);
                insertar.Parameters.AddWithValue("@fechainiProyecto", SqlDbType.Date).Value = DateTime.Today;
                insertar.Parameters.AddWithValue("@duracionProyecto", SqlDbType.VarChar).Value = txtDuracionProyecto.Text;
                insertar.Parameters.AddWithValue("@estadoProyecto", SqlDbType.VarChar).Value = txtEstadoProyecto.Text;
                insertar.Parameters.AddWithValue("@tituloProyecto", SqlDbType.VarChar).Value = txtTituloProyecto.Text;
                insertar.Parameters.AddWithValue("@descripcionProyecto", SqlDbType.VarChar).Value = txtDescripcionProyecto.Text;
                insertar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
                insertar.ExecuteNonQuery();
                MessageBox.Show("Proyecto registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarProyecto_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiarProyecto_Click(object sender, EventArgs e)
        {
            txtIdProyecto.Clear();
            txtDuracionProyecto.Clear();
            txtEstadoProyecto.Clear();
            txtTituloProyecto.Clear();
            txtDescripcionProyecto.Clear();
            dataGridView1.DataSource = null;
        }

        private void btnConsultarEventos_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT e.* FROM Eventos e, SemilleroEventos se WHERE e.idEvento = se.idEvento AND se.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Eventos");
            dataGridView1.DataMember = "Eventos";
            dataGridView1.DataSource = ds;
        }

        private void btnActualizarEvento_Click(object sender, EventArgs e)
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
            actualizar = new SqlCommand("UPDATE Eventos SET nombreEvento = @nombreEvento, tipoEvento = @tipoEvento, fechaEvento = @fechaEvento, lugarEvento = @lugarEvento, horaEvento = @horaEvento, descripcionEvento = @descripcionEvento WHERE idEvento = @idEvento AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento.Text);
            actualizar.Parameters.AddWithValue("@nombreEvento", SqlDbType.VarChar).Value = txtNombreEvento.Text;
            actualizar.Parameters.AddWithValue("@tipoEvento", SqlDbType.VarChar).Value = txtTipoEvento.Text;
            actualizar.Parameters.AddWithValue("@fechaEvento", SqlDbType.Date).Value = dateTimePickerEvento.Value;
            actualizar.Parameters.AddWithValue("@lugarEvento", SqlDbType.VarChar).Value = txtLugarEvento.Text;
            actualizar.Parameters.AddWithValue("@horaEvento", SqlDbType.VarChar).Value = txtHoraEvento.Text;
            actualizar.Parameters.AddWithValue("@descripcionEvento", SqlDbType.VarChar).Value = txtDescripcionEvento.Text;
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el evento?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Evento actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarEventos_Click(sender, e);
            }
        }

        private void btnEliminarEvento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDeventoE.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del evento a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Eventos WHERE idEvento = @idEvento AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIDeventoE.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar el evento?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Evento eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarEventos_Click(sender, e);
            }
        }

        private void btnEliminarProyecto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDproyectoP.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del proyecto a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Proyecto WHERE idProyecto = @idProyecto AND idSemillero = @idSemillero", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idProyecto", SqlDbType.Int).Value = int.Parse(txtIDproyectoP.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar el proyecto?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Proyecto eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarProyecto_Click(sender, e);
            }
        }

        private void btnGuardarEvento_Click(object sender, EventArgs e)
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

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Eventos (idEvento,nombreEvento,tipoEvento,fechaEvento,lugarEvento,horaEvento,descripcionEvento) VALUES (@idEvento,@nombreEvento,@tipoEvento,@fechaEvento,@lugarEvento,@horaEvento,@descripcionEvento)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento.Text);
                insertar.Parameters.AddWithValue("@nombreEvento", SqlDbType.VarChar).Value = txtNombreEvento.Text;
                insertar.Parameters.AddWithValue("@tipoEvento", SqlDbType.VarChar).Value = txtTipoEvento.Text;
                insertar.Parameters.AddWithValue("@fechaEvento", SqlDbType.Date).Value = dateTimePickerEvento.Value;
                insertar.Parameters.AddWithValue("@lugarEvento", SqlDbType.VarChar).Value = txtLugarEvento.Text;
                insertar.Parameters.AddWithValue("@horaEvento", SqlDbType.VarChar).Value = txtHoraEvento.Text;
                insertar.Parameters.AddWithValue("@descripcionEvento", SqlDbType.VarChar).Value = txtDescripcionEvento.Text;
                insertar.ExecuteNonQuery();

                // Tambien inserta en SemilleroEventos para vincular el evento con el semillero
                SqlCommand insertarSE;
                insertarSE = new SqlCommand("INSERT INTO SemilleroEventos VALUES (@idSemillero, @idEvento)", cn.Conectar());
                insertarSE.CommandType = CommandType.Text;
                insertarSE.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
                insertarSE.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento.Text);
                insertarSE.ExecuteNonQuery();

                MessageBox.Show("Evento registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarEventos_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            dataGridView1.DataSource = null;
        }

        private void btnConsultarReunion_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT r.* FROM Reunion r, Semillero s WHERE r.idReunion = s.idReunion AND s.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Reunion");
            dataGridView1.DataMember = "Reunion";
            dataGridView1.DataSource = ds;
        }

        private void btnGuardarReunion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReunion.Text) ||
                string.IsNullOrWhiteSpace(txtMotivoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtLugarReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Reunion (idReunion,fechaReunion,motivoReunion,estadoReunion,lugarReunion) VALUES (@idReunion,@fechaReunion,@motivoReunion,@estadoReunion,@lugarReunion)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIdReunion.Text);
                insertar.Parameters.AddWithValue("@fechaReunion", SqlDbType.Date).Value = dateTimePickerEvento.Value;
                insertar.Parameters.AddWithValue("@motivoReunion", SqlDbType.VarChar).Value = txtMotivoReunion.Text;
                insertar.Parameters.AddWithValue("@estadoReunion", SqlDbType.VarChar).Value = txtEstadoReunion.Text;
                insertar.Parameters.AddWithValue("@lugarReunion", SqlDbType.VarChar).Value = txtLugarReunion.Text;
                insertar.ExecuteNonQuery();
                MessageBox.Show("Reunion registrada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReunion_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarReunion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReunion.Text) ||
                string.IsNullOrWhiteSpace(txtMotivoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoReunion.Text) ||
                string.IsNullOrWhiteSpace(txtLugarReunion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Reunion SET fechaReunion = @fechaReunion, motivoReunion = @motivoReunion, estadoReunion = @estadoReunion, lugarReunion = @lugarReunion WHERE idReunion = @idReunion AND idReunion IN (SELECT idReunion FROM Semillero WHERE idSemillero = @idSemillero)", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIdReunion.Text);
            actualizar.Parameters.AddWithValue("@motivoReunion", SqlDbType.VarChar).Value = txtMotivoReunion.Text;
            actualizar.Parameters.AddWithValue("@fechaReunion", SqlDbType.Date).Value = dateTimePickerEvento.Value;
            actualizar.Parameters.AddWithValue("@estadoReunion", SqlDbType.VarChar).Value = txtEstadoReunion.Text;
            actualizar.Parameters.AddWithValue("@lugarReunion", SqlDbType.VarChar).Value = txtLugarReunion.Text;
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar la reunion?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Reunion actualizada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReunion_Click(sender, e);
            }
        }

        private void btnEliminarReunion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDreunionr1.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID de la reunion a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Reunion WHERE idReunion = @idReunion AND idReunion IN (SELECT idReunion FROM Semillero WHERE idSemillero = @idSemillero)", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idReunion", SqlDbType.Int).Value = int.Parse(txtIDreunionr1.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar la reunion?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Reunion eliminada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReunion_Click(sender, e);
            }
        }

        private void btnLimpiarReunion_Click(object sender, EventArgs e)
        {
            txtIdReunion.Clear();
            txtMotivoReunion.Clear();
            txtEstadoReunion.Clear();
            txtLugarReunion.Clear();
            dataGridView1.DataSource = null;
            dateTimePickerReunion.Value = DateTime.Now;
        }

        private void btnConsultarReporte_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT r.* FROM Reporte r, Usuario u, Investigadores i WHERE r.idReporte = u.idReporte AND u.idUsuario = i.idUsuario AND i.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Reporte");
            dataGridView1.DataMember = "Reporte";
            dataGridView1.DataSource = ds;
        }

        private void btnGuardarReporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReporte.Text) ||
                string.IsNullOrWhiteSpace(txtTipoReporte.Text) ||
                string.IsNullOrWhiteSpace(txtHoraReporte.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Reporte (idReporte,tipoReporte,fechaReporte,horaReporte,formatoReporte) VALUES (@idReporte,@tipoReporte,@fechaReporte,@horaReporte,@formatoReporte)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIdReporte.Text);
                insertar.Parameters.AddWithValue("@tipoReporte", SqlDbType.VarChar).Value = txtTipoReporte.Text;
                insertar.Parameters.AddWithValue("@fechaReporte", SqlDbType.Date).Value = DateTime.Today;
                insertar.Parameters.AddWithValue("@horaReporte", SqlDbType.VarChar).Value = txtHoraReporte.Text;
                insertar.Parameters.AddWithValue("@formatoReporte", SqlDbType.VarChar).Value = txtFormatoReporte.Text;
                insertar.ExecuteNonQuery();
                MessageBox.Show("Reporte registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReporte_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarReporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdReporte.Text) ||
                string.IsNullOrWhiteSpace(txtTipoReporte.Text) ||
                string.IsNullOrWhiteSpace(txtHoraReporte.Text) ||
                string.IsNullOrWhiteSpace(txtFormatoReporte.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Reporte SET tipoReporte = @tipoReporte, horaReporte = @horaReporte, formatoReporte = @formatoReporte WHERE idReporte = @idReporte AND idReporte IN (SELECT idReporte FROM Usuario u, Investigadores i WHERE u.idReporte = Reporte.idReporte AND u.idUsuario = i.idUsuario AND i.idSemillero = @idSemillero)", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIdReporte.Text);
            actualizar.Parameters.AddWithValue("@tipoReporte", SqlDbType.VarChar).Value = txtTipoReporte.Text;
            actualizar.Parameters.AddWithValue("@horaReporte", SqlDbType.VarChar).Value = txtHoraReporte.Text;
            actualizar.Parameters.AddWithValue("@formatoReporte", SqlDbType.VarChar).Value = txtFormatoReporte.Text;
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el reporte?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Reporte actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReporte_Click(sender, e);
            }
        }

        private void btnEliminarReporte_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDreporteR.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del reporte a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Reporte WHERE idReporte = @idReporte", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idReporte", SqlDbType.Int).Value = int.Parse(txtIDreporteR.Text);
            if (MessageBox.Show("¿Estás seguro que desea eliminar el reporte?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Reporte eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarReporte_Click(sender, e);
            }
        }

        private void btnLimpiarReporte_Click(object sender, EventArgs e)
        {
            txtIdReporte.Clear();
            txtTipoReporte.Clear();
            txtHoraReporte.Clear();
            txtFormatoReporte.Clear();
            dataGridView1.DataSource = null;
        }

        private void btnConsultarPatrocinador_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT p.* FROM Patrocinadores p, SemilleroEventos se WHERE p.idEvento = se.idEvento AND se.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Patrocinadores");
            dataGridView1.DataMember = "Patrocinadores";
            dataGridView1.DataSource = ds;
        }

        private void btnGuardarPatrocinadores_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtIdEvento1.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Patrocinadores (idPatrocinador,nombrePatrocinador,tipoPatrocinador,correoPatrocinador,telefonoPatrocinador,direccionPatrocinador,idEvento) VALUES (@idPatrocinador,@nombrePatrocinador,@tipoPatrocinador,@correoPatrocinador,@telefonoPatrocinador,@direccionPatrocinador,@idEvento)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIdPatrocinador.Text);
                insertar.Parameters.AddWithValue("@nombrePatrocinador", SqlDbType.VarChar).Value = txtNombrePatrocinador.Text;
                insertar.Parameters.AddWithValue("@tipoPatrocinador", SqlDbType.VarChar).Value = txtTipoPatrocinador.Text;
                insertar.Parameters.AddWithValue("@correoPatrocinador", SqlDbType.VarChar).Value = txtCorreoPatrocinador.Text;
                insertar.Parameters.AddWithValue("@telefonoPatrocinador", SqlDbType.Int).Value = int.Parse(txtTelefonoPatrocinador.Text);
                insertar.Parameters.AddWithValue("@direccionPatrocinador", SqlDbType.VarChar).Value = txtDireccionPatrocinador.Text;
                insertar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento1.Text);
                insertar.ExecuteNonQuery();
                MessageBox.Show("Patrocinador registrado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarPatrocinador_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarPatrocinador_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtNombrePatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTipoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtCorreoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtTelefonoPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtDireccionPatrocinador.Text) ||
                string.IsNullOrWhiteSpace(txtIdEvento1.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Patrocinadores SET nombrePatrocinador = @nombrePatrocinador, tipoPatrocinador = @tipoPatrocinador, correoPatrocinador = @correoPatrocinador, telefonoPatrocinador = @telefonoPatrocinador, direccionPatrocinador = @direccionPatrocinador, idEvento = @idEvento WHERE idPatrocinador = @idPatrocinador AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIdPatrocinador.Text);
            actualizar.Parameters.AddWithValue("@nombrePatrocinador", SqlDbType.VarChar).Value = txtNombrePatrocinador.Text;
            actualizar.Parameters.AddWithValue("@tipoPatrocinador", SqlDbType.VarChar).Value = txtTipoPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@correoPatrocinador", SqlDbType.VarChar).Value = txtCorreoPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@telefonoPatrocinador", SqlDbType.Int).Value = int.Parse(txtTelefonoPatrocinador.Text);
            actualizar.Parameters.AddWithValue("@direccionPatrocinador", SqlDbType.VarChar).Value = txtDireccionPatrocinador.Text;
            actualizar.Parameters.AddWithValue("@idEvento", SqlDbType.Int).Value = int.Parse(txtIdEvento1.Text);
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar el patrocinador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Patrocinador actualizado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarPatrocinador_Click(sender, e);
            }
        }

        private void btnEliminarPatrocinador_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDpatrocinadoresP.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID del patrocinador a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Patrocinadores WHERE idPatrocinador = @idPatrocinador AND idEvento IN (SELECT idEvento FROM SemilleroEventos WHERE idSemillero = @idSemillero)", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idPatrocinador", SqlDbType.Int).Value = int.Parse(txtIDpatrocinadoresP.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar el patrocinador?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Patrocinador eliminado exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarPatrocinador_Click(sender, e);
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            txtIdPatrocinador.Clear();
            txtNombrePatrocinador.Clear();
            txtTipoPatrocinador.Clear();
            txtCorreoPatrocinador.Clear();
            txtTelefonoPatrocinador.Clear();
            txtDireccionPatrocinador.Clear();
            txtIdEvento1.Clear();
            dataGridView1.DataSource = null;
        }

        private void btnConsultarFase_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT f.* FROM Fase f, Proyecto p WHERE f.idProyecto = p.idProyecto AND p.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Fase");
            dataGridView1.DataMember = "Fase";
            dataGridView1.DataSource = ds;
        }

        private void btnGuardarFase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdFase.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionFase.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoFase.Text) ||
                string.IsNullOrWhiteSpace(txtIdProyecto1.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Fase (idFase,fechaFase,descripcionFase,estadoFase,idProyecto) VALUES (@idFase,@fechaFase,@descripcionFase,@estadoFase,@idProyecto)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idFase", SqlDbType.Int).Value = int.Parse(txtIdFase.Text);
                insertar.Parameters.AddWithValue("@fechaFase", SqlDbType.Date).Value = DateTime.Today;
                insertar.Parameters.AddWithValue("@descripcionFase", SqlDbType.VarChar).Value = txtDescripcionFase.Text;
                insertar.Parameters.AddWithValue("@estadoFase", SqlDbType.VarChar).Value = txtEstadoFase.Text;
                insertar.Parameters.AddWithValue("@idProyecto", SqlDbType.Int).Value = int.Parse(txtIdProyecto1.Text);
                insertar.ExecuteNonQuery();
                MessageBox.Show("Fase registrada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarFase_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarFase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdFase.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcionFase.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoFase.Text) ||
                string.IsNullOrWhiteSpace(txtIdProyecto1.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Fase SET fechaFase = @fechaFase, descripcionFase = @descripcionFase, estadoFase = @estadoFase, idProyecto = @idProyecto WHERE idFase = @idFase AND idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero)", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idFase", SqlDbType.Int).Value = int.Parse(txtIdFase.Text);
            actualizar.Parameters.AddWithValue("@descripcionFase", SqlDbType.VarChar).Value = txtDescripcionFase.Text;
            actualizar.Parameters.AddWithValue("@estadoFase", SqlDbType.VarChar).Value = txtEstadoFase.Text;
            actualizar.Parameters.AddWithValue("@fechaFase", SqlDbType.Date).Value = DateTime.Today;
            actualizar.Parameters.AddWithValue("@idProyecto", SqlDbType.Int).Value = int.Parse(txtIdProyecto1.Text);
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar la fase?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Fase actualizada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarFase_Click(sender, e);
            }
        }

        private void btnLimpiarFase_Click(object sender, EventArgs e)
        {
            txtIdFase.Clear();
            txtDescripcionFase.Clear();
            txtEstadoFase.Clear();
            txtIdProyecto1.Clear();
            dataGridView1.DataSource = null;
            txtIDfasesF.Clear();
        }

        private void btnEliminarFase_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDfasesF.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID de la fase a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Fase WHERE idFase = @idFase AND idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero)", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idFase", SqlDbType.Int).Value = int.Parse(txtIDfasesF.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar la fase?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Fase eliminada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarFase_Click(sender, e);
            }
        }

        private void btnConsultarActividad_Click(object sender, EventArgs e)
        {
            SqlCommand consulta;
            consulta = new SqlCommand("SELECT a.* FROM Actividad a, Fase f, Proyecto p WHERE a.idFase = f.idFase AND f.idProyecto = p.idProyecto AND p.idSemillero = @idSemillero", cn.Conectar());
            consulta.CommandType = CommandType.Text;
            consulta.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            ds.Clear();
            SqlDataAdapter da = new SqlDataAdapter(consulta);
            da.Fill(ds, "Actividad");
            dataGridView1.DataMember = "Actividad";
            dataGridView1.DataSource = ds;
        }

        private void btnGuardarActividad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdActividad.Text) ||
                string.IsNullOrWhiteSpace(txtNombreActividad.Text) ||
                string.IsNullOrWhiteSpace(txtDescripActividad.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoActividad.Text) ||
                string.IsNullOrWhiteSpace(txtLugarActividad.Text) ||
                string.IsNullOrWhiteSpace(txtIdFaseActividad.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand insertar;
            try
            {
                insertar = new SqlCommand("INSERT INTO Actividad (idActividad,nombreActividad,descripActividad,fechaActividad,estadoActividad,lugarActividad,idFase) VALUES (@idActividad,@nombreActividad,@descripActividad,@fechaActividad,@estadoActividad,@lugarActividad,@idFase)", cn.Conectar());
                insertar.CommandType = CommandType.Text;
                insertar.Parameters.AddWithValue("@idActividad", SqlDbType.Int).Value = int.Parse(txtIdActividad.Text);
                insertar.Parameters.AddWithValue("@nombreActividad", SqlDbType.VarChar).Value = txtNombreActividad.Text;
                insertar.Parameters.AddWithValue("@descripActividad", SqlDbType.VarChar).Value = txtDescripActividad.Text;
                insertar.Parameters.AddWithValue("@fechaActividad", SqlDbType.Date).Value = DateTime.Today;
                insertar.Parameters.AddWithValue("@estadoActividad", SqlDbType.VarChar).Value = txtEstadoActividad.Text;
                insertar.Parameters.AddWithValue("@lugarActividad", SqlDbType.VarChar).Value = txtLugarActividad.Text;
                insertar.Parameters.AddWithValue("@idFase", SqlDbType.Int).Value = int.Parse(txtIdFaseActividad.Text);
                insertar.ExecuteNonQuery();
                MessageBox.Show("Actividad registrada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarActividad_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizarActividad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdActividad.Text) ||
                string.IsNullOrWhiteSpace(txtNombreActividad.Text) ||
                string.IsNullOrWhiteSpace(txtDescripActividad.Text) ||
                string.IsNullOrWhiteSpace(txtEstadoActividad.Text) ||
                string.IsNullOrWhiteSpace(txtLugarActividad.Text) ||
                string.IsNullOrWhiteSpace(txtIdFaseActividad.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de continuar.",
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand actualizar;
            actualizar = new SqlCommand("UPDATE Actividad SET nombreActividad = @nombreActividad, descripActividad = @descripActividad, estadoActividad = @estadoActividad, lugarActividad = @lugarActividad, idFase = @idFase WHERE idActividad = @idActividad AND idFase IN (SELECT idFase FROM Fase WHERE idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero))", cn.Conectar());
            actualizar.CommandType = CommandType.Text;
            actualizar.Parameters.AddWithValue("@idActividad", SqlDbType.Int).Value = int.Parse(txtIdActividad.Text);
            actualizar.Parameters.AddWithValue("@nombreActividad", SqlDbType.VarChar).Value = txtNombreActividad.Text;
            actualizar.Parameters.AddWithValue("@descripActividad", SqlDbType.VarChar).Value = txtDescripActividad.Text;
            actualizar.Parameters.AddWithValue("@estadoActividad", SqlDbType.VarChar).Value = txtEstadoActividad.Text;
            actualizar.Parameters.AddWithValue("@lugarActividad", SqlDbType.VarChar).Value = txtLugarActividad.Text;
            actualizar.Parameters.AddWithValue("@idFase", SqlDbType.Int).Value = int.Parse(txtIdFaseActividad.Text);
            actualizar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea actualizar la actividad?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Actividad actualizada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarActividad_Click(sender, e);
            }
        }

        private void btnEliminarActividad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDactividadA.Text))
            {
                MessageBox.Show("Por favor, ingrese el ID de la actividad a eliminar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlCommand eliminar;
            eliminar = new SqlCommand("DELETE FROM Actividad WHERE idActividad = @idActividad AND idFase IN (SELECT idFase FROM Fase WHERE idProyecto IN (SELECT idProyecto FROM Proyecto WHERE idSemillero = @idSemillero))", cn.Conectar());
            eliminar.CommandType = CommandType.Text;
            eliminar.Parameters.AddWithValue("@idActividad", SqlDbType.Int).Value = int.Parse(txtIDactividadA.Text);
            eliminar.Parameters.AddWithValue("@idSemillero", SqlDbType.Int).Value = this.idSemillero;
            if (MessageBox.Show("¿Estás seguro que desea eliminar la actividad?",
                                "ProyectoSemillero", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                eliminar.ExecuteNonQuery();
                MessageBox.Show("Actividad eliminada exitosamente", "ProyectoSemillero",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConsultarActividad_Click(sender, e);
            }
        }

        private void btnLimpiarActividad_Click(object sender, EventArgs e)
        {
            txtIdActividad.Clear();
            txtNombreActividad.Clear();
            txtDescripActividad.Clear();
            txtEstadoActividad.Clear();
            txtLugarActividad.Clear();
            txtIdFaseActividad.Clear();
            dataGridView1.DataSource = null;
            txtIDactividadA.Clear();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Desea cerrar sesion?", "SemilleroProyecto", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void ValidacionNumeros(KeyPressEventArgs e)// Método para validar que solo se ingresen números, tecla de retroceso y dos puntos (para campos de hora)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ':')// Permite solo dígitos, tecla de retroceso y dos puntos (para campos de hora)
            {
                // Si el carácter no es un dígito, retroceso o dos puntos, se marca como manejado para evitar que se ingrese
                e.Handled = true;
                MessageBox.Show("Solo se permiten números",
                    "Caracter invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = true;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = true;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = true;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = true;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = true;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = true;
            FaseActividadGroupBox.Visible = false;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            MiembrosGroupBox.Visible = false;
            SemilleroGroupBox.Visible = false;
            ProyectosGroupBox.Visible = false;
            EventosGroupBox.Visible = false;
            ReunionGroupBox.Visible = false;
            ReportesGroupBox.Visible = false;
            PatrocinadoresGroupBox.Visible = false;
            FaseActividadGroupBox.Visible = true;
        }

        private void txtIdFase_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdProyecto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdFaseActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdPatrocinador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtTelefonoPatrocinador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdEvento1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtHoraReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReunionr_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtIdProyecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdInvestigador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtTelefonoInvestigador_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void txtIdReunion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros(e);
        }

        private void cboTablaInvest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTablaInvest.SelectedItem != null)// Verificar que se haya seleccionado un elemento antes de intentar cargar los parámetros
            {
                // Cargar los parámetros correspondientes a la tabla seleccionada en el segundo ComboBox
                ConsultarConParametro.CargarParametros(cboTablaInvest.SelectedItem.ToString(), cboParametroInvest);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado una tabla, un parámetro y que se haya ingresado un valor de búsqueda antes de realizar la consulta
            if (cboTablaInvest.SelectedItem == null ||
                cboParametroInvest.SelectedItem == null ||
                string.IsNullOrWhiteSpace(cboParametroInvest.SelectedItem.ToString()) ||
                string.IsNullOrWhiteSpace(txtValorBusquedaInvest.Text))
            {
                // Mostrar un mensaje de advertencia si falta alguno de los campos requeridos para la búsqueda
                MessageBox.Show("Por favor seleccione tabla, parametro e ingrese un valor.",
                                "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Realizar la consulta utilizando el método ConsultarParametroSegunSemillero y mostrar los resultados en el DataGridView
            DataTable resultado = ConsultarConParametro.ConsultarParametroSegunSemillero(
                cboTablaInvest.SelectedItem.ToString(),
                cboParametroInvest.SelectedItem.ToString(),
                txtValorBusquedaInvest.Text,
                this.idSemillero,
                cn);

            if (resultado.Rows.Count == 0)// Verificar si la consulta no devolvió resultados y mostrar un mensaje informativo en ese caso
            {
                // Mostrar un mensaje informativo si no se encontraron resultados para la búsqueda realizada
                MessageBox.Show("No se encontraron resultados para la búsqueda realizada.",
                                "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
                return;
            }

            dataGridView1.DataSource = resultado;// Mostrar los resultados de la consulta en el DataGridView
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de búsqueda y el DataGridView para permitir realizar una nueva búsqueda
            cboTablaInvest.SelectedIndex = -1;
            cboParametroInvest.Items.Clear();
            txtValorBusquedaInvest.Clear();
            dataGridView1.DataSource = null;
        }
    }
}
