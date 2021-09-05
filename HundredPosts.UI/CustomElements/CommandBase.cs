using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HundredPosts.UI.CustomElements
{
    public abstract class CommandBase : ICommand
    {
        public CommandBase(bool skipIfExecuting, IErrorHandler errorHandler = null)
        {
            SkipIfExecuting = skipIfExecuting;
            ErrorHandler = errorHandler;
        }

        private int IsExecuting; // 0 == false
        private readonly IErrorHandler ErrorHandler;

        /// <summary>
        /// Allows to skip the concurrent execution if one is already in progress.
        /// The default value is true.
        /// </summary>
        public bool SkipIfExecuting { get; } = true;

        private event EventHandler canExecuteChanged;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                canExecuteChanged += value;
            }
            remove
            {
                canExecuteChanged -= value;
            }
        }

        public bool CanExecute(object parameter) => !SkipIfExecuting || IsExecuting == 0;

        public void Execute(object parameter) => _ = ExecuteAsync(parameter);

        public async Task ExecuteAsync(object parameter)
        {
            if (SkipIfExecuting && Interlocked.CompareExchange(ref IsExecuting, 1, 0) == 0)
            {
                try
                {
                    await ExecuteAsyncCore(parameter);
                }
                catch (Exception ex)
                {
                    ErrorHandler?.Handle(ex);
                }
                finally
                {
                    IsExecuting = 0;
                }
            }
        }

        protected abstract Task ExecuteAsyncCore(object parameter);
    }
}