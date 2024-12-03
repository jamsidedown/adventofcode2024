using AdventOfCode2024.Solutions;

namespace AdventOfCode2024.Test;

public class Day02Tests
{
    private readonly int[][] _exampleInput =
    [
        [7, 6, 4, 2, 1],
        [1, 2, 7, 8, 9],
        [9, 7, 6, 2, 1],
        [1, 3, 2, 4, 5],
        [8, 6, 4, 4, 1],
        [1, 3, 6, 7, 9],
    ];

    private readonly Day02 _solution = new();

    [Fact]
    public void TestPart1Example1()
    {
        Assert.True(_solution.IsValid(_exampleInput[0]));
    }
    
    [Fact]
    public void TestPart1Example2()
    {
        Assert.False(_solution.IsValid(_exampleInput[1]));
    }
    
    [Fact]
    public void TestPart1Example3()
    {
        Assert.False(_solution.IsValid(_exampleInput[2]));
    }
    
    [Fact]
    public void TestPart1Example4()
    {
        Assert.False(_solution.IsValid(_exampleInput[3]));
    }
    
    [Fact]
    public void TestPart1Example5()
    {
        Assert.False(_solution.IsValid(_exampleInput[4]));
    }
    
    [Fact]
    public void TestPart1Example6()
    {
        Assert.True(_solution.IsValid(_exampleInput[5]));
    }

    [Fact]
    public void TestPart1()
    {
        Assert.Equal(2, _solution.Part1(_exampleInput));
    }

    [Fact]
    public void TestPart2Example1()
    {
        Assert.True(_solution.IsValidAfterDamping(_exampleInput[0]));
    }
    
    [Fact]
    public void TestPart2Example2()
    {
        Assert.False(_solution.IsValidAfterDamping(_exampleInput[1]));
    }
    
    [Fact]
    public void TestPart2Example3()
    {
        Assert.False(_solution.IsValidAfterDamping(_exampleInput[2]));
    }
    
    [Fact]
    public void TestPart2Example4()
    {
        Assert.True(_solution.IsValidAfterDamping(_exampleInput[3]));
    }
    
    [Fact]
    public void TestPart2Example5()
    {
        Assert.True(_solution.IsValidAfterDamping(_exampleInput[4]));
    }
    
    [Fact]
    public void TestPart2Example6()
    {
        Assert.True(_solution.IsValidAfterDamping(_exampleInput[5]));
    }

    [Fact]
    public void TestPart2()
    {
        Assert.Equal(4, _solution.Part2(_exampleInput));
    }
}