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
using System.Windows.Shapes;

namespace CalcHome
{
    public partial class Calc : Window
    {
        string filePathname, _squared;
        double _Gortar, _Holtar, _Otwtar, _Gaztar, _Daytar, _Nigtar;
        double _gorwoter, _holwoter, _woter, _gaz, _otptar, _elday, _elnig;
        public Calc(string FileName)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            GorWoter.ValueChanged += GorWoter_ValueChanged;
            HolWoter.ValueChanged += HolWoter_ValueChanged;
            ElectroDay.ValueChanged += ElectroDay_ValueChanged;
            Gaz.ValueChanged += Gaz_ValueChanged;
            ElectroNight.ValueChanged += ElectroNight_ValueChanged;
            Form_Load(FileName);
        }
        private void Form_Load(string Filename)
        {
            NameHome.Content = Filename;
            string filePath = $"{Filename}.txt";
            filePathname = filePath;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    try
                    {
                        // тарифы
                        if (line.StartsWith("Площадь:"))
                        {
                            _squared = line.Substring("Площадь:".Length).Trim();
                        }
                        else if (line.StartsWith("Тариф горячая:"))
                        {
                            string Value = line.Substring("Тариф горячая:".Length).Trim();
                            _Gortar = Convert.ToDouble(Value);
                            Gortar.Content = Convert.ToString($"Тариф: {Value}₽");
                        }
                        else if (line.StartsWith("Тариф холодная:"))
                        {
                            string Value = line.Substring("Тариф холодная:".Length).Trim();
                            _Holtar = Convert.ToDouble(Value);
                            Holtar.Content = Convert.ToString($"Тариф: {Value}₽");
                        }
                        else if (line.StartsWith("Тариф водоотведение:"))
                        {
                            string Value = line.Substring("Тариф водоотведение:".Length).Trim();
                            _Otwtar = Convert.ToDouble(Value);
                            Otwtar.Content = Convert.ToString($"Тариф: {Value}₽");
                        }
                        else if (line.StartsWith("Тариф газ:"))
                        {
                            string Value = line.Substring("Тариф газ:".Length).Trim();
                            _Gaztar = Convert.ToDouble(Value);
                            Gaztar.Content = Convert.ToString($"Тариф: {Value}₽");
                        }
                        else if (line.StartsWith("Тариф отопление:"))
                        {
                            string Value = line.Substring("Тариф отопление:".Length).Trim();
                            _otptar = Convert.ToDouble(Value);
                        }
                        else if (line.StartsWith("Тариф электричество день:"))
                        {
                            string Value = line.Substring("Тариф электричество день:".Length).Trim();
                            _Daytar = Convert.ToDouble(Value);
                            ElDattar.Content = Convert.ToString($"Тариф: {Value}₽");
                        }
                        else if (line.StartsWith("Тариф электричество ночь:"))
                        {
                            string Value = line.Substring("Тариф электричество ночь:".Length).Trim();
                            _Nigtar = Convert.ToDouble(Value);
                            ElNightar.Content = Convert.ToString($"Тариф: {Value}₽");
                        } // данные
                        else if (line.StartsWith("Горячая:"))
                        {
                            string Value = line.Substring("Горячая:".Length).Trim();
                            GorWoter.Value = Convert.ToDouble(Value);
                        }
                        else if (line.StartsWith("Холодная:"))
                        {
                            string Value = line.Substring("Холодная:".Length).Trim();
                            HolWoter.Value = Convert.ToDouble(Value);
                        }
                        else if (line.StartsWith("Площадь:"))
                        {
                            _squared = line.Substring("Площадь:".Length).Trim();
                        }
                        else if (line.StartsWith("Газ:"))
                        {
                            string Value = line.Substring("Газ:".Length).Trim();
                            Gaz.Value = Convert.ToDouble(Value);
                        }
                        else if (line.StartsWith("День:"))
                        {
                            string Value = line.Substring("День:".Length).Trim();
                            ElectroDay.Value = Convert.ToDouble(Value);
                        }
                        else if (line.StartsWith("Ночь:"))
                        {
                            string Value = line.Substring("День:".Length).Trim();
                            ElectroNight.Value = Convert.ToDouble(Value);
                        }
                    } catch 
                    {
                        MessageBox.Show("Неверно введено значение! Требуется использовать ',' вместо '.'");
                        Application.Current.Shutdown();
                    }
                    
                }
                Calcs();
            }
            else
            {
                MessageBox.Show("Файл не найден.");
            }
        }
        private void GorWoter_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _gorwoter = e.NewValue;
            GorWoterText.Content = $"Горячая вода ({e.NewValue}м³)";
            Calcs();
        }
        private void HolWoter_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _holwoter = e.NewValue;
            HolWoterText.Content = $"Холодная вода ({e.NewValue}м³)";
            Calcs();
        }
        private void Gaz_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _gaz = e.NewValue;
            GazText.Content = $"Газ ({e.NewValue}м³/ч)";
            Calcs();
        }
        private void ElectroDay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _elday = e.NewValue;
            El.Content = $"Электричество день ({e.NewValue}кв/ч)";
            Calcs();
        }
        private void ElectroNight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _elnig = e.NewValue;
            El2.Content = $"Электричество ночь ({e.NewValue}кв/ч)";
            Calcs();
        }
        private void Calcs()
        {
            double itogGor, itogHol, itogOtw, itogGaz, itogOtp, itogEld, itogEln ;
            
            itogGor = _Gortar * _gorwoter;
            Gortaritog.Content = $"Итог за месяц: {itogGor}₽";

            itogHol = _Holtar * _holwoter;
            Holtaritog.Content = $"Итог за месяц: {itogHol}₽";

            itogOtw = (_gorwoter + _holwoter) * _Otwtar;
            Otwtaritog.Content = $"Итог за месяц: {itogOtw}₽";

            itogEld = _elday * _Daytar;
            ElDayitog.Content = $"Итог за месяц: {itogEld}₽";

            itogEln = _elnig * _Nigtar;
            ElNighitog.Content = $"Итог за месяц: {itogEln}₽";

            itogGaz = _Gaztar * _gaz;
            Gaztaritog.Content = $"Итог за месяц: {itogGaz}₽";

            itogOtp = _otptar * Convert.ToDouble(_squared);

            summer.Content = Convert.ToString( itogGor + itogHol + itogOtw + itogGaz + itogEld + itogEln + "₽");
            winter.Content = Convert.ToString( itogOtp + itogGor + itogHol + itogOtw + itogGaz + itogEld + itogEln + "₽");
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(filePathname))
            {
                writer.WriteLine($"Площадь: {_squared}");
                writer.WriteLine($"Горячая: {_gorwoter}");
                writer.WriteLine($"Холодная: {_holwoter}");
                writer.WriteLine($"День: {_elday}");
                writer.WriteLine($"Ночь: {_elnig}");
                writer.WriteLine($"Водоотведение: {_woter}");
                writer.WriteLine($"Газ: {_gaz}");
                writer.WriteLine($"Тариф горячая: {_Gortar}");
                writer.WriteLine($"Тариф холодная: {_Holtar}");
                writer.WriteLine($"Тариф водоотведение: {_Otwtar}");
                writer.WriteLine($"Тариф газ: {_Gaztar}");
                writer.WriteLine($"Тариф отопление: {_otptar}");
                writer.WriteLine($"Тариф электричество день: {_Daytar}");
                writer.WriteLine($"Тариф электричество ночь: {_Nigtar}");
                
            }
        }
    }
}