using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class UsuariosWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable usuariosTable;
        private DataRowView selectedRow;

        public UsuariosWindow()
        {
            InitializeComponent();
            LoadUsuarios();
        }

        private void LoadUsuarios()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT ID, Nombre, Email, FechaRegistro, Foto FROM Usuarios";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    usuariosTable = new DataTable();
                    adapter.Fill(usuariosTable);

                    UsuariosGrid.ItemsSource = usuariosTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UsuariosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsuariosGrid.SelectedItem != null)
            {
                selectedRow = (DataRowView)UsuariosGrid.SelectedItem;

                TxtNombre.Text = selectedRow["Nombre"].ToString();
                TxtEmail.Text = selectedRow["Email"].ToString();
                FechaRegistroPicker.SelectedDate = (selectedRow["FechaRegistro"] != DBNull.Value) ? (DateTime?)selectedRow["FechaRegistro"] : null;
                TxtFoto.Text = selectedRow["Foto"].ToString();
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
                        string query = "UPDATE Usuarios SET Nombre = @nombre, Email = @Email, FechaRegistro = @FechaRegistro, Foto = @Foto WHERE ID = @id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                        command.Parameters.AddWithValue("@Email", TxtEmail.Text);
                        if (FechaRegistroPicker.SelectedDate.HasValue)
                            command.Parameters.AddWithValue("@FechaRegistro", FechaRegistroPicker.SelectedDate.Value);
                        else
                            command.Parameters.AddWithValue("@FechaRegistro", DBNull.Value);
                        command.Parameters.AddWithValue("@Foto", TxtFoto.Text);

                        command.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);

                        connection.Open();
                        command.ExecuteNonQuery();

                        LoadUsuarios();
                        ClearInputs();
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    string query = "INSERT INTO Usuarios (Nombre, Email, FechaRegistro, Foto) VALUES (@nombre, @Email, @FechaRegistro, @Foto)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    command.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    if (FechaRegistroPicker.SelectedDate.HasValue)
                        command.Parameters.AddWithValue("@FechaRegistro", FechaRegistroPicker.SelectedDate.Value);
                    else
                        command.Parameters.AddWithValue("@FechaRegistro", DBNull.Value);
                    command.Parameters.AddWithValue("@Foto", TxtFoto.Text);

                    connection.Open();
                    command.ExecuteNonQuery();

                    LoadUsuarios();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedRow != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Eliminar las filas relacionadas en la tabla Calificaciones
                            string deleteCalificacionesQuery = "DELETE FROM Calificaciones WHERE UsuarioID = @usuarioID";
                            SqlCommand deleteCalificacionesCommand = new SqlCommand(deleteCalificacionesQuery, connection);
                            deleteCalificacionesCommand.Parameters.AddWithValue("@usuarioID", (int)selectedRow["ID"]);
                            deleteCalificacionesCommand.ExecuteNonQuery();

                            // Ahora eliminar el usuario
                            string deleteUserQuery = "DELETE FROM Usuarios WHERE ID = @id";
                            SqlCommand deleteUserCommand = new SqlCommand(deleteUserQuery, connection);
                            deleteUserCommand.Parameters.AddWithValue("@id", (int)selectedRow["ID"]);
                            deleteUserCommand.ExecuteNonQuery();

                            LoadUsuarios();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearInputs()
        {
            TxtNombre.Text = "";
            TxtEmail.Text = "";
            FechaRegistroPicker.SelectedDate = null;
            TxtFoto.Text = "";
        }
    }
}
