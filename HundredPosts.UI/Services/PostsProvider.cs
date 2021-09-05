using HundredPosts.Client.Interface;
using HundredPosts.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HundredPosts.UI.Services
{
    public class PostsProvider : IPostsProvider
    {
        public PostsProvider(IHundredPostsClient postsClient)
        {
            PostsClient = postsClient;
        }

        public IHundredPostsClient PostsClient { get; }

        public async Task<List<Post>> GetPosts()
        {
            try
            {
                return (await PostsClient.GetPosts())
                    .Select(Map)
                    .ToList();
            }
            catch (HundredPostsClientException e)
            {
                throw new PostsProviderException(e)
                {
                    FriendlyMessage = e.FriendlyMessage
                };
            }
            catch (Exception e)
            {
                throw new PostsProviderException(e)
                {
                    FriendlyMessage = "Failed to retrieve the Posts."
                };
            }
        }

        private static Post Map(Client.Interface.Model.Post servicePost)
        {
            return servicePost == null
                ? null
                : new Post(servicePost.Id, servicePost.UserId);
        }
    }
}
