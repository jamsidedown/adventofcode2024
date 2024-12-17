using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day17Tests
{
    private readonly string _firstTestInput = """
                                         Register A: 729
                                         Register B: 0
                                         Register C: 0

                                         Program: 0,1,5,4,3,0
                                         """;

    private readonly string _secondTestInput = """
                                               Register A: 2024
                                               Register B: 0
                                               Register C: 0

                                               Program: 0,3,5,4,3,0
                                               """;

    private readonly Day17 _solution = new();

    [Fact]
    public void TestPart1()
    {
        var (registers, program) = _solution.Parse(_firstTestInput);
        var result = _solution.Part1(registers, program);
        Assert.Equal("4,6,3,5,6,3,5,2,1,0", result);
    }

    [Fact]
    public void TestPart2()
    {
        var (registers, program) = _solution.Parse(_secondTestInput);
        var result = _solution.Part2(registers, program);
        Assert.Equal(117440L, result);
    }
}