namespace SolarisUI
{
    using System.Windows.Input;
    using System.Windows;

    public class MainCommandViewModel
    {
        private Window _window;

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainCommandViewModel(Window window)
        {
            _window = window;

            MinimizeCommand = new BaseCommand(MinimizeCommandAction);
            MaximizeCommand = new BaseCommand(MaximizeCommandAction);
            ExitCommand = new BaseCommand(ExitCommandAction);
        }

        private void MinimizeCommandAction()
        {
            _window.WindowState = WindowState.Minimized;
        }

        private void MaximizeCommandAction()
        {
            _window.WindowState = WindowState.Maximized;
        }

        private void ExitCommandAction()
        {
            _window.Close();
        }
    }
}