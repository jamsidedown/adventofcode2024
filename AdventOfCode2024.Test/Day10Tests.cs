using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day10Tests
{
    private readonly string _exampleInput = """
                                            89010123
                                            78121874
                                            87430965
                                            96549874
                                            45678903
                                            32019012
                                            01329801
                                            10456732
                                            """;

    private readonly Day10 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var map = _exampleInput.Split(Environment.NewLine)
            .Select(line => line.Select(c => c - '0').ToArray())
            .ToArray();
        var result = _solution.Part1(map);
        Assert.Equal(36, result);
    }
    
    [Fact]
    public void TestPart2()
    {
        var map = _exampleInput.Split(Environment.NewLine)
            .Select(line => line.Select(c => c - '0').ToArray())
            .ToArray();
        var result = _solution.Part2(map);
        Assert.Equal(81, result);
    }
}