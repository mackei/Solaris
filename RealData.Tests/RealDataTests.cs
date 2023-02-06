namespace RealData.Tests;

using NUnit.Framework;
using System.IO;

[TestFixture]
public class RealDataTests
{
    [Test]
    [TestCase(@"C:\Users\mjurkowski\source\Private\Solaris\Data\199_Mercury.txt")]
    public void ReadCelestialDataFromFileTest(string fullPath)
    {
        CelestialModel realDataModel;
        using (var streamReader = new StreamReader(fullPath))
        {
            realDataModel = RealDataReader.ReadRealCelestialDataFromStreamReader(streamReader);
        }

        Assert.AreEqual("Mercury", realDataModel.Name);
        Assert.AreEqual(330.2, (double)realDataModel.GetMass(MassUnits.ZKG), 1e-16);
        Assert.AreEqual(2440, realDataModel.GetRadius(PositionUnits.KM), 1e-16);
        Assert.AreEqual(2.370131269312628E-01, realDataModel.GetX(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-3.579856308716632E-01, realDataModel.GetY(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-5.099461395336485E-02, realDataModel.GetZ(PositionUnits.AU), 1e-16);
        Assert.AreEqual(1.784486855293458E-02, realDataModel.GetV_X(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(1.690865866613966E-02, realDataModel.GetV_Y(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-2.552191466995848E-04, realDataModel.GetV_Z(SpeedUnits.AU_DAYS), 1e-16);
    }

    [Test]
    [TestCase(@"C:\Users\mjurkowski\source\Private\Solaris\Data")]
    public void ReadCelestialsDataFromFolderTest(string fullPath)
    {
        var realDataModels = RealDataBatchReader.ReadRealCelestialsDataFromFolder(fullPath);

        Assert.AreEqual(8, realDataModels.Count);
        Assert.IsTrue(realDataModels.ContainsKey("Mercury"));
        Assert.AreEqual("Mercury", realDataModels["Mercury"].Name);
        Assert.AreEqual(330.2, realDataModels["Mercury"].GetMass(MassUnits.ZKG));
        Assert.AreEqual(2440, realDataModels["Mercury"].GetRadius(PositionUnits.KM), 1e-16);
        Assert.AreEqual(2.370131269312628E-01, realDataModels["Mercury"].GetX(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-3.579856308716632E-01, realDataModels["Mercury"].GetY(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-5.099461395336485E-02, realDataModels["Mercury"].GetZ(PositionUnits.AU), 1e-16);
        Assert.AreEqual(1.784486855293458E-02, realDataModels["Mercury"].GetV_X(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(1.690865866613966E-02, realDataModels["Mercury"].GetV_Y(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-2.552191466995848E-04, realDataModels["Mercury"].GetV_Z(SpeedUnits.AU_DAYS), 1e-16);
        Assert.IsTrue(realDataModels.ContainsKey("Venus"));
        Assert.AreEqual("Venus", realDataModels["Venus"].Name);
        Assert.AreEqual(4868.5, realDataModels["Venus"].GetMass(MassUnits.ZKG));
        Assert.AreEqual(6051.84, realDataModels["Venus"].GetRadius(PositionUnits.KM), 1e-16);
        Assert.AreEqual(-4.464159718759997E-01, realDataModels["Venus"].GetX(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-5.699136181385723E-01, realDataModels["Venus"].GetY(PositionUnits.AU), 1e-16);
        Assert.AreEqual(1.793980959789276E-02, realDataModels["Venus"].GetZ(PositionUnits.AU), 1e-16);
        Assert.AreEqual(1.578118205894339E-02, realDataModels["Venus"].GetV_X(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-1.256729409744827E-02, realDataModels["Venus"].GetV_Y(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-1.083132398319351E-03, realDataModels["Venus"].GetV_Z(SpeedUnits.AU_DAYS), 1e-16);
        Assert.IsTrue(realDataModels.ContainsKey("Earth"));
        Assert.AreEqual("Earth", realDataModels["Earth"].Name);
        Assert.AreEqual(5972.19, realDataModels["Earth"].GetMass(MassUnits.ZKG));
        Assert.AreEqual(6371.01, realDataModels["Earth"].GetRadius(PositionUnits.KM), 1e-16);
        Assert.AreEqual(-1.791160211966743E-01, realDataModels["Earth"].GetX(PositionUnits.AU), 1e-16);
        Assert.AreEqual(9.668129275452991E-01, realDataModels["Earth"].GetY(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-4.685077670480967E-05, realDataModels["Earth"].GetZ(PositionUnits.AU), 1e-16);
        Assert.AreEqual(-1.719106099742565E-02, realDataModels["Earth"].GetV_X(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-3.195004852797649E-03, realDataModels["Earth"].GetV_Y(SpeedUnits.AU_DAYS), 1e-16);
        Assert.AreEqual(-2.653762256588121E-07, realDataModels["Earth"].GetV_Z(SpeedUnits.AU_DAYS), 1e-16);
    }
}