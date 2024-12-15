using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day15Tests
{
    private readonly string _smallExampleInput = """
                                                 ########
                                                 #..O.O.#
                                                 ##@.O..#
                                                 #...O..#
                                                 #.#.O..#
                                                 #...O..#
                                                 #......#
                                                 ########

                                                 <^^>>>vv<v>>v<<
                                                 """;

    private readonly string _largeExampleInput = """
                                                 ##########
                                                 #..O..O.O#
                                                 #......O.#
                                                 #.OO..O.O#
                                                 #..O@..O.#
                                                 #O#..O...#
                                                 #O..O..O.#
                                                 #.OO.O.OO#
                                                 #....O...#
                                                 ##########

                                                 <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
                                                 vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
                                                 ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
                                                 <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
                                                 ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
                                                 ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
                                                 >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
                                                 <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
                                                 ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
                                                 v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
                                                 """;

    private readonly Day15 _solution = new();

    [Fact]
    public void TestSmallExamplePart1()
    {
        var (map, instructions) = _solution.Parse(_smallExampleInput);
        var result = _solution.Part1(map, instructions);
        Assert.Equal(2028, result);
    }

    [Fact]
    public void TestLargeExamplePart1()
    {
        var (map, instructions) = _solution.Parse(_largeExampleInput);
        var result = _solution.Part1(map, instructions);
        Assert.Equal(10092, result);
    }

    [Fact]
    public void TestLargeExamplePart2()
    {
        var (map, instructions) = _solution.Parse(_largeExampleInput);
        var result = _solution.Part2(map, instructions);
        Assert.Equal(9021, result);
    }
}