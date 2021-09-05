using HundredPosts.Common.Exceptions;
using System;

namespace HundredPosts.Client
{
    public class HundredPostsClientException : FriendlyException
    {
        public HundredPostsClientException(Exception innerException) : base(innerException)
        {
        }
    }
}
