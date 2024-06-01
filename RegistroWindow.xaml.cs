using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace GestionAprendisaje
{
    public partial class RegistroWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public RegistroWindow()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO CredencialesUsuario (Nombre, Email, FechaRegistro, Username, Password) VALUES (@nombre, @Email, @FechaRegistro, @Username, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", TxtNombre.Text);
                    command.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    command.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);
                    command.Parameters.AddWithValue("@Username", TxtUsername.Text);
                    command.Parameters.AddWithValue("@Password", TxtPassword.Password); // Considerar el hash de la contraseña para mayor seguridad

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Usuario registrado exitosamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
