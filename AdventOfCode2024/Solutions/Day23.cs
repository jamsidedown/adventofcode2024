using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day23 : IDay
{
    public Dictionary<string, HashSet<string>> Parse(string s)
    {
        var connections = new Dictionary<string, HashSet<string>>();

        foreach (var line in s.Trim().Split(Environment.NewLine))
        {
            var parts = line.Trim().Split('-');
            var first = parts[0];
            var second = parts[1];

            if (connections.TryGetValue(first, out var firstSet))
                firstSet.Add(second);
            else
                connections[first] = [second];

            if (connections.TryGetValue(second, out var secondSet))
                secondSet.Add(first);
            else
                connections[second] = [first];
        }

        return connections;
    }

    private (string, string, string) AsTriplet(string a, string b, string c)
    {
        var arr = new[] { a, b, c };
        Array.Sort(arr);
        return (arr[0], arr[1], arr[2]);
    }
    
    private HashSet<(string, string, string)> GetTriplets(Dictionary<string, HashSet<string>> connections, char startLetter)
    {
        var triplets = new HashSet<(string, string, string)>();

        foreach (var a in connections.Keys.Where(key => key.StartsWith(startLetter)))
        {
            var aSet = connections[a];
            foreach (var b in aSet)
            {
                var bSet = connections[b];
                foreach (var c in aSet.Intersect(bSet))
                {
                    var triplet = AsTriplet(a, b, c);
                    triplets.Add(triplet);
                }
            }
        }

        return triplets;
    }
    
    public int Part1(Dictionary<string, HashSet<string>> connections)
    {
        var triplets = GetTriplets(connections, 't');
        return triplets.Count;
    }
    
    public void Run()
    {
        var connections = Parse(FileHelpers.ReadText(23));
        Console.WriteLine($"Part 1: {Part1(connections)}");
    }
}