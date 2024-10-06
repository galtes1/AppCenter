using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppCenter
{
    /// <summary>
    /// Interaction logic for XoGame.xaml
    /// </summary>
    public partial class XoGame : Window
    {

        private string player1Name;
        private string player2Name;
        private string currentPlayer;
        private Button[,] board;
        private int player1Score;
        private int player2Score;

        public XoGame()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            board = new Button[3, 3]
            {
                { Button00, Button01, Button02 },
                { Button10, Button11, Button12 },
                { Button20, Button21, Button22 }
            };

            // Disable buttons initially
            foreach (var button in board)
            {
                button.IsEnabled = false;
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            player1Name = Player1NameTextBox.Text;
            player2Name = Player2NameTextBox.Text;

            currentPlayer = player1Name;
            player1Score = 0;
            player2Score = 0;

            UpdateScoreLabel();
            ResetBoard();

            // Hide input panel and show reset button
            NameInputPanel.Visibility = Visibility.Collapsed;
            ResetButton.Visibility = Visibility.Visible;

            // Enable buttons when the game starts
            foreach (var button in board)
            {
                button.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            button.Content = currentPlayer == player1Name ? "X" : "O";
            button.IsEnabled = false;

            if (CheckWin())
            {
                MessageBox.Show($"{currentPlayer} wins this round!");
                if (currentPlayer == player1Name)
                    player1Score++;
                else
                    player2Score++;

                UpdateScoreLabel();
                ResetBoard();
                return;
            }

            if (CheckDraw())
            {
                MessageBox.Show("It's a draw!");
                ResetBoard();
                return;
            }

            currentPlayer = currentPlayer == player1Name ? player2Name : player1Name;
        }

        private bool CheckWin()
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0].Content.ToString() == board[i, 1].Content.ToString() &&
                    board[i, 1].Content.ToString() == board[i, 2].Content.ToString() &&
                    !string.IsNullOrEmpty(board[i, 0].Content.ToString()))
                    return true;

                if (board[0, i].Content.ToString() == board[1, i].Content.ToString() &&
                    board[1, i].Content.ToString() == board[2, i].Content.ToString() &&
                    !string.IsNullOrEmpty(board[0, i].Content.ToString()))
                    return true;
            }

            if (board[0, 0].Content.ToString() == board[1, 1].Content.ToString() &&
                board[1, 1].Content.ToString() == board[2, 2].Content.ToString() &&
                !string.IsNullOrEmpty(board[0, 0].Content.ToString()))
                return true;

            if (board[0, 2].Content.ToString() == board[1, 1].Content.ToString() &&
                board[1, 1].Content.ToString() == board[2, 0].Content.ToString() &&
                !string.IsNullOrEmpty(board[0, 2].Content.ToString()))
                return true;

            return false;
        }

        private bool CheckDraw()
        {
            foreach (var button in board)
            {
                if (string.IsNullOrEmpty(button.Content.ToString()))
                    return false;
            }
            return true;
        }

        private void UpdateScoreLabel()
        {
            PlayerNameDisplay.Text = $"{player1Name} (X): {player1Score} | {player2Name} (O): {player2Score}";
        }

        private void ResetBoard()
        {
            foreach (var button in board)
            {
                button.Content = "";
                button.IsEnabled = true;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Show input panel and hide reset button
            NameInputPanel.Visibility = Visibility.Visible;
            ResetButton.Visibility = Visibility.Collapsed;

            // Reset the game board
            ResetBoard();
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Null checks for the text boxes and button
            if (Player1NameTextBox == null || Player2NameTextBox == null || StartGameButton == null)
                return;

            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                if (textBox == Player1NameTextBox)
                {
                    var player1NameValid = ValidatePlayerName(Player1NameTextBox.Text);
                    StartGameButton.IsEnabled = player1NameValid && ValidatePlayerName(Player2NameTextBox.Text);
                }
                else if (textBox == Player2NameTextBox)
                {
                    var player2NameValid = ValidatePlayerName(Player2NameTextBox.Text);
                    StartGameButton.IsEnabled = player2NameValid && ValidatePlayerName(Player1NameTextBox.Text);
                }
            }
        }

        private bool ValidatePlayerName(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName) || playerName.Length < 2 || playerName.Length > 10)
                return false;

            var regex = new Regex(@"^[\u0590-\u05FFa-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$");
            return regex.IsMatch(playerName);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "Player 1 Name" || textBox.Text == "Player 2 Name")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox == Player1NameTextBox ? "Player 1 Name" : "Player 2 Name";
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
