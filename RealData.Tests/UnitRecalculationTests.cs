namespace RealData.Tests;

using NUnit.Framework;
using System.Collections;

[TestFixture]
public class UnitRecalculationTests
{
    [Test]
    [TestCaseSource(nameof(PositionTestCases))]
    public double RecalculatePositionUnitsTest(double value, PositionUnits sourceUnits, PositionUnits destinationUnits)
    {
        return UnitRecalculation.RecalculatePositionUnits(value, sourceUnits, destinationUnits);
    }

    public static IEnumerable PositionTestCases
    {
        get
        {
            yield return new TestCaseData(1.0, PositionUnits.AU, PositionUnits.AU).Returns(1);
            yield return new TestCaseData(1.0, PositionUnits.KM, PositionUnits.KM).Returns(1);
            yield return new TestCaseData(1.0, PositionUnits.M, PositionUnits.M).Returns(1);
            yield return new TestCaseData(1.0, PositionUnits.AU, PositionUnits.KM).Returns(1.495978707e8);
            yield return new TestCaseData(1.0, PositionUnits.AU, PositionUnits.M).Returns(1.495978707e11);
            yield return new TestCaseData(1.0, PositionUnits.KM, PositionUnits.M).Returns(1e3);
            yield return new TestCaseData(1.0, PositionUnits.KM, PositionUnits.AU).Returns(1.0 / 1.495978707e8);
            yield return new TestCaseData(1.0, PositionUnits.M, PositionUnits.AU).Returns(1.0 / 1.495978707e11);
            yield return new TestCaseData(1.0, PositionUnits.M, PositionUnits.KM).Returns(1e-3);
        }
    }

    [Test]
    [TestCaseSource(nameof(SpeedTestCases))]
    public double RecalculateSpeedUnitsTest(double value, SpeedUnits sourceUnits, SpeedUnits destinationUnits)
    {
        return UnitRecalculation.RecalculateSpeedUnits(value, sourceUnits, destinationUnits);
    }

    public static IEnumerable SpeedTestCases
    {
        get
        {
            yield return new TestCaseData(1.0, SpeedUnits.AU_DAYS, SpeedUnits.AU_DAYS).Returns(1);
            yield return new TestCaseData(1.0, SpeedUnits.KM_S, SpeedUnits.KM_S).Returns(1);
            yield return new TestCaseData(1.0, SpeedUnits.M_S, SpeedUnits.M_S).Returns(1);
            yield return new TestCaseData(1.0, SpeedUnits.AU_DAYS, SpeedUnits.KM_S).Returns(1.495978707e8 / 24.0 / 60.0 / 60.0);
            yield return new TestCaseData(1.0, SpeedUnits.AU_DAYS, SpeedUnits.M_S).Returns(1.495978707e11 / 24.0 / 60.0 / 60.0);
            yield return new TestCaseData(1e8, SpeedUnits.KM_S, SpeedUnits.AU_DAYS).Returns(1e8 / (1.495978707e8 / 24.0 / 60.0 / 60.0));
            yield return new TestCaseData(1e8, SpeedUnits.KM_S, SpeedUnits.M_S).Returns(1e8 * 1000.0);
            yield return new TestCaseData(1e8, SpeedUnits.M_S, SpeedUnits.AU_DAYS).Returns(1e8 / (1.495978707e11 / 24.0 / 60.0 / 60.0));
            yield return new TestCaseData(1e8, SpeedUnits.M_S, SpeedUnits.KM_S).Returns(1e8 / 1000.0);
        }
    }

    [Test]
    [TestCaseSource(nameof(MassTestCases))]
    public decimal RecalculateMassUnitsTest(decimal value, MassUnits sourceUnits, MassUnits destinationUnits)
    {
        return UnitRecalculation.RecalculateMassUnits(value, sourceUnits, destinationUnits);
    }

    public static IEnumerable MassTestCases
    {
        get
        {
            yield return new TestCaseData((decimal)1.0e23, MassUnits.KG, MassUnits.KG).Returns((decimal)1.0e23);
            yield return new TestCaseData((decimal)100.0, MassUnits.ZKG, MassUnits.ZKG).Returns((decimal)100.0);
            yield return new TestCaseData((decimal)100.0, MassUnits.ZKG, MassUnits.KG).Returns((decimal)1.0e23);
            yield return new TestCaseData((decimal)1.0e23, MassUnits.KG, MassUnits.ZKG).Returns((decimal)100.0);
        }
    }
}
