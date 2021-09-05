using System;

namespace HundredPosts.UI.CustomElements
{
    internal class ActionErrorHandler : IErrorHandler
    {
        public ActionErrorHandler(Action<Exception> handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        private Action<Exception> Handler { get; }

        public void Handle(Exception e) => Handler(e);
    }

}
