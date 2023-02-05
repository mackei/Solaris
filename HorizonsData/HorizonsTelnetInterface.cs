namespace HorizonsData;

using PrimS.Telnet;
using System.Threading.Tasks;

public class HorizonsTelnetInterface
{
    public static Dictionary<string, int> HorizonsDataIds = new()
    {
        { "Mercury", 199 },
        { "Venus", 299 },
        { "Earth", 399 },
        { "Mars", 499 },
        { "Jupiter", 599 },
        { "Saturn", 699 },
        { "Uranus", 799 },
        { "Neptune", 899 }
    };

    public async Task ReadAndSaveDataFromHorizonsAsync(string selection, DateTime dateTime, string fullPath)
    {
        var result = await ReadDataFromHorizonsAsync(HorizonsDataIds[selection], dateTime);
        using (var sw = new StreamWriter($"{fullPath}\\{HorizonsDataIds[selection]}_{selection}.txt", false))
        {
            sw.Write(result);
        }
    }

    public async Task<string> ReadDataFromHorizonsAsync(int id, DateTime dateTime)
    {
        var basicData = "";
        var results = "";
        using (var client = new Client("horizons.jpl.nasa.gov", 6775, new CancellationToken()))
        {
            var timeout = 5000;
            var response = "";
            do
            {
                response = await client.TerminatedReadAsync("> ", TimeSpan.FromMilliseconds(timeout));
            } while (!response.EndsWith("Horizons> "));

            await client.WriteLine(id.ToString());
            basicData = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            basicData = basicData.Substring(basicData.IndexOf('*'));

            await client.WriteLine("E");
            var s1 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("V");
            var s2 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("500@sun");
            var s3 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("Y");
            var s4 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("eclip");
            var s5 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine($"{dateTime.ToString("yyyy-MMMM-dd HH:mm:ss")}");
            var s6 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine($"{dateTime.AddDays(1.0).ToString("yyyy-MMMM-dd HH:mm:ss")}");
            var s7 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("1d");
            var s8 = await client.TerminatedReadAsync(" :", TimeSpan.FromMilliseconds(timeout));
            await client.WriteLine("Y");

            results = await client.TerminatedReadAsync("? :", TimeSpan.FromMilliseconds(timeout));
            results = results.Substring(results.IndexOf('*'));
        }

        return basicData + results;
    }
}