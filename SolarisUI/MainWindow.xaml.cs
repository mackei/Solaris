using System.Windows.Input;

namespace SolarisUI
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowViewModel _windowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _windowViewModel = new WindowViewModel(this);
            DataContext = _windowViewModel;
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _windowViewModel.ZoomableWindowViewModel.ZoomIn(e.Delta, e.GetPosition(e.Source as IInputElement));
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _windowViewModel.DraggableWindowViewModel.DragStartingPoint = e.GetPosition(e.Source as IInputElement);
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _windowViewModel.RotatableWindowViewModel.RotateStartingPoint = e.GetPosition(e.Source as IInputElement);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _windowViewModel.DraggableWindowViewModel.ReleaseDrag();
        }

        private void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _windowViewModel.RotatableWindowViewModel.ReleaseRotate();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _windowViewModel.DraggableWindowViewModel.DragPosition = e.GetPosition((IInputElement)e.Source);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                _windowViewModel.RotatableWindowViewModel.RotatePoint = e.GetPosition((IInputElement)e.Source);
            }
        }
    }
}
