using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class CalificacionesWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable calificacionesTable;
        private DataRowView selectedRow;

        public CalificacionesWindow()
        {
            InitializeComponent();
            LoadCalificaciones();
            LoadUsuarios();
            LoadCursos();
        }

        private void LoadCalificaciones()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT c.UsuarioID, c.CursoID, c.Calificacion, c.Comentarios, u.Nombre as UsuarioNombre, cu.Titulo as CursoNombre " +
                                   "FROM Calificaciones c " +
                                   "JOIN Usuarios u ON c.UsuarioID = u.ID " +
                                   "JOIN Cursos cu ON c.CursoID = cu.ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    calificacionesTable = new DataTable();
                    adapter.Fill(calificacionesTable);

                    CalificacionesGrid.ItemsSource = calificacionesTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las calificaciones: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUsuarios()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Nombre FROM Usuarios";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable usuariosTable = new DataTable();
                    adapter.Fill(usuariosTable);

                    ComboUsuario.ItemsSource = usuariosTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCursos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Titulo FROM Cursos";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    DataTable cursosTable = new DataTable();
                    adapter.Fill(cursosTable);

                    ComboCurso.ItemsSource = cursosTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cursos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalificacionesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CalificacionesGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)CalificacionesGrid.SelectedItem;

                ComboUsuario.SelectedValue = selectedRow["UsuarioID"];
                ComboCurso.SelectedValue = selectedRow["CursoID"];
                TxtCalificacion.Text = selectedRow["Calificacion"].ToString();
                TxtComentarios.Text = selectedRow["Comentarios"].ToString();
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Calificaciones SET UsuarioID = @usuarioID, CursoID = @cursoID, Calificacion = @calificacion, Comentarios = @comentarios WHERE UsuarioID = @oldUsuarioID AND CursoID = @oldCursoID";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@usuarioID", ComboUsuario.SelectedValue);
                        command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                        command.Parameters.AddWithValue("@calificacion", TxtCalificacion.Text);
                        command.Parameters.AddWithValue("@comentarios", TxtComentarios.Text);
                        command.Parameters.AddWithValue("@oldUsuarioID", selectedRow["UsuarioID"]);
                        command.Parameters.AddWithValue("@oldCursoID", selectedRow["CursoID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadCalificaciones();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna calificación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Calificaciones (UsuarioID, CursoID, Calificacion, Comentarios) VALUES (@usuarioID, @cursoID, @calificacion, @comentarios)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@usuarioID", ComboUsuario.SelectedValue);
                    command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                    command.Parameters.AddWithValue("@calificacion", TxtCalificacion.Text);
                    command.Parameters.AddWithValue("@comentarios", TxtComentarios.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadCalificaciones();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la calificación: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta calificación?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Calificaciones WHERE UsuarioID = @usuarioID AND CursoID = @cursoID";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@usuarioID", (int)selectedRow["UsuarioID"]);
                            command.Parameters.AddWithValue("@cursoID", (int)selectedRow["CursoID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadCalificaciones();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna calificación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la calificación: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            ComboUsuario.SelectedIndex = -1;
            ComboCurso.SelectedIndex = -1;
            TxtCalificacion.Text = "";
            TxtComentarios.Text = "";
        }
    }
}
