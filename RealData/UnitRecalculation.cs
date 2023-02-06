namespace RealData;

public static class UnitRecalculation
{
    public const double AU_TO_KM = 1.495978707e8;
    public const double KM_TO_M = 1000.0;
    public const double AU_TO_M = AU_TO_KM * KM_TO_M;

    public const double DAY_TO_H = 24.0;
    public const double H_TO_MIN = 60.0;
    public const double MIN_TO_S = 60.0;
    public const double DAY_TO_MIN = DAY_TO_H * H_TO_MIN;
    public const double DAY_TO_S = DAY_TO_MIN * MIN_TO_S;
    public const double H_TO_S = H_TO_MIN * MIN_TO_S;

    public const decimal KILO = (decimal)1000.0;
    public const decimal MEGA = KILO * (decimal)1000.0;
    public const decimal GIGA = MEGA * (decimal)1000.0;
    public const decimal TERA = GIGA * (decimal)1000.0;
    public const decimal PETA = TERA * (decimal)1000.0;
    public const decimal EXA = PETA * (decimal)1000.0;
    public const decimal ZETTA = EXA * (decimal)1000.0;

    public static double RecalculatePositionUnits(double x, PositionUnits sourceUnit, PositionUnits destinationUnit)
    {
        return (sourceUnit, destinationUnit) switch
        {
            (PositionUnits.AU, PositionUnits.KM) => x * AU_TO_KM,
            (PositionUnits.AU, PositionUnits.M) => x * AU_TO_M,
            (PositionUnits.KM, PositionUnits.AU) => x / AU_TO_KM,
            (PositionUnits.KM, PositionUnits.M) => x * KM_TO_M,
            (PositionUnits.M, PositionUnits.AU) => x / AU_TO_M,
            (PositionUnits.M, PositionUnits.KM) => x / KM_TO_M,
            _ => x
        };
    }

    public static double RecalculateSpeedUnits(double x, SpeedUnits sourceUnit, SpeedUnits destinationUnit)
    {
        return (sourceUnit, destinationUnit) switch
        {
            (SpeedUnits.AU_DAYS, SpeedUnits.KM_S) => x * AU_TO_KM / DAY_TO_S,
            (SpeedUnits.AU_DAYS, SpeedUnits.M_S) => x * AU_TO_M / DAY_TO_S,
            (SpeedUnits.KM_S, SpeedUnits.AU_DAYS) => x / AU_TO_KM * DAY_TO_S,
            (SpeedUnits.KM_S, SpeedUnits.M_S) => x * KM_TO_M,
            (SpeedUnits.M_S, SpeedUnits.AU_DAYS) => x / AU_TO_M * DAY_TO_S,
            (SpeedUnits.M_S, SpeedUnits.KM_S) => x / KM_TO_M,
            _ => x
        };
    }

    public static decimal RecalculateMassUnits(decimal mass, MassUnits sourceUnit, MassUnits destinationUnit)
    {
        return (sourceUnit, destinationUnit) switch
        {
            (MassUnits.KG, MassUnits.ZKG) => mass / ZETTA,
            (MassUnits.ZKG, MassUnits.KG) => mass * ZETTA,
            _ => mass
        };
    }
}
