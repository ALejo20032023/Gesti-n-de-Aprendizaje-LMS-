using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class ForosWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable forosTable;
        private DataRowView selectedRow;

        public ForosWindow()
        {
            InitializeComponent();
            LoadForos();
            LoadCursos();
        }

        private void LoadForos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT f.ID, f.CursoID, f.Titulo, f.Descripcion, f.FechaCreacion, c.Titulo as CursoNombre " +
                                   "FROM Foros f " +
                                   "JOIN Cursos c ON f.CursoID = c.ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    forosTable = new DataTable();
                    adapter.Fill(forosTable);

                    ForosGrid.ItemsSource = forosTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los foros: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void ForosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ForosGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)ForosGrid.SelectedItem;

                ComboCurso.SelectedValue = selectedRow["CursoID"];
                TxtTitulo.Text = selectedRow["Titulo"].ToString();
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
                FechaCreacionPicker.SelectedDate = (selectedRow["FechaCreacion"] != DBNull.Value) ? (DateTime?)selectedRow["FechaCreacion"] : null;
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
                        string query = "UPDATE Foros SET CursoID = @cursoID, Titulo = @titulo, Descripcion = @descripcion, FechaCreacion = @FechaCreacion WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                        command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        if (FechaCreacionPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaCreacion", FechaCreacionPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaCreacion", DBNull.Value);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadForos();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún foro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Foros (CursoID, Titulo, Descripcion, FechaCreacion) VALUES (@cursoID, @titulo, @descripcion, @FechaCreacion)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cursoID", ComboCurso.SelectedValue);
                    command.Parameters.AddWithValue("@titulo", TxtTitulo.Text);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                    if (FechaCreacionPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaCreacion", FechaCreacionPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaCreacion", DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadForos();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el foro: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este foro?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Foros WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadForos();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún foro.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el foro: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            ComboCurso.SelectedIndex = -1;
            TxtTitulo.Text = "";
            TxtDescripcion.Text = "";
            FechaCreacionPicker.SelectedDate = null;
        }
    }
}
