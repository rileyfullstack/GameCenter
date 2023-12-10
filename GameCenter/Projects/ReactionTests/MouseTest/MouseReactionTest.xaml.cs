using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GameCenter.Projects.ReactionTests.MouseTest.Utils;

namespace GameCenter.Projects.ReactionTests.MouseTest
{
    public partial class MouseReactionTest : Window
    {
        private MouseReactionTestGameLogic gameLogic;

        public MouseReactionTest(double difficulty)
        {
            InitializeComponent();
            StartNewGame(difficulty);
        }

        private void GameOver(bool won)
        {
            gameLogic.EndGame(); 

            Dispatcher.Invoke(() =>
            {
                GameOverText.Text = won ? "You won! Try again?" : "You lost:( Try again?";
                GameOverText.Visibility = Visibility.Visible;
                NewGameButton.Visibility = Visibility.Visible;
            });
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameOverText.Visibility = Visibility.Collapsed;
            NewGameButton.Visibility = Visibility.Collapsed;
            ScoreTextBlock.Text = "Score: 0";
            StartNewGame(gameLogic.difficulty);
        }

        private void StartNewGame(double difficulty)
        {
            if (gameLogic != null)
            {
                gameLogic.OnGameOver -= GameOver; // Unsubscribe from the old game logic's event
            }

            GameGrid.Children.Clear();
            gameLogic = new MouseReactionTestGameLogic(difficulty, DisplayTarget, UpdateScoreDisplay);
            gameLogic.OnGameOver += GameOver;
        }


        private void DisplayTarget(Image target)
        {
            Uri targetUri = new Uri("pack://application:,,,/Images/Image-Target.png");
            BitmapImage bitmapImage = new BitmapImage(targetUri);
            target.Source = bitmapImage;

            Grid mainGrid = this.Content as Grid;
            if (GameGrid != null)
            {
                // Set the size of the target if not already set
                target.Width = 50;
                target.Height = 50;

                Canvas canvas = new Canvas();
                GameGrid.Children.Add(canvas);
                canvas.Children.Add(target);

                // Generate random position within the bounds of the grid
                double maxX = GameGrid.ActualWidth - target.Width;
                double maxY = GameGrid.ActualHeight - target.Height;
                double randomX = new Random().NextDouble() * maxX;
                double randomY = new Random().NextDouble() * maxY;

                // Set the position of the target
                Canvas.SetLeft(target, randomX);
                Canvas.SetTop(target, randomY);
            }
        }

        private void UpdateScoreDisplay(int score)
        {
            Dispatcher.Invoke(() =>
            {
                ScoreTextBlock.Text = $"Score: {score}";
                if (score < 0)
                {
                    ScoreTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                    // Set a timer to reset the color after flashing
                    DispatcherTimer colorTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.5) };
                    colorTimer.Tick += (s, args) =>
                    {
                        colorTimer.Stop();
                        ScoreTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    };
                    colorTimer.Start();
                }
            });
        }

    }
}
