using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

internal class OhNoException(string message) : Exception(message) {}

public class Day24 : IDay
{
    public (Dictionary<string, int>, Dictionary<string, (string, string, string)>) Parse(string s)
    {
        var parts = s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}");

        var initial = parts[0].Trim()
            .Split(Environment.NewLine)
            .Select(line =>
            {
                var lineParts = line.Trim().Split(": ");
                return (lineParts[0], int.Parse(lineParts[1]));
            }).ToDictionary();

        var connections = parts[1].Trim()
            .Split(Environment.NewLine)
            .Select(line =>
            {
                var lineParts = line.Split(" ");
                return (lineParts[4], (lineParts[0], lineParts[1], lineParts[2]));
            }).ToDictionary();

        return (initial, connections);
    }

    public long Part1(Dictionary<string, int> initial, Dictionary<string, (string, string, string)> connections)
    {
        var known = initial.ToDictionary();
        var remaining = connections.ToDictionary();

        while (remaining.Count > 0)
        {
            foreach (var (variable, equation) in remaining)
            {
                var (left, op, right) = equation;
                if (known.TryGetValue(left, out var leftValue)
                    && known.TryGetValue(right, out var rightValue))
                {
                    known[variable] = op switch
                    {
                        "AND" => leftValue & rightValue,
                        "OR" => leftValue | rightValue,
                        "XOR" => leftValue ^ rightValue,
                        _ => throw new OhNoException("This shouldn't happen")
                    };

                    remaining.Remove(variable);
                    break;
                }
            }
        }

        var bits = known.Where(pair => pair.Key.StartsWith('z'))
            .OrderBy(pair => pair.Key)
            .ToArray();

        var result = 0L;
        for (var i = 0; i < bits.Length; i++)
            result += (long)bits[i].Value << i;

        return result;
    }
    
    public void Run()
    {
        var (initial, connections) = Parse(FileHelpers.ReadText(24));
        Console.WriteLine($"Part 1: {Part1(initial, connections)}");
    }
}