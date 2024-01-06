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
    public partial class DeleteProduct : Window
    {
        public DeleteProduct()
        {
            InitializeComponent();
            Button_Click_3(null, null);
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
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT [Id],[Name],[Location],[Hight],[Width],[Warehouse] FROM [inz_xd].[dbo].[Products_inz];";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            deletee.ItemsSource = dataTable.DefaultView;
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            // Sprawdź, czy coś jest zaznaczone
            if (deletee.SelectedItem != null)
            {
                // Pobierz zaznaczony rekord
                DataRowView selectedRow = (DataRowView)deletee.SelectedItem;

                // Pobierz wartość klucza głównego (przykładowo, ID) z zaznaczonego rekordu
                int idToDelete = (int)selectedRow["Id"]; // Zastąp "ID" odpowiednią nazwą kolumny klucza głównego

                // Usuń rekord z bazy danych lub innej struktury danych
                // Przykład: Jeśli korzystasz z ObservableCollection
                // yourObservableCollection.Remove(selectedRecord);

                // Usuń rekord z bazy danych (przykładowe zapytanie SQL)
                string deleteSql = $"DELETE  FROM [inz_xd].[dbo].[Products_inz] WHERE Id = {idToDelete}";

                using SqlConnection deleteConnection = new(connectionString);
                deleteConnection.Open();

                SqlCommand deleteCommand = new(deleteSql, deleteConnection);
                deleteCommand.ExecuteNonQuery();

                // Odśwież widok DataGrid
                Button_Click_3(sender, e);
            }
            else
            {
                MessageBox.Show("Wybierz rekord do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}