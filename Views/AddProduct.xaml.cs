using inzynier.Temp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace inzynier.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class AddProduct : Window
    {


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Tutaj umieść kod, który chcesz wykonać przy inicjalizacji okna
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT  [Id],[Name]   ,[Location]  ,[Hight]   ,[Width],[Warehouse] FROM [inz_xd].[dbo].[Products_inz]";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            gri.ItemsSource = dataTable.DefaultView;
        }
        public AddProduct()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }
        private void Windows_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Click2(object sender, RoutedEventArgs e)
        {

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
            string Namee = namee.Text;
            string Location = location.Text;
            string Hight = hight.Text;
            string Width = width.Text;
            string Warehousee = warehousee.Text;

            // Sprawdzenie czy pola są uzupełnione
            if (string.IsNullOrEmpty(Namee) || string.IsNullOrEmpty(Location) || string.IsNullOrEmpty(Hight) || string.IsNullOrEmpty(Width) || string.IsNullOrEmpty(Warehousee))
            {
                MessageBox.Show("Wypełnij wszystkie pola przed dodaniem produktu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Przerwij dodawanie rekordu, jeśli jakieś pole jest puste
            }

            // Walidacja czy Height to liczba
            if (!int.TryParse(Hight, out int heightValue))
            {
                MessageBox.Show("Wprowadź poprawną wartość dla pola 'Height'.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja czy Width to liczba
            if (!int.TryParse(Width, out int widthValue))
            {
                MessageBox.Show("Wprowadź poprawną wartość dla pola 'Width'.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = null;

            try
            {
                // Rozpoczęcie transakcji
                transaction = connection.BeginTransaction();

                // Sprawdzenie czy magazyn i lokalizacja już istnieją w tabeli Warehouse
                string checkIfExistsQuery = $"SELECT COUNT(*) FROM [inz_xd].[dbo].[Warehouse] WHERE [Warehouse] = '{Warehousee}' AND [Location] = '{Location}'";

                using SqlCommand checkIfExistsCommand = new SqlCommand(checkIfExistsQuery, connection, transaction);
                int existingRecordsCount = (int)checkIfExistsCommand.ExecuteScalar();

                if (existingRecordsCount == 0)
                {
                    // Jeżeli nie istnieje rekord o takim magazynie i lokalizacji, wyświetl komunikat błędu
                    MessageBox.Show("Podany magazyn i lokalizacja nie istnieją w bazie danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Sprawdzenie czy produkt o tej samej nazwie już istnieje
                string checkProductIfExistsQuery = $"SELECT COUNT(*) FROM [inz_xd].[dbo].[Products_inz] WHERE [Name] = '{Namee}'";

                using SqlCommand checkProductIfExistsCommand = new SqlCommand(checkProductIfExistsQuery, connection, transaction);
                int existingProductCount = (int)checkProductIfExistsCommand.ExecuteScalar();

                if (existingProductCount > 0)
                {
                    // Jeżeli produkt o takiej nazwie już istnieje, wyświetl komunikat błędu
                    MessageBox.Show("Produkt o tej samej nazwie już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Polecenie SQL do wstawienia danych do tabeli Products_inz
                string insertProductQuery = $"INSERT INTO [inz_xd].[dbo].[Products_inz] ([Name], [Location], [Hight], [Width], [Warehouse]) VALUES" +
                    $" ('{Namee}','{Location}',{heightValue},{widthValue},'{Warehousee}');";

                using SqlCommand insertProductCommand = new SqlCommand(insertProductQuery, connection, transaction);
                insertProductCommand.ExecuteNonQuery();

                // Pobranie identyfikatora dodanego rekordu z Products_inz
                string getProductIdQuery = "SELECT SCOPE_IDENTITY();";

                using SqlCommand getProductIdCommand = new SqlCommand(getProductIdQuery, connection, transaction);
                int productId = Convert.ToInt32(getProductIdCommand.ExecuteScalar());

                // Polecenie SQL do wstawienia danych do tabeli Quantity
                string insertQuantityQuery = $"INSERT INTO [inz_xd].[dbo].[Quantity] ([ItemID], [Qty]) VALUES ({productId}, 1);";

                using SqlCommand insertQuantityCommand = new SqlCommand(insertQuantityQuery, connection, transaction);
                insertQuantityCommand.ExecuteNonQuery();

                // Zatwierdzenie transakcji
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // W razie błędu anuluj transakcję
                transaction?.Rollback();
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Zamykanie połączenia
                connection.Close();
            }

            // Aktualizowanie danych w DataGridu
            RefreshData();
        }


        private void RefreshData()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT  [Id]\r\n      ,[Name]\r\n      ,[Location]\r\n      ,[Hight]\r\n      ,[Width]\r\n,[Warehouse] FROM [inz_xd].[dbo].[Products_inz]";

                // Tworzenie obiektu SqlDataAdapter
                SqlDataAdapter adapter = new(sql, connection);

                // Tworzenie obiektu DataTable
                DataTable dataTable = new();

                // Wypełnianie DataTable danymi z bazy danych
                adapter.Fill(dataTable);

                // Przypisanie DataTable do DataGrida
                gri.ItemsSource = dataTable.DefaultView;
            }
        }

    }
}