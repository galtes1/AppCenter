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

namespace AppCenter
{
    /// <summary>
    /// Interaction logic for WeatherIntro.xaml
    /// </summary>
    public partial class WeatherIntro : Window
    {
        public WeatherIntro()
        {
            InitializeComponent();
        }

        private void StartApp_Click(object sender, RoutedEventArgs e)
        {
            WeatherApp weatherApp = new WeatherApp();
            weatherApp.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
