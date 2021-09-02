using HundredPosts.Client.Interface;
using System;

namespace HundredPosts.UI.Services
{
    public class PostsProviderException : Exception
    {
        public PostsProviderException(Exception e)
            : base(e.Message, e)
        {
        }

        public string FriendlyMessage { get; set; }
    }
}
