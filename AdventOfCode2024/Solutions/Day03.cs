using System.Text.RegularExpressions;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day03 : IDay
{
    private readonly Regex _mulPattern = new(@"mul\(\d{1,3},\d{1,3}\)");
    private readonly Regex _allPattern = new(@"(mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\))");
    
    private int CalcMul(string s)
    {
        return s[4..^1]
            .Split(',')
            .Select(int.Parse)
            .Aggregate((a, b) => a * b);
    }
    
    public int Part1(string[] input)
    {
        var total = 0;

        foreach (var line in input)
        {
            total += _mulPattern.Matches(line)
                .Sum(match => CalcMul(match.Value));
        }

        return total;
    }

    public int Part2(string[] input)
    {
        var total = 0;
        var enable = true;

        foreach (var line in input)
        {
            foreach (Match match in _allPattern.Matches(line))
            {
                if (match.Value == "do()")
                    enable = true;
                else if (match.Value == "don't()")
                    enable = false;
                else if (enable)
                    total += CalcMul(match.Value);
            }
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