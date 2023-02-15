namespace SolarisUI;

using System;
using System.ComponentModel;
using System.Windows;
using RealData;

public class CelestialViewModel : BaseNotifyPropertyChanged, IZoomable, IDraggable, IRotatable
{
    private CelestialModel CelestialModel { get; set; }

    public OrbitViewModel OrbitViewModel { get; private set; }

    public CelestialViewModel(CelestialModel celestialModel, double viewOrbitCenterX, double viewOrbitCenterY, double viewOrbitAngle, double zoom)
    {
        CelestialModel = celestialModel;
        if (CelestialModel.OrbitModel != null)
            OrbitViewModel = new OrbitViewModel(CelestialModel.OrbitModel, viewOrbitCenterX, viewOrbitCenterY,
                viewOrbitAngle, zoom);
/*            OrbitViewModel = new OrbitViewModel(CelestialModel.OrbitModel,
                CelestialModel.OrbitModel.CenterCelestial?.GetX(PositionUnits.AU) ?? 0.0,
                -CelestialModel.OrbitModel.CenterCelestial?.GetY(PositionUnits.AU) ?? 0.0,
                -CelestialModel.OrbitModel.MajorAxisAngle, 1.0);*/

        CelestialModel.PropertyChanged += OnCelestialViewModelChanged;
        if (CelestialModel.OrbitModel != null) CelestialModel.OrbitModel.PropertyChanged += OnCelestialViewModelChanged;
    }

    public string Name => CelestialModel.Name;
    public double Radius => CelestialModel.GetRadius(PositionUnits.AU);
    public double ViewRadius => Math.Max(0.5, Radius * Zoom);
    public double ViewDiameter => 2 * ViewRadius;

    public double ViewOrbitCenterX => OrbitViewModel?.ViewOrbitCenterX ?? ViewCelestialX;
    public double ViewOrbitCenterY => OrbitViewModel?.ViewOrbitCenterY ?? ViewCelestialY;

    public double CelestialX => CelestialModel.GetX(PositionUnits.AU);
    public double CelestialY => CelestialModel.GetY(PositionUnits.AU);

    public double ViewCelestialX => CelestialX * Zoom + (OrbitViewModel?.ViewOrbitCenterX ?? 0.0);
    public double ViewCelestialY => -CelestialY * Zoom + (OrbitViewModel?.ViewOrbitCenterY ?? 0.0);

    public double ViewCircleCelestialX => ViewCelestialX - ViewRadius;
    public double ViewCircleCelestialY => ViewCelestialY - ViewRadius;

    public double Zoom { get; set; } = 1.0;

    public void Drag(Vector currentDrag)
    {
        OrbitViewModel?.Drag(currentDrag);
        OnPropertyChanged(nameof(ViewCelestialX));
        OnPropertyChanged(nameof(ViewCelestialY));
        OnPropertyChanged(nameof(ViewCircleCelestialX));
        OnPropertyChanged(nameof(ViewCircleCelestialY));
    }

    public void ReleaseDrag()
    {
        OrbitViewModel?.ReleaseDrag();
    }

    public void ZoomIn(double zoom, Point zoomCenter)
    {
        Zoom = zoom;
        OrbitViewModel?.ZoomIn(zoom, zoomCenter);
        OnPropertyChanged(nameof(ViewRadius));
        OnPropertyChanged(nameof(ViewDiameter));
        OnPropertyChanged(nameof(ViewCelestialX));
        OnPropertyChanged(nameof(ViewCelestialY));
        OnPropertyChanged(nameof(ViewCircleCelestialX));
        OnPropertyChanged(nameof(ViewCircleCelestialY));
    }

    public void Rotate(double currentRotation)
    {
        OrbitViewModel?.Rotate(currentRotation);
        OnPropertyChanged(nameof(ViewCelestialX));
        OnPropertyChanged(nameof(ViewCelestialY));
        OnPropertyChanged(nameof(ViewCircleCelestialX));
        OnPropertyChanged(nameof(ViewCircleCelestialY));
    }

    public void ReleaseRotate()
    {
        OrbitViewModel?.ReleaseRotate();
    }

    private void OnCelestialViewModelChanged(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged("");
    }
}