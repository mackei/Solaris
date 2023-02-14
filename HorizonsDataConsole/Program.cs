// See https://aka.ms/new-console-template for more information

using HorizonsData;
using HorizonsDataConsole;
using RealData;

Console.WriteLine("\n\nWelcome to the JPL Horizon System's console interface\n\n");

Console.WriteLine("Choose the celestial object to pull data for:");
var selection = ProgramSubroutines.CelestialObjectSelection();

Console.WriteLine("Set time and date to pull data for:");
var dateTime = ProgramSubroutines.DateTimeSelection();

var h = new HorizonsTelnetInterface();
var data = await h.ReadDataFromHorizonsAsync(selection, dateTime);

CelestialModel celestialObject;
celestialObject = RealDataReader.ReadRealCelestialDataFromStringData(data);

Console.Clear();
Console.WriteLine($"\n{celestialObject.Name}\n");
Console.WriteLine($"Mass {celestialObject.GetMass(MassUnits.KG)} KG");
Console.WriteLine("\nPosition");
Console.WriteLine($"X - {celestialObject.GetX(PositionUnits.AU)} AU");
Console.WriteLine($"Y - {celestialObject.GetY(PositionUnits.AU)} AU");
Console.WriteLine($"Z - {celestialObject.GetZ(PositionUnits.AU)} AU");
Console.WriteLine("\nSpeed");
Console.WriteLine($"V_X - {celestialObject.GetV_X(SpeedUnits.AU_DAYS)} AU/day");
Console.WriteLine($"V_Y - {celestialObject.GetV_Y(SpeedUnits.AU_DAYS)} AU/day");
Console.WriteLine($"V_Z - {celestialObject.GetV_Z(SpeedUnits.AU_DAYS)} AU/day");