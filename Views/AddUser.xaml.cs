using inzynier.Temp;
using System;
using System.Collections.Generic;
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
    public partial class AddUser : Window
    {
        public AddUser()
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
            string Login = LoginBox.Text;
            string Password = PasswordBox.Password;
            string FirstName = Role_CBox.Text;
            string SecondName = First_NameBox.Text;
            string Role = Second_NameBox.Text;
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            connection.Open();

            // Polecenie SQL do wstawienia danych
            string sql = $"INSERT INTO [inz_xd].[dbo].[Users] ([First_Name], [Second_Name], [Role], [Login], [Password]) VALUES ('{FirstName}', '{SecondName}','{Role}','{Login}','{Password}')";

            // Wykonanie polecenia SQL
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();

            // Aktualizowanie danych w DataGridu
            // RefreshDataGrid();

        }

        private void LoginBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}