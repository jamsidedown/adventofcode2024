using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day12 : IDay
{
    public char[][] Parse(string s)
    {
        return s.Trim()
            .Split(Environment.NewLine)
            .Select(line => line.Trim().ToArray())
            .ToArray();
    }

    private HashSet<XyPair<int>> GetCoordSet(char[][] map)
    {
        var set = new HashSet<XyPair<int>>();
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            set.Add(new XyPair<int>(x, y));

        return set;
    }

    private readonly XyPair<int>[] _neighbours = [new(1, 0), new(-1, 0), new(0, 1), new(0, -1)];
    
    private HashSet<XyPair<int>> FloodFill(char[][] map, XyPair<int> start, ref HashSet<XyPair<int>> remaining)
    {
        var current = map[start.Y][start.X];
        var region = new HashSet<XyPair<int>> { start };

        var neighbours = _neighbours.Select(n => n + start);
        foreach (var neighbour in neighbours)
        {
            if (!remaining.Contains(neighbour))
                continue;
            
            if (map[neighbour.Y][neighbour.X] != current)
                continue;
            
            remaining.Remove(neighbour);
            var subRegion = FloodFill(map, neighbour, ref remaining);
            region.UnionWith(subRegion);
        }
        
        return region;
    }

    private int Perimeter(HashSet<XyPair<int>> region)
    {
        var perimeter = 0;
        
        foreach (var plant in region)
        {
            var neighbours = _neighbours.Select(n => n + plant).Where(region.Contains).Count();
            perimeter += 4 - neighbours;
        }

        return perimeter;
    }

    private int Area(HashSet<XyPair<int>> region) => region.Count;

    private HashSet<XyPair<int>> FloodEdge(XyPair<int> start, ref HashSet<XyPair<int>> remaining)
    {
        var region = new HashSet<XyPair<int>> { start };

        var neighbours = _neighbours.Select(n => n + start);
        foreach (var neighbour in neighbours)
        {
            if (!remaining.Contains(neighbour))
                continue;

            remaining.Remove(neighbour);
            var subRegion = FloodEdge(neighbour, ref remaining);
            region.UnionWith(subRegion);
        }
        
        return region;
    }
    
    private List<HashSet<XyPair<int>>> DistinctEdges(IEnumerable<XyPair<int>> plants)
    {
        var distinct = new List<HashSet<XyPair<int>>>();
        var remaining = plants.ToHashSet();

        while (remaining.Count > 0)
        {
            var current = remaining.First();
            remaining.Remove(current);

            var edge = FloodEdge(current, ref remaining);
            distinct.Add(edge);
        }

        return distinct;
    }

    private int Sides(HashSet<XyPair<int>> region)
    {
        var sides = 0;
        
        foreach (var side in _neighbours)
        {
            var edgePlants = region.Where(plant => !region.Contains(plant + side));
            var edges = DistinctEdges(edgePlants);
            sides += edges.Count;
        }
        
        return sides;
    }

    private List<HashSet<XyPair<int>>> GetRegions(char[][] map)
    {
        var regions = new List<HashSet<XyPair<int>>>();
        var remaining = GetCoordSet(map);

        while (remaining.Count > 0)
        { 
            var current = remaining.First();
            remaining.Remove(current);

            var region = FloodFill(map, current, ref remaining);
            regions.Add(region);
        }

        return regions;
    }

    public int Part1(char[][] map) =>
        GetRegions(map).Select(r => Area(r) * Perimeter(r)).Sum();

    public int Part2(char[][] map) =>
        GetRegions(map).Select(r => Area(r) * Sides(r)).Sum();
    
    public void Run()
    {
        var s = File.ReadAllText(FileHelpers.GetFilepath(12));
        var map = Parse(s);
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}