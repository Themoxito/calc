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

namespace CalcHome
{
    /// <summary>
    /// Логика взаимодействия для AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public string NewItem { get; private set; }
        public string Square { get; private set; }
        public string Gortar { get; private set; }
        public string Holtar { get; private set; }
        public string Otwtar { get; private set; }
        public string Gaztar { get; private set; }
        public string Otptar { get; private set; }
        public string ElDtar { get; private set; }
        public string ElNtar { get; private set; }
        public AddItemWindow()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewItem = ItemNameTextBox.Text;
            Square = ItemSquareTextBox.Text;
            Gortar = GorValue.Text;
            Holtar = HolValue.Text;
            Otwtar = OtwValue.Text;
            Gaztar = GazValue.Text;
            Otptar = OtpValue.Text;
            ElDtar = ElGayValue.Text;
            ElNtar = ElNigValue.Text;
            if (!string.IsNullOrWhiteSpace(NewItem) && !string.IsNullOrWhiteSpace(Square) && !string.IsNullOrWhiteSpace(Gortar) && !string.IsNullOrWhiteSpace(Square) && !string.IsNullOrWhiteSpace(Holtar) && !string.IsNullOrWhiteSpace(Otwtar) && !string.IsNullOrWhiteSpace(Otptar))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Поля не должны быть пустыми.");
            }
        }
    }
}
