﻿using GameCenter.Projects.CarGame.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace GameCenter.Projects.CarGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CarGame : Window
    {
        private PlayerCar playerCar;
        private List<Obstacle> obstacles;
        private Random random;
        public static string baseDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string bombImagePath = System.IO.Path.Combine(baseDirectory, "Images/Bomb.png");

        public CarGame()
        {
            InitializeComponent();
            playerCar = new PlayerCar(200, 300, 2, playerCarImage);
            obstacles = new List<Obstacle>();
            random = new Random();

            DispatcherTimer gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = true;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = true;
                    break;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    playerCar.LeftKeyPressed = false;
                    break;
                case Key.Right:
                    playerCar.RightKeyPressed = false;
                    break;
            }
        }


        private void GameLoop(object sender, EventArgs e)
        {
            playerCar.Move();

            if (random.Next(0, 50) == 1)
            {
                Image obstacleImage = new Image();
                obstacleImage.Source = new BitmapImage(new Uri(bombImagePath));
                obstacleImage.Width = 50;
                obstacleImage.Height = 50;
                Obstacle obstacle = new Obstacle(random.Next(0, (int)Width), 0, 2, obstacleImage);
                obstacles.Add(obstacle);
                gameCanvas.Children.Add(obstacleImage);
            }

            double collisionBuffer = 5;

            foreach (var obstacle in obstacles)
            {
                obstacle.Move();
                if (playerCar.Representation.Margin.Left + playerCar.Representation.Width - collisionBuffer >= obstacle.Representation.Margin.Left + collisionBuffer
                    && playerCar.Representation.Margin.Left + collisionBuffer <= obstacle.Representation.Margin.Left + obstacle.Representation.Width - collisionBuffer
                    && playerCar.Representation.Margin.Top + collisionBuffer <= obstacle.Representation.Margin.Top + obstacle.Representation.Height - collisionBuffer
                    && playerCar.Representation.Margin.Top + playerCar.Representation.Height - collisionBuffer >= obstacle.Representation.Margin.Top + collisionBuffer)
                {
                    // End the game
                    (sender as DispatcherTimer).Stop();
                    break;
                }
            }


        }

    }
}


