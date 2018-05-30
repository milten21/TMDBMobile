using System;
using System.Windows.Input;
using TMDBMobile.Core.Redux;

namespace TMDBMobile.Core.Utils
{
    public class CommandToAction<State> : ICommand
    {
        Store<State> store;
        Func<Object> execute;

        public CommandToAction(Store<State> store, Func<Object> execute)
        {
            this.execute = execute;
            this.store = store;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            store.Dispatch(execute());
        }
    }

    public class CommandToAction<State, T> : ICommand
    {
        Store<State> store;
        Func<T, Object> execute;

        public CommandToAction(Store<State> store, Func<T, Object> execute)
        {
            this.execute = execute;
            this.store = store;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            store.Dispatch(execute((T)parameter));
        }
    }

    public class CommandToAsyncAction<State> : ICommand
    {
        Store<State> store;
        Func<Store<State>.AsyncAction> action;

        public CommandToAsyncAction(Store<State> store, Func<Store<State>.AsyncAction> action)
        {
            this.action = action;
            this.store = store;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            store.Dispatch(action());
        }
    }

    public static class ReductorMVVMExtensions
    {
        public static ICommand CreateActionCommand<State>(this Store<State> store, Func<Object> actionMaker)
        {
            return new CommandToAction<State>(store, actionMaker);
        }

        public static ICommand CreateActionCommand<State, T>(this Store<State> store, Func<T, Object> actionMaker)
        {
            return new CommandToAction<State, T>(store, actionMaker);
        }

        public static ICommand CreateAsyncActionCommand<State>(this Store<State> store, Func<Store<State>.AsyncAction> actionMaker)
        {
            return new CommandToAsyncAction<State>(store, actionMaker);
        }
    }
}
