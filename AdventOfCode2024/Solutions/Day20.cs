using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day20 : IDay
{
    private char[][] Parse() =>
        FileHelpers.ReadLines(20, s => s.Trim().ToArray());
    
    // N, E, S, W
    private readonly XyPair<int>[] _vectors = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    private readonly XyPair<int>[] _cheatRange =
        [new(0, -2), new(1, -1), new(2, 0), new(1, 1), new(0, 2), new(-1, 1), new(-2, 0), new(-1, -1)];

    private bool InBounds(char[][] map, XyPair<int> coord) =>
        coord.X > 0 && coord.X < (map[0].Length - 1) && coord.Y > 0 && coord.Y < (map.Length - 1);
    
    private XyPair<int> Find(char[][] map, char target)
    {
        for (var y = 1; y < map.Length - 1; y++)
        for (var x = 1; x < map[0].Length - 1; x++)
            if (map[y][x] == target)
                return new XyPair<int>(x, y);

        throw new InvalidDataException($"Can't find target: {target}");
    }
    
    private Dictionary<XyPair<int>, List<XyPair<int>>> GetNeighbours(char[][] map)
    {
        var neighbours = new Dictionary<XyPair<int>, List<XyPair<int>>>();
        
        for (var y = 1; y < map.Length - 1; y++)
        for (var x = 1; x < map[0].Length - 1; x++)
        {
            if (map[y][x] == '#')
                continue;

            var coord = new XyPair<int>(x, y);
            var coordNeighbours = _vectors.Select(vector => coord + vector)
                .Where(neighbour => map[neighbour.Y][neighbour.X] != '#')
                .ToList();

            neighbours.Add(coord, coordNeighbours);
        }

        return neighbours;
    }

    private Dictionary<XyPair<int>, (int, XyPair<int>?)> GetDistances(Dictionary<XyPair<int>, List<XyPair<int>>> neighbours,
        XyPair<int> start)
    {
        var active = new HashSet<XyPair<int>> { start };

        var distances = new Dictionary<XyPair<int>, (int, XyPair<int>?)>();
        foreach (var coord in neighbours.Keys)
            distances.Add(coord, (int.MaxValue, null));
        distances[start] = (0, null);

        while (active.Count > 0)
        {
            var source = active.MinBy(coord => distances[coord].Item1);
            var (sourceDistance, _) = distances[source];

            foreach (var neighbour in neighbours[source])
            {
                if (active.Contains(neighbour))
                    continue;

                var (previousDistance, _) = distances[neighbour];
                var newDistance = sourceDistance + 1;
                
                if (newDistance >= previousDistance)
                    continue;

                distances[neighbour] = (newDistance, source);
                active.Add(neighbour);
            }

            active.Remove(source);
        }

        return distances;
    }
    
    private HashSet<XyPair<int>> GetVisited(Dictionary<XyPair<int>, (int, XyPair<int>?)> distances, XyPair<int> end)
    {
        var visited = new HashSet<XyPair<int>>();
        var current = end;

        while (true)
        {
            visited.Add(current);

            var (_, previous) = distances[current];
            if (previous is null)
                break;

            current = previous.Value;
        }

        return visited;
    }
        
    public int Part1(char[][] map, int timeSave)
    {
        var count = 0;
        
        var neighbours = GetNeighbours(map);
        var start = Find(map, 'S');
        var end = Find(map, 'E');

        var distancesFromStart = GetDistances(neighbours, start);
        var distancesFromEnd = GetDistances(neighbours, end);

        var (noCheat, _) = distancesFromStart[end];
        var target = noCheat - timeSave;

        var visited = GetVisited(distancesFromStart, end);
        
        // don't bother checking in the final <timeSave> steps of the route
        var relevantFromStart = visited.Where(coord => distancesFromStart[coord].Item1 < target).ToArray();
        foreach (var potentialCheatStart in relevantFromStart)
        {
            var (distanceFromStart, _) = distancesFromStart[potentialCheatStart];
            
            foreach (var potentialCheatEnd in _cheatRange.Select(vector => potentialCheatStart + vector))
            {
                if (!InBounds(map, potentialCheatEnd))
                    continue;

                if (distancesFromEnd.TryGetValue(potentialCheatEnd, out var pair))
                {
                    var (distanceFromEnd, _) = pair;
                    if (distanceFromStart + distanceFromEnd + 2 <= target)
                        count++;
                }
            }
        }
        
        return count;
    }
    
    public void Run()
    {
        var map = Parse();
        Console.WriteLine($"Part 1: {Part1(map, 100)}");
    }
}