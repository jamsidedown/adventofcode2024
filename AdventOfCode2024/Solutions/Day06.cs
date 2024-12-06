using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day06 : IDay
{
    private char[][] Parse() => FileHelpers.ReadLines(6, s => s.ToArray());

    private readonly XyPair[] _vectors = new[]
    {
        new XyPair(0, -1),
        new XyPair(1, 0),
        new XyPair(0, 1),
        new XyPair(-1, 0)
    };

    public XyPair FindStart(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[y].Length; x++)
            if (map[y][x] == '^')
                return new XyPair(x, y);

        return new XyPair(0, 0);
    }

    private bool IsObstacle(char[][] map, XyPair coord)
    {
        if (coord.X < 0 || coord.X >= map[0].Length)
            return false;

        if (coord.Y < 0 || coord.Y >= map.Length)
            return false;

        return map[coord.Y][coord.X] == '#';
    }
    
    private HashSet<XyPair> Walk(char[][] map, XyPair start)
    {
        var position = start;
        var visited = new HashSet<XyPair>();
        var direction = 0;

        while (position.X >= 0 && position.X < map[0].Length
            && position.Y >= 0&& position.Y < map.Length)
        {
            visited.Add(position);
            
            var next = position + _vectors[direction % 4];
            if (IsObstacle(map, next))
                direction++;
            else
                position = next;
        }

        return visited;
    }

    private char[][] MapWithObstacle(char[][] map, XyPair obstacle)
    {
        var width = map[0].Length;
        var newMap = new char[map.Length][];

        for (var y = 0; y < map.Length; y++)
        {
            if (obstacle.Y == y)
            {
                newMap[y] = new char[width];
                Array.Copy(map[y], newMap[y], width);
                newMap[y][obstacle.X] = '#';
            }
            else
            {
                newMap[y] = map[y];
            }
        }

        return newMap;
    }

    private bool IsLoop(char[][] map, XyPair start)
    {
        var position = start;
        var visited = new Dictionary<XyPair, int>();
        var direction = 0;

        while (position.X >= 0 && position.X < map[0].Length
            && position.Y >= 0&& position.Y < map.Length)
        {
            direction %= 4;
            var dir = 1 << direction;
            var vector = _vectors[direction];

            if (visited.TryGetValue(position, out var vectors))
            {
                
                if ((vectors & dir) > 0)
                    return true;

                visited[position] |= dir;
            }
            else
            {
                visited.Add(position, dir);
            }
            
            var next = position + vector;
            if (IsObstacle(map, next))
                direction++;
            else
                position = next;
        }

        return false;
    }
    
    public int Part1(char[][] map)
    {
        var start = FindStart(map);
        return Walk(map, start).Count;
    }

    public int Part2(char[][] map)
    {
        var start = FindStart(map);
        var originalVisited = Walk(map, start);
        originalVisited.Remove(FindStart(map));
        
        var count = 0;

        Parallel.ForEach(originalVisited, obstacle =>
        {
            var newMap = MapWithObstacle(map, obstacle);

            if (IsLoop(newMap, start))
                Interlocked.Increment(ref count);
        });
        
        return count;
    }
    
    public void Run()
    {
        var map = Parse();
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}