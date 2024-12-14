using AdventOfCode2024.Core;
using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day14Tests
{
    private readonly string[] _exampleInput = """
                                            p=0,4 v=3,-3
                                            p=6,3 v=-1,-3
                                            p=10,3 v=-1,2
                                            p=2,0 v=2,-1
                                            p=0,0 v=1,3
                                            p=3,0 v=-2,-2
                                            p=7,6 v=-1,-3
                                            p=3,0 v=-1,-2
                                            p=9,3 v=2,3
                                            p=7,3 v=-1,2
                                            p=2,4 v=2,-3
                                            p=9,5 v=-3,-3
                                            """.Trim().Split(Environment.NewLine);

    private readonly Day14 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var robots = _solution.Parse(_exampleInput);
        var dimensions = new XyPair<long>(11, 7);
        var result = _solution.Part1(robots, dimensions);
        Assert.Equal(12, result);
    }
}