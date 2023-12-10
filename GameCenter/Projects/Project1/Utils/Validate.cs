using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameCenter.Projects.Project1.Utils
{
    public class Validate
    {
        public static bool UserName(TextBox box)
        {
            Regex regex = new(@"^[A-Za-z].{2,20}");
            Match match = regex.Match(box.Text);

            if (!match.Success) 
            {
                MessageBox.Show("please enter at least 3 characters");
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            box.BorderBrush = new SolidColorBrush(Colors.Black);
            return true;
        }
        public static bool Email(TextBox box)
        {
            Regex regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            Match match = regex.Match(box.Text);

            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid email address.");
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }

            box.BorderBrush = new SolidColorBrush(Colors.Black);
            return true;
        }

    }
}
