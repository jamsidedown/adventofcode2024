using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

using Step = (XyPair<int>, int);

internal class Route(long distance)
{
    public long Distance { get; set; } = distance;
    public List<Step> Previous { get; } = [];
}

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
    
    private HashSet<Step> GetVisited(Dictionary<Step, Route> distances, params Step[] ends)
    {
        var visited = new HashSet<Step>();
        var active = ends.ToHashSet();

        while (active.Count > 0)
        {
            var current = active.First();
            visited.Add(current);
            active.Remove(current);

            var route = distances[current];
            foreach (var previous in route.Previous)
                if (!visited.Contains(previous))
                    active.Add(previous);
        }

        return visited;
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
    
    public int Part2(char[][] map)
    {
        var start = Find(map, 'S');
        Step startStep = (start, 1); // facing E

        var neighbours = GetNeighbours(map);
        var active = new HashSet<Step> { startStep };

        var distances = new Dictionary<Step, Route>();
        foreach (var step in neighbours.Values.SelectMany(x => x).Distinct())
            distances[step] = new Route(long.MaxValue);
        distances[startStep] = new Route(0L);

        while (active.Count > 0)
        {
            var source = active.MinBy(step => distances[step].Distance);
            var (coord, direction) = source;

            foreach (var neighbour in neighbours[coord])
            {
                var (_, nDirection) = neighbour;
                if (Math.Abs(nDirection - direction) == 2)
                    // opposite direction, ignore
                    continue;

                var route = distances[source];
                var distance = route.Distance + (direction == nDirection ? 1 : 1001);
                var nRoute = distances[neighbour];
                
                if (distance < nRoute.Distance)
                {
                    nRoute.Distance = distance;
                    nRoute.Previous.Add(source);
                    active.Add(neighbour);
                }
                else if (distance == nRoute.Distance)
                {
                    nRoute.Previous.Add(source);
                    active.Add(neighbour);
                }
            }

            active.Remove(source);
        }

        var end = Find(map, 'E');
        var endSteps = Enumerable.Range(0, _vectors.Length).Select(Step (dir) => (end, dir));
        var endRoutes = new List<(Route, Step)>();
        foreach (var endStep in endSteps)
            if (distances.TryGetValue(endStep, out var route))
                endRoutes.Add((route, endStep));
        
        var minDistance = endRoutes.Min(r => r.Item1.Distance);
        var bestRoutes = endRoutes.Where(route => route.Item1.Distance == minDistance);
        var bestSteps = bestRoutes.Select(route => route.Item2).ToArray();

        var visitedSteps = GetVisited(distances, bestSteps);
        var visitedCoords = visitedSteps.Select(step => step.Item1).ToHashSet();
        
        // PrintBenches(map, visitedCoords);

        return visitedCoords.Count;
    }
    
    public void Run()
    {
        var map = Parse(FileHelpers.ReadText(16));
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}