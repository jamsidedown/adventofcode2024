using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day04Tests
{
    private readonly char[][] exampleInput =
    [
        "MMMSXXMASM".ToArray(),
        "MSAMXMSMSA".ToArray(),
        "AMXSXMAAMM".ToArray(),
        "MSAMASMSMX".ToArray(),
        "XMASAMXAMM".ToArray(),
        "XXAMMXXAMA".ToArray(),
        "SMSMSASXSS".ToArray(),
        "SAXAMASAAA".ToArray(),
        "MAMMMXMMMM".ToArray(),
        "MXMXAXMASX".ToArray(),
    ];

    private readonly Day04 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var result = _solution.Part1(exampleInput);
        Assert.Equal(18, result);
    }
}