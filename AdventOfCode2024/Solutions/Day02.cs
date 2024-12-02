using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day02 : IDay
{
    private int[][] Parse()
    {
        return File.ReadAllLines(FileHelpers.GetFilepath(2))
            .Select(line => line.Split(' ')
                .Select(int.Parse).ToArray())
            .ToArray();
    }
    
    public bool IsValid(int[] report)
    {
        var diffs = report[..^1]
            .Zip(report[1..])
            .Select(pair => pair.Second - pair.First)
            .ToArray();

        return diffs.All(x => x is > 0 and < 4) || diffs.All(x => x is < 0 and > -4);
    }

    private IEnumerable<int[]> Combinations(int[] report)
    {
        for (var i = 0; i < report.Length; i++)
        {
            var combination = new List<int>();
            for (var j = 0; j < report.Length; j++)
            {
                if (i != j)
                    combination.Add(report[j]);
            }

            yield return combination.ToArray();
        }
    }

    public bool IsValidAfterDamping(int[] report)
    {
        return Combinations(report)
            .Any(IsValid);
    }
    
    public int Part1(int[][] reports)
    {
        return reports.Count(IsValid);
    }
    
    public int Part2(int[][] reports)
    {
        return reports.Count(IsValidAfterDamping);
    }
    
    public void Run()
    {
        var input = Parse();
        Console.WriteLine($"Part 1: {Part1(input)}");
        Console.WriteLine($"Part 2: {Part2(input)}");
    }
}