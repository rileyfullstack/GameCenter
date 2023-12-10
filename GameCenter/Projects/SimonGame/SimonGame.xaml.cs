using GameCenter.Projects.SimonGame.Models;
using GameCenter.Projects.SimonGame.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GameCenter.Projects.SimonGame
{
    public partial class SimonGame : Window
    {
        private GameLogic gameLogic;
        private DispatcherTimer highlightTimer;
        private int highlightIndex;
        private string baseDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        private int highScore = 0;
        public SimonGame()
        {
            InitializeComponent();
            gameLogic = new GameLogic();
            highlightTimer = new DispatcherTimer();
            highlightTimer.Tick += HighlightTimer_Tick;
            highlightTimer.Interval = TimeSpan.FromSeconds(1);
            StartGame();
        }

        private void StartGame()
        {
            StatusTextBlock.Text = "Watch the sequence!";
            NewGameButton.Visibility = Visibility.Collapsed;
            highlightIndex = 0;
            highlightTimer.Start();
        }

        private void HighlightTimer_Tick(object sender, EventArgs e)
        {
            var sequence = gameLogic.GetSequence();

            if (highlightIndex < sequence.Count)
            {
                var buttonColor = sequence[highlightIndex];
                HighlightButton(buttonColor);
                highlightIndex++;
            }
            else
            {
                highlightTimer.Stop();
                StatusTextBlock.Text = "Now, repeat the sequence!";
                gameLogic.ResetCurrentInputIndex();
            }
        }

        private void HighlightButton(ButtonColor color)
        {
            PlayButtonSound(color);  // Play the sound corresponding to the button color

            switch (color)
            {
                case ButtonColor.Green:
                    GreenButton.Background = Brushes.LightGreen;
                    break;
                case ButtonColor.Red:
                    RedButton.Background = Brushes.LightCoral;
                    break;
                case ButtonColor.Yellow:
                    YellowButton.Background = Brushes.LightYellow;
                    break;
                case ButtonColor.Blue:
                    BlueButton.Background = Brushes.LightBlue;
                    break;
            }

            DispatcherTimer restoreTimer = new DispatcherTimer();
            restoreTimer.Interval = TimeSpan.FromSeconds(0.5);
            restoreTimer.Tick += (s, ev) =>
            {
                ResetButtonColors();
                restoreTimer.Stop();
            };
            restoreTimer.Start();
        }

        private void ResetButtonColors()
        {
            GreenButton.Background = Brushes.Green;
            RedButton.Background = Brushes.Red;
            YellowButton.Background = Brushes.Yellow;
            BlueButton.Background = Brushes.Blue;
        }
        private void PlayButtonSound(ButtonColor color)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Volume = 0.3;

            string soundPath = System.IO.Path.Combine(baseDirectory, "Sounds/");

            switch (color)
            {
                case ButtonColor.Green:
                    mediaPlayer.Open(new Uri(soundPath + "Green-Button.mp3", UriKind.Absolute));
                    break;
                case ButtonColor.Red:
                    mediaPlayer.Open(new Uri(soundPath + "Red-Button.mp3", UriKind.Absolute));
                    break;
                case ButtonColor.Yellow:
                    mediaPlayer.Open(new Uri(soundPath + "Yellow-Button.mp3", UriKind.Absolute));
                    break;
                case ButtonColor.Blue:
                    mediaPlayer.Open(new Uri(soundPath + "Blue-Button.mp3", UriKind.Absolute));
                    break;
            }


            mediaPlayer.Play();
        }
        private void PlaySuccessSound()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Volume = 0.5;

            string soundPath = System.IO.Path.Combine(baseDirectory, "Sounds/Success.mp3");
            mediaPlayer.Open(new Uri(soundPath, UriKind.Absolute));
            mediaPlayer.Play();
        }
        private void PlayLoseSound()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Volume = 0.3;

            string soundPath = System.IO.Path.Combine(baseDirectory, "Sounds/PlayerLose.mp3");
            mediaPlayer.Open(new Uri(soundPath, UriKind.Absolute));

            mediaPlayer.Play();

            mediaPlayer.MediaFailed += (sender, args) =>
            {
                MessageBox.Show($"Error playing sound: {args.ErrorException.Message}");
            };
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonColor pressedColor;
            if (sender == GreenButton) pressedColor = ButtonColor.Green;
            else if (sender == RedButton) pressedColor = ButtonColor.Red;
            else if (sender == YellowButton) pressedColor = ButtonColor.Yellow;
            else pressedColor = ButtonColor.Blue;

            var result = gameLogic.CheckInput(pressedColor);

            switch (result)
            {
                case CheckResult.Incorrect:
                    highlightTimer.Stop();
                    if(highScore < gameLogic.GetScore())
                    {
                        highScore = gameLogic.GetScore();
                    }
                    StatusTextBlock.Text = "You lose! New game? \n";
                    StatusTextBlock.Text += $"Your score: {gameLogic.GetScore()} \n";
                    StatusTextBlock.Text += $"Your highscore: {highScore}";
                    NewGameButton.Visibility = Visibility.Visible;
                    GreenButton.Visibility = Visibility.Hidden;
                    RedButton.Visibility = Visibility.Hidden;
                    BlueButton.Visibility = Visibility.Hidden;
                    YellowButton.Visibility = Visibility.Hidden;
                    PlayLoseSound();
                    break;
                case CheckResult.CompletedSequence:
                    PlaySuccessSound();
                    gameLogic.NewRound();
                    StartGame();
                    break;
                case CheckResult.Correct:
                    PlayButtonSound(pressedColor);
                    break;
            }

        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1: // Number 1 key
                case Key.NumPad1: // Number 1 on NumPad
                    GreenButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D2: // Number 2 key
                case Key.NumPad2: // Number 2 on NumPad
                    RedButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D3: // Number 3 key
                case Key.NumPad3: // Number 3 on NumPad
                    YellowButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D4: // Number 4 key
                case Key.NumPad4: // Number 4 on NumPad
                    BlueButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
            }
        }
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GreenButton.Visibility = Visibility.Visible;
            RedButton.Visibility = Visibility.Visible;
            BlueButton.Visibility = Visibility.Visible;
            YellowButton.Visibility = Visibility.Visible;
            gameLogic.Reset();
            StartGame();
        }
    }
}