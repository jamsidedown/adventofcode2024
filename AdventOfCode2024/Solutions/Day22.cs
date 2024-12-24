using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

using SellCondition = (int, int , int, int);
using Price = int;

public class Day22 : IDay
{
    private const long _limit = 16777216 - 1;

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

    private Dictionary<SellCondition, Price> _prices = new();
    
    private void CalculatePrices(long input)
    {
        var prices = new int[2000];
        var changes = new int[2000];
        
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
        
        var seen = new HashSet<SellCondition>();

        for (var i = 3; i < 2000; i++)
        {
            var sellCondition = (changes[i - 3], changes[i - 2], changes[i - 1], changes[i]);
            if (!seen.Contains(sellCondition))
            {
                if (_prices.ContainsKey(sellCondition))
                    _prices[sellCondition] += prices[i];
                else
                    _prices[sellCondition] = prices[i];

                seen.Add(sellCondition);
            }
        }
    }
    
    private long Part1(long[] numbers) =>
        numbers.Sum(n => SecretNumber(n, 2000));

    private int Part2(long[] numbers)
    {
        foreach (var number in numbers)
            CalculatePrices(number);

        return _prices.Values.Max();
    }

    public void Run()
    {
        var numbers = FileHelpers.ReadLines<long>(22);
        Console.WriteLine($"Part 1: {Part1(numbers)}");
        Console.WriteLine($"Part 2: {Part2(numbers)}");
    }
}