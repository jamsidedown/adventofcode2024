using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

using Step = (XyPair<int>, int);

internal record Route(HashSet<XyPair<int>> Visited, long Distance);

public class Day16 : IDay
{
    public char[][] Parse(string s) =>
        s.Trim()
            .Split(Environment.NewLine)
            .Select(line => line.Trim().ToArray())
            .ToArray();

    // N, E, S, W
    private readonly XyPair<int>[] _vectors = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    private XyPair<int> Find(char[][] map, char target)
    {
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == target)
                return new XyPair<int>(x, y);

        throw new InvalidDataException($"Can't find target: {target}");
    }

    private Dictionary<XyPair<int>, List<Step>> GetNeighbours(char[][] map)
    {
        var neighbours = new Dictionary<XyPair<int>, List<Step>>();
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
        {
            if (map[y][x] == '#')
                continue;

            var coord = new XyPair<int>(x, y);

            var coordNeighbours = new List<Step>();

            for (var direction = 0; direction < _vectors.Length; direction++)
            {
                var vector = _vectors[direction];
                var neighbour = coord + vector;
                
                if (map[neighbour.Y][neighbour.X] == '#')
                    continue;
                
                coordNeighbours.Add((neighbour, direction));
            }
            
            neighbours.Add(coord, coordNeighbours);
        }

        return neighbours;
    }
    
    public long Part1(char[][] map)
    {
        var start = Find(map, 'S');
        Step startStep = (start, 1);

        var neighbours = GetNeighbours(map);
        var active = new HashSet<Step> { startStep };
            
        var distances = new Dictionary<Step, long> ();
        foreach (var step in neighbours.Values.SelectMany(x => x))
            distances[step] = long.MaxValue;
        distances[startStep] = 0L;

        while (active.Count > 0)
        {
            var source = active.MinBy(step => distances[step]);
            var (coord, direction) = source;

            foreach (var neighbour in neighbours[coord].Where(n => !active.Contains(n)))
            {
                var (_, nDirection) = neighbour;
                if (Math.Abs(nDirection - direction) == 2)
                    // opposite direction, ignore
                    continue;

                var distance = distances[source] + (direction == nDirection ? 1 : 1001);
                
                if (distance < distances[neighbour])
                {
                    distances[neighbour] = distance;
                    active.Add(neighbour);
                }
            }
            
            active.Remove(source);
        }
        
        var end = Find(map, 'E');
        var endSteps = Enumerable.Range(0, _vectors.Length).Select(Step (dir) => (end, dir));
        var endDistances = new List<long>();
        foreach (var endStep in endSteps)
        {
            if (distances.TryGetValue(endStep, out var distance))
                endDistances.Add(distance);
        }

        return endDistances.Min();
    }

    private void PrintBenches(char[][] map, HashSet<XyPair<int>> route)
    {
        for (var y = 0; y < map.Length; y++)
        {
            var line = new List<char>();
            for (var x = 0; x < map[0].Length; x++)
            {
                var coord = new XyPair<int>(x, y);
                line.Add(route.Contains(coord) ? 'O' : map[y][x]);
            }
            Console.WriteLine(string.Join("", line));
        }
    }

    public int Part2Dijkstra(char[][] map)
    {
        var start = Find(map, 'S');
        Step startStep = (start, 1);

        var neighbours = GetNeighbours(map);
        var active = new HashSet<Step> { startStep };
            
        var distances = new Dictionary<Step, Route> ();
        foreach (var step in neighbours.Values.SelectMany(x => x))
            distances[step] = new Route([], long.MaxValue);
        distances[startStep] = new Route([start], 0L);

        while (active.Count > 0)
        {
            var source = active.MinBy(step => distances[step].Distance);
            var (coord, direction) = source;

            foreach (var neighbour in neighbours[coord].Where(n => !active.Contains(n)))
            {
                var (nCoord, nDirection) = neighbour;
                if (Math.Abs(nDirection - direction) == 2)
                    // opposite direction, ignore
                    continue;

                var route = distances[source];
                var distance = route.Distance + (direction == nDirection ? 1 : 1001);
                
                if (distance < distances[neighbour].Distance)
                {
                    var nVisited = route.Visited.ToHashSet();
                    nVisited.Add(nCoord);
                    distances[neighbour] = new Route(nVisited, distance);
                    active.Add(neighbour);
                }
                else if (distance == distances[neighbour].Distance)
                {
                    var nVisited = distances[neighbour].Visited.ToHashSet();
                    nVisited.UnionWith(route.Visited);
                    nVisited.Add(nCoord);
                    distances[neighbour] = new Route(nVisited, distance);
                    active.Add(neighbour);
                }
            }
            
            active.Remove(source);
        }
        
        var end = Find(map, 'E');
        var endSteps = Enumerable.Range(0, _vectors.Length).Select(Step (dir) => (end, dir));
        var endRoutes = new List<Route>();
        foreach (var endStep in endSteps)
        {
            if (distances.TryGetValue(endStep, out var route))
                endRoutes.Add(route);
        }

        var minDistance = endRoutes.Min(r => r.Distance);
        var bestRoutes = endRoutes.Where(route => route.Distance == minDistance);
        var bestVisited = new HashSet<XyPair<int>>();
        foreach (var route in bestRoutes)
            bestVisited.UnionWith(route.Visited);
        
        PrintBenches(map, bestVisited);

        return bestVisited.Count;
    }
    
    public int Part2(char[][] map)
    {
        return 0;
    }
    
    public void Run()
    {
        var map = Parse(FileHelpers.ReadText(16));
        Console.Write($"Part 1: {Part1(map)}");
    }
}