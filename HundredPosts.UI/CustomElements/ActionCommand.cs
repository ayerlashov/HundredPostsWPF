using System;
using System.Threading.Tasks;

namespace HundredPosts.UI.CustomElements
{
    public class ActionCommand : CommandBase
    {
        public ActionCommand(Func<object, Task> action, bool skipIfExecuting, IErrorHandler errorHandler = null)
            : base(skipIfExecuting, errorHandler)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public ActionCommand(Action<object> action, bool skipIfExecuting, IErrorHandler errorHandler = null)
            : base(skipIfExecuting, errorHandler)
        {
            if(action == null)
                throw new ArgumentNullException(nameof(action));

            Action = Convert(action);
        }

        public Func<object, Task> Action { get; }

        protected override Task ExecuteAsyncCore(object parameter) => Action(parameter);

        private static Func<object, Task> Convert(Action<object> action)
        {
            return Wrapper;

            Task Wrapper(object obj)
            {
                action(obj);

                return Task.CompletedTask;
            }
        }
    }
}
