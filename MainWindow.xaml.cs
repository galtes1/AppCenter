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

namespace AppCenter
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


        private void XoIntroButton_Click(object sender, RoutedEventArgs e)
        {

            XoIntro xoIntro1 = new XoIntro();
            xoIntro1.Show();
            this.Close();

        }

        private void WeatherAppIntroButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherIntro weatherIntro = new WeatherIntro();
            weatherIntro.Show();
            this.Close();
        }

        private void SnakeIntroButton_Click(object sender, RoutedEventArgs e)
        {
            SnakeIntro snakeIntro = new SnakeIntro();
            snakeIntro.Show();
            this.Close();
        }

        private void CalculatorIntroButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatorIntro calculatorIntro = new CalculatorIntro();
            calculatorIntro.Show();
            this.Close();
        }

        private void RunnerIntroButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerIntro runnerIntro = new RunnerIntro();
            runnerIntro.Show();
            this.Close();
        }

        private void PacManIntroButton_Click(object sender, RoutedEventArgs e)
        {
            PacManIntro pacManIntro = new PacManIntro();
            pacManIntro.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}