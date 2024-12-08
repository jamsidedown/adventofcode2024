using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day08Tests
{
    private readonly string _exampleInput = """
                                            ............
                                            ........0...
                                            .....0......
                                            .......0....
                                            ....0.......
                                            ......A.....
                                            ............
                                            ............
                                            ........A...
                                            .........A..
                                            ............
                                            ............
                                            """;
    
    private readonly Day08 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var map = _exampleInput.Split(Environment.NewLine)
            .Select(line => line.ToArray())
            .ToArray();
        var result = _solution.Part1(map);
        Assert.Equal(14, result);
    }
    
    [Fact]
    public void TestPart2()
    {
        var map = _exampleInput.Split(Environment.NewLine)
            .Select(line => line.ToArray())
            .ToArray();
        var result = _solution.Part2(map);
        Assert.Equal(34, result);
    }
}