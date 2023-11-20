using System;
using System.Collections.Generic;
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
using System.Windows;

namespace inzynier
{
    public partial class EditWindow : Window
    {
        public string EditedValue1 { get; private set; }
        public string EditedValue2 { get; private set; }

        public EditWindow(string value1, string value2)
        {
            InitializeComponent();
            textBoxValue1.Text = value1;
            textBoxValue2.Text = value2;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz edytowane wartości z kontrolek
            EditedValue1 = textBoxValue1.Text;
            EditedValue2 = textBoxValue2.Text;

            // Zamknij okno po zapisie
            Close();
        }
    }
}
