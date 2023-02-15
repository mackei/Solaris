namespace SolarisUI
{
    public interface IEllipseViewModel : IOrbitViewModel
    {
        double ViewCenterPositionX { get; set; }
        double ViewCenterPositionY { get; set; }
        double EllipseStartingX { get; set; }
        double EllipseStartingY { get; set; }
        double EllipseHeight { get; set; }
        double EllipseWidth { get; set; }
        double OrbitAngle { get; set; }
    }
}
