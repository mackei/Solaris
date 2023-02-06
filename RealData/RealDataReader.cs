using System.Text;

namespace RealData;

public static class RealDataReader
{
    public static CelestialModel ReadRealCelestialDataFromStringData(string dataString)
    {
        using var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
        using var dataStreamReader = new StreamReader(dataStream);
        return ReadRealCelestialDataFromStreamReader(dataStreamReader);
    }

    public static CelestialModel ReadRealCelestialDataFromFile(string fullPath)
    {
        using var streamReader = new StreamReader(fullPath);
        return ReadRealCelestialDataFromStreamReader(streamReader);
    }

    public static CelestialModel ReadRealCelestialDataFromStreamReader(StreamReader streamReader)
    {
        var name = ReadName(streamReader);
        var properties = ReadProperties(streamReader);
        var mass = ReadMass(properties);
        var radius = ReadRadius(properties);
        var realDataEntry = ReadFirstRealDataEntryFromFileStream(streamReader);

        return new CelestialModel(name, mass, radius, realDataEntry.Item2,
            new OrbitModel(new CelestialModel("Sun", (decimal)1.9891e9, 696340,
                new Coordinates(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, PositionUnits.AU, SpeedUnits.AU_DAYS), null))
            {
                Aphelion = Math.Sqrt(
                    realDataEntry.Item2.GetX(PositionUnits.AU) * realDataEntry.Item2.GetX(PositionUnits.AU) +
                    realDataEntry.Item2.GetY(PositionUnits.AU) * realDataEntry.Item2.GetY(PositionUnits.AU)),
                Perihelion = Math.Sqrt(
                    realDataEntry.Item2.GetX(PositionUnits.AU) * realDataEntry.Item2.GetX(PositionUnits.AU) +
                    realDataEntry.Item2.GetY(PositionUnits.AU) * realDataEntry.Item2.GetY(PositionUnits.AU))
            });
    }

    private static string ReadName(StreamReader sr)
    {
        sr.ReadLine(); //decorator
        var nameLine = sr.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return nameLine[4];
    }

    private static Dictionary<string, string> ReadProperties(StreamReader sr)
    {
        var properties = new Dictionary<string, string>();
        sr.ReadLine(); //empty
        sr.ReadLine(); //properties header
        string line;
        while ((line = sr.ReadLine()) !=
               "*******************************************************************************")
        {
            var equalSigns = line.Count(ch => ch == '=');
            if (equalSigns == 2)
            {
                var property1 = ReadProperty(line.Substring(0, 42));
                var property2 = ReadProperty(line.Substring(42));

                try
                {
                    properties.Add(property1.Name, property1.Value);
                }
                catch (ArgumentException)
                {
                }

                try
                {
                    properties.Add(property2.Name, property2.Value);
                }
                catch (ArgumentException)
                {
                }
            }
            else if (equalSigns == 1)
            {
                var property = ReadProperty(line);
                try
                {
                    properties.Add(property.Name, property.Value);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        return properties;
    }

    private static (string Name, string Value) ReadProperty(string line)
    {
        var equalSignPosition = line.IndexOf('=');
        var name = line.Substring(0, equalSignPosition).Trim();
        var value = line.Substring(equalSignPosition + 1).Trim();
        return (name, value);
    }

    private static decimal ReadMass(Dictionary<string, string> properties)
    {
        var property = properties.First(p => p.Key.Contains("Mass"));
        var propertyName = property.Key;
        var propertyValue = property.Value;

        var nameParts = propertyName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var nameIndex = nameParts.ToList().FindIndex(s => s.Contains("Mass"));
        var xIndex = ++nameIndex;
        var massExponentIndex = nameParts[xIndex] == "x" ? ++xIndex : xIndex;
        var massExponent = nameParts[massExponentIndex][(nameParts[massExponentIndex].IndexOf('^') + 1)..];
        var massUnitIndex = ++massExponentIndex;
        var massUnit = nameParts[massUnitIndex];

        var valueParts = propertyValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var mass = valueParts[0];
        if (mass.Contains("+-")) mass = mass.Substring(0, mass.IndexOf('+'));

        return decimal.Parse(mass)
               * (decimal)Math.Pow(10, double.Parse(massExponent) - 21) //in ZETTA kg for better precision
               * (decimal)(massUnit == "(g)"
                   ? 0.0001
                   : 1.0); //in case some genius decides to give Jupiter mass in grams...
    }

    private static double ReadRadius(Dictionary<string, string> properties)
    {
        var property = properties.First(p => p.Key.Contains("Vol. Mean Radius") || p.Key.Contains("Vol. mean radius"));
        var propertyName = property.Key;
        var propertyValue = property.Value;

        var valueParts = propertyValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var radius = valueParts[0];
        if (radius.Contains("+-")) radius = radius.Substring(0, radius.IndexOf('+'));

        return double.Parse(radius); //in km
    }

    private static (DateTime, Coordinates) ReadFirstRealDataEntryFromFileStream(StreamReader sr)
    {
        while (sr.ReadLine() != "$$SOE")
        {
            //ignore the file header
        }

        return ReadRealDataEntryFromFileStream(sr);
    }

    private static (DateTime, Coordinates) ReadRealDataEntryFromFileStream(StreamReader sr)
    {
        var dateTime = ReadRealDataEntryTimeFromFileStream(sr);
        var position = ReadRealDataEntryPositionFromFileStream(sr);
        var speed = ReadRealDataEntrySpeedFromFileStream(sr);
        var range = ReadRealDataEntryRangeFromFileStream(sr);
        var coordinates = new Coordinates(position.Item1, position.Item2, position.Item3, speed.Item1, speed.Item2,
            speed.Item3, PositionUnits.AU, SpeedUnits.AU_DAYS);
        return (dateTime, coordinates);
    }

    private static DateTime ReadRealDataEntryTimeFromFileStream(StreamReader sr)
    {
        var time = sr.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var day = time[3];
        var hour = time[4];
        return DateTime.Parse(day + ' ' + hour);
    }

    private static (double, double, double) ReadRealDataEntryPositionFromFileStream(StreamReader sr)
    {
        var positionLine = sr.ReadLine();
        var position = positionLine.Replace(" =", "=").Replace("= ", "=")
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var x = position[0].Replace("X=", "");
        var y = position[1].Replace("Y=", "");
        var z = position[2].Replace("Z=", "");
        return (double.Parse(x), double.Parse(y), double.Parse(z));
    }

    private static (double, double, double) ReadRealDataEntrySpeedFromFileStream(StreamReader sr)
    {
        var speed = sr.ReadLine().Replace(" =", "=").Replace("= ", "=")
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var vx = speed[0].Replace("VX=", "");
        var vy = speed[1].Replace("VY=", "");
        var vz = speed[2].Replace("VZ=", "");
        return (double.Parse(vx), double.Parse(vy), double.Parse(vz));
    }

    private static double ReadRealDataEntryRangeFromFileStream(StreamReader sr)
    {
        var extra = sr.ReadLine().Replace(" =", "=").Replace("= ", "=")
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var range = extra[1].Replace("RG=", "");
        return double.Parse(range);
    }
}