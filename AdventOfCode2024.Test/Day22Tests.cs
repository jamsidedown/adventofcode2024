using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day22Tests
{
    private readonly Day22 _solution = new();

    [Fact]
    public void TestSamples()
    {
        Assert.Equal(8685429, _solution.SecretNumber(1, 2000));
        Assert.Equal(4700978, _solution.SecretNumber(10, 2000));
        Assert.Equal(15273692, _solution.SecretNumber(100, 2000));
        Assert.Equal(8667524, _solution.SecretNumber(2024, 2000));
    }
}