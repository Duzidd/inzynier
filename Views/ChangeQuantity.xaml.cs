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
    public partial class ChangeQuantity : Window
    {
        public ChangeQuantity()
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
            string sql = "SELECT id, P.[Name],P.[Location],Q.[Qty] FROM [inz].[dbo].[Products_inz] P\r\nLEFT JOIN [inz].[dbo].[Quantity] Q ON P.[Id] = Q.[ItemID]";
            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Przypisanie DataTable do DataGrida
            gri.ItemsSource = dataTable.DefaultView;
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
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)gri.SelectedItem;

            if (selectedRow != null)
            {
                int itemId = (int)selectedRow["id"];
                // Perform edit operation using itemId, you can open a new window or dialog for editing.
                // For example, you can create an EditWindow and pass itemId to it.
                EditQtyWindow editWindow = new EditQtyWindow(itemId);
                editWindow.ShowDialog();

                // Refresh the data after editing
                RefreshData();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void RefreshData()
        {
            // Refresh the DataGrid with updated data
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT id, P.[Name],P.[Location],Q.[Qty] FROM [inz].[dbo].[Products_inz] P\r\nLEFT JOIN [inz].[dbo].[Quantity] Q ON P.[Id] = Q.[ItemID]";

            SqlDataAdapter adapter = new(sql, connection);
            DataTable dataTable = new();
            adapter.Fill(dataTable);

            gri.ItemsSource = dataTable.DefaultView;
        }

    }
}