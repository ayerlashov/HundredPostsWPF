using System;

namespace HundredPosts.Client.Interface
{
    public class HundredPostsClientException : Exception
    {
        public HundredPostsClientException(Exception innerException) : base(innerException.Message, innerException)
        {
        }

        public string FriendlyMessage { get; set; }
    }
}
