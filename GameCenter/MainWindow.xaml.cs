using gameCenter.Projects.Project1;
using GameCenter.Projects;
using GameCenter.Projects.CarGame;
using GameCenter.Projects.CurrencyConvertor;
using GameCenter.Projects.ReactionTests.MainMenu;
using GameCenter.Projects.SimonGame;
using GameCenter.Projects.TicTacToe;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using GameCenter.Projects;

namespace GameCenter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            clock.Tick += ShowCurrentDate!;
            clock.Start();
        }

        private void ShowCurrentDate(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                ProjectNameTextBox.Visibility = Visibility.Visible;
                var imageName = System.IO.Path.GetFileNameWithoutExtension(image.Source.ToString());
                ProjectNameTextBox.Text = imageName.Replace("-", " ");
            }

            if (sender is Image img)
            {
                img.Opacity = 0.6;
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            ProjectNameTextBox.Visibility = Visibility.Hidden;

            if (sender is Image img)
            {
                img.Opacity = 1;
            }
        }

        //Initialize the window to be lazy, as it starts automatically if put into a Window veriable.
        private void OnImage1Click(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createProject1 = () => new Project1();
            ShowPresentationPage("User Manager", "Welcome to the user manager! This is a small CRD excersise in which you can remove, add, and view users.Try it out by clicking the image below!", Image1.Source, createProject1);
        }

        private void OnImage2Click(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createTicTacToe = () => new TicTacToe();
            ShowPresentationPage("TicTacToe", "Welcome to my Tictactoe project. Here, you can find a simple tictactoe game that you can play with your friends. Try it out by clicking the image below!", Image2.Source, createTicTacToe);
        }

        private void OnImage3Click(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createCurrencyConverter = () => new CurrencyConvertorView();
            ShowPresentationPage("Currency Convertor", "Welcome to currency convertor, here you can see the Api project that uses real world data in order to convert currencies from one to another. Try it out by clicking the image below!", Image3.Source, createCurrencyConverter);
        }

        private void Image4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createSimonGame = () => new SimonGame();
            ShowPresentationPage("Simon Game", "Welcome to simon game! This is a game based on the real life Simon game, in which you have to press buttons in the order that they were played to you. You can use either your mouse or the corresponding keys on your keyboard. Try it out by clicking the image below!", Image4.Source, createSimonGame);
        }

        private void Image5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createCarGame = () => new CarGame();
            ShowPresentationPage("Car Game", "Welcome to car game, try it out by clicking the image below!", Image5.Source, createCarGame);
        }

        private void Image6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Func<Window> createReactionTests = () => new ReactionTestsMainMenu();
            ShowPresentationPage("Reaction Tests", "Welcome to the reaction tests, the project im most proud of! In this project you can find 2 games, one that tests your reaction speed by quickly requiring you to react to a changing image, and the other one being a mouse reaction test, that requires you to quickly click targets on the screen. If you do not click on time, your score reduces by one point. If you score 15 you win, and if you get to -5 you fail. There are multiple difficalty choices. Try it out by clicking the image below!", Image6.Source, createReactionTests);
        }

        private void ShowPresentationPage(string title, string description, ImageSource imageSource, Func<Window> createProject)
        {
            projectPresentetationPage presentation = new(createProject);
            presentation.OnStart(title, description, imageSource);
            presentation.ShowDialog();
        }
    }
}