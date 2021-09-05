using HundredPosts.Common.Exceptions;
using System;

namespace HundredPosts.Client.Interface
{
    public class HundredPostsClientException : FriendlyException
    {
        public HundredPostsClientException(Exception innerException) : base(innerException, innerException.Message)
        {
        }
    }
}
