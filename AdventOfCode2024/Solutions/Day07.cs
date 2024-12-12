using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public record Equation(long Target, long[] Numbers);

public class Day07 :IDay
{
    public Equation[] Parse(string[] lines)
    {
        return lines.Select(line =>
        {
            var parts = line.Split(':');
            var target = long.Parse(parts[0]);
            var numbers = parts[1]
                .Trim()
                .Split(' ')
                .Select(long.Parse)
                .ToArray();
            return new Equation(target, numbers);
        }).ToArray();
    }

    public bool CanCalibrate(long target, long current, long[] numbers)
    {
        if (current > target)
            return false;
        
        if (numbers.Length == 0)
            return current == target;
        
        var add = current + numbers[0];
        var multiply = current * numbers[0];
        var remaining = numbers[1..];

        return CanCalibrate(target, add, remaining)
            || CanCalibrate(target, multiply, remaining);
    }

    private long Concat(long left, long right)
    {
        var log10 = Math.Floor(Math.Log(right, 10)) + 1.0;
        return left * (long)Math.Pow(10, log10) + right;
    }

    private bool CanRecalibrate(long target, long current, long[] numbers)
    {
        if (current > target)
            return false;

        if (numbers.Length == 0)
            return current == target;

        var add = current + numbers[0];
        var multiply = current * numbers[0];
        var concat = Concat(current, numbers[0]);
        var remaining = numbers[1..];
        
        return CanRecalibrate(target, add, remaining)
            || CanRecalibrate(target, multiply, remaining)
            || CanRecalibrate(target, concat, remaining);
    }
    
    public long Part1(Equation[] equations)
    {
        // try both operators recursively
        // end branches early if it passes the target result
        // no negative or zero values, so a new value will always increase the result
        
        return equations
            .Where(eq => CanCalibrate(eq.Target, eq.Numbers[0], eq.Numbers[1..]))
            .Sum(eq => eq.Target);
    }
    
    public long Part2(Equation[] equations)
    {
        // exactly the same as part 1, but with three operators
        
        return equations
            .Where(eq => CanRecalibrate(eq.Target, eq.Numbers[0], eq.Numbers[1..]))
            .Sum(eq => eq.Target);
    }
    
    public void Run()
    {
        var equations = Parse(FileHelpers.ReadLines(7));
        Console.WriteLine($"Part 1: {Part1(equations)}");
        Console.WriteLine($"Part 2: {Part2(equations)}");
    }
}