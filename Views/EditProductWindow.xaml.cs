using System.Data.SqlClient;
using System.Windows;




namespace inzynier.Views
{
    public partial class EditProductWindow : Window
    {
        private int productId;

        public EditProductWindow(int productId, string name, int hight, int width, string warehouse, string location)
        {
            InitializeComponent();

            // Wypełnij pola tekstowe danymi zaznaczonego produktu
            this.productId = productId;
            txtName.Text = name;
            txtHight.Text = hight.ToString();
            txtWidth.Text = width.ToString();
            txtWarehouse.Text = warehouse;
            txtLocation.Text = location;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtHight.Text, out int newHight) && int.TryParse(txtWidth.Text, out int newWidth))
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

                // Sprawdź, czy magazyn istnieje w tabeli Warehouse
                if (!WarehouseExists(txtWarehouse.Text))
                {
                    MessageBox.Show("Podany magazyn nie istnieje.");
                    return;
                }

                // Sprawdź, czy lokalizacja istnieje w tabeli Warehouse
                if (!LocationExists(txtLocation.Text))
                {
                    MessageBox.Show("Podana lokalizacja nie istnieje.");
                    return;
                }

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string updateSql = $"UPDATE [inz].[dbo].[Products_inz] SET [Name] = '{txtName.Text}', [Hight] = {newHight}, [Width] = {newWidth}, [Warehouse] = '{txtWarehouse.Text}', [Location] = '{txtLocation.Text}' WHERE [Id] = {productId}";

                using SqlCommand command = new SqlCommand(updateSql, connection);
                command.ExecuteNonQuery();

                MessageBox.Show("Zaktualizowano dane produktu.");
                Close(); // Zamyka okno po zatwierdzeniu edycji
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne dane.");
            }
        }

        private bool WarehouseExists(string warehouse)
        {
            // Sprawdź, czy magazyn istnieje w tabeli Warehouse
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string checkWarehouseSql = $"SELECT COUNT(*) FROM [inz].[dbo].[Warehouse] WHERE [Warehouse] = '{warehouse}'";

            using SqlCommand checkWarehouseCommand = new SqlCommand(checkWarehouseSql, connection);
            int count = (int)checkWarehouseCommand.ExecuteScalar();

            return count > 0;
        }

        private bool LocationExists(string location)
        {
            // Sprawdź, czy lokalizacja istnieje w tabeli Warehouse
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string checkLocationSql = $"SELECT COUNT(*) FROM [inz].[dbo].[Warehouse] WHERE [Location] = '{location}'";

            using SqlCommand checkLocationCommand = new SqlCommand(checkLocationSql, connection);
            int count = (int)checkLocationCommand.ExecuteScalar();

            return count > 0;
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Zamknij okno bez zapisywania zmian
            Close();
        }
    }
}
