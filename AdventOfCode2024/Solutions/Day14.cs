using System.Text.RegularExpressions;
using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public record Robot(XyPair<long> Position, XyPair<long> Velocity);

public class Day14 :IDay
{
    private readonly Regex _robotPattern = new(@"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
    
    public Robot[] Parse(string[] lines)
    {
        var robots = new List<Robot>();

        foreach (var line in lines)
        {
            var match = _robotPattern.Match(line);
            if (!match.Success)
                continue;
            
            var groups = match.Groups
                .Cast<Group>()
                .Skip(1)
                .Select(g => g.Value)
                .Select(long.Parse)
                .ToArray();

            var position = new XyPair<long>(groups[0], groups[1]);
            var velocity = new XyPair<long>(groups[2], groups[3]);
            robots.Add(new Robot(position, velocity));
        }
        
        return robots.ToArray();
    }

    private Robot Advance(Robot robot, long seconds, XyPair<long> dimensions)
    {
        var distance = robot.Velocity * seconds;
        var newPosition = (robot.Position + distance) % dimensions;
        newPosition = (newPosition + dimensions) % dimensions;
        return robot with { Position = newPosition };
    }

    private Robot[][] Quadrants(Robot[] robots, XyPair<long> dimensions)
    {
        // tl, tr, bl, br
        //  0,  1,  2,  3
        var quads = new List<Robot>[4];
        for (var q = 0; q < 4; q++)
            quads[q] = [];

        var middleX = dimensions.X / 2;
        var middleY = dimensions.Y / 2;

        foreach (var robot in robots)
        {
            if (robot.Position.X == middleX)
                continue;
            if (robot.Position.Y == middleY)
                continue;

            var quad = robot.Position.X > middleX ? 1 : 0;
            quad += robot.Position.Y > middleY ? 2 : 0;
            quads[quad].Add(robot);
        }

        return quads.Select(q => q.ToArray())
            .ToArray();
    }

    private void Output(Robot[] robots, XyPair<long> dimensions, string filepath)
    {
        var map = new char[dimensions.Y][];
        for (var y = 0; y < dimensions.Y; y++)
        {
            map[y] = new char[dimensions.X];
            for (var x = 0; x < dimensions.X; x++)
                map[y][x] = ' ';
        }

        foreach (var robot in robots)
            map[robot.Position.Y][robot.Position.X] = '#';

        var rows = map.Select(row => string.Join("", row));

        var dir = Directory.GetParent(filepath);
        if (dir is not null && !Directory.Exists(dir.FullName))
        {
            Directory.CreateDirectory(dir.FullName);
            Console.WriteLine($"Created {dir.FullName}");
        }
        
        File.WriteAllLines(filepath, rows);

        var filename = Path.GetFileName(filepath);
        Console.WriteLine($"Wrote {filename}");
    }

    private void Dump(Robot[] robots, XyPair<long> dimensions, int iterations)
    {
        var current = robots.ToArray();
        
        var i = 0;
        while (i < iterations)
        {
            var filepath = $"temp/aoc_tree_{i}.txt";
            Output(current, dimensions, filepath);
            for (var r = 0; r < current.Length; r++)
                current[r] = Advance(current[r], 1, dimensions);
            i++;
        }
    }

    private long FindTree()
    {
        // hardcoded for my input after looking at a couple hundred text files
        
        var y = 42L;
        
        while (true)
        {
            y += 103L;
            if ((y - 99L) % 101L == 0L)
                return y;
        }
    }

    public int Part1(Robot[] robots, XyPair<long> dimensions)
    {
        // given constant velocity, move 100 turns and take the modulus by the size of the map
        // then split into quads ignoring anything along the centre lines
        
        var moved = robots.Select(robot => Advance(robot, 100, dimensions)).ToArray();
        var quadrants = Quadrants(moved, dimensions);

        return quadrants.Aggregate(1, (current, quad) => current * quad.Length);
    }

    public long Part2(Robot[] robots, XyPair<long> dimensions)
    {
        // robots cycle some multiple of 101 seconds in the x-axis
        // some multiple of 103 seconds in the y-axis
        // through manual inspection of output text files, found clustering in the x-axis starting at 99 seconds, every 101 seconds
        // found clustering in the y-axis starting at 42 seconds, every 103 seconds
        // tried and failed to solve linear equations
        // looped through all number of seconds where values grouped vertically (every 103 seconds) until it divided evenly into x
        // that happened to work
        // the next step would have been looking at how clustered the robots were, or the average number of neighbours each had
        
        // Dump(robots, dimensions, 400);
        
        var current = robots.ToArray();
        var tree = FindTree();
        
        for (var r = 0; r < current.Length; r++)
            current[r] = Advance(current[r], tree, dimensions);
        
        Output(current, dimensions, $"temp/aoc_tree_{tree}.txt");
        
        return tree;
    }
    
    public void Run()
    {
        var robots = Parse(FileHelpers.ReadLines(14));
        var dimensions = new XyPair<long>(101, 103);
        Console.WriteLine($"Part 1: {Part1(robots, dimensions)}");
        Console.WriteLine($"Part 2: {Part2(robots, dimensions)}");
    }
}