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
using MySql.Data.MySqlClient;

namespace AccesoBaseDatos1
{
    public partial class Form1 : Form
    {
        private string connectionString = "";  // Se actualizará dinámicamente
        private SqlConnection sqlConnection;   // Para SQL Server
        private MySqlConnection mySqlConnection; // Para MySQL

        // Variables de conexión para MySQL
        private string Servidor = "localhost";
        private string Basedatos = "ESCOLAR";
        private string UsuarioId = "root";
        private string Password = "";

        public Form1()
        {
            InitializeComponent();
            CargarDatos();  // Cargar los datos al iniciar el formulario
        }

        private void chkSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSQLServer.Checked)
            {
                chkMySQL.Checked = false;
                connectionString = "Server=BOREGO-PC\\SQLEXPRESS;Database=ESCOLAR;User Id=sa;Password=roguez64;";
            }
        }

        private void txtNoControl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCarrera_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "INSERT INTO Estudiante VALUES (@NoControl, @Nombre, @Carrera)";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                        command.ExecuteNonQuery();
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "INSERT INTO Estudiante (NoControl, Nombre, Carrera) VALUES (@NoControl, @Nombre, @Carrera)";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registro insertado correctamente.");
                CargarDatos();  // Refresca la tabla automáticamente
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "UPDATE Estudiante SET Nombre=@Nombre, Carrera=@Carrera WHERE NoControl=@NoControl";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                        command.ExecuteNonQuery();
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "UPDATE Estudiante SET Nombre=@Nombre, Carrera=@Carrera WHERE NoControl=@NoControl";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Carrera", txtCarrera.Text);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registro actualizado correctamente.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "DELETE FROM Estudiante WHERE NoControl=@NoControl";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.ExecuteNonQuery();
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "DELETE FROM Estudiante WHERE NoControl=@NoControl";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.Parameters.AddWithValue("@NoControl", int.Parse(txtNoControl.Text));
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registro eliminado correctamente.");
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "SELECT * FROM Estudiante WHERE NoControl=@NoControl";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.Parameters.AddWithValue("@NoControl", txtNoControl.Text);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtCarrera.Text = reader["Carrera"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Registro no encontrado.");
                        }
                        reader.Close();
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "SELECT * FROM Estudiante WHERE NoControl=@NoControl";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.Parameters.AddWithValue("@NoControl", txtNoControl.Text);
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtCarrera.Text = reader["Carrera"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Registro no encontrado.");
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void chkMySQL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMySQL.Checked)
            {
                chkSQLServer.Checked = false;
                connectionString = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password};";
            }
        }

        private void btnCrearBD_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "CREATE DATABASE ESCOLAR";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Base de datos creada correctamente en SQL Server.");
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection($"Server={Servidor};User Id={UsuarioId};Password={Password};"))
                    {
                        mySqlConnection.Open();
                        string query = "CREATE DATABASE ESCOLAR";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Base de datos creada correctamente en MySQL.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCrearTabla_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "USE ESCOLAR; CREATE TABLE Estudiante (NoControl INT PRIMARY KEY, Nombre NVARCHAR(100), Carrera NVARCHAR(50));";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Tabla creada correctamente en SQL Server.");
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "CREATE TABLE IF NOT EXISTS Estudiante (NoControl INT PRIMARY KEY, Nombre VARCHAR(100), Carrera VARCHAR(50));";
                        MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Tabla creada correctamente en MySQL.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            txtNoControl.Clear();
            txtNombre.Clear();
            txtCarrera.Clear();
            CargarDatos();  // Llama a la función para actualizar el `DataGridView`
        }

        private void dgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CargarDatos()
        {
            try
            {
                DataTable dt = new DataTable();

                if (chkSQLServer.Checked)
                {
                    using (sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "SELECT * FROM Estudiante";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
                        adapter.Fill(dt);
                    }
                }
                else if (chkMySQL.Checked)
                {
                    using (mySqlConnection = new MySqlConnection(connectionString))
                    {
                        mySqlConnection.Open();
                        string query = "SELECT * FROM Estudiante";
                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection);
                        adapter.Fill(dt);
                    }
                }

                dgvAlumnos.DataSource = dt;  // Asignamos los datos al DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }
    }
}
