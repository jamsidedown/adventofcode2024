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

    private Func<XyPair<int>, HashSet<XyPair<int>>>? _getTrailheads;
    
    private HashSet<XyPair<int>> GetTrailheads(int[][] map, XyPair<int> position)
    {
        _getTrailheads ??= Cache.Memoize<XyPair<int>, HashSet<XyPair<int>>>((func, pos) =>
        {
            var current = map[pos.Y][pos.X];

            if (current == 9)
                return [pos];

            var trailheads = new HashSet<XyPair<int>>();

            foreach (var direction in _vectors)
            {
                var next = pos + direction;
                if (InGrid(map, next) && map[next.Y][next.X] == current + 1)
                    trailheads.UnionWith(func(next));
            }

            return trailheads;
        });

        return _getTrailheads(position);
    }

    private Func<XyPair<int>, int>? _countTrailheads;
    
    private int CountTrailheads(int[][] map, XyPair<int> position)
    {
        _countTrailheads ??= Cache.Memoize<XyPair<int>, int>((func, pos) =>
        {
            var current = map[pos.Y][pos.X];

            if (current == 9)
                return 1;

            var count = 0;
            
            foreach (var direction in _vectors)
            {
                var next = pos + direction;
                if (InGrid(map, next) && map[next.Y][next.X] == current + 1)
                    count += func(next);
            }

            return count;
        });

        return _countTrailheads(position);
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