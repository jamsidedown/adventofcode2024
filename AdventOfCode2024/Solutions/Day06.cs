using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day06 : IDay
{
    private char[][] Parse() => FileHelpers.ReadLines(6, s => s.ToArray());

    private readonly XyPair<int>[] _vectors = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    public XyPair<int> FindStart(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[y].Length; x++)
            if (map[y][x] == '^')
                return new XyPair<int>(x, y);

        return new XyPair<int>(0, 0);
    }

    private bool Inside(char[][] map, XyPair<int> coord) =>
        coord.X >= 0 && coord.X < map[0].Length && coord.Y >= 0 && coord.Y < map.Length;

    private bool IsObstacle(char[][] map, XyPair<int> coord) =>
        map[coord.Y][coord.X] == '#';

    private HashSet<XyPair<int>> Walk(char[][] map, XyPair<int> start)
    {
        var position = start;
        var visited = new HashSet<XyPair<int>>();
        var direction = 0;

        while (true)
        {
            visited.Add(position);
            
            var next = position + _vectors[direction % 4];

            if (!Inside(map, next))
                return visited;
            
            if (IsObstacle(map, next))
                direction++;
            else
                position = next;
        }
    }
    
    private char[][] MapWithObstacle(char[][] map, XyPair<int> obstacle)
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

    private bool Branch(char[][] map, HashSet<(XyPair<int>, int)> visited, XyPair<int> start, int direction)
    {
        var position = start;
        
        while (true)
        {
            // if current position and direction has been seen before it's a loop
            if (!visited.Add((position, direction)))
                return true;
            
            var next = position + _vectors[direction];

            if (!Inside(map, next))
                return false;

            if (IsObstacle(map, next))
                direction = (direction + 1) % 4;
            else
                position = next;
        }
    }

    private int CountLoops(char[][] map, XyPair<int> start)
    {
        var count = 0;
        
        var position = start;
        var visited = new HashSet<(XyPair<int>, int)>();
        var looped = new HashSet<XyPair<int>>();
        var direction = 0;

        while (true)
        {
            visited.Add((position, direction));
            
            var next = position + _vectors[direction];

            if (!Inside(map, next))
                break;

            if (IsObstacle(map, next))
            {
                direction = (direction + 1) % 4;
                continue;
            }
            
            // only branch if there hasn't previously been an obstacle at `next`
            if (looped.Add(next))
            {
                var branchMap = MapWithObstacle(map, next);
                var branchVisited = new HashSet<(XyPair<int>, int)>(visited);
                if (Branch(branchMap, branchVisited, position, (direction + 1) % 4))
                    count++;
            }
                
            position = next;
        }

        return count;
    }
    
    public int Part1(char[][] map)
    {
        // the array of direction vectors does a lot of heavy lifting here
        // from the start position, step forwards until outside the bounds of the map
        // every collision, increment the direction counter
        // current vector is direction%4
        
        var start = FindStart(map);
        return Walk(map, start).Count;
    }

    public int Part2(char[][] map)
    {
        // walk the same path as in part 1
        // at every step, place an obstacle in front and walk that path until it loops
        // check for looping by keeping a hashset of the current position and direction%4
        // brute forcing takes a while, but it's still a lot faster than my first attempt
        
        var start = FindStart(map);
        return CountLoops(map, start);
    }
    
    public void Run()
    {
        var map = Parse();
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}