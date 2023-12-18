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
    public partial class InventoryOnHand : Window
    {
        public InventoryOnHand()
        {
            InitializeComponent();
           // RefreshData();
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
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TOP (1000) [Id],[Name],[Location],[Hight],[Width] FROM [inz_xd].[dbo].[Products_inz]";

                SqlDataAdapter adapter = new(sql, connection);
                DataTable dataTable = new();

                adapter.Fill(dataTable);

                // Wyczyść istniejące elementy przed ustawieniem ItemsSource
                xyz.ItemsSource = null;
                xyz.Items.Clear();

                // Przypisz DataTable do ItemsSource, co zastąpi poprzednie dane
                xyz.ItemsSource = dataTable.DefaultView;
            }
        }


        private void RefreshData()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TOP (1000) [Id],[Name],[Location],[Hight],[Width] FROM [inz_xd].[dbo].[Products_inz]";

                SqlDataAdapter adapter = new(sql, connection);
                DataTable dataTable = new();
                adapter.Fill(dataTable);

                // Przypisz DataTable do ItemsSource, co zastąpi poprzednie dane
                xyz.ItemsSource = dataTable.DefaultView;
            }
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
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




        private void Edit(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            DataRowView selectedRow = (DataRowView)xyz.SelectedItem;

            if (selectedRow != null)
            {
                string valueId = selectedRow["Id"].ToString();
                string valueName = selectedRow["Name"].ToString();
                string valueLocation = selectedRow["Location"].ToString();
                string valueHight = selectedRow["Hight"].ToString();
                string valueWidth = selectedRow["Width"].ToString();

                EditWindow editWindow = new EditWindow(valueId, valueName, valueLocation, valueHight, valueWidth);
                editWindow.ShowDialog();

                string editedValueHight = editWindow.EditedHight;
                string editedValueId = editWindow.EditedId;
                string editedValueName = editWindow.EditedName;
                string editedValueLocation = editWindow.EditedLocation;
                string editedValueWidth = editWindow.EditedWidth;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateSql = "UPDATE [inz_xd].[dbo].[Products_inz] SET Name=@ValueName, Location=@ValueLocation, Hight=@ValueHight, Width=@ValueWidth WHERE Id=@ValueId";

                    using (SqlCommand cmd = new SqlCommand(updateSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@ValueId", editedValueId);
                        cmd.Parameters.AddWithValue("@ValueName", editedValueName);
                        cmd.Parameters.AddWithValue("@ValueLocation", editedValueLocation);
                        cmd.Parameters.AddWithValue("@ValueHight", editedValueHight);
                        cmd.Parameters.AddWithValue("@ValueWidth", editedValueWidth);
                        cmd.ExecuteNonQuery();
                    }
                }

                RefreshData();
            }
        }



    }
}