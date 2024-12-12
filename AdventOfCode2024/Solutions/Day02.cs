using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day02 : IDay
{
    private int[][] Parse() =>
        FileHelpers.ReadAndSplitLines<int>(2, " ");

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
        // calculate differences between successive levels in each report
        // check they're all increasing by 1-3, or decreasing by 1-3 each step
        
        return reports.Count(IsValid);
    }
    
    public int Part2(int[][] reports)
    {
        // generate all possible reports where one element has been removed
        // check if any of the possible variations for each report are valid
        
        return reports.Count(IsValidAfterDamping);
    }
    
    public void Run()
    {
        var input = Parse();
        Console.WriteLine($"Part 1: {Part1(input)}");
        Console.WriteLine($"Part 2: {Part2(input)}");
    }
}