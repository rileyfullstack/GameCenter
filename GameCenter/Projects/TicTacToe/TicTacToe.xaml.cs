using GameCenter.Projects.TicTacToe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace GameCenter.Projects.TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    public partial class TicTacToe : Window
    {
        GameLogic _gameLogic = new GameLogic();
        private bool isFlashing;
        private DispatcherTimer flashTimer;
        private SolidColorBrush[] rainbowColors =
        {
            Brushes.Red,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.Green,
            Brushes.Blue,
            Brushes.Indigo,
            Brushes.Violet
        };
        private int currentColorIndex = 0;
        public TicTacToe()
        {
            InitializeComponent();
        }
        public void PlayerClicksEmptySpace(object sender, RoutedEventArgs e)
        {
            var space = (Button)sender;
            if (!String.IsNullOrWhiteSpace(space.Content?.ToString())) return; //If the space is occupied, do nothing.
            space.Content = _gameLogic.CurrentPlayer; //If it isn't, set the Content to the current player's letter

            string[] coordinents = space.Tag.ToString().Split(",");
            int Xvalue = int.Parse(coordinents[0]);
            int Yvalue = int.Parse(coordinents[1]);

            Position buttonPosition = new Position(Xvalue, Yvalue);

            _gameLogic.UpdateBoard(buttonPosition, _gameLogic.CurrentPlayer);

            if (_gameLogic.PlayerWin())
            {
                WinScreen.Text = "Player " + _gameLogic.CurrentPlayer + " has won!!!";
                WinScreen.Visibility = Visibility.Visible;
                NewGameBtnHighlight();
            }

            _gameLogic.SetNextPlayer(); //Set the player to the next one.
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (var control in gridBoard.Children)
            {
                if (control is Button)
                {
                    ((Button)control).Content = String.Empty;
                }
            }
            _gameLogic = new GameLogic();
            WinScreen.Visibility = Visibility.Collapsed;
            isFlashing = false;
            if (flashTimer != null)
            {
                flashTimer.Stop();
            }
        }
        public void NewGameBtnHighlight()
        {
            flashTimer = new DispatcherTimer();
            flashTimer.Interval = TimeSpan.FromSeconds(0.3);
            flashTimer.Tick += FlashTimer_Tick;
            isFlashing = true;
            flashTimer.Start();
        }

        private void FlashTimer_Tick(object sender, EventArgs e)
        {
            if (isFlashing)
            {
                btnNewGame.Background = rainbowColors[currentColorIndex];
                WinScreen.Background = rainbowColors[currentColorIndex];
                currentColorIndex = (currentColorIndex + 1) % rainbowColors.Length;
            }
        }
    }
}
