using System.Data.SqlClient;
using System.Windows;

namespace inzynier.Views
{
    public partial class EditQtyWindow : Window
    {
        private int itemId;

        public EditQtyWindow(int itemId)
        {
            InitializeComponent();
            this.itemId = itemId;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtNewQuantity.Text, out int newQuantity))
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string updateSql = $"UPDATE [inz_xd].[dbo].[Quantity] SET Qty = {newQuantity} WHERE ItemID = {itemId}";

                using SqlCommand command = new SqlCommand(updateSql, connection);
                command.ExecuteNonQuery();

                MessageBox.Show("Zaktualizowano ilość.");
                Close(); // Zamyka okno po zatwierdzeniu edycji
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną ilość.");
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Zamyka okno bez zapisywania zmian
        }
    }
}
