using HundredPosts.UI.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HundredPosts.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(PostsViewModel posts)
        {
            InitializeComponent();
            DataContext = posts;
        }
    }
}
