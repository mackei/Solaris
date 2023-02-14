using HorizonsData;

namespace HorizonsDataConsole;

internal static class ProgramSubroutines
{
    internal static int CelestialObjectSelection()
    {
        (string? RawValue, int Value, bool IsValid) selection = (null, 0, false);
        do
        {
            foreach (var option in HorizonsTelnetInterface.HorizonsDataIds)
                Console.WriteLine($"\t{option.Value}\t-\t{option.Key}");

            selection.RawValue = Console.ReadLine();
            if (int.TryParse(selection.RawValue, out selection.Value))
                if (HorizonsTelnetInterface.HorizonsDataIds.ContainsValue(selection.Value))
                {
                    selection.IsValid = true;
                    break;
                }

            Console.WriteLine("Invalid selection. Please choose from the available options:");
        } while (!selection.IsValid);

        return selection.Value;
    }

    internal static DateTime DateTimeSelection()
    {
        (string? RawValue, DateTime Value, bool IsValid) dateTimeSelection = (null, new DateTime(), false);
        do
        {
            dateTimeSelection.RawValue = Console.ReadLine();
            if (DateTime.TryParse(dateTimeSelection.RawValue, out dateTimeSelection.Value))
            {
                dateTimeSelection.IsValid = true;
                break;
            }

            Console.WriteLine("Invalid date and time format. Please try again.");
        } while (!dateTimeSelection.IsValid);

        return dateTimeSelection.Value;
    }
}