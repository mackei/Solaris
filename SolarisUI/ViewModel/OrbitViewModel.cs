using System.Windows;
using RealData;

namespace SolarisUI;

public class OrbitViewModel : BaseNotifyPropertyChanged, IRotatable, IDraggable, IZoomable
{
    public OrbitModel OrbitModel { get; private set; }

    public OrbitViewModel(OrbitModel orbitModel, double viewOrbitCenterX, double viewOrbitCenterY, double viewOrbitAngle, double zoom)
    {
        OrbitModel = orbitModel;
        ViewOrbitCenterXBeforeDrag = ViewOrbitCenterX = viewOrbitCenterX;
        ViewOrbitCenterYBeforeDrag = ViewOrbitCenterY = viewOrbitCenterY;
        ViewOrbitAngleBeforeRotate = ViewOrbitAngle = viewOrbitAngle;
        Zoom = zoom;

        //ViewOrbitCenterXBeforeDrag = ViewOrbitCenterX = OrbitModel.CenterCelestial?.GetX(PositionUnits.AU) ?? 0.0;
        //ViewOrbitCenterYBeforeDrag = ViewOrbitCenterY = -OrbitModel.CenterCelestial?.GetY(PositionUnits.AU) ?? 0.0;
        //ViewOrbitAngleBeforeRotate = ViewOrbitAngle = -OrbitModel.MajorAxisAngle;
    }

    public double Perihelion
    {
        get => OrbitModel.Perihelion;
        set => OrbitModel.Perihelion = value;
    }

    public double Aphelion
    {
        get => OrbitModel.Aphelion;
        set => OrbitModel.Aphelion = value;
    }

    public double OrbitCenterX => OrbitModel.CenterCelestial?.GetX(PositionUnits.AU) ?? 0.0;
    public double OrbitCenterY => OrbitModel.CenterCelestial?.GetY(PositionUnits.AU) ?? 0.0;

    public double ViewOrbitCenterXBeforeDrag { get; set; }
    public double ViewOrbitCenterYBeforeDrag { get; set; }
    public double ViewOrbitCenterX { get; set; }
    public double ViewOrbitCenterY { get; set; }

    public double ViewEllipseStartingX => ViewOrbitCenterX - Perihelion * Zoom;
    public double ViewEllipseStartingY => ViewOrbitCenterY - OrbitModel.MinorAxis / 2 * Zoom;

    public double ViewEllipseHeight => (OrbitModel?.MinorAxis ?? 0.0) * Zoom;
    public double ViewEllipseWidth => (OrbitModel?.MajorAxis ?? 0.0) * Zoom;

    public double ViewOrbitAngleBeforeRotate { get; set; }
    public double ViewOrbitAngle { get; set; }
    public double OrbitAngle => ViewOrbitAngle;
    public double SemiMajorAxis => (OrbitModel?.MajorAxis ?? 0.0) / 2 * Zoom;
    public double SemiMinorAxis => (OrbitModel?.MinorAxis ?? 0.0) / 2 * Zoom;

    public double Zoom { get; set; }

    public void Drag(Vector currentDrag)
    {
        ViewOrbitCenterX = ViewOrbitCenterXBeforeDrag + currentDrag.X;
        ViewOrbitCenterY = ViewOrbitCenterYBeforeDrag + currentDrag.Y;
    }

    public void ReleaseDrag()
    {
        ViewOrbitCenterXBeforeDrag = ViewOrbitCenterX;
        ViewOrbitCenterYBeforeDrag = ViewOrbitCenterY;
    }

    public void ZoomIn(double zoom, Point zoomCenter)
    {
        var newZoom = zoom / Zoom;
        Zoom = zoom;
        ViewOrbitCenterX = zoomCenter.X + (ViewOrbitCenterX - zoomCenter.X) * newZoom;
        ViewOrbitCenterY = zoomCenter.Y + (ViewOrbitCenterY - zoomCenter.Y) * newZoom;
        ViewOrbitCenterXBeforeDrag = ViewOrbitCenterX;
        ViewOrbitCenterYBeforeDrag = ViewOrbitCenterY;
        OnPropertyChanged(nameof(ViewEllipseStartingX));
        OnPropertyChanged(nameof(ViewEllipseStartingY));
        OnPropertyChanged(nameof(ViewEllipseHeight));
        OnPropertyChanged(nameof(ViewEllipseWidth));
    }

    public void Rotate(double currentRotation)
    {
        ViewOrbitAngle = ViewOrbitAngleBeforeRotate - currentRotation;
    }

    public void ReleaseRotate()
    {
        ViewOrbitAngleBeforeRotate = ViewOrbitAngle;
    }
}