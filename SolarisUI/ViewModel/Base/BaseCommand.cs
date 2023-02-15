namespace SolarisUI
{
    using System;
    using System.Windows.Input;

    public class BaseCommand : ICommand
    {
        private Action _action;

        public BaseCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged = (sender, args) => { };
    }
}
