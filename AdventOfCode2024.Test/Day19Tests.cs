using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day19Tests
{
    private readonly string _exampleInput = """
                                            r, wr, b, g, bwu, rb, gb, br

                                            brwrr
                                            bggr
                                            gbbr
                                            rrbgbr
                                            ubwu
                                            bwurrg
                                            brgr
                                            bbrgwb
                                            """;

    private readonly Day19 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var (patterns, towels) = _solution.Parse(_exampleInput);
        var result = _solution.Part1(patterns, towels);
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void TestPart2()
    {
        var (patterns, towels) = _solution.Parse(_exampleInput);
        var result = _solution.Part2(patterns, towels);
        Assert.Equal(16L, result);
    }
}