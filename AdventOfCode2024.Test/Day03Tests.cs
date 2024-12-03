using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day03Tests
{
    private readonly Day03 _solution = new();

    private readonly (string, int)[] _part1Scenarios =
    [
        ("mul(2,4)", 8),
        ("mul(5,5)", 25),
        ("mul ( 2 , 4 )", 0),
        ("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", 161)
    ];

    private readonly (string, int)[] _part2Scenarios =
    [
        ("mul(2,4)", 8),
        ("don't()_mul(5,5)", 0),
        ("don't()do()?mul(8,5)", 40),
        ("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48),
    ];
    
    [Fact]
    public void TestPart1()
    {
        foreach (var (input, expected) in _part1Scenarios)
        {
            var result = _solution.Part1([input]);
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public void TestPart2()
    {
        foreach (var (input, expected) in _part2Scenarios)
        {
            var result = _solution.Part2([input]);
            Assert.Equal(expected, result);
        }
    }
}