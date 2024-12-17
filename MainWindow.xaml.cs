using System;
using System.Collections.Generic;
using System.IO;
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

namespace CalcHome
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadHousesFromFiles();
        }

        // Загрузка
        private void LoadHousesFromFiles()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(directoryPath, "*.txt");

            foreach (string file in files)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                listhome.Items.Add(new ListBoxItem { Content = new TextBlock { Text = fileName, FontSize = 20 } });
            }
        }

        // Добавление
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();

            if (addItemWindow.ShowDialog() == true)
            {
                string newItem = addItemWindow.NewItem;
                string square = addItemWindow.Square;
                string Gortar = addItemWindow.Gortar;
                string Holtar = addItemWindow.Holtar;
                string Otwtar = addItemWindow.Otwtar;
                string Gaztar = addItemWindow.Gaztar;
                string Otptar = addItemWindow.Otptar;
                string ElDtar = addItemWindow.ElDtar;
                string ElNtar = addItemWindow.ElNtar;
                listhome.Items.Add(new ListBoxItem { Content = new TextBlock { Text = newItem, FontSize = 20 } });
                CreateFile(newItem, square, Gortar, Holtar, Otwtar, Gaztar, Otptar, ElDtar, ElNtar);
            }
        }
        private void CreateFile(string itemName, string square, string Gortar, string Holtar, string Otwtar, string Gaztar, string Otptar, string ElDtar, string ElNtar)
        {
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), $"{itemName}.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Название дома: {itemName}");
                writer.WriteLine($"Площадь: {square}");
                writer.WriteLine($"Тариф горячая: {Gortar}");
                writer.WriteLine($"Тариф холодная: {Holtar}");
                writer.WriteLine($"Тариф водоотведение: {Otwtar}");
                writer.WriteLine($"Тариф газ: {Gaztar}");
                writer.WriteLine($"Тариф отопление: {Otptar}");
                writer.WriteLine($"Тариф электричество день: {ElDtar}");
                writer.WriteLine($"Тариф электричество ночь: {ElNtar}");
            }
        }

        // Удаление
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listhome.SelectedItem as ListBoxItem;

            if (selectedItem != null)
            {
                string houseName = (selectedItem.Content as TextBlock).Text;
                listhome.Items.Remove(selectedItem);
                DeleteTextFile(houseName);
            }
        }
        private void DeleteTextFile(string itemName)
        {
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), $"{itemName}.txt");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listhome.SelectedItem as ListBoxItem;

            if (selectedItem != null)
            {
                TextBlock textBlock = selectedItem.Content as TextBlock;
                if (textBlock != null)
                {
                    string fileName = textBlock.Text;
                    Calc calc = new Calc(fileName);
                    calc.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Выберите дом", "Ошибка", MessageBoxButton.OK);
        }
    }
}
