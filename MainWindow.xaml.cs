using inzynier.Temp;
using inzynier.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace inzynier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Windows_MouseDown (object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            string f = txtUser.Text.Trim();
           
            if (f == "Admin")
            {
                IsAdmin.IsAdminn = "Admin";
                AdminPage win2 = new();
                win2.Show();
                this.Close(); //only if you want to close the current form.
            }
            else if (f == "User")
            {
                IsAdmin.IsAdminn = "User";
                UserPage new44 = new();
                new44.Show();
                this.Close();
                



            }
            else if (f == "Super")
            {
                IsAdmin.IsAdminn = "Super";
                SuperUser new61 = new();
                new61.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędne dane");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();

                MessageBox.Show("połączono");

                // Wykonaj operacje na bazie danych


            }
            catch (Exception)
            {

                MessageBox.Show("błąd");
            }
        }
    }
}
