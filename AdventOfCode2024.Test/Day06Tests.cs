using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day06Tests
{
    private readonly string exampleInput = """
                                           ....#.....
                                           .........#
                                           ..........
                                           ..#.......
                                           .......#..
                                           ..........
                                           .#..^.....
                                           ........#.
                                           #.........
                                           ......#...
                                           """;

    private readonly Day06 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var map = exampleInput.Split(Environment.NewLine)
            .Select(line => line.Trim().ToArray())
            .ToArray();
        var result = _solution.Part1(map);
        Assert.Equal(41, result);
    }

    [Fact]
    public void TestPart2()
    {
        var map = exampleInput.Split(Environment.NewLine)
            .Select(line => line.Trim().ToArray())
            .ToArray();
        var result = _solution.Part2(map);
        Assert.Equal(6, result);
    }
}