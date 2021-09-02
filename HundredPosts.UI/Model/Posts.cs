using HundredPosts.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace HundredPosts.UI.Model
{
    public class Posts : ReadOnlyCollection<Post>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public Posts(IPostsProvider postsProvider) : this(postsProvider, new List<Post>())
        {
        }

        private Posts(IPostsProvider postsProvider, List<Post> posts)
            : base(posts)
        {
            PostsProvider = postsProvider ?? throw new ArgumentNullException(nameof(postsProvider));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private IPostsProvider PostsProvider { get; }

        public async Task LoadData()
        {
            //TODO: need exception handling here
            var posts = await PostsProvider.GetPosts();
            int itemsCount = Count;

            try
            {
                Items.Clear();

                for (int i = 0; i < posts.Count; i++)
                {
                    Items.Add(posts[i]);
                }
            }
            finally
            {
                if (itemsCount != Count)
                    OnCountChanged();

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs) =>
            CollectionChanged?.Invoke(this, notifyCollectionChangedEventArgs);

        private bool showUserId;
        public bool ShowUserId
        {
            get => showUserId;
            set
            {
                if (value == showUserId)
                    return;

                showUserId = value;
                OnShowUserIdChanged();
            }
        }

        private void OnShowUserIdChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowUserId)));
        private void OnCountChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));

        public void Toggle() => ShowUserId = !ShowUserId;
    }
}
