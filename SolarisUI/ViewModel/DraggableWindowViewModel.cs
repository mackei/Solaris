namespace SolarisUI
{
    using System.Windows;

    public class DraggableWindowViewModel : BaseNotifyPropertyChanged
    {
        private WindowViewModel _ownerWindowViewModel;
        
        public Point DragStartingPoint { get; set; }

        public Point DragPosition
        {
            set
            {
                var diff = value - DragStartingPoint;
                foreach (var draggableViemModel in _ownerWindowViewModel.CelestialViewModels)
                {
                    draggableViemModel.Drag(diff);
                }
            }
        }

        public DraggableWindowViewModel(WindowViewModel ownerWindowViewModel)
        {
            _ownerWindowViewModel = ownerWindowViewModel;
        }

        public void ReleaseDrag()
        {
            foreach (var draggableViemModel in _ownerWindowViewModel.CelestialViewModels)
            {
                draggableViemModel.ReleaseDrag();
            }
        }
    }
}
