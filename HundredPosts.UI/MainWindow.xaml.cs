using HundredPosts.UI.Model;
using System.Windows;
using System.Windows.Controls;

namespace HundredPosts.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Posts posts)
        {
            InitializeComponent();
            DataContext = posts;
            _ = posts.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is FrameworkElement fwSender
                && fwSender.DataContext is Posts posts
                && e.OriginalSource is Button button
                && button.DataContext is Post)
            {
                posts.Toggle();
            }
        }
    }
}
