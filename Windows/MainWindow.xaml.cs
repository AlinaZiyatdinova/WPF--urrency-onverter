using prakt_5.Classes;
using System.IO;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace prakt_5
{
    public partial class MainWindow :Window
    {
        private List<Valute> valutesList ;
        public MainWindow ()
        {
            InitializeComponent();

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                
                XmlDocument doc = new XmlDocument();
                string xmlText = File.ReadAllText("valutes.xml");
                doc.LoadXml(xmlText);

                string date = doc.DocumentElement.Attributes["Date"].Value;
                string dateToday = DateTime.Now.ToString("dd.MM.yyyy");

                if (date != dateToday)
                {
                    MessageBox.Show("Данные обновлены");
                    readFromWeb();
                }
                else
                {
                    MessageBox.Show("Данные актуальны");
                    readFromFile();
                }
                settingsComboboxDefault();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Нет подключения к Интернету\nДанные загружены из файла","Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Information);
                readFromFile();
                settingsComboboxDefault();
            }

        }
        private void readFromWeb()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HttpClient client = new HttpClient();
            var respose =
                client.GetAsync("https://www.cbr.ru/scripts/XML_daily.asp")
                    .GetAwaiter().GetResult();

            var text = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            valutesList = Data.ValuteLoader.LoadValutes(text);
            addValute("R11000", 196, "RUB", 1, "Российсий рубль", 1);
            comboBox_valuteFrom.ItemsSource = valutesList;
            comboBox_valuteIn.ItemsSource = valutesList;
        }
        private void settingsComboboxDefault()
        {
            comboBox_valuteFrom.SelectedIndex = 0;
            comboBox_valuteIn.SelectedIndex = 0;
        }
        private void readFromFile()
        {
            string xmlText = File.ReadAllText("valutes.xml");
            valutesList = Data.ValuteLoader.LoadValutes(xmlText);
            addValute("R11000", 196, "RUB", 1, "Российсий рубль", 1);
            comboBox_valuteFrom.ItemsSource = valutesList;
            comboBox_valuteIn.ItemsSource = valutesList;
        }
        private void addValute(string id, int numCode, string charCode, int nominal, string name, double value)
        {
            string xmlText = File.ReadAllText("valutes.xml");
            valutesList = valutesList = Data.ValuteLoader.LoadValutes(xmlText);
            valutesList.Insert(0, new Valute(id, numCode, charCode, nominal, name, value));
        }
        private void filterText (object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int x))
            {
                e.Handled = true;
            }
        }

        private void Calculate (object sender, TextChangedEventArgs e)
        {
            CalculateFunction();
        }

        private void CalculateFunction()
        {
            Valute? inValute = comboBox_valuteFrom.SelectedItem as Valute;
            Valute? outValute = comboBox_valuteIn.SelectedItem as Valute;
            if (inValute == null || outValute == null)
            {
                return;
            }

            if (textBox_originalValue.Text != string.Empty)
            {
                long value = int.Parse(textBox_originalValue.Text);
                double rubles = value * inValute.Value;
                double result = rubles / outValute.Value;

                textBox_resultValue.Text = Math.Round(result, 2).ToString();
            }
            else
            {
                textBox_resultValue.Text = string.Empty;
            }
        }
        private void comboBox_valuteIn_SelectionChanged (object sender, SelectionChangedEventArgs e)
        {
            CalculateFunction();
        }
    }
}