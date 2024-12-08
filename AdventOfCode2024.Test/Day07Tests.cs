using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day07Tests
{
    private readonly string[] _exampleLines = """
                                      190: 10 19
                                      3267: 81 40 27
                                      83: 17 5
                                      156: 15 6
                                      7290: 6 8 6 15
                                      161011: 16 10 13
                                      192: 17 8 14
                                      21037: 9 7 18 13
                                      292: 11 6 16 20
                                      """.Split(Environment.NewLine);
    
    private readonly Day07 _solution = new();

    [Fact]
    public void TestCanParseFirstExample()
    {
        var parsed = _solution.Parse(_exampleLines);
        Assert.Equal(_exampleLines.Length, parsed.Length);
        Assert.Equal(190L, parsed[0].Target);
        Assert.Equal(2, parsed[0].Numbers.Length);
        Assert.Equal(10L, parsed[0].Numbers[0]);
        Assert.Equal(19L, parsed[0].Numbers[1]);
    }

    [Fact]
    public void TestPart1()
    {
        var equations = _solution.Parse(_exampleLines);
        var result = _solution.Part1(equations);
        Assert.Equal(3749L, result);
    }
    
    [Fact]
    public void TestPart2()
    {
        var equations = _solution.Parse(_exampleLines);
        var result = _solution.Part2(equations);
        Assert.Equal(11387L, result);
    }
}