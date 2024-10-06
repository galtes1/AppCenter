using AppCenter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for XoWindow.xaml
    /// </summary>
    public partial class XoIntro : Window
    {
        public XoIntro()
        {
            InitializeComponent();

        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming your game is in another solution, you might need to invoke it by launching it
            // with Process.Start or another method.
            XoGame xoGame = new XoGame();
            xoGame.Show();
            this.Close();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
