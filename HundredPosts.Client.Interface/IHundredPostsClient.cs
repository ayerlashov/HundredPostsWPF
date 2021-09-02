using HundredPosts.Client.Interface.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HundredPosts.Client.Interface
{
    public interface IHundredPostsClient : IDisposable
    {
        Task<List<Post>> GetPosts();
    }
}
