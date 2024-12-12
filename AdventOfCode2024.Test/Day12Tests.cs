using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day12Tests
{
    private readonly string _firstExample = """
                                            AAAA
                                            BBCD
                                            BBCC
                                            EEEC
                                            """;

    private readonly string _secondExample = """
                                             RRRRIICCFF
                                             RRRRIICCCF
                                             VVRRRCCFFF
                                             VVRCCCJFFF
                                             VVVVCJJCFE
                                             VVIVCCJJEE
                                             VVIIICJJEE
                                             MIIIIIJJEE
                                             MIIISIJEEE
                                             MMMISSJEEE
                                             """;

    private readonly Day12 _solution = new();

    [Fact]
    public void TestPart1FirstExample()
    {
        var map = _solution.Parse(_firstExample);
        var result = _solution.Part1(map);
        Assert.Equal(140, result);
    }

    [Fact]
    public void TestPart1SecondExample()
    {
        var map = _solution.Parse(_secondExample);
        var result = _solution.Part1(map);
        Assert.Equal(1930, result);
    }
    
    [Fact]
    public void TestPart2FirstExample()
    {
        var map = _solution.Parse(_firstExample);
        var result = _solution.Part2(map);
        Assert.Equal(80, result);
    }
    
    [Fact]
    public void TestPart2SecondExample()
    {
        var map = _solution.Parse(_secondExample);
        var result = _solution.Part2(map);
        Assert.Equal(1206, result);
    }
}