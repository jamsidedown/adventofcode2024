using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day21 : IDay
{
    // N, E, S, W
    private static readonly XyPair<int> _north = new(0, -1);
    private static readonly XyPair<int> _east = new(1, 0);
    private static readonly XyPair<int> _south = new(0, 1);
    private static readonly XyPair<int> _west = new(-1, 0);
    private readonly XyPair<int>[] _vectors = [_north, _east, _south, _west];
    
    private readonly Dictionary<char, XyPair<int>> _keypadButtons = new()
    {
        { 'A', new XyPair<int>(2, 3) }, { '0', new XyPair<int>(1, 3) },
        { '1', new XyPair<int>(0, 2) }, { '2', new XyPair<int>(1, 2) }, { '3', new XyPair<int>(2, 2) },
        { '4', new XyPair<int>(0, 1) }, { '5', new XyPair<int>(1, 1) }, { '6', new XyPair<int>(2, 1) },
        { '7', new XyPair<int>(0, 0) }, { '8', new XyPair<int>(1, 0) }, { '9', new XyPair<int>(2, 0) }
    };

    private readonly XyPair<int> _invalidKeypad = new(0,  3);

    private readonly Dictionary<char, XyPair<int>> _robotButtons = new()
    {
        {'A', new XyPair<int>(2, 0)}, {'^', new XyPair<int>(1, 0)},
        {'<', new XyPair<int>(0, 1)}, {'v', new XyPair<int>(1, 1)}, {'>', new XyPair<int>(2, 1)}
    };

    private readonly XyPair<int> _invalidRobot = new(0, 0);

    private static readonly Dictionary<char, XyPair<int>> _robotVectors = new()
    {
        { '^', _north }, { '>', _east }, { 'v', _south},{ '<', _west }
    };

    private readonly Dictionary<XyPair<int>, char> _robotChars =
        _robotVectors.Select(pair => (pair.Value, pair.Key)).ToDictionary();
    
    private Dictionary<(char, char), string[]> _keypadCache = new();
    
    private string[] KeypadSequences(char from, char to)
    {
        if (_keypadCache.TryGetValue((from, to), out var sequences))
            return sequences;
        
        var toCoord = _keypadButtons[to];
        var fromCoord = _keypadButtons[from];
        
        List<LinkedList<char>> Recurse(XyPair<int> current)
        {
            if (current == _invalidKeypad)
                return [];
            
            if (current == toCoord)
            {
                var list = new LinkedList<char>();
                list.AddFirst('A');
                return [list];
            }

            var iterationPresses = new List<LinkedList<char>>();
            
            var diff = toCoord - current;

            if (diff.X > 0)
            {
                foreach (var combination in Recurse(current + _east))
                {
                    combination.AddFirst(_robotChars[_east]);
                    iterationPresses.Add(combination);
                }
            }
            else if (diff.X < 0)
            {
                foreach (var combination in Recurse(current + _west))
                {
                    combination.AddFirst(_robotChars[_west]);
                    iterationPresses.Add(combination);
                }
            }

            if (diff.Y > 0)
            {
                foreach (var combination in Recurse(current + _south))
                {
                    combination.AddFirst(_robotChars[_south]);
                    iterationPresses.Add(combination);
                }
            }
            else if (diff.Y < 0)
            {
                foreach (var combination in Recurse(current + _north))
                {
                    combination.AddFirst(_robotChars[_north]);
                    iterationPresses.Add(combination);
                }
            }

            return iterationPresses;
        }
        
        var output = Recurse(fromCoord)
            .Select(presses => string.Join("", presses))
            .ToArray();
        
        _keypadCache.Add((from, to), output);
        return output;
    }
    
    public string[] ShortestKeypadSequences(string sequence)
    {
        List<LinkedList<string>> Recurse(char[] remaining)
        {
            var iterationSequences = new List<LinkedList<string>>();
            
            if (remaining.Length < 2)
            {
                iterationSequences.Add(new LinkedList<string>());
                return iterationSequences;
            }

            var subSequences = KeypadSequences(remaining[0], remaining[1]);

            foreach (var subSequence in subSequences)
            {
                var lists = Recurse(remaining[1..]);
                foreach (var list in lists)
                {
                    list.AddFirst(subSequence);
                    iterationSequences.Add(list);
                }
            }

            return iterationSequences;
        }
        
        var allSequences = Recurse($"A{sequence}".ToArray())
            .Select(list => string.Join("", list))
            .ToArray();

        var shortestSequence = allSequences.Min(s => s.Length);

        return allSequences.Where(s => s.Length == shortestSequence)
            .ToArray();
    }

    private Dictionary<(char, char), string[]> _robotCache = new();
    
    private string[] RobotSequences(char from, char to)
    {
        if (_robotCache.TryGetValue((from, to), out var sequences))
            return sequences;

        var toCoord = _robotButtons[to];
        var fromCoord = _robotButtons[from];

        List<LinkedList<char>> Recurse(XyPair<int> current)
        {
            if (current == _invalidRobot)
                return [];
            
            if (current == toCoord)
            {
                var list = new LinkedList<char>();
                list.AddFirst('A');
                return [list];
            }

            var iterationPresses = new List<LinkedList<char>>();
            var diff = toCoord - current;

            if (diff.X > 0)
            {
                foreach (var combination in Recurse(current + _east))
                {
                    combination.AddFirst(_robotChars[_east]);
                    iterationPresses.Add(combination);
                }
            }
            else if (diff.X < 0)
            {
                foreach (var combination in Recurse(current + _west))
                {
                    combination.AddFirst(_robotChars[_west]);
                    iterationPresses.Add(combination);
                }
            }
            
            if (diff.Y > 0)
            {
                foreach (var combination in Recurse(current + _south))
                {
                    combination.AddFirst(_robotChars[_south]);
                    iterationPresses.Add(combination);
                }
            }
            else if (diff.Y < 0)
            {
                foreach (var combination in Recurse(current + _north))
                {
                    combination.AddFirst(_robotChars[_north]);
                    iterationPresses.Add(combination);
                }
            }

            return iterationPresses;
        }

        var output = Recurse(fromCoord)
            .Select(presses => string.Join("", presses))
            .ToArray();
        
        _robotCache.Add((from, to), output);
        return output;
    }

    public string[] ShortestRobotSequences(string sequence)
    {
        List<LinkedList<string>> Recurse(char[] remaining)
        {
            var iterationSequences = new List<LinkedList<string>>();

            if (remaining.Length < 2)
            {
                iterationSequences.Add(new LinkedList<string>());
                return iterationSequences;
            }

            var subSequences = RobotSequences(remaining[0], remaining[1]);

            foreach (var subSequence in subSequences)
            {
                var lists = Recurse(remaining[1..]);
                foreach (var list in lists)
                {
                    list.AddFirst(subSequence);
                    iterationSequences.Add(list);
                }
            }

            return iterationSequences;
        }

        var allSequences = Recurse($"A{sequence}".ToArray())
            .Select(list => string.Join("", list))
            .ToArray();

        var shortestSequence = allSequences.Min(s => s.Length);

        return allSequences.Where(s => s.Length == shortestSequence)
            .ToArray();
    }

    public long Part1(string[] sequences)
    {
        var result = 0L;
        
        foreach (var sequence in sequences)
        {
            var shortestSequence = long.MaxValue;
            
            var keypadSequences = ShortestKeypadSequences(sequence);
            foreach (var keypadSequence in keypadSequences)
            {
                var robotSequences = ShortestRobotSequences(keypadSequence);
                foreach (var robotSequence in robotSequences)
                {
                    var humanSequences = ShortestRobotSequences(robotSequence);
                    shortestSequence = Math.Min(shortestSequence, humanSequences.Min(s => s.Length));
                }
            }

            result += shortestSequence * int.Parse(sequence[..^1]);
        }

        return result;
    }
    
    public void Run()
    {
        var sequences = FileHelpers.ReadLines(21);
        Console.WriteLine($"Part 1: {Part1(sequences)}");
    }
}