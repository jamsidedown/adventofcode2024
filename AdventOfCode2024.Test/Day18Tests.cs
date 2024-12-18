using AdventOfCode2024.Core;
using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day18Tests
{
    private readonly XyPair<int>[] _testInput = """
                                         5,4
                                         4,2
                                         4,5
                                         3,0
                                         2,1
                                         6,3
                                         2,4
                                         1,5
                                         0,6
                                         3,3
                                         2,6
                                         5,1
                                         1,2
                                         5,5
                                         2,5
                                         6,5
                                         1,4
                                         0,4
                                         6,4
                                         1,1
                                         6,1
                                         1,0
                                         0,5
                                         1,6
                                         2,0
                                         """.Split(Environment.NewLine)
        .Select(line => line.Split(',').Select(int.Parse).ToArray())
        .Select(parts => new XyPair<int>(parts[0], parts[1]))
        .ToArray();
    
    private readonly Day18 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var corruptions = _testInput.Take(12).ToArray();
        var result = _solution.Part1(corruptions, 7, 7);
        Assert.Equal(22, result);
    }

    [Fact]
    public void TestPart2()
    {
        var expected = new XyPair<int>(6, 1);
        var result = _solution.Part2(_testInput, 7, 7, 12);
        Assert.Equal(expected, result);
    }
}