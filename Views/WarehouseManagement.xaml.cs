using inzynier.Temp;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace inzynier.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class WarehouseManagement : Window
    {
        public WarehouseManagement()
        {
            InitializeComponent();
            RefreshData();
        }
        private void Windows_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsAdmin.IsAdminn == "Admin")
            {
                AdminPage win2 = new();
                win2.Show();
                this.Close(); //only if you want to close the current form.
            }
            else if (IsAdmin.IsAdminn == "User")
            {
                UserPage new44 = new();
                new44.Show();
                this.Close();
            }
            else
            {
                SuperUser new61 = new();
                new61.Show();
                this.Close();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Odczytanie wartości z TextBoxów
            string Namee = Warehouse.Text;
            string Locationn = Location.Text;

            // Sprawdzenie, czy pola nie są puste
            if (string.IsNullOrWhiteSpace(Namee) || string.IsNullOrWhiteSpace(Locationn))
            {
                MessageBox.Show("Wypełnij wszystkie pola przed dodaniem do bazy danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Przerwanie działania metody, jeśli pola są puste
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Polecenie SQL do wstawienia danych
            string sql = $"INSERT INTO [inz].[dbo].[Warehouse] ([Warehouse], [Location]) VALUES" +
                         $" ('{Namee}','{Locationn}');";

            try
            {
                // Wykonanie polecenia SQL
                SqlCommand command = new(sql, connection);
                command.ExecuteNonQuery();

                // Aktualizowanie danych w DataGridu
                RefreshData();

                // Wyczyszczenie pola Location po dodaniu rekordu
                Location.Text = string.Empty;
            }
            catch (SqlException ex)
            {
                // Obsługa sytuacji, gdy próbujemy dodać rekord o istniejącej już wartości w kolumnie Location
                if (ex.Number == 2627) // Numer błędu dla duplikatu klucza
                {
                    MessageBox.Show("Rekord o podanej lokalizacji już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Błąd SQL: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void RefreshData()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TOP (1000) [Warehouse],[Location]FROM [inz].[dbo].[Warehouse]";

                // Tworzenie obiektu SqlDataAdapter
                SqlDataAdapter adapter = new(sql, connection);

                // Tworzenie obiektu DataTable
                DataTable dataTable = new();

                // Wypełnianie DataTable danymi z bazy danych
                adapter.Fill(dataTable);

                // Przypisanie DataTable do DataGrida
                War.ItemsSource = dataTable.DefaultView;
            }
        }


    }
}