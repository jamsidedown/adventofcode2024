using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day16Tests
{
    private readonly string _firstExampleInput = """
                                                 ###############
                                                 #.......#....E#
                                                 #.#.###.#.###.#
                                                 #.....#.#...#.#
                                                 #.###.#####.#.#
                                                 #.#.#.......#.#
                                                 #.#.#####.###.#
                                                 #...........#.#
                                                 ###.#.#####.#.#
                                                 #...#.....#.#.#
                                                 #.#.#.###.#.#.#
                                                 #.....#...#.#.#
                                                 #.###.#.#.#.#.#
                                                 #S..#.....#...#
                                                 ###############
                                                 """;

    private readonly string _secondExampleInput = """
                                                  #################
                                                  #...#...#...#..E#
                                                  #.#.#.#.#.#.#.#.#
                                                  #.#.#.#...#...#.#
                                                  #.#.#.#.###.#.#.#
                                                  #...#.#.#.....#.#
                                                  #.#.#.#.#.#####.#
                                                  #.#...#.#.#.....#
                                                  #.#.#####.#.###.#
                                                  #.#.#.......#...#
                                                  #.#.###.#####.###
                                                  #.#.#...#.....#.#
                                                  #.#.#.#####.###.#
                                                  #.#.#.........#.#
                                                  #.#.#.#########.#
                                                  #S#.............#
                                                  #################
                                                  """;

    private readonly Day16 _solution = new();

    [Fact]
    public void TestPart1WithFirstExample()
    {
        var map = _solution.Parse(_firstExampleInput);
        var result = _solution.Part1(map);
        Assert.Equal(7036L, result);
    }
    
    [Fact]
    public void TestPart1WithSecondExample()
    {
        var map = _solution.Parse(_secondExampleInput);
        var result = _solution.Part1(map);
        Assert.Equal(11048L, result);
    }

    [Fact]
    public void TestPart2WithFirstExample()
    {
        var map = _solution.Parse(_firstExampleInput);
        var result = _solution.Part2(map);
        Assert.Equal(45, result);
    }
    
    [Fact]
    public void TestPart2WithSecondExample()
    {
        var map = _solution.Parse(_firstExampleInput);
        var result = _solution.Part2(map);
        Assert.Equal(64, result);
    }
}