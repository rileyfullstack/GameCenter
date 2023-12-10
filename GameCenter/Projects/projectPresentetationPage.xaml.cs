using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GameCenter.Projects
{
    public partial class projectPresentetationPage : Window
    {
        private Func<Window>? createProject;
        private Window? currentProject;

        public projectPresentetationPage(Func<Window> createProject)
        {
            this.createProject = createProject;
            InitializeComponent();
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSource)
        {
            TitleLabel.Content = title;
            ProjectText.Text = projectDescription;
            ProjectImage.Source = imageSource;
        }

        private void ProjectImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (createProject != null)
            {
                currentProject = createProject();
                Close();
                currentProject.ShowDialog();
                currentProject.Close();
            }
        }
    }
}
