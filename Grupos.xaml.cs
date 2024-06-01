using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class GruposWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable gruposTable;
        private DataRowView selectedRow;

        public GruposWindow()
        {
            InitializeComponent();
            LoadGrupos();
        }

        private void LoadGrupos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Nombre, Descripcion, FechaCreacion FROM Grupos";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    gruposTable = new DataTable();
                    adapter.Fill(gruposTable);

                    GruposGrid.ItemsSource = gruposTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los grupos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GruposGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GruposGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)GruposGrid.SelectedItem;

                TxtNombre.Text = selectedRow["Nombre"].ToString();
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
                        string query = "UPDATE Grupos SET Nombre = @nombre, Descripcion = @descripcion, FechaCreacion = @FechaCreacion WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        if (FechaCreacionPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaCreacion", FechaCreacionPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaCreacion", DBNull.Value);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadGrupos();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún grupo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Grupos (Nombre, Descripcion, FechaCreacion) VALUES (@nombre, @descripcion, @FechaCreacion)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                    if (FechaCreacionPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaCreacion", FechaCreacionPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaCreacion", DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadGrupos();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el grupo: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este grupo?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Grupos WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadGrupos();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún grupo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el grupo: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            FechaCreacionPicker.SelectedDate = null;
        }
    }
}
