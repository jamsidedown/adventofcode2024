using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day09Tests
{
    private readonly string _shortExampleInput = "12345";
    private readonly string _longExampleInput = "2333133121414131402";
    
    private readonly Day09 _solution = new();

    [Fact]
    public void TestCanParseShortInput()
    {
        var expected = new[] { 0, -1, -1, 1, 1, 1, -1, -1, -1, -1, 2, 2, 2, 2, 2 };
        var disk = _solution.ParseDisk(_shortExampleInput);
        Assert.Equal(expected.Length, disk.Length);
        for (var i = 0; i < expected.Length; i++)
             Assert.Equal(expected[i], disk[i]);
    }

    [Fact]
    public void TestPart1WithLongInput()
    {
        var disk = _solution.ParseDisk(_longExampleInput);
        var result = _solution.Part1(disk);
        Assert.Equal(1928L, result);
    }

    [Fact]
    public void TestPart2WithLongInput()
    {
        var (files, gaps) = _solution.ParseBlocks(_longExampleInput);
        var result = _solution.Part2(files, gaps);
        Assert.Equal(2858L, result);
    }
}