namespace RealData;

public class Coordinates
{
    /// <summary>
    /// Position in meters
    /// Sun is the (0,0,0) point
    /// </summary>
    private double x { get; }
    private double y { get; }
    private double z { get; }

    /// <summary>
    /// Speed in m/s
    /// </summary>
    private double v_x { get; }
    private double v_y { get; }
    private double v_z { get; }

    public Coordinates(double x, double y, double z, double v_x, double v_y, double v_z, PositionUnits positionUnit, SpeedUnits speedUnit)
    {
        this.x = UnitRecalculation.RecalculatePositionUnits(x, positionUnit, PositionUnits.M);
        this.y = UnitRecalculation.RecalculatePositionUnits(y, positionUnit, PositionUnits.M);
        this.z = UnitRecalculation.RecalculatePositionUnits(z, positionUnit, PositionUnits.M);
        this.v_x = UnitRecalculation.RecalculateSpeedUnits(v_x, speedUnit, SpeedUnits.M_S);
        this.v_y = UnitRecalculation.RecalculateSpeedUnits(v_y, speedUnit, SpeedUnits.M_S);
        this.v_z = UnitRecalculation.RecalculateSpeedUnits(v_z, speedUnit, SpeedUnits.M_S);
    }

    public Coordinates(Coordinates coordinates)
    {
        x = coordinates.x;
        y = coordinates.y;
        z = coordinates.z;
        v_x = coordinates.v_x;
        v_y = coordinates.v_y;
        v_z = coordinates.v_z;
    }

    public double GetX(PositionUnits unit)
    {
        return UnitRecalculation.RecalculatePositionUnits(x, PositionUnits.M, unit);
    }

    public double GetY(PositionUnits unit)
    {
        return UnitRecalculation.RecalculatePositionUnits(y, PositionUnits.M, unit);
    }

    public double GetZ(PositionUnits unit)
    {
        return UnitRecalculation.RecalculatePositionUnits(z, PositionUnits.M, unit);
    }

    public double GetV_X(SpeedUnits unit)
    {
        return UnitRecalculation.RecalculateSpeedUnits(v_x, SpeedUnits.M_S, unit);
    }

    public double GetV_Y(SpeedUnits unit)
    {
        return UnitRecalculation.RecalculateSpeedUnits(v_y, SpeedUnits.M_S, unit);
    }

    public double GetV_Z(SpeedUnits unit)
    {
        return UnitRecalculation.RecalculateSpeedUnits(v_z, SpeedUnits.M_S, unit);
    }
}