using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class EvaluacionesWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable evaluacionesTable;
        private DataRowView selectedRow;

        public EvaluacionesWindow()
        {
            InitializeComponent();
            LoadEvaluaciones();
            LoadCursos();
        }

        private void LoadEvaluaciones()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT e.ID, e.CursoID, e.Titulo, e.Descripcion, e.Fecha, c.Titulo as CursoNombre " +
                                   "FROM Evaluaciones e " +
                                   "JOIN Cursos c ON e.CursoID = c.ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    evaluacionesTable = new DataTable();
                    adapter.Fill(evaluacionesTable);

                    EvaluacionesGrid.ItemsSource = evaluacionesTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las evaluaciones: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void EvaluacionesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EvaluacionesGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)EvaluacionesGrid.SelectedItem;

                ComboCurso.SelectedValue = selectedRow["CursoID"];
                TxtTitulo.Text = selectedRow["Titulo"].ToString();
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
                FechaPicker.SelectedDate = (selectedRow["Fecha"] != DBNull.Value) ? (DateTime?)selectedRow["Fecha"] : null;
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
                        string query = "UPDATE Evaluaciones SET CursoID = @cursoID, Titulo = @titulo, Descripcion = @descripcion, Fecha = @fecha WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                        command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        if (FechaPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@fecha", FechaPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@fecha", DBNull.Value);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadEvaluaciones();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna evaluación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Evaluaciones (CursoID, Titulo, Descripcion, Fecha) VALUES (@cursoID, @titulo, @descripcion, @fecha)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                    command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                    if (FechaPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@fecha", FechaPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@fecha", DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadEvaluaciones();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la evaluación: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar esta evaluación?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Evaluaciones WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadEvaluaciones();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna evaluación.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la evaluación: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            ComboCurso.SelectedIndex = -1;
            TxtTitulo.Text = "";
            TxtDescripcion.Text = "";
            FechaPicker.SelectedDate = null;
        }
    }
}
