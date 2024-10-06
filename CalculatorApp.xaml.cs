using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class CalculatorApp : Window
    {
        public CalculatorApp()
        {
            InitializeComponent();
        }
        private void zero_Click(object sender, RoutedEventArgs e) => screen.Text += "0";

        private void point_Click(object sender, RoutedEventArgs e) => screen.Text += ".";

        private void clear_Click(object sender, RoutedEventArgs e) => screen.Text = "";

        private void one_Click(object sender, RoutedEventArgs e) => screen.Text += "1";

        private void two_Click(object sender, RoutedEventArgs e) => screen.Text += "2";

        private void three_Click(object sender, RoutedEventArgs e) => screen.Text += "3";

        private void four_Click(object sender, RoutedEventArgs e) => screen.Text += "4";

        private void five_Click(object sender, RoutedEventArgs e) => screen.Text += "5";

        private void six_Click(object sender, RoutedEventArgs e) => screen.Text += "6";

        private void seven_Click(object sender, RoutedEventArgs e) => screen.Text += "7";

        private void eight_Click(object sender, RoutedEventArgs e) => screen.Text += "8";

        private void nine_Click(object sender, RoutedEventArgs e) => screen.Text += "9";

        private void lPar_Click(object sender, RoutedEventArgs e) => screen.Text += "(";

        private void rPar_Click(object sender, RoutedEventArgs e) => screen.Text += ")";

        private void plus_Click(object sender, RoutedEventArgs e) => screen.Text += "+";

        private void minus_Click(object sender, RoutedEventArgs e) => screen.Text += "-";

        private void mult_Click(object sender, RoutedEventArgs e) => screen.Text += "x";

        private void div_Click(object sender, RoutedEventArgs e) => screen.Text += "÷";

        private void result_Click(object sender, RoutedEventArgs e) => Calculate();

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (screen.Text != "")
                screen.Text = screen.Text.Remove(screen.Text.Length - 1);
        }
        private void Calculate()
        {
            if (!string.IsNullOrEmpty(screen.Text))
            {
                try
                {
                    // Replace displayed 'x' and '÷' with '*' and '/' for calculations
                    string expression = screen.Text.Replace("x", "*").Replace("÷", "/");

                    DataTable dt = new DataTable();
                    var result = dt.Compute(expression, null);
                    screen.Text += "=" + result.ToString();
                }
                catch (SyntaxErrorException)
                {
                    screen.Text = "Error";
                }
                catch (EvaluateException)
                {
                    screen.Text = "Evaluation Error";
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    zero_Click(sender, e);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    one_Click(sender, e);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    two_Click(sender, e);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    three_Click(sender, e);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    four_Click(sender, e);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    five_Click(sender, e);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    six_Click(sender, e);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    seven_Click(sender, e);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    eight_Click(sender, e);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    nine_Click(sender, e);
                    break;
                case Key.OemPeriod:
                case Key.Decimal:
                    point_Click(sender, e);
                    break;
                case Key.Add:
                case Key.OemPlus:
                    plus_Click(sender, e);
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    minus_Click(sender, e); // minus
                    break;
                case Key.Multiply:
                    mult_Click(sender, e);
                    break;
                case Key.Divide:
                case Key.OemQuestion: // For '/' on the main keyboard
                    div_Click(sender, e);
                    break;
                case Key.Enter:
                    result_Click(sender, e);
                    break;
                case Key.Back:
                    back_Click(sender, e);
                    break;
                case Key.C:
                    clear_Click(sender, e);
                    break;
                case Key.OemOpenBrackets:
                    lPar_Click(sender, e); // '('
                    break;
                case Key.OemCloseBrackets:
                    rPar_Click(sender, e); // ')'
                    break;
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
