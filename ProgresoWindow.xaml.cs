using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GestionAprendisaje
{
    public partial class ProgresoWindow : Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private DataTable progresoTable;

        public ProgresoWindow()
        {
            InitializeComponent();
            LoadProgreso();
        }

        private void LoadProgreso()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UsuarioID, CursoID, Progreso, UltimaActualizacion FROM Progreso";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    progresoTable = new DataTable();
                    adapter.Fill(progresoTable);

                    ProgresoGrid.ItemsSource = progresoTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el progreso: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
