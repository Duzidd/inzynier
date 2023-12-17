using inzynier.Temp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace inzynier.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
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
            string Namee = namee.Text;
            string Location = location.Text;
            string Hight = hight.Text;
            string Width = width.Text;

            // Sprawdzenie czy pola są uzupełnione
            if (string.IsNullOrEmpty(Namee) || string.IsNullOrEmpty(Location) || string.IsNullOrEmpty(Hight) || string.IsNullOrEmpty(Width))
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

            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Polecenie SQL do wstawienia danych
            string sql = $"INSERT INTO [inz_xd].[dbo].[Products_inz] ([Name], [Location], [Hight], [Width]) VALUES" +
                $" ('{Namee}','{Location}',{heightValue},{widthValue});";

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
                string sql = "SELECT TOP (1000) [Id]\r\n      ,[Name]\r\n      ,[Location]\r\n      ,[Hight]\r\n      ,[Width]\r\n  FROM [inz_xd].[dbo].[Products_inz]";

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