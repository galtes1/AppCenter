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
    /// Interaction logic for RunnerGame.xaml
    /// </summary>
    public partial class RunnerGame : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;

        int force = 20;
        int speed = 5;
        Random rnd = new Random();
        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePositions = { 320, 310, 300, 305, 315 };
        int scrore = 0;


        public RunnerGame()
        {
            InitializeComponent();
            MyCanvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);  // להוסיף סקור בתוך הסגוריים?

            backgroundSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/background.png", UriKind.Relative));
            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();
            gameTimer.Start();
        }
        private void GameEngine(object? sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }
            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            scoreText.Content = "Score: " + scrore;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height - 10);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;
                spriteIndex += .5;

                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }

            if (jumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
            {
                speed = 12;
            }
            if (force < 0)
            {
                jumping = false;
            }

            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePositions[rnd.Next(0, obstaclePositions.Length)]);
                scrore += 1;
            }
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
            }
            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;
                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score: " + scrore + ". Press Enter To Restart";
            }
            else
            {
                obstacle.StrokeThickness = 0;
                player.StrokeThickness = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
                gameTimer.Start();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;
                playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_02.png", UriKind.Relative));
            }
        }


        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 320);

            RunSprite(1);
            obstacleSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/obstacle.png", UriKind.Relative));
            obstacle.Fill = obstacleSprite;

            jumping = false;
            gameOver = false;
            scrore = 0;

            scoreText.Content = "Score: " + scrore;
        }

        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_01.png", UriKind.Relative));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_02.png", UriKind.Relative));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_03.png", UriKind.Relative));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_04.png", UriKind.Relative));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_05.png", UriKind.Relative));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_06.png", UriKind.Relative));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_07.png", UriKind.Relative));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri(uriString: "images/newRunner_08.png", UriKind.Relative));
                    break;
            }
            player.Fill = playerSprite;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
