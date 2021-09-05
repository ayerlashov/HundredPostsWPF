namespace HundredPosts.UI.Services
{
    public class Post
    {
        public Post(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public int Id { get; }

        public int UserId { get; }
    }
}
