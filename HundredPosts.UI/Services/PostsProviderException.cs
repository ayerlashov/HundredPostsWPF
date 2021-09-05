using HundredPosts.Common.Exceptions;
using System;

namespace HundredPosts.UI.Services
{
    public class PostsProviderException : FriendlyException
    {
        public PostsProviderException(Exception e)
            : base(e)
        {
        }
    }
}
