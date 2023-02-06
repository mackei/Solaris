namespace RealData;

public class CelestialModel : Coordinates
{
    /// <summary>
    /// Oh, come on...
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// mass in ZKG = 10^21kg
    /// </summary>
    private decimal _mass { get; }

    /// <summary>
    /// radius in km
    /// </summary>
    private double _radius { get; }

    private double _angle;
    public double Angle
    {
        get => _angle;
        set
        {
            _angle = value;
            if (_angle < -180.0)
            {
                _angle += 360.0;
            }
            else if (_angle > 180.0)
            {
                _angle -= 360.0;
            }
        }
    }

    /*public double AngleRad
    {
        get => _angle * Math.PI / 180.0;
        set => Angle = value / Math.PI * 180.0;
    }

    public double DistanceToCentre
    {
        get
        {
            if (OrbitModel == null)
            {
                return 0.0;
            }
            return (OrbitModel.LinearEccentricity * OrbitModel.LinearEccentricity -
                    OrbitModel.MajorAxis * OrbitModel.MajorAxis / 4.0)
                   / (OrbitModel.LinearEccentricity * Math.Cos(AngleRad) - OrbitModel.MajorAxis / 2);
        }
    }
    
    public double AngularSpeed
    {
        get
        {
            if (OrbitModel == null)
            {
                return 0.0;
            }
            return 0.25 * OrbitModel.MajorAxis * OrbitModel.MinorAxis * OrbitModel.MeanAngularSpeed /
                   (DistanceToCentre * DistanceToCentre);
        }
    }*/
    
    public OrbitModel OrbitModel { get; private set; }

    public decimal GetMass(MassUnits massUnit)
    {
        return UnitRecalculation.RecalculateMassUnits(_mass, MassUnits.ZKG, massUnit);
    }

    public double GetRadius(PositionUnits sizeUnits)
    {
        return UnitRecalculation.RecalculatePositionUnits(_radius, PositionUnits.KM, sizeUnits);
    }

    public CelestialModel(string name,
        decimal mass,
        double radius,
        Coordinates coordinates,
        OrbitModel orbitModel)
        : base(coordinates)
    {
        Name = name;
        _mass = mass;
        _radius = radius;
        OrbitModel = orbitModel;
    }
}