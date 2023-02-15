using System.Windows;

namespace SolarisUI
{
    public interface IZoomable
    {
        double Zoom { get; set; }
        void ZoomIn(double zoom, Point zoomCenter);
    }
}