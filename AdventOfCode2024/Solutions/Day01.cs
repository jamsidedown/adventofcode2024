using System.Reflection;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day01 : IDay
{
    private (int[], int[]) Parse()
    {
        var lines = FileHelpers.ReadAndSplitLines<int>(1, "   ");
        
        var first = lines.Select(line => line[0]).ToArray();
        var second = lines.Select(line => line[1]).ToArray();

        return (first, second);
    }

    public int Part1(int[] left, int[] right)
    {
        // sort both arrays of numbers
        // sum the difference of each of the elements
        
        return left.Order()
            .Zip(right.Order())
            .Select(pair => Math.Abs(pair.Second - pair.First))
            .Sum();
    }

    public int Part2(int[] left, int[] right)
    {
        // count the elements in the right array
        // sum the counts where an element is in both arrays
        
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