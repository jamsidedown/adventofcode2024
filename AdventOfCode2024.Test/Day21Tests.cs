using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day21Tests
{
    private readonly Day21 _solution = new();

    [Fact]
    public void TestShortestKeypadSequencesFor029A()
    {
        var result = _solution.ShortestKeypadSequences("029A");
        var resultSet = result.ToHashSet();
        Assert.Equal(3, resultSet.Count);
        Assert.Contains("<A^A>^^AvvvA", resultSet);
        Assert.Contains("<A^A^>^AvvvA", resultSet);
        Assert.Contains("<A^A^^>AvvvA", resultSet);
    }
    
    [Fact]
    public void TestShortestRobotSequencesFor029A()
    {
        var result = _solution.ShortestRobotSequences("<A^A>^^AvvvA");
        var resultSet = result.ToHashSet();
        Assert.Contains("v<<A>>^A<A>AvA<^AA>A<vAAA>^A", resultSet);
    }

    [Fact]
    public void TestShortestHumanSequenceFor029A()
    {
        var result = _solution.ShortestRobotSequences("v<<A>>^A<A>AvA<^AA>A<vAAA>^A");
        var resultSet = result.ToHashSet();
        Assert.Contains("<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A", resultSet);
    }

    [Fact]
    public void TestPart1For029A()
    {
        var result = _solution.Part1(["029A"]);
        Assert.Equal(1972L, result);
    }
}