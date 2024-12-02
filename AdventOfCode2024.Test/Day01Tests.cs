using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day01Tests
{
    private readonly int[] _exampleLeft = [3, 4, 2, 1, 3, 3];
    private readonly int[] _exampleRight = [4, 3, 5, 3, 9, 3];
    
    private readonly Day01 _solution = new();
    
    [Fact]
    public void TestPart1Returns11()
    {
        var result = _solution.Part1(_exampleLeft, _exampleRight);
        Assert.Equal(11, result);
    }

    [Fact]
    public void TestPart2Returns31()
    {
        var result = _solution.Part2(_exampleLeft, _exampleRight);
        Assert.Equal(31, result);
    }
}
