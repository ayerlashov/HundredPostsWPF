using System;

namespace HundredPosts.Client
{
    public class HundredPostsClientException : Exception
    {
        public HundredPostsClientException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        public string FriendlyMessage { get; set; }
    }
}
