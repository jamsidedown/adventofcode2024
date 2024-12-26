using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public record Key(int[] Heights);
public record Lock(int[] Pins);

public class Day25 : IDay
{
    private bool IsKey(string[] lines)
    {
        foreach (var c in lines[0])
        {
            if (c != '#')
                return false;
        }

        return true;
    }

    private Key ParseKey(string[] lines)
    {
        var heights = new int[lines[0].Length];

        foreach (var line in lines[1..])
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                    heights[i] += 1;
            }
        }

        return new Key(heights);
    }

    private Lock ParseLock(string[] lines)
    {
        var pins = new int[lines[0].Length];

        foreach (var line in lines[..^1])
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                    pins[i] += 1;
            }
        }

        return new Lock(pins);
    }
    
    public (Key[], Lock[]) Parse(string s)
    {
        var keys = new List<Key>();
        var locks = new List<Lock>();

        var chunks = s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}");

        foreach (var chunk in chunks)
        {
            var lines = chunk.Trim().Split(Environment.NewLine);
            if (IsKey(lines))
                keys.Add(ParseKey(lines));
            else
                locks.Add(ParseLock(lines));
        }
        
        return (keys.ToArray(), locks.ToArray());
    }

    private bool Fits(Lock @lock, Key key)
    {
        if (@lock.Pins.Length != key.Heights.Length)
            return false;

        for (var i = 0; i < @lock.Pins.Length; i++)
        {
            if (@lock.Pins[i] + key.Heights[i] > 5)
                return false;
        }

        return true;
    }

    public int Part1(Key[] keys, Lock[] locks)
    {
        return keys.Sum(key => locks.Count(@lock => Fits(@lock, key)));
    }
    
    public void Run()
    {
        var (keys, locks) = Parse(FileHelpers.ReadText(25));
        Console.WriteLine($"Part 1: {Part1(keys, locks)}");
    }
}