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

        var result = Blink(stone).Sum(s => CountBlinks(s, times - 1));
        _cache.Add((stone, times), result);
        
        return result;
    }

    public long Part1(long[] stones) =>
        stones.Sum(stone => CountBlinks(stone, 25));

    public long Part2(long[] stones) =>
        stones.Sum(stone => CountBlinks(stone, 75));

    public void Run()
    {
        var stones = Parse();
        Console.WriteLine($"Part 1: {Part1(stones)}");
        Console.WriteLine($"Part 2: {Part2(stones)}");
    }
}