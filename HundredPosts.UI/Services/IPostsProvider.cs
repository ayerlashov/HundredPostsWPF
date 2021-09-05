using HundredPosts.UI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HundredPosts.UI.Services
{
    public interface IPostsProvider
    {
        Task<List<Post>> GetPosts();
    }
}
