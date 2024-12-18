using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day18 : IDay
{
    private XyPair<int>[] Parse()
    {
        return FileHelpers.ReadAndSplitLines<int>(18, ",")
            .Select(row => new XyPair<int>(row[0], row[1]))
            .ToArray();
    }
    
    // N, E, S, W
    private readonly XyPair<int>[] _vectors = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    private bool InBounds(XyPair<int> coord, int width, int height)
        => coord.X >= 0 && coord.X < width && coord.Y >= 0 && coord.Y < height;

    private Dictionary<XyPair<int>, List<XyPair<int>>> Neighbours(HashSet<XyPair<int>> corruptions, int width, int height)
    {
        var neighbours = new Dictionary<XyPair<int>, List<XyPair<int>>>();
        
        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
        {
            var coord = new XyPair<int>(x, y);
            if (corruptions.Contains(coord))
                continue;

            var coordNeighbours = new List<XyPair<int>>();

            foreach (var vector in _vectors)
            {
                var neighbour = vector + coord;
                if (InBounds(neighbour, width, height) && !corruptions.Contains(neighbour))
                    coordNeighbours.Add(neighbour);
            }
            
            if (coordNeighbours.Count > 0)
                neighbours.Add(coord, coordNeighbours);
        }

        return neighbours;
    }

    private Dictionary<XyPair<int>, (int, XyPair<int>?)> Dijkstra(HashSet<XyPair<int>> corruptions, int width, int height)
    {
        var start = new XyPair<int>(0, 0);
        var neighbours = Neighbours(corruptions, width, height);

        var active = new HashSet<XyPair<int>> { start };

        var distances = new Dictionary<XyPair<int>, (int, XyPair<int>?)>();
        foreach (var neighbour in neighbours.Keys)
            distances.Add(neighbour, (int.MaxValue, null));
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

    public int Part1(XyPair<int>[] corruptions, int width, int height)
    {
        var distances = Dijkstra(corruptions.ToHashSet(), width, height);
        var end = new XyPair<int>(width - 1, height - 1);
        var (distance, _) = distances[end];
        return distance;
    }

    public XyPair<int> Part2(XyPair<int>[] corruptions, int width, int height, int atLeast)
    {
        var end = new XyPair<int>(width - 1, height - 1);
        var length = atLeast;
        
        while (length < corruptions.Length)
        {
            var bytes = corruptions.Take(length).ToHashSet();
            var distances = Dijkstra(bytes, width, height);
            var (distance, _) = distances[end];
            if (distance == int.MaxValue)
                return corruptions[length - 1];

            length++;
            var visited = GetVisited(distances, end);
            while (!visited.Contains(corruptions[length - 1]))
                length++;
        }

        return new XyPair<int>(-1, -1);
    }
    
    public void Run()
    {
        var corruptions = Parse();
        Console.WriteLine($"Part 1: {Part1(corruptions.Take(1024).ToArray(), 71, 71)}");
        Console.WriteLine($"Part 2: {Part2(corruptions, 71, 71, 1024)}");
    }
}