using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class CursosWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable cursosTable;
        private DataRowView selectedRow;

        public CursosWindow()
        {
            InitializeComponent();
            LoadCursos();
        }

        private void LoadCursos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Titulo, Descripcion, FechaInicio, FechaFin FROM Cursos";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    cursosTable = new DataTable();
                    adapter.Fill(cursosTable);

                    CursosGrid.ItemsSource = cursosTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cursos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CursosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CursosGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)CursosGrid.SelectedItem;

                TxtTitulo.Text = selectedRow["Titulo"].ToString();
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
                FechaInicioPicker.SelectedDate = (selectedRow["FechaInicio"] != DBNull.Value) ? (DateTime?)selectedRow["FechaInicio"] : null;
                FechaFinPicker.SelectedDate = (selectedRow["FechaFin"] != DBNull.Value) ? (DateTime?)selectedRow["FechaFin"] : null;
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
                        string query = "UPDATE Cursos SET Titulo = @titulo, Descripcion = @descripcion, FechaInicio = @FechaInicio, FechaFin = @FechaFin WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        if (FechaInicioPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaInicio", FechaInicioPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaInicio", DBNull.Value);
                        if (FechaFinPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaFin", FechaFinPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaFin", DBNull.Value);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadCursos();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún curso.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Cursos (Titulo, Descripcion, FechaInicio, FechaFin) VALUES (@titulo, @descripcion, @FechaInicio, @FechaFin)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                    if (FechaInicioPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaInicio", FechaInicioPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaInicio", DBNull.Value);
                    if (FechaFinPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaFin", FechaFinPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaFin", DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadCursos();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el curso: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este curso?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Cursos WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadCursos();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún curso.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el curso: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            TxtTitulo.Text = "";
            TxtDescripcion.Text = "";
            FechaInicioPicker.SelectedDate = null;
            FechaFinPicker.SelectedDate = null;
        }
    }
}
