using HundredPosts.Client;
using HundredPosts.Client.Interface;
using HundredPosts.UI.Configs;
using HundredPosts.UI.Services;
using System.Windows;
using Unity;

namespace HundredPosts.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer Container { get; } = new UnityContainer();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RegisterServices();

            MainWindow = Container.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private void RegisterServices()
        {
            _ = Container.RegisterSingleton<IHundredPostsClientConfig, HundredPostsClientConfig>()
                .RegisterSingleton<IHundredPostsClient, HundredPostsClient>()
                .RegisterSingleton<IPostsProvider, PostsProvider>();
        }
    }
}