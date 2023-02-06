namespace RealData;

public static class RealDataBatchReader
{
    public static IDictionary<string, CelestialModel> ReadRealCelestialsDataFromFolder(string fullPath)
    {
        return Directory.EnumerateFiles(fullPath).Select(RealDataReader.ReadRealCelestialDataFromFile)
            .ToDictionary(celestialModel => celestialModel.Name);
    }
}