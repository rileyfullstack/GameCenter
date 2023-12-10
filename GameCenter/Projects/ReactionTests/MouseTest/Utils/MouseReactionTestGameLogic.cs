using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameCenter.Projects.ReactionTests.MouseTest.Utils
{
    internal class MouseReactionTestGameLogic
    {

        private int score;
        public double difficulty;
        private DispatcherTimer timer;
        private Random random;
        private Action<Image> displayTarget;
        private Action<int> updateScoreDisplay;
        public delegate void GameOverHandler(bool won);
        public event GameOverHandler OnGameOver;

        public MouseReactionTestGameLogic(double difficulty, Action<Image> displayTarget, Action<int> updateScoreDisplay)
        {
            this.difficulty = difficulty; //difficulty directly effects the time it takes for a new target to be destroyed and a point taken. The lower the integer, the harder the game.
            this.displayTarget = displayTarget;
            this.updateScoreDisplay = updateScoreDisplay;
            score = 0;
            random = new Random();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Interval can be adjusted based on difficulty
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Image target = new Image();
            target.MouseUp += Target_MouseUp;

            // Set a timer for this target
            DispatcherTimer targetTimer = new DispatcherTimer();
            targetTimer.Interval = TimeSpan.FromSeconds(difficulty); 
            targetTimer.Tick += (s, args) =>
            {
                targetTimer.Stop();
                if (target.Visibility == Visibility.Visible)
                {
                    target.Visibility = Visibility.Collapsed;
                    MissedTarget(); // Handle missed target
                }
            };
            targetTimer.Start();

            displayTarget(target);
        }
        public void MissedTarget()
        {
            score--;
            updateScoreDisplay(score);
            if (score == -5)
            {
                OnGameOver?.Invoke(false);
            }
        }

        public void EndGame()
        {
            timer.Stop(); 
        }

        private void Target_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image target)
            {
                target.Visibility = Visibility.Collapsed;
                score++;
                updateScoreDisplay(score);
                if (score == 15)
                {
                    OnGameOver?.Invoke(true);
                }
            }
        }
    }
}
