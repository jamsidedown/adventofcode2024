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
}