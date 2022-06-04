using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Helpers.Command
{
    public class AsyncSingleCommand : AsyncCommand
    {
        public AsyncSingleCommand(Func<Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false,
            bool allowsMultipleExecutions = false)
            : base(execute, canExecute, onException, continueOnCapturedContext, allowsMultipleExecutions)
        {
        }

        public AsyncSingleCommand(Func<Task> execute,
            Func<bool> canExecute,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false,
            bool allowsMultipleExecutions = false)
            : base(execute, canExecute, onException, continueOnCapturedContext, allowsMultipleExecutions)
        {
        }
    }

    public class AsyncSingleCommand<T> : AsyncCommand<T>
    {
        public AsyncSingleCommand(Func<T?, Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false,
            bool allowsMultipleExecutions = false)
            : base(execute, canExecute, onException, continueOnCapturedContext, allowsMultipleExecutions)
        {
        }

        public AsyncSingleCommand(Func<T?, Task> execute,
            Func<bool> canExecute,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false,
            bool allowsMultipleExecutions = false)
            : base(execute, canExecute, onException, continueOnCapturedContext, allowsMultipleExecutions)
        {
        }
    }

    public class AsyncSingleCommand<TExecute, TCanExecute> : AsyncCommand<TExecute, TCanExecute>
    {
        public AsyncSingleCommand(Func<TExecute?, Task> execute,
            Func<TCanExecute?, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false,
            bool allowsMultipleExecutions = false)
            : base(execute, canExecute, onException, continueOnCapturedContext, allowsMultipleExecutions)
        {
        }
    }
}