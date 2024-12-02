using System.Reflection;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day01 : IDay
{
    private (int[], int[]) Parse()
    {
        var first = new List<int>();
        var second = new List<int>();

        foreach (var line in File.ReadAllLines(FileHelpers.GetFilepath(1)))
        {
            var parts = line
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray();
            first.Add(parts[0]);
            second.Add(parts[1]);
        }

        return (first.ToArray(), second.ToArray());
    }

    public int Part1(int[] left, int[] right)
    {
        return left.Order()
            .Zip(right.Order())
            .Select(pair => Math.Abs(pair.Second - pair.First))
            .Sum();
    }

    public int Part2(int[] left, int[] right)
    {
        var counts = right.CountBy(x => x).ToDictionary();
        return left
            .Where(x => counts.ContainsKey(x))
            .Select(x => x * counts[x])
            .Sum();
    }
    
    public void Run()
    {
        var (left, right) = Parse();
        Console.WriteLine($"Part 1: {Part1(left, right)}");
        Console.WriteLine($"Part 2: {Part2(left, right)}");
    }
}