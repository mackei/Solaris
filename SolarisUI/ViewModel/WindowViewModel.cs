namespace SolarisUI
{
    using System.Collections.ObjectModel;
    using System.Windows;

    public class WindowViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<CelestialViewModel> CelestialViewModels { get; set; }

        public DatabaseViewModel DatabaseViewModel { get; set; }
        public DraggableWindowViewModel DraggableWindowViewModel { get; }
        public RotatableWindowViewModel RotatableWindowViewModel { get; }
        public ZoomableWindowViewModel ZoomableWindowViewModel { get; }
        public MainCommandViewModel MainCommandViewModel { get; }

        public WindowViewModel(Window window)
        {
            DatabaseViewModel = new DatabaseViewModel(this, @"\..\..\..\..\Data");
            DraggableWindowViewModel = new DraggableWindowViewModel(this);
            RotatableWindowViewModel = new RotatableWindowViewModel(this);
            ZoomableWindowViewModel = new ZoomableWindowViewModel(this);
            MainCommandViewModel = new MainCommandViewModel(window);
        }
    }
}