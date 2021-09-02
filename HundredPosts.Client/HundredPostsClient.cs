using HundredPosts.Client.Interface;
using HundredPosts.Client.Interface.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HundredPosts.Client
{
    public class HundredPostsClient : IHundredPostsClient
    {
        private static JsonSerializerOptions JsonOptions { get; } =
            new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        private HttpClient Client { get; } = new HttpClient();

        private IHundredPostsClientConfig Config { get; }

        public HundredPostsClient(IHundredPostsClientConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));

            if (string.IsNullOrWhiteSpace(Config.HundredPostsUrl))
                throw new ArgumentException($"Property {nameof(config.HundredPostsUrl)} of parameter {nameof(config)} must be set.");
        }

        public async Task<List<Post>> GetPosts()
        {
            Stream getResponse;

            try
            {
                getResponse = await Client.GetStreamAsync(Config.HundredPostsUrl);
                return await JsonSerializer.DeserializeAsync<List<Post>>(getResponse, JsonOptions);
            }
            catch (Exception e)
            {
                throw new HundredPostsClientException(e)
                {
                    FriendlyMessage = $"Something went wrong with the call to {Config.HundredPostsUrl}."
                };
            }
        }

        public void Dispose() => Client.Dispose();
    }
}
