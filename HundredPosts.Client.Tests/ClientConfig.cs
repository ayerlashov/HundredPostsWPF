namespace HundredPosts.Client.Tests
{
    internal class ClientConfig : IHundredPostsClientConfig
    {
        public string HundredPostsUrl => "https://jsonplaceholder.typicode.com/posts";
    }
}