using System.Windows;

namespace inzynier
{
    public partial class EditWindow : Window
    {
        // Dodaj właściwości dla wszystkich pól, które chcesz edytować
        public string EditedId { get; private set; }
        public string EditedName { get; private set; }
        public string EditedLocation { get; private set; }
        public string EditedHight { get; private set; }
        public string EditedWidth { get; private set; }
        public string EditedQty { get; private set; }

        // Dodaj konstruktor przyjmujący 6 argumentów
        public EditWindow(string id, string name, string location, string hight, string width, string qty)
        {
            InitializeComponent();

            // Przypisz wartości do kontrolek w oknie edycji
            textBoxId.Text = id;
            textBoxName.Text = name;
            textBoxLocation.Text = location;
            textBoxHight.Text = hight;
            textBoxWidth.Text = width;

            // Przypisz wartość qty
            EditedQty = qty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz edytowane wartości z kontrolek
            EditedId = textBoxId.Text;
            EditedName = textBoxName.Text;
            EditedLocation = textBoxLocation.Text;
            EditedHight = textBoxHight.Text;
            EditedWidth = textBoxWidth.Text;

            // Zamknij okno po zapisie
            Close();
        }
    }
}
