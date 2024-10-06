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
using System.Windows.Threading;

namespace AppCenter
{
    /// <summary>
    /// Interaction logic for PacManGame.xaml
    /// </summary>
    public partial class PacManGame : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;

        int speed = 8;
        Rect pacmanHitBox;
        int ghostSpeed = 10;
        int ghostMoveStep = 130;
        int currentGhostStep;
        int score = 0;
        double initialPinkGhostLeft;
        double initialPinkGhostTop;
        double initialRedGhostLeft;
        double initialRedGhostTop;
        double initialOrangeGhostLeft;
        double initialOrangeGhostTop;

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public PacManGame()
        {
            InitializeComponent();
            GameSetUp();
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && noLeft == false)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
                goLeft = true;
                pacman.RenderTransform = new ScaleTransform(-1, 1, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Right && noRight == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;
                goRight = true;
                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Up && noUp == false)
            {
                goLeft = goRight = goDown = false;
                noLeft = noRight = noDown = false;
                goUp = true;
                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Down && noDown == false)
            {
                goLeft = goRight = goUp = false;
                noLeft = noRight = noUp = false;
                goDown = true;
                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
        }

        private void GameSetUp()
        {
            MyCanvas.Focus();
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            currentGhostStep = ghostMoveStep;

            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("./images/pacman.png", UriKind.Relative));
            pacman.Fill = pacmanImage;

            ImageBrush redGhost = new ImageBrush();
            redGhost.ImageSource = new BitmapImage(new Uri("./images/red.jpg", UriKind.Relative));
            redSquare.Fill = redGhost;

            ImageBrush orangeGhost = new ImageBrush();
            orangeGhost.ImageSource = new BitmapImage(new Uri("./images/orange.jpg", UriKind.Relative));
            orangeSquare.Fill = orangeGhost;

            ImageBrush pinkGhost = new ImageBrush();
            pinkGhost.ImageSource = new BitmapImage(new Uri("./images/pink.jpg", UriKind.Relative));
            pinkSquare.Fill = pinkGhost;

            initialPinkGhostLeft = Canvas.GetLeft(pinkSquare);
            initialPinkGhostTop = Canvas.GetTop(pinkSquare);
            initialRedGhostLeft = Canvas.GetLeft(redSquare);
            initialRedGhostTop = Canvas.GetTop(redSquare);
            initialOrangeGhostLeft = Canvas.GetLeft(orangeSquare);
            initialOrangeGhostTop = Canvas.GetTop(orangeSquare);

        }

        private void GameLoop(object? sender, EventArgs e)
        {
            txtScore.Content = "Score: " + score;

            if (goRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + speed);
            }

            if (goDown && Canvas.GetTop(pacman) + 80 > Application.Current.MainWindow.Height)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && Canvas.GetTop(pacman) < 1)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(pacman) - 10 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {
                noRight = true;
                goRight = false;
            }

            pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            foreach (var rectangle in MyCanvas.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rectangle.Width, rectangle.Height);
                if ((string)rectangle.Tag == "wall")
                {
                    if (goLeft == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        noLeft = true;
                        goLeft = false;
                    }
                    if (goRight == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        noRight = true;
                        goRight = false;
                    }
                    if (goDown == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        noDown = true;
                        goDown = false;
                    }
                    if (goUp == true && pacmanHitBox.IntersectsWith(hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        noUp = true;
                        goUp = false;
                    }
                }

                if ((string)rectangle.Tag == "coin")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox) && rectangle.Visibility == Visibility.Visible)
                    {
                        rectangle.Visibility = Visibility.Hidden;
                        score++;
                    }
                }

                if ((string)rectangle.Tag == "ghost")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        GameOver("The Ghost Got You!");
                    }
                    if (rectangle.Name.ToString() == "pinkSquare")
                    {
                        Canvas.SetLeft(rectangle, Canvas.GetLeft(rectangle) - ghostSpeed);
                    }
                    else
                    {
                        Canvas.SetLeft(rectangle, Canvas.GetLeft(rectangle) + ghostSpeed);
                    }
                    currentGhostStep--;
                    if (currentGhostStep < 1)
                    {
                        currentGhostStep = ghostMoveStep;
                        ghostSpeed = -ghostSpeed;
                    }
                }
            }
            if (score == 85)
            {
                GameOver("You win!");
            }
        }

        private void GameReset()
        {

            Canvas.SetLeft(pacman, 100);
            Canvas.SetTop(pacman, 100);

            Canvas.SetLeft(pinkSquare, initialPinkGhostLeft);
            Canvas.SetTop(pinkSquare, initialPinkGhostTop);
            Canvas.SetLeft(redSquare, initialRedGhostLeft);
            Canvas.SetTop(redSquare, initialRedGhostTop);
            Canvas.SetLeft(orangeSquare, initialOrangeGhostLeft);
            Canvas.SetTop(orangeSquare, initialOrangeGhostTop);

            goLeft = goRight = goUp = goDown = false;
            noLeft = noRight = noUp = noDown = false;

            score = 0;
            txtScore.Content = "Score: 0";

            foreach (var rectangle in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)rectangle.Tag == "coin")
                {
                    rectangle.Visibility = Visibility.Visible;
                }
            }

            currentGhostStep = ghostMoveStep;
            ghostSpeed = 10;

            gameTimer.Start();
        }

        private void GameOver(string message)
        {
            gameTimer.Stop();
            MessageBox.Show(message, "PAC MAN");
            GameReset();
        }
    }
}
