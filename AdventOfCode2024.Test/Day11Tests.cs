using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day11Tests
{
    private readonly long[] _firstExample = [0L, 1L, 10L, 99L, 999L];

    private readonly long[] _secondExample = [125L, 17L];

    private readonly Day11 _solution = new();

    private long[] Blink(long[] stones, int times)
    {
        var current = stones.ToList();

        for (var i = 0; i < times; i++)
        {
            var subStones = new List<long>(current.Count);
            
            foreach (var stone in current)
            foreach(var part in _solution.Blink(stone))
                subStones.Add(part);

            current = subStones;
        }

        return current.ToArray();
    }

    [Fact]
    public void TestBlinkFirstExampleOnce()
    {
        var expected = new[] { 1L, 2024L, 1L, 0L, 9L, 9L, 2021976L };
        var result = Blink(_firstExample, 1);
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Fact]
    public void TestBlinkSecondExampleSixTimes()
    {
        var expected = new[]
        {
            2097446912L, 14168L, 4048L, 2L, 0L, 2L, 4L, 40L, 48L, 2024L, 40L, 48L, 80L, 96L, 2L, 8L, 6L, 7L, 6L, 0L, 3L,
            2L
        };
        var result = Blink(_secondExample, 6);
        Assert.Equal(expected.Length, result.Length);
        for (var i = 0; i < expected.Length; i++)
            Assert.Equal(expected[i], result[i]);
    }

    [Fact]
    public void TestPart1()
    {
        var result = _solution.Part1(_secondExample);
        Assert.Equal(55312L, result);
    }
}