using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day10 : IDay
{
    private readonly XyPair<int>[] _vectors = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
    
    private int[][] Parse() =>
        FileHelpers.ReadLines<int[]>(10, s => s.Select(c => c - '0').ToArray());

    private bool InGrid(int[][] map, XyPair<int> position)
    {
        return position.X >= 0 && position.X < map[0].Length
            && position.Y >= 0 && position.Y < map.Length;
    }

    private readonly Dictionary<XyPair<int>, HashSet<XyPair<int>>> _trailheadCache = new();
    
    private HashSet<XyPair<int>> GetTrailheads(int[][] map, XyPair<int> position)
    {
        if (_trailheadCache.TryGetValue(position, out var cached))
            return cached;
        
        var current = map[position.Y][position.X];
        
        if (current == 9)
            return [position];

        var trailheads = new HashSet<XyPair<int>>();

        foreach (var direction in _vectors)
        {
            var next = position + direction;
            if (InGrid(map, next) && map[next.Y][next.X] == current + 1)
                trailheads.UnionWith(GetTrailheads(map, next));
        }
        
        _trailheadCache.Add(position, trailheads);

        return trailheads;
    }

    private readonly Dictionary<XyPair<int>, int> _countCache = new();
    
    private int CountTrailheads(int[][] map, XyPair<int> position)
    {
        if (_countCache.TryGetValue(position, out var cached))
            return cached;
        
        var current = map[position.Y][position.X];
        
        if (current == 9)
            return 1;

        var count = 0;

        foreach (var direction in _vectors)
        {
            var next = position + direction;
            if (InGrid(map, next) && map[next.Y][next.X] == current + 1)
                count += CountTrailheads(map, next);
        }
        
        _countCache.Add(position, count);

        return count;
    }
    
    public int Part1(int[][] map)
    {
        var count = 0;
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == 0)
                count += GetTrailheads(map, new XyPair<int>(x, y)).Count;

        return count;
    }

    public int Part2(int[][] map)
    {
        var count = 0;
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == 0)
                count += CountTrailheads(map, new XyPair<int>(x, y));

        return count;
    }
    
    public void Run()
    {
        var map = Parse();
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}