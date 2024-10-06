using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;

namespace Approve.Desktop
{
    public partial class App : Application
    {
        // Sets background color of comboboxes because there is no other way to do it for some reason
        public static void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ToggleButton toggleButton = comboBox.Template.FindName("toggleButton", comboBox) as ToggleButton;
            if (toggleButton != null)
            {
                Border border = toggleButton.Template.FindName("templateRoot", toggleButton) as Border;
                if (border != null)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(59, 59, 59));
                }
            }
        }
    }
}
