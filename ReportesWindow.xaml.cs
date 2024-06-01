using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class ReportesWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable reportesTable;
        private DataRowView selectedRow;

        public ReportesWindow()
        {
            InitializeComponent();
            LoadReportes();
        }

        private void LoadReportes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Nombre, FechaGeneracion, Descripcion FROM Reportes";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    reportesTable = new DataTable();
                    adapter.Fill(reportesTable);

                    ReportesGrid.ItemsSource = reportesTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los reportes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReportesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReportesGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)ReportesGrid.SelectedItem;

                TxtNombre.Text = selectedRow["Nombre"].ToString();
                FechaGeneracionPicker.SelectedDate = (selectedRow["FechaGeneracion"] != DBNull.Value) ? (DateTime?)selectedRow["FechaGeneracion"] : null;
                TxtDescripcion.Text = selectedRow["Descripcion"].ToString();
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
                        string query = "UPDATE Reportes SET Nombre = @nombre, FechaGeneracion = @FechaGeneracion, Descripcion = @descripcion WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                        if (FechaGeneracionPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaGeneracion", FechaGeneracionPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaGeneracion", DBNull.Value);
                        command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);
                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadReportes();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún reporte.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Reportes (Nombre, FechaGeneracion, Descripcion) VALUES (@nombre, @FechaGeneracion, @descripcion)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    if (FechaGeneracionPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaGeneracion", FechaGeneracionPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaGeneracion", DBNull.Value);
                    command.Parameters.AddWithValue("@descripcion", TxtDescripcion.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadReportes();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este reporte?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string query = "DELETE FROM Reportes WHERE ID = @id";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                            connection.Open();
                            command.ExecuteNonQuery();

                            LoadReportes();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún reporte.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el reporte: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearInputs()
        {
            TxtNombre.Text = "";
            FechaGeneracionPicker.SelectedDate = null;
            TxtDescripcion.Text = "";
        }
    }
}
