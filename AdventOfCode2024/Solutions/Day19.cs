using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day19 : IDay
{
    public (string[], string[]) Parse(string s)
    {
        var parts = s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}");
        var patterns = parts[0].Trim().Split(", ");
        var towels = parts[1].Trim().Split(Environment.NewLine);
        return (patterns, towels);
    }

    private bool StartsWith(ReadOnlyMemory<char> pattern, ReadOnlyMemory<char> towel)
    {
        if (pattern.Length > towel.Length)
            return false;

        var relevant = towel[..(pattern.Length)];

        return pattern.Span.SequenceEqual(relevant.Span);
    }

    private readonly Dictionary<ReadOnlyMemory<char>, long> _cache = new();

    private long CountPossibleTowelCombinations(ReadOnlyMemory<char>[] patterns, ReadOnlyMemory<char> towel)
    {
        if (towel.Length == 0)
            return 1L;

        if (_cache.TryGetValue(towel, out var cached))
            return cached;
        
        var count = 0L;
        
        foreach (var pattern in patterns)
        {
            if (StartsWith(pattern, towel))
                count += CountPossibleTowelCombinations(patterns, towel[(pattern.Length)..]);
        }
        
        _cache.Add(towel, count);
        
        return count;
    }

    public int Part1(string[] patterns, string[] towels)
    {
        // accidentally solved part 1 & 2 at the same time
        // recursively count possible combinations of patterns that make up towels
        // cache counts for remaining sections of towel 
        
        var count = 0;
        var patternMem = patterns.Select(p => p.AsMemory()).ToArray();
        
        foreach (var towel in towels)
        {
            var towelMem = towel.AsMemory();
            var possibleCombinations = CountPossibleTowelCombinations(patternMem, towelMem);
            if (possibleCombinations > 0)
                count++;
        }
        
        return count;
    }

    public long Part2(string[] patterns, string[] towels)
    {
        var patternMem = patterns.Select(p => p.AsMemory()).ToArray();
        return towels.Sum(towel => CountPossibleTowelCombinations(patternMem, towel.AsMemory()));
    }
    
    public void Run()
    {
        var (patterns, towels) = Parse(FileHelpers.ReadText(19));
        Console.WriteLine($"Part 1: {Part1(patterns, towels)}");
        Console.WriteLine($"Part 2: {Part2(patterns, towels)}");
    }
}