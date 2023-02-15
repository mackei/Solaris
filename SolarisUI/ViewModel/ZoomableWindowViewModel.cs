using System.Windows;

namespace SolarisUI
{
    public class ZoomableWindowViewModel : BaseNotifyPropertyChanged
    {
        private WindowViewModel _ownerWindowViewModel;
        private double _zoom;

        public double Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;
                foreach (var zoomableViewModel in _ownerWindowViewModel.CelestialViewModels)
                {
                    zoomableViewModel.ZoomIn(Zoom, ZoomCenter);
                }
            }
        }

        public Point ZoomCenter { get; set; } = new(0.0, 0.0);

        public ZoomableWindowViewModel(WindowViewModel ownerWindowViewModel)
        {
            _ownerWindowViewModel = ownerWindowViewModel;
            Zoom = 1.0;
        }

        public void ZoomIn(int direction, Point zoomCenter)
        {
            ZoomCenter = zoomCenter;
            var zoomFactor = 1.1;
            Zoom *= (direction > 0 ? zoomFactor : (1 / zoomFactor));
        }
    }
}