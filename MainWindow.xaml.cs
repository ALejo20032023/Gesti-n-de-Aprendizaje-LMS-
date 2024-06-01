using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace GestionAprendisaje
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Mostrar ventana de inicio de sesión
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (!loginWindow.IsLoggedIn)
            {
                Application.Current.Shutdown();
                return;
            }

            InitializeComponent();
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionStringSettings == null)
            {
                MessageBox.Show("La cadena de conexión no se encuentra en el archivo de configuración.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = connectionStringSettings.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("La cadena de conexión está vacía o no está configurada correctamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show("Conexión a la base de datos exitosa.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new UsuariosWindow());
        }

        private void BtnCursos_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new CursosWindow());
        }

        private void BtnEvaluaciones_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new EvaluacionesWindow());
        }

        private void BtnMateriales_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new MaterialesWindow());
        }

        private void BtnForos_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new ForosWindow());
        }

        private void BtnCalificaciones_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new CalificacionesWindow());
        }

        private void BtnGrupos_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new GruposWindow());
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new ReportesWindow());
        }
    }
}
