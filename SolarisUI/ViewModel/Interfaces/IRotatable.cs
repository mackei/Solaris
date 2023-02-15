namespace SolarisUI
{
    public interface IRotatable
    {
        double ViewOrbitCenterX { get; }
        double ViewOrbitCenterY { get; }

        void Rotate(double currentRotation);
        void ReleaseRotate();
    }
}