using GameCenter.Projects.ReactionTests.MouseTest;
using GameCenter.Projects.ReactionTests.StaticReaction;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace GameCenter.Projects.ReactionTests.MainMenu
{
    public partial class ReactionTestsMainMenu : Window
    {
        public ReactionTestsMainMenu()
        {
            InitializeComponent();
        }

        private void ImageTest1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DifficultyChoice difficultyChoice = new DifficultyChoice();
            difficultyChoice.Show();
            this.Close();
        }

        private void ImageTest2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StaticReactionTest staticReactionTest = new StaticReactionTest();
            staticReactionTest.Show();
            this.Close();
        }
    }
}
