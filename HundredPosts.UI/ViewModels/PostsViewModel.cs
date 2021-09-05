using HundredPosts.Common.Exceptions;
using HundredPosts.UI.CustomElements;
using HundredPosts.UI.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HundredPosts.UI.Services
{
    public class PostsViewModel : INotifyPropertyChanged
    {
        private const string DefaultLoadErrorText = "Oops. Failed to load your data.";

        public PostsViewModel(IPostsProvider postsProvider)
        {
            PostsProvider = postsProvider ?? throw new ArgumentNullException(nameof(postsProvider));

            var errorHandler = new ActionErrorHandler(e => StatusText = GetErrorText(e, DefaultLoadErrorText));

            ContainedPosts = new();
            Posts = new(ContainedPosts);

            LoadDataCommand = new ActionCommand(_ => LoadDataAsync(), true, errorHandler);
            ToggleCommand = new ActionCommand(_ => Toggle(), true, errorHandler);

            StatusText = "Initialized";
        }

        public ObservableCollection<Post> Postss { get; set; }

        public CommandBase LoadDataCommand { get; }
        public CommandBase ToggleCommand { get; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private IPostsProvider PostsProvider { get; }

        private ObservableCollection<Post> ContainedPosts { get; }
        public ReadOnlyObservableCollection<Post> Posts { get; }


        private string statusText;
        public string StatusText
        {
            get => statusText;
            private set
            {
                if (value != statusText)
                    statusText = value;

                OnStatusTextChanged();
            }
        }

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

        private async Task LoadDataAsync()
        {
            StatusText = "Data loading...";

            var posts = await PostsProvider.GetPosts();

            int i = 0;

            for (; i < posts.Count && i < Posts.Count; i++)
            {
                ContainedPosts[i] = posts[i];
            }

            for (int j = Posts.Count - 1; j >= i; j--)
            {
                ContainedPosts.RemoveAt(j);
            }

            for (; i < posts.Count; i++)
            {
                ContainedPosts.Add(posts[i]);
            }

            StatusText = "Data loaded";
        }

        public void Toggle() => ShowUserId = !ShowUserId;

        private void OnShowUserIdChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowUserId)));
        private void OnStatusTextChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusText)));

        private static string GetErrorText(Exception ex, string defaultMessage)
        {
            string exceptionMessage = defaultMessage;

            if (ex is FriendlyException uiException)
            {
                exceptionMessage = uiException.FriendlyMessage;
            }
            else if (ex is AggregateException aggregateException)
            {
                uiException = (FriendlyException)aggregateException.InnerExceptions.FirstOrDefault(e => e is FriendlyException);

                if (uiException != null)
                    exceptionMessage = uiException.FriendlyMessage;
            }

            return exceptionMessage;
        }
    }
}
