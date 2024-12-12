using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day11 : IDay
{
    private long[] Parse() =>
        FileHelpers.ReadAndSplitLines<long>(11, " ").First();

    public static readonly Func<long, List<long>> Blink = Cache.Memoize<long, List<long>>(stone =>
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
    });

    private static readonly Func<(long, int), long> CountBlinks = Cache.Memoize<(long, int), long>((countBlinks, args) =>
    {
        var (stone, times) = args;
        
        if (times == 0)
            return 1L;

        return Blink(stone).Sum(s => countBlinks((s, times - 1)));
    });

    public long Part1(long[] stones) =>
        // cache the result of splitting a stone, so each split is only calculated once
        // cache the number of stones after splitting a stone n times
        // added functions to memoize function and recursive function results
        stones.Sum(stone => CountBlinks((stone, 25)));

    public long Part2(long[] stones) =>
        // same as part 1
        stones.Sum(stone => CountBlinks((stone, 75)));

    public void Run()
    {
        var stones = Parse();
        Console.WriteLine($"Part 1: {Part1(stones)}");
        Console.WriteLine($"Part 2: {Part2(stones)}");
    }
}