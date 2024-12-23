using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day21Tests
{
    private readonly string[] _exampleInput =
    [
        "029A",
        "980A",
        "179A",
        "456A",
        "379A",
    ];
    
    private readonly Day21 _solution = new();

    [Fact]
    public void TestLastRobotAToLeft()
    {
        var result = _solution.CountRobotSequences('A', '<', 1);
        Assert.Equal(4, result);
    }

    [Fact]
    public void TestSecondLastRobotAToLeft()
    {
        var result = _solution.CountRobotSequences('A', '<', 2);
        Assert.Equal(10, result);
    }

    [Fact]
    public void TestButtonPressesFor029AWithZeroRobots()
    {
        var result = _solution.CountButtonPresses("029A", 0);
        Assert.Equal(12, result);
    }

    [Fact]
    public void TestButtonPressesFor029AWithOneRobot()
    {
        var result = _solution.CountButtonPresses("029A", 1);
        Assert.Equal(28, result);
    }

    [Fact]
    public void TestButtonPressesFor029AWithTwoRobots()
    {
        var result = _solution.CountButtonPresses("029A", 2);
        Assert.Equal(68, result);
    }

    [Fact]
    public void TestButtonPressesFor980A()
    {
        var result = _solution.CountButtonPresses("980A", 2);
        Assert.Equal(60, result);
    }

    [Fact]
    public void TestButtonPressesFor179A()
    {
        var result = _solution.CountButtonPresses("179A", 2);
        // _solution.Debug("<v<A>>^A<vA<A>>^AAvAA<^A>A<v<A>>^AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A", 2);
        Assert.Equal(68, result);
    }

    [Fact]
    public void TestButtonPressesFor456A()
    {
        var result = _solution.CountButtonPresses("456A", 2);
        Assert.Equal(64, result);
    }

    [Fact]
    public void TestButtonPressesFor379A()
    {
        var result = _solution.CountButtonPresses("379A", 2);
        // _solution.Debug("<v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A", 2);
        Assert.Equal(64, result);
    }

    [Fact]
    public void TestButtonPressesFor539A()
    {
        var result = _solution.CountButtonPresses("539A", 2);
        Assert.Equal(76, result);
    }

    [Fact]
    public void TestButtonPressesFor789A()
    {
        var result = _solution.CountButtonPresses("789A", 2);
        // _solution.Debug("v<<A>>^AAAv<A<A>>^AAvAA<^A>Av<A>^A<A>Av<A>^A<A>Av<A<A>>^AAAvA<^A>A", 2);
        Assert.Equal(78, result);
    }

    [Fact]
    public void TestPart1()
    {
        var result = _solution.Part1(_exampleInput);
        Assert.Equal(126384, result);
    }
}