namespace SolarisUI
{
    using System;
    using System.Linq;
    using System.Windows;

    public class RotatableWindowViewModel
    {
        private WindowViewModel _ownerWindowViewModel;

        public RotatableWindowViewModel(WindowViewModel ownerWindowViewModel)
        {
            _ownerWindowViewModel = ownerWindowViewModel;
        }

        public Point RotateStartingPoint { get; set; }

        public Point RotatePoint
        {
            set
            {
                var centerPoint = new Point(_ownerWindowViewModel.CelestialViewModels.First().ViewOrbitCenterX,
                    _ownerWindowViewModel.CelestialViewModels.First().ViewOrbitCenterY);
                var startingAngle = CalculateAngle(centerPoint, RotateStartingPoint);
                var newAngle = CalculateAngle(centerPoint, value);
                var diff = newAngle - startingAngle;
                foreach (var rotatableViewModel in _ownerWindowViewModel.CelestialViewModels)
                {
                    rotatableViewModel.Rotate(diff);
                }
            }
        }

        private double CalculateAngle(Point center, Point point)
        {
            var diffX = point.X - center.X;
            var diffY = center.Y - point.Y;
            if (Math.Abs(diffX) < 0.0000001)
            {
                return 0.0;
            }
            var atan = Math.Atan(diffY / diffX) * 180.0 / Math.PI;
            if (diffX > 0.0 && diffY > 0)
            {
                return atan;
            }
            else if (diffX < 0.0 && diffY > 0.0)
            {
                return atan + 180.0;
            }
            else if (diffX > 0.0 && diffY < 0.0)
            {
                return atan;
            }
            else
            {
                return atan - 180.0;
            }
        }

        public void ReleaseRotate()
        {
            foreach (var rotatableViewModel in _ownerWindowViewModel.CelestialViewModels)
            {
                rotatableViewModel.ReleaseRotate();
            }
        }
    }
}