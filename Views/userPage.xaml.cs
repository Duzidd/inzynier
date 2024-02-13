using System.Windows;
using System.Windows.Input;

namespace inzynier.Views
{
    /// <summary>
    /// Logika interakcji dla klasy userPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage()
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

            InventoryOnHand new49 = new();
            new49.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MoveProduct new48 = new();
            new48.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddProduct new47 = new();
            new47.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MainWindow new60 = new();
            new60.Show();
            this.Close();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ChangeQuantity new46 = new();
            new46.Show();
            this.Close();
        }
    }
}


