using System.Globalization;
using System.Numerics;

namespace AdventOfCode2024.Core;

public static class FileHelpers
{
    public static string GetFilepath(int day)
    {
        var filename = $"input/day{day:X2}.txt";
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (!File.Exists(Path.Join(directory.FullName, filename)))
        {
            directory = directory.Parent;
            if (directory is null)
                throw new FileNotFoundException($"Can't find {filename}");
        }

        return Path.Join(directory.FullName, filename);
    }

    public static T[] ReadLines<T>(int day) where T : IParsable<T>, INumber<T>
    {
        var provider = new NumberFormatInfo();
        return File.ReadAllLines(GetFilepath(day))
            .Select(line => T.Parse(line, provider)).ToArray();
    }

    public static T[] ReadLines<T>(int day, Func<string, T> transform)
    {
        return File.ReadAllLines(GetFilepath(day))
            .Select(transform)
            .ToArray();
    }

    public static T[][] ReadAndSplitLines<T>(int day, string separator) where T : IParsable<T>, INumber<T>
    {
        var provider = new NumberFormatInfo();
        var result = new List<T[]>();

        foreach (var line in File.ReadAllLines(GetFilepath(day)))
        {
            var parts = line.Split(separator)
                .Select(x => T.Parse(x, provider))
                .ToArray();
            result.Add(parts);
        }

        return result.ToArray();
    }
}