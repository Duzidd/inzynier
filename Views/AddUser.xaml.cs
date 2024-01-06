using inzynier.Temp;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace inzynier.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Tutaj umieść kod, który chcesz wykonać przy inicjalizacji okna
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT TOP (1000) [First_Name],[Second_Name],[Role],[Login],[Password] FROM [inz_xd].[dbo].[Users]";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            xyz.ItemsSource = dataTable.DefaultView;
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
            Application.Current.Shutdown();
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
            string Login = LoginBox.Text;
            string Password = PasswordBox.Password;
            string FirstName = First_NameBox.Text;
            string SecondName = Second_NameBox.Text;
            string Role = Role_CBox.Text;
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Polecenie SQL do wstawienia danych
            string sql = $"INSERT INTO [inz_xd].[dbo].[Users] ([First_Name], [Second_Name], [Role], [Login], [Password]) VALUES ('{FirstName}', '{SecondName}','{Role}','{Login}','{Password}')";

            // Wykonanie polecenia SQL
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();

            // Aktualizowanie danych w DataGridu
            RefreshData();


        }

        private void RefreshData()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TOP (1000) [First_Name],[Second_Name],[Role],[Login],[Password] FROM [inz_xd].[dbo].[Users]";

                // Tworzenie obiektu SqlDataAdapter
                SqlDataAdapter adapter = new(sql, connection);

                // Tworzenie obiektu DataTable
                DataTable dataTable = new();

                // Wypełnianie DataTable danymi z bazy danych
                adapter.Fill(dataTable);

                // Przypisanie DataTable do DataGrida
                xyz.ItemsSource = dataTable.DefaultView;
            }
        }
        private void LoginBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT TOP (1000) [First_Name],[Second_Name],[Role],[Login],[Password] FROM [inz_xd].[dbo].[Users]";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            xyz.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            // Pobierz zaznaczone wiersze w DataGrid
            var selectedRow = xyz.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Pobierz wartość kolumny "Login", która jest kluczem głównym w Twojej tabeli
                string loginToDelete = selectedRow["Login"].ToString();

                // Połączenie z bazą danych
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Polecenie SQL do usunięcia rekordu
                string deleteSql = $"DELETE FROM [inz_xd].[dbo].[Users] WHERE [Login] = '{loginToDelete}'";

                // Wykonanie polecenia SQL
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.ExecuteNonQuery();

                // Aktualizowanie danych w DataGridu
                RefreshData();
            }
            else
            {
                MessageBox.Show("Proszę wybrać wiersz do usunięcia.");
            }
        }


    }
}