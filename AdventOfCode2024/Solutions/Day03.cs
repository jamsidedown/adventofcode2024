using System.Text.RegularExpressions;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day03 : IDay
{
    private readonly Regex _pattern = new(@"mul\((\d{1,3}),(\d{1,3})\)");

    private int CalcMatch(Match match)
    {
        var groups = match.Groups.ToList<Group>();
        var left = int.Parse(groups[1].Value);
        var right = int.Parse(groups[2].Value);
        return left * right;
    }
    
    public int Part1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
            total += _pattern.Matches(line).Sum(CalcMatch);

        return total;
    }

    private (int, bool) Calculate(string line, bool enable)
    {
        var matches = _pattern.Matches(line).ToList();
        
        if (enable)
        {
            var disableIndex = line.IndexOf("don't()", StringComparison.Ordinal);

            // no more "don't()" in this line
            if (disableIndex < 0)
                return (_pattern.Matches(line).Sum(CalcMatch), enable);

            var start = matches.Where(match => match.Index < disableIndex).Sum(CalcMatch);
            (var subtotal, enable) = Calculate(line[(disableIndex + 1)..], false);
            return (start + subtotal, enable);
        }

        var enableIndex = line.IndexOf("do()", StringComparison.Ordinal);

        // no more "do()" in this line
        if (enableIndex < 0)
            return (0, enable);

        return Calculate(line[(enableIndex + 1)..], true);
    }

    public int Part2(string[] input)
    {
        var total = 0;
        var enable = true;

        foreach (var line in input)
        {
            (var subtotal, enable) = Calculate(line, enable);
            total += subtotal;
        }

        return total;
    }
    
    public void Run()
    {
        var input = File.ReadAllLines(FileHelpers.GetFilepath(3));
        Console.WriteLine($"Part 1: {Part1(input)}");
        Console.WriteLine($"Part 2: {Part2(input)}");
    }
}