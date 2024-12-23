using AdventOfCode2024.Core;

namespace AdventOfCode2024.Solutions;

public class Day21 : IDay
{
    private static readonly XyPair<int> _north = new(0, -1);
    private static readonly XyPair<int> _east = new(1, 0);
    private static readonly XyPair<int> _south = new(0, 1);
    private static readonly XyPair<int> _west = new(-1, 0);
    
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

    private readonly Dictionary<char, XyPair<int>> _vectors = new()
    {
        { '^', _north }, { '>', _east }, { 'v', _south},{ '<', _west }
    };

    private readonly Dictionary<XyPair<int>, char> _vectorChars = new()
    {
        { _north, '^' }, { _east, '>' }, { _south, 'v' }, { _west, '<' }
    };
    
    private string[] Parse() => FileHelpers.ReadLines(21);

    private Dictionary<(char, char), List<string>> _combinationsCache = new();
    
    private List<string> Combinations(char from, char to, Dictionary<char, XyPair<int>> charToCoord,
        XyPair<int> invalid)
    {
        if (_combinationsCache.TryGetValue((from, to), out var result))
            return result;
        
        var target = charToCoord[to];

        List<LinkedList<char>> Recurse(XyPair<int> current)
        {
            if (current == invalid)
                return [];

            if (target == current)
            {
                var list = new LinkedList<char>();
                list.AddFirst('A');
                return [list];
            }
            
            var iterationCombinations = new List<LinkedList<char>>();
            var diff = target - current;

            if (diff.X != 0)
            {
                var direction = diff.X < 0 ? _west : _east;
                var next = current + direction;
                foreach (var sequence in Recurse(next))
                {
                    sequence.AddFirst(_vectorChars[direction]);
                    iterationCombinations.Add(sequence);
                }
            }

            if (diff.Y != 0)
            {
                var direction = diff.Y < 0 ? _north : _south;
                var next = current + direction;
                foreach (var sequence in Recurse(next))
                {
                    sequence.AddFirst(_vectorChars[direction]);
                    iterationCombinations.Add(sequence);
                }
            }
            
            return iterationCombinations;
        }

        var start = charToCoord[from];
        var combinations = Recurse(start)
            .Select(list => string.Join("", list))
            .ToList();
        
        _combinationsCache.Add((from, to), combinations);
        return combinations;
    }

    private List<string> KeypadCombinations(char from, char to)
        => Combinations(from, to, _keypadButtons, _invalidKeypad);
    
    private List<string> RobotCombinations(char from, char to)
        => Combinations(from, to, _robotButtons, _invalidRobot);

    public long CountKeypadSequences(char from, char to, int robots)
    {
        var sequences = KeypadCombinations(from, to);

        if (robots == 0)
            return sequences.Min(s => s.Length);

        return sequences.Min(sequence =>
        {
            return $"A{sequence}".ToArray().Pairwise().Sum(pair =>
            {
                var (f, t) = pair;
                return CountRobotSequences(f, t, robots);
            });
        });
    }

    private Dictionary<(char, char, int), long> _robotCache = new();

    public long CountRobotSequences(char from, char to, int robots)
    {
        if (robots == 0)
            return 0L;

        var key = (from, to, robots);

        if (_robotCache.TryGetValue(key, out var optimal))
            return optimal;
        
        var sequences = RobotCombinations(from, to);

        if (robots == 1)
        {
            var subResult = sequences.Min(s => s.Length);
            _robotCache[key] = subResult;
            return subResult;
        }
        
        var result =  sequences.Min(sequence =>
        {
            return $"A{sequence}".ToArray().Pairwise().Sum(pair =>
            {
                var (f, t) = pair;
                return CountRobotSequences(f, t, robots - 1);
            });
        });

        _robotCache[key] = result;
        return result;
    }

    public long CountButtonPresses(string sequence, int robots)
    {
        var count = 0L;

        foreach (var pair in $"A{sequence}".ToArray().Pairwise())
        {
            var (from, to) = pair;
            count += CountKeypadSequences(from, to, robots);
        }

        return count;
    }
    
    public long Part1(string[] sequences)
    {
        var total = 0L;

        foreach (var sequence in sequences)
        {
            var value = int.Parse(sequence[..^1]);
            var count = CountButtonPresses(sequence, 2);
            total += value * count;
        }

        return total;
    }
    
    public long Part2(string[] sequences)
    {
        var total = 0L;

        foreach (var sequence in sequences)
        {
            var value = int.Parse(sequence[..^1]);
            var count = CountButtonPresses(sequence, 25);
            total += value * count;
        }

        return total;
    }
    
    public void Run()
    {
        var sequences = Parse();
        Console.WriteLine($"Part 1: {Part1(sequences)}");
        Console.WriteLine($"Part 2: {Part2(sequences)}");
    }
}