using System;

namespace HundredPosts.UI.CustomElements
{
    public interface IErrorHandler
    {
        void Handle(Exception e);
    }
}
