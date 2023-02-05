using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace HorizonsData.Tests;

[TestFixture]
public class HorizonsTelnetInterfaceTests
{
    [Test]
    [TestCase("Mercury")]
    [TestCase("Venus")]
    [TestCase("Earth")]
    [TestCase("Mars")]
    [TestCase("Jupiter")]
    [TestCase("Saturn")]
    [TestCase("Uranus")]
    [TestCase("Neptune")]
    public async Task Test(string selection)
    {
        var h = new HorizonsTelnetInterface();
        await h.ReadAndSaveDataFromHorizonsAsync(selection, new DateTime(2021, 01, 01, 00, 00, 00),
            GetFullPathToDataFolder());
    }

    private static string GetFullPathToDataFolder()
    {
        var thisFileDirectory = Directory.GetParent(GetThisFilePath());
        var solutionDirectory = thisFileDirectory.Parent;
        return solutionDirectory.GetDirectories("Data").First().FullName;
    }

    private static string GetThisFilePath([CallerFilePath] string path = null)
    {
        return path;
    }
}