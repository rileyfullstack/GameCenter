using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;

namespace GameCenter.Projects.ReactionTests.StaticReaction
{
    public partial class StaticReactionTest : Window
    {
        private DispatcherTimer timer;
        private DateTime startTime;
        private bool isTestActive = false;

        public StaticReactionTest()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            this.KeyDown += new KeyEventHandler(Space_Keydown);
        }

        private void Space_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                switch (e.Key)
                {
                    case Key _ when ImgReady.Visibility == Visibility.Visible:
                        ReadyClickEvent();
                        break;

                    case Key _ when ImgClickNow.Visibility == Visibility.Visible:
                        ImgClickNowEvent();
                        break;

                    default:
                        BtnRestartEvent();
                        break;
                }
            }
        }
        private void PrepareForTest()
        {
            ImgReady.Visibility = Visibility.Collapsed;
            TextInstructions.Visibility = Visibility.Collapsed;
            ImgWarning.Visibility = Visibility.Visible;

            Random rand = new Random();
            int delay = rand.Next(500, 5001);
            timer.Interval = TimeSpan.FromMilliseconds(delay);
            timer.Start();
        }

        private void ReadyClickEvent()
        {
            PrepareForTest();
        }

        private void BtnReady_Click(object sender, RoutedEventArgs e)
        {
            PrepareForTest();
        }


        private void ProcessReactionTime()
        {
            if (isTestActive)
            {
                TimeSpan reactionTime = DateTime.Now - startTime;
                TxtResult.Text = $"Your reaction time is {reactionTime.TotalMilliseconds} milliseconds, but you can do better! Try again?";
                PanelResults.Visibility = Visibility.Visible;
                ImgClickNow.Visibility = Visibility.Collapsed;
                isTestActive = false;
            }
        }

        private void ImgClickNow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ProcessReactionTime();
        }

        private void ImgClickNowEvent()
        {
            ProcessReactionTime();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            ImgWarning.Visibility = Visibility.Collapsed;
            ImgClickNow.Visibility = Visibility.Visible;
            startTime = DateTime.Now;
            isTestActive = true;
        }
        private void ResetUI()
        {
            PanelResults.Visibility = Visibility.Collapsed;
            ImgReady.Visibility = Visibility.Visible;
            TextInstructions.Visibility = Visibility.Visible;
        }

        private void BtnRestartEvent()
        {
            ResetUI();
        }

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            ResetUI();
        }
    }
}
