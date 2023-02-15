namespace SolarisUI
{
    using System.Windows;

    public interface IDraggable
    {
        void Drag(Vector currentDrag);
        void ReleaseDrag();
    }
}