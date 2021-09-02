using HundredPosts.Client;
using System.Configuration;

namespace HundredPosts.UI.Configs
{
    internal class HundredPostsClientConfig : IHundredPostsClientConfig
    {
        private const string UrlKey = "HundredPostsClientConfigURL";

        public string HundredPostsUrl => ConfigurationManager.AppSettings.Get(UrlKey);
    }
}
