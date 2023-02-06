namespace RealData;

public class OrbitModel
{
    public double Perihelion { get; set; }
    public double Aphelion { get; set; }
    public double Period { get; set; }
    public double MajorAxisAngle { get; set; }

    public CelestialModel CenterCelestial { get; private set; }

    public OrbitModel(CelestialModel centerCelestial)
    {
        CenterCelestial = centerCelestial;
    }
    
    public double Eccentricity => MajorAxis > 0.0 ? 2 * LinearEccentricity / MajorAxis : 0.0;
    
    public double LinearEccentricity => (Aphelion - Perihelion) / 2;
    
    public double MajorAxis => Perihelion + Aphelion;
    
    public double MinorAxis => 2 * Math.Sqrt(Perihelion * Aphelion);
    
    public double Area => 0.25 * Math.PI * MajorAxis * MinorAxis;
    
    public double MeanAngularSpeed => 2 * Math.PI / Period;
}