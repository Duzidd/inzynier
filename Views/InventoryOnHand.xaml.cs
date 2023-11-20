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
using inzynier; // Zastąp "inzynier" rzeczywistą nazwą przestrzeni nazw


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

            using SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT * FROM [inz_xd].[dbo].[Tabela_testowa]";

            // Tworzenie obiektu SqlDataAdapter
            SqlDataAdapter adapter = new(sql, connection);

            // Tworzenie obiektu DataTable
            DataTable dataTable = new();

            // Wypełnianie DataTable danymi z bazy danych
            adapter.Fill(dataTable);

            // Wyczyść istniejące elementy z kolekcji Items w DataGrid
            xyz.Items.Clear();

            // Przypisanie DataTable do DataGrida
            xyz.ItemsSource = dataTable.DefaultView;
        }







        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (IsAdmin.IsAdminn == "Admin")
            {
                AdminPage win2 = new();
                win2.Show();
                this.Close(); //only if you want to close the current form.
            }
            else if(IsAdmin.IsAdminn == "User")
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

        private void RefreshData()
        {
            // Tutaj dodaj kod do odświeżania danych w DataGrid
            // Możesz powtórzyć kod, który używasz do wczytania danych po naciśnięciu przycisku
            // na przykład, kod z metody Button_Click
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM [inz_xd].[dbo].[Tabela_testowa]";

                SqlDataAdapter adapter = new(sql, connection);
                DataTable dataTable = new();
                adapter.Fill(dataTable);

                xyz.ItemsSource = dataTable.DefaultView;
            }
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            DataRowView selectedRow = (DataRowView)xyz.SelectedItem;

            if (selectedRow != null)
            {
                // Pobierz wartości pól do edycji
                string value1 = selectedRow["pole1"].ToString();
                string value2 = selectedRow["pole2"].ToString();

                // Tutaj możesz otworzyć okno edycji lub inne kontrole do edycji danych
                // i przekazać wybrane dane do edycji
                // Na przykład:
                EditWindow editWindow = new EditWindow(value1, value2);
                editWindow.ShowDialog();

                // Pobierz zaktualizowane wartości z okna edycji
                string editedValue1 = editWindow.EditedValue1;
                string editedValue2 = editWindow.EditedValue2;

                // Po edycji zaktualizuj bazę danych
                // Przykładowy kod (edytuj go zgodnie z własnymi potrzebami):
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateSql = "UPDATE [inz_xd].[dbo].[Tabela_testowa] SET pole1=@Value1, pole2=@Value2 WHERE pole1=@Klucz";

                    using (SqlCommand cmd = new SqlCommand(updateSql, connection))
                    {
                        // Przypisz nowe wartości parametrów SQL na podstawie edytowanych danych
                        cmd.Parameters.AddWithValue("@Value1", editedValue1);
                        cmd.Parameters.AddWithValue("@Value2", editedValue2);
                        cmd.Parameters.AddWithValue("@Klucz", selectedRow["pole1"]); // Zastąp "KolumnaKluczowa" nazwą rzeczywistej kolumny klucza
                        cmd.ExecuteNonQuery();
                    }
                }

                // Odśwież dane w DataGrid po edycji
                RefreshData();
            }
        }


    }
}