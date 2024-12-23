using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day22 : IDay
{
    private static long _limit = 16777216 - 1;
    
    public long SecretNumber(long input, int iterations)
    {
        var current = input;
        
        for (var i = 0; i < iterations; i++)
        {
            current ^= current << 6;
            current &= _limit;
            current ^= current >> 5;
            current &= _limit;
            current ^= current << 11;
            current &= _limit;
        }

        return current;
    }

    public long Part1(long[] numbers) =>
        numbers.Sum(n => SecretNumber(n, 2000));

    public long Part2(long[] numbers)
    {
        return 0L;
    }

    public void Run()
    {
        var numbers = FileHelpers.ReadLines<long>(22);
        Console.WriteLine($"Part 1: {Part1(numbers)}");
    }
}