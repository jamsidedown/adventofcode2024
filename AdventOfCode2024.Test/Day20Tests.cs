using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day20Tests
{
    private readonly char[][] _exampleInput = """
                                            ###############
                                            #...#...#.....#
                                            #.#.#.#.#.###.#
                                            #S#...#.#.#...#
                                            #######.#.#.###
                                            #######.#.#...#
                                            #######.#.###.#
                                            ###..E#...#...#
                                            ###.#######.###
                                            #...###...#...#
                                            #.#####.#.###.#
                                            #.#...#.#.#...#
                                            #.#.#.#.#.#.###
                                            #...#...#...###
                                            ###############
                                            """
        .Trim()
        .Split(Environment.NewLine)
        .Select(line => line.Trim().ToArray())
        .ToArray();

    private readonly Day20 _solution = new();

    [Fact]
    public void TestPart1Saves64psOnce()
    {
        var result = _solution.Part1(_exampleInput, 64);
        Assert.Equal(1, result);
    }

    [Fact]
    public void TestPart1Saves40psTwice()
    {
        var result = _solution.Part1(_exampleInput, 40);
        Assert.Equal(2, result);
    }
    
    [Fact]
    public void TestPart1Saves12psEightTimes()
    {
        var result = _solution.Part1(_exampleInput, 12);
        Assert.Equal(8, result);
    }
}