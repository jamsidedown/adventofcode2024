using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day11 : IDay
{
    private long[] Parse() =>
        FileHelpers.ReadAndSplitLines<long>(11, " ").First();

    public List<long> Blink(long stone)
    {
        if (stone == 0L)
            return [1L];

        var baseTen = (int)Math.Log(stone, 10);
        if ((baseTen & 1) == 1)
        {
            var divisor = (long)Math.Pow(10, (baseTen >> 1) + 1);
            return [stone / divisor, stone % divisor];
        }

        return [stone * 2024];
    }

    private readonly Dictionary<(long, int), long> _cache = new();

    private long CountBlinks(long stone, int times)
    {
        if (times == 0)
            return 1L;

        if (_cache.TryGetValue((stone, times), out var count))
            return count;

        var stones = Blink(stone);

        var result = 0L;

        foreach (var s in stones)
            result += CountBlinks(s, times - 1);
        
        _cache.Add((stone, times), result);
        
        return result;
    }

    public long Part1(long[] stones)
    {
        var result = 0L;

        foreach (var stone in stones)
            result += CountBlinks(stone, 25);

        return result;
    }

    public long Part2(long[] stones)
    {
        var result = 0L;

        foreach (var stone in stones)
            result += CountBlinks(stone, 75);

        return result;
    }
    
    public void Run()
    {
        var stones = Parse();
        Console.WriteLine($"Part 1: {Part1(stones)}");
        Console.WriteLine($"Part 2: {Part2(stones)}");
    }
}