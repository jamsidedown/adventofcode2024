using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day04 : IDay
{
    // - X +
    // -
    // Y
    // +
    private readonly XyPair<int>[] _directions =
    [
        new(1, 0), new(1, 1), new(0, 1), new(-1, 1),
        new(-1, 0), new(-1, -1), new(0, -1), new(1, -1),
    ];

    private readonly char[] _xmas = ['X', 'M', 'A', 'S'];
    
    private char[][] Parse() =>
        FileHelpers.ReadLines<char[]>(4, (s) => s.ToArray());

    private bool Inside(char[][] grid, XyPair<int> coord)
    {
        if (coord.X < 0 || coord.Y < 0)
            return false;

        if (coord.X >= grid[0].Length)
            return false;

        if (coord.Y >= grid.Length)
            return false;

        return true;
    }

    private bool Walk(char[][] grid, XyPair<int> coord, XyPair<int> direction, char[] remaining)
    {
        if (remaining.Length == 0)
            return true;
        
        coord += direction;
        if (!Inside(grid, coord))
            return false;
        
        return grid[coord.X][coord.Y] == remaining[0]
               && Walk(grid, coord, direction, remaining[1..]);
    }
    
    

    private bool IsMas(char[][] grid, int x, int y)
    {
        if (grid[x][y] != 'A')
            return false;
        
        return
        ((grid[x-1][y-1] == 'M' && grid[x+1][y+1] == 'S') || (grid[x-1][y-1] == 'S' && grid[x+1][y+1] == 'M'))
        && ((grid[x+1][y-1] == 'M' && grid[x-1][y+1] == 'S') || (grid[x+1][y-1] == 'S' && grid[x-1][y+1] == 'M'));
    }

    public int Part1(char[][] grid)
    {
        var count = 0;
        
        for (var x = 0; x < grid[0].Length; x++)
        {
            for (var y = 0; y < grid.Length; y++)
            {
                if (grid[x][y] != _xmas[0])
                    continue;
                
                var coord = new XyPair<int>(x, y);
                count += _directions.Count(dir => Walk(grid, coord, dir, _xmas[1..]));
            }
        }

        return count;
    }

    public int Part2(char[][] grid)
    {
        var count = 0;
        
        for (var x = 1; x < grid[0].Length - 1; x++)
        {
            for (var y = 1; y < grid.Length - 1; y++)
            {
                if (IsMas(grid, x, y))
                    count++;
            }
        }

        return count;
    }
    
    public void Run()
    {
        var grid = Parse();
        Console.WriteLine($"Part 1: {Part1(grid)}");
        Console.WriteLine($"Part 2: {Part2(grid)}");
    }
}