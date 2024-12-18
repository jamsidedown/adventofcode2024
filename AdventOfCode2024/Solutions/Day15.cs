using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day15 : IDay
{
    private record Box(XyPair<int> Left, XyPair<int> Right);
    
    private readonly XyPair<int>[] _vectors = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];
    
    public (char[][], char[]) Parse(string s)
    {
        var parts = s.Trim().Split($"{Environment.NewLine}{Environment.NewLine}");
        
        var map = parts[0].Trim()
            .Split(Environment.NewLine)
            .Select(line => line.ToArray())
            .ToArray();
        
        var instructions = parts[1].Trim()
            .Split(Environment.NewLine)
            .SelectMany(line => line.ToArray())
            .ToArray();
        
        return (map, instructions);
    }

    private HashSet<XyPair<int>> GetBoxes(char[][] map)
    {
        var boxes = new HashSet<XyPair<int>>();
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == 'O')
                boxes.Add(new XyPair<int>(x, y));

        return boxes;
    }

    private XyPair<int> GetStart(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == '@')
                return new XyPair<int>(x, y);

        throw new KeyNotFoundException("Couldn't find starting point");
    }

    private HashSet<XyPair<int>> GetWalls(char[][] map)
    {
        var walls = new HashSet<XyPair<int>>();
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
            if (map[y][x] == '#')
                walls.Add(new XyPair<int>(x, y));

        return walls;
    }

    private XyPair<int> GetDirection(char instruction) =>
        instruction switch
        {
            '^' => _vectors[0],
            '>' => _vectors[1],
            'v' => _vectors[2],
            '<' => _vectors[3],
            _ => throw new ArgumentException($"Invalid instruction: {instruction}")
        };

    private (XyPair<int>, HashSet<XyPair<int>>) Move(XyPair<int> robot, HashSet<XyPair<int>> boxes, HashSet<XyPair<int>> walls, char instruction)
    {
        var vector = GetDirection(instruction);
        var moving = new HashSet<XyPair<int>>();
        var current = robot + vector;
        
        while (boxes.Contains(current))
        {
            if (boxes.Contains(current))
            {
                boxes.Remove(current);
                moving.Add(current);
            }
            
            current += vector;
        }
        
        if (walls.Contains(current))
        {
            boxes.UnionWith(moving);
            return (robot, boxes);
        }

        robot += vector;
        var moved = moving.Select(box => box + vector);
        boxes.UnionWith(moved);

        return (robot, boxes);
    }

    private char[][] Expand(char[][] map)
    {
        var expanded = new char[map.Length][];

        for (var y = 0; y < map.Length; y++)
        {
            var row = new List<char>();
            
            foreach (var c in map[y])
            {
                switch (c)
                {
                    case '#':
                        row.Add('#');
                        row.Add('#');
                        break;
                    case 'O':
                        row.Add('[');
                        row.Add(']');
                        break;
                    case '.':
                        row.Add('.');
                        row.Add('.');
                        break;
                    case '@':
                        row.Add('@');
                        row.Add('.');
                        break;
                    default:
                        throw new InvalidDataException($"Unexpected character: {c}");
                }
            }

            expanded[y] = row.ToArray();
        }

        return expanded;
    }

    private Dictionary<XyPair<int>, Box> GetWideBoxes(char[][] map)
    {
        var boxes = new Dictionary<XyPair<int>, Box>();
        
        for (var y = 0; y < map.Length; y++)
        for (var x = 0; x < map[0].Length; x++)
        {
            if (map[y][x] == '[')
            {
                var left = new XyPair<int>(x, y);
                var right = new XyPair<int>(x + 1, y);
                var box = new Box(left, right);
                boxes[left] = box;
                boxes[right] = box;
            }
        }

        return boxes;
    }

    private (XyPair<int>, Dictionary<XyPair<int>, Box>) MoveWide(XyPair<int> robot, Dictionary<XyPair<int>, Box> boxes,
        HashSet<XyPair<int>> walls, char instruction)
    {
        var vector = GetDirection(instruction);
        var removed = new List<Box>();
        var moving = new List<Box>();
        HashSet<XyPair<int>> targets = [robot + vector];

        while (true)
        {
            if (walls.Intersect(targets).Any())
            {
                foreach (var box in removed)
                {
                    boxes.Add(box.Left, box);
                    boxes.Add(box.Right, box);
                }

                return (robot, boxes);
            }

            var nextTargets = new HashSet<XyPair<int>>();
            foreach (var target in targets)
            {
                if (boxes.TryGetValue(target, out var targetBox))
                {
                    var nextTarget = new Box(targetBox.Left + vector, targetBox.Right + vector);
                    moving.Add(nextTarget);
                    
                    removed.Add(targetBox);
                    boxes.Remove(targetBox.Left);
                    boxes.Remove(targetBox.Right);
                    
                    nextTargets.Add(nextTarget.Left);
                    nextTargets.Add(nextTarget.Right);
                }
            }

            if (nextTargets.Count == 0)
                break;
            
            targets = nextTargets;
        }

        foreach (var box in moving)
        {
            boxes.Add(box.Left, box);
            boxes.Add(box.Right, box);
        }

        return (robot + vector, boxes);
    }
    
    public int Part1(char[][] map, char[] instructions)
    {
        // for a given instruction find all contiguous boxes in front of the robot
        // if a wall is found, then do nothing
        // if a gap is found, then move all boxes in that direction, then the robot
        
        var robot = GetStart(map);
        var boxes = GetBoxes(map);
        var walls = GetWalls(map);

        foreach (var instruction in instructions)
        {
            (robot, boxes) = Move(robot, boxes, walls, instruction);
        }

        return boxes.Sum(box => (100 * box.Y) + box.X);
    }

    public int Part2(char[][] map, char[] instructions)
    {
        // expand the map so boxes are 2 spaces wide
        // create a dictionary mapping every edge of a box with the box as a whole
        // find edge of boxes in front of the robot, get both halves of each box, and use those to look for more collisions
        // if all boxes have a gap in front then move, otherwise do nothing
        
        map = Expand(map);
        var robot = GetStart(map);
        var walls = GetWalls(map);
        var boxes = GetWideBoxes(map);

        foreach (var instruction in instructions)
        {
            (robot, boxes) = MoveWide(robot, boxes, walls, instruction);
        }
        
        return boxes.Values
            .Select(box => box.Left)
            .ToHashSet()
            .Sum(box => (100 * box.Y) + box.X);
    }
    
    public void Run()
    {
        var (map, instructions) = Parse(FileHelpers.ReadText(15));
        Console.WriteLine($"Part 1: {Part1(map, instructions)}");
        Console.WriteLine($"Part 2: {Part2(map, instructions)}");
    }
}