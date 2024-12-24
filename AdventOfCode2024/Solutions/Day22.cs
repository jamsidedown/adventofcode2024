using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day22 : IDay
{
    private static long _limit = 16777216 - 1;

    private long Iterate(long input)
    {
        var current = input;
        current ^= current << 6;
        current &= _limit;
        current ^= current >> 5;
        current &= _limit;
        current ^= current << 11;
        current &= _limit;
        return current;
    }
    
    public long SecretNumber(long input, int iterations)
    {
        var current = input;
        
        for (var i = 0; i < iterations; i++)
            current = Iterate(current);

        return current;
    }

    private Dictionary<long, int[]> _prices = new();
    private Dictionary<long, int[]> _changes = new();

    private int _cacheCount = 0;
    
    private void Cache(long input)
    {
        _cacheCount++;
        Console.WriteLine($"Cache hit {_cacheCount} times");
        
        var changes = new int[2000];
        var prices = new int[2000];
        
        var current = input;
        var previousPrice = (int)current % 10;

        for (var i = 0; i < 2000; i++)
        {
            current = Iterate(current);
            var price = (int)current % 10;
            var diff = price - previousPrice;

            prices[i] = price;
            changes[i] = diff;

            previousPrice = price;
        }

        _prices[input] = prices;
        _changes[input] = changes;
    }
    
    private int Sell(long input, (int, int, int, int) sellCondition)
    {
        if (!_prices.ContainsKey(input))
            Cache(input);

        var (a, b, c, d) = sellCondition;

        var prices = _prices[input];
        var changes = _changes[input];

        for (var i = 3; i < 2000; i++)
        {
            if (a == changes[i - 3] && b == changes[i - 2] && c == changes[i - 1] && d == changes[i])
                return prices[i];
        }

        return 0;
    }

    public int Sell(long[] numbers, (int, int, int, int) sellCondition) =>
        numbers.Sum(number => Sell(number, sellCondition));

    public long Part1(long[] numbers) =>
        numbers.Sum(n => SecretNumber(n, 2000));

    public int Part2(long[] numbers)
    {
        var maximum = 0;
        
        for (var a = -9; a <= 9; a++)
        for (var b = -9; b <= 9; b++)
        {
            Console.WriteLine($"{a}, {b}");
            
            for (var c = -9; c <= 9; c++)
            for (var d = -9; d <= 9; d++)
            {
                var sell = (a, b, c, d);
                var total = Sell(numbers, sell);
                
                if (total > maximum)
                    maximum = total;
            }
        }

        return maximum;
    }

    public void Run()
    {
        var numbers = FileHelpers.ReadLines<long>(22);
        Console.WriteLine($"Part 1: {Part1(numbers)}");
        Console.WriteLine($"Part 2: {Part2(numbers)}");
    }
}