using System;

namespace HundredPosts.Common.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException(string friendlyMessage = null)
            : base()
        {
            FriendlyMessage = friendlyMessage;
        }

        public FriendlyException(string message, string friendlyMessage = null)
            : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        public FriendlyException(Exception e, string message = null, string friendlyMessage = null)
            : base(message ?? e.Message, e)
        {
            FriendlyMessage = friendlyMessage;
        }

        public string FriendlyMessage { get; set; }

        public override string ToString()
        {
            return FriendlyMessage == null
                ? base.ToString()
                : string.Concat("Friendly message: ", FriendlyMessage, Environment.NewLine, base.ToString());
        }
    }
}
