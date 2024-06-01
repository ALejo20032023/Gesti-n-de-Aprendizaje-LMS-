using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace GestionAprendisaje
{
    public partial class LoginWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public bool IsLoggedIn { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(1) FROM CredencialesUsuario WHERE Username = @Username AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", TxtUsername.Text);
                    command.Parameters.AddWithValue("@Password", TxtPassword.Password); // Considerar el hash de la contraseña para mayor seguridad

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {
                        MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsLoggedIn = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsLoggedIn = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsLoggedIn = false;
            }
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            RegistroWindow registroWindow = new RegistroWindow();
            registroWindow.ShowDialog();
        }
    }
}
