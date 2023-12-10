using GameCenter.Projects.ReactionTests.MouseTest;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GameCenter.Projects.ReactionTests
{
    public partial class DifficultyChoice : Window
    {
        public DifficultyChoice()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            double difficaltyLevel = 1; //Deafult is one, will change if any button other then 'easy' is chosen.

            if (clickedImage != null)
            {
                switch (clickedImage.Name) //Lower the difficalty number, higher the challange 
                {
                    //Dont do nothing for the easy button, as it is deafult and does not need changing.

                    case "MediumImage":
                        difficaltyLevel = 0.9;
                        break;
                    case "HardImage":
                        difficaltyLevel = 0.75;
                        break;
                    case "InsaneImage":
                        difficaltyLevel = 0.65;
                        break;
                }
                MouseReactionTest mouseReactionTest = new MouseReactionTest(difficaltyLevel);
                mouseReactionTest.Show();
                this.Close();
            }
        }
    }
}
