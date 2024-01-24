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
    public partial class MoveProduct : Window
    {
        


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Tutaj umieść kod, który chcesz wykonać przy inicjalizacji okna
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT  [Id],[Name],[Hight]   ,[Width],[Warehouse]   ,[Location]   FROM [inz_xd].[dbo].[Products_inz]";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            gri.ItemsSource = dataTable.DefaultView;
        }
        public MoveProduct()
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
            // Pobierz zaznaczony wiersz z DataGrid
            DataRowView selectedRow = (DataRowView)gri.SelectedItem;

            if (selectedRow != null)
            {
                // Pobierz dane z zaznaczonego rekordu
                int productId = (int)selectedRow["Id"];
                string name = selectedRow["Name"].ToString();

                // Sprawdź, czy dane są odpowiedniego typu
                int hight;
                int width;
                if (int.TryParse(selectedRow["Hight"].ToString(), out hight) &&
                    int.TryParse(selectedRow["Width"].ToString(), out width))
                {
                    string warehouse = selectedRow["Warehouse"].ToString();
                    string location = selectedRow["Location"].ToString();

                    // Utwórz i otwórz okno edycji
                    EditProductWindow editWindow = new EditProductWindow(productId, name, hight, width, warehouse, location);
                    editWindow.ShowDialog();

                    // Aktualizuj dane w DataGrid po edycji
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe dane w kolumnach 'Hight' lub 'Width'.");
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać wiersz do edycji.");
            }
        }

        private void RefreshData()
        {
            // Refresh the DataGrid with updated data
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT  [Id],[Name],[Hight]   ,[Width],[Warehouse]   ,[Location]   FROM [inz_xd].[dbo].[Products_inz]";

            SqlDataAdapter adapter = new(sql, connection);
            DataTable dataTable = new();
            adapter.Fill(dataTable);

            gri.ItemsSource = dataTable.DefaultView;
        }

    }
}