using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class MaterialesWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable materialesTable;
        private DataRowView selectedRow;

        public MaterialesWindow()
        {
            InitializeComponent();
            LoadMateriales();
        }

        private void LoadMateriales()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Nombre, Descripcion, Cantidad, FechaIngreso FROM Materiales";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    materialesTable = new DataTable();
                    adapter.Fill(materialesTable);

                    MaterialesGrid.ItemsSource = materialesTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los materiales: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MaterialesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MaterialesGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)MaterialesGrid.SelectedItem;

                TxtNombre.Text = selectedRow["Nombre"].ToString();
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
                TxtCantidad.Text = selectedRow["Cantidad"].ToString();
                FechaIngresoPicker.SelectedDate = (selectedRow["FechaIngreso"] != DBNull.Value) ? (DateTime?)selectedRow["FechaIngreso"] : null;
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
                        string query = "UPDATE Materiales SET Nombre = @nombre, Descripcion = @descripcion, Cantidad = @cantidad, FechaIngreso = @FechaIngreso WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        command.Parameters.AddWithValue("@cantidad", TxtCantidad.Text);
                        if (FechaIngresoPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaIngreso", FechaIngresoPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaIngreso", DBNull.Value);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadMateriales();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún material.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Materiales (Nombre, Descripcion, Cantidad, FechaIngreso) VALUES (@nombre, @descripcion, @cantidad, @FechaIngreso)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                    command.Parameters.AddWithValue("@cantidad", TxtCantidad.Text);
                    if (FechaIngresoPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaIngreso", FechaIngresoPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaIngreso", DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadMateriales();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el material: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este material?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Materiales WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadMateriales();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún material.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el material: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            TxtCantidad.Text = "";
            FechaIngresoPicker.SelectedDate = null;
        }
    }
}
