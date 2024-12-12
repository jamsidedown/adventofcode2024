using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day08 : IDay
{
    private char[][] Parse() =>
        FileHelpers.ReadLines<char[]>(8, line => line.ToArray());

    private Dictionary<char, List<XyPair<int>>> GetCoords(char[][] map)
    {
        var coords = new Dictionary<char, List<XyPair<int>>>();
        
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[0].Length; x++)
            {
                var c = map[y][x];

                if (c == '.')
                    continue;

                var coord = new XyPair<int>(x, y);

                if (coords.TryGetValue(c, out var list))
                    list.Add(coord);
                else
                    coords[c] = [coord];
            }
        }

        return coords;
    }

    private bool InMap(char[][] map, XyPair<int> coord) =>
        coord.X >= 0 && coord.X < map[0].Length
            && coord.Y >= 0 && coord.Y < map.Length;

    private IEnumerable<XyPair<int>> GetValidPositions(char[][] map, XyPair<int> first, XyPair<int> second)
    {
        var vector = first - second;
        var a = first + vector;
        var b = second - vector;

        if (InMap(map, a))
            yield return a;
        
        if (InMap(map, b))
            yield return b;
    }

    private XyPair<int> Reduce(XyPair<int> vector)
    {
        var limit = Math.Min(Math.Abs(vector.X), Math.Abs(vector.Y));
        for (var i = 2; i <= limit; i++)
        {
            while (vector.X % i == 0 && vector.Y % i == 0)
            {
                vector /= i;
                limit /= i;
            }
        }

        return vector;
    }

    private IEnumerable<XyPair<int>> GetValidTPositions(char[][] map, XyPair<int> first, XyPair<int> second)
    {
        var vector = Reduce(first - second);

        var a = first;
        while (InMap(map, a))
        {
            yield return a;
            a += vector;
        }

        var b = second;
        while (InMap(map, b))
        {
            yield return b;
            b -= vector;
        }
    }

    private IEnumerable<(XyPair<int>, XyPair<int>)> Combinations(List<XyPair<int>> antennas)
    {
        for (var i = 0; i < antennas.Count - 1; i++)
        for (var j = i + 1; j < antennas.Count; j++)
            yield return (antennas[i], antennas[j]);
    }
    
    public int Part1(char[][] map)
    {
        // parse the input into a dictionary mapping a character to all the coordinates of that character
        // for each character, calculate the x,y distance between each pair of coordinates
        // add the distance to one of the coords in the pair, subtract from the other
        // count the number of positions that are within the bounds of the map
        
        var positions = new HashSet<XyPair<int>>();
        var coords = GetCoords(map);
        
        foreach (var frequency in coords.Keys)
        {
            var antennas = coords[frequency];
            foreach (var (a, b) in Combinations(antennas))
                positions.UnionWith(GetValidPositions(map, a, b));
        }

        return positions.Count;
    }

    public int Part2(char[][] map)
    {
        // similar to part 1, but reduce the x,y distance in cases where x and y share factors
        // not sure if that matters, but there could be pairs that are 2,4 apart, so everything 1,2 from them would be in the line
        // continuously add or subtract the vector while inside the map
        
        var positions = new HashSet<XyPair<int>>();
        var coords = GetCoords(map);
        
        foreach (var frequency in coords.Keys)
        {
            var antennas = coords[frequency];
            foreach (var (a, b) in Combinations(antennas))
                positions.UnionWith(GetValidTPositions(map, a, b));
        }

        return positions.Count;
    }
    
    public void Run()
    {
        var map = Parse();
        Console.WriteLine($"Part 1: {Part1(map)}");
        Console.WriteLine($"Part 2: {Part2(map)}");
    }
}